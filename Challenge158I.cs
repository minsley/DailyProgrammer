using System;
using System.Text.RegularExpressions;

namespace DailyProg
{
    class Challenge158I
    {
        public static void MainMethod()
        {
            Console.WriteLine("Enter blueprint pattern:");
            var input = Console.ReadLine();

            const string defaultBlueprint = "j3f3e3e3d3d3c3cee3c3c3d3d3e3e3f3fjij3f3f3e3e3d3d3c3cee3c3c3d3d3e3e3fj";
            input = input == "" ? defaultBlueprint : input;
            
            var house = GenerateHouse(input);
            
            // Tilt the house right side up :D
            for (var i = 18; i >= 0; i--)
            {
                foreach (var line in house)
                {
                    Console.Write(line[i]);
                }
                Console.Write("\n");
            }
        }

        public static string[] GenerateHouse(string blueprint)
        {
            const string walls = "++--***...";
            var pattern = new Regex(@"\d?[a-j]");

            var matches = pattern.Matches(blueprint);
            var house = new String[matches.Count];

            for(var i=0; i<matches.Count; i++)
            {
                string front = "", mid = "", end = "";
                var step = matches[i].ToString().ToCharArray();
                if (step.Length == 1)
                {
                    mid = walls.Substring(0, step[0] - '`');
                    end = new string(' ', 19 - mid.Length);
                }
                else
                {
                    front = new string(' ', (int)char.GetNumericValue(step[0]));
                    mid = walls.Substring(0, step[1] - '`');
                    end = new string(' ', 19 - front.Length - mid.Length);
                }
                house[i] = front + mid + end;
            }

            return house;
        }
    }
}
