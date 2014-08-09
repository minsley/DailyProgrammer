using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DailyProg
{
    class Challenge161I
    {
        public static void MainMethod()
        {
            var workers = new Worker[]
            {
                new Worker("Alice", new[] {"Wiring", "Insulation", "Plumbing"}),
                new Worker("Bob", new[] {"Wiring", "Decoration"}),
                new Worker("Charlie", new[] {"Wiring", "Plumbing"}),
                new Worker("David", new[] {"Plumbing"}),
                new Worker("Erin", new[] {"Insulation", "Decoration", "Finances"}),
            };

            var jobs = new Job[]
            {
                new Job("Wiring"),
                new Job("Insulation"),
                new Job("Plumbing"),
                new Job("Decoration"),
                new Job("Finances"),
            };

            GetWorkers(jobs, workers);
        }

        private static void GetWorkers(Job[] jobs, Worker[] workers)
        {
            // All jobs are filled
            if (jobs.All(x => x.AssignedWorker != null)) return;
            else
            {
                // Find least skilled workers
                int leastSkills = 100;
                foreach (var worker in workers)
                {
                    if (worker.Skills.Count() < leastSkills) leastSkills = worker.Skills.Count();
                }

                for (var i = 0; i < workers.Count(x => x != null); i++)
                {
                    if (workers[i].Skills.Count() == leastSkills)
                    {
                        foreach (var skill in workers[i].Skills)
                        {
                            jobs.Single(x => x.Title == skill).AssignedWorker = workers[i];
                            workers[i] = null;
                            GetWorkers(jobs, workers);
                        }
                    }
                }
            }
        }

        private class Worker
        {
            public string Name;
            public string[] Skills;

            public Worker(string name, string[] skills)
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
