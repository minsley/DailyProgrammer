using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyProg
{
    class Challenge161I
    {
        public static void MainMethod()
        {
            var workers = new List<Worker>
            {
                new Worker("Alice", new List<string>{"GUI", "Backend", "Support"}),
                new Worker("Bill", new List<string>{"Finances", "Backend"}),
                new Worker("Cath", new List<string>{"Documentation", "Finances"}),
                new Worker("Jack", new List<string>{"Documentation", "Frontend", "Support"}),
                new Worker("Michael", new List<string>{"Frontend"}),
                new Worker("Steve", new List<string>{"Documentation", "Backend"}),
            };

            var jobs = new List<Job>
            {
                new Job("GUI"),
                new Job("Documentation"),
                new Job("Finances"),
                new Job("Frontend"),
                new Job("Backend"),
                new Job("Support"),
            };

            workers.Add(new Worker("P1", new List<string> { "J1", "J2" }));
            workers.Add(new Worker("P2", new List<string> { "J3", "J2" }));
            workers.Add(new Worker("P3", new List<string> { "J3" }));
            workers.Add(new Worker("P4", new List<string> { "J4", "J5" }));
            workers.Add(new Worker("P5", new List<string> { "J4", "J6" }));

            jobs.Add(new Job("J1"));
            jobs.Add(new Job("J2"));
            jobs.Add(new Job("J3"));
            jobs.Add(new Job("J4"));
            jobs.Add(new Job("J5"));
            jobs.Add(new Job("J6"));

            AssignWorkers(jobs, workers);

            foreach (var job in jobs)
            {
                Console.WriteLine(job.Title + ":\t" + (job.AssignedWorker==null?"":job.AssignedWorker.Name));
            }
        }

        private static void AssignWorkers(List<Job> jobs, List<Worker> workers)
        {
            while (workers.Count > 0)
            {
                var leastSkills = int.MaxValue;
                foreach (var worker in workers)
                {
                    if (worker.Skills.Count < leastSkills && worker.Skills.Count > 0)
                        leastSkills = worker.Skills.Count;
                }

                var leastSkilled = workers.First(x => x.Skills.Count == leastSkills);
                var firstSkill = leastSkilled.Skills.First();
                jobs.Single(x => x.Title == firstSkill).AssignedWorker = leastSkilled;
                workers.Remove(leastSkilled);
                workers.ForEach(x => x.Skills.Remove(firstSkill));
            }
        }

        private class Worker
        {
            public string Name;
            public List<string> Skills;

            public Worker(string name, List<string> skills)
            {
                Name = name;
                Skills = skills;
            }
        }

        private class Job
        {
            public string Title;
            public Worker AssignedWorker;

            public Job(string title)
            {
                Title = title;
            }
        }
    }
}
