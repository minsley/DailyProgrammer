using System;
using System.Reflection;

namespace DailyProg
{
    class Program
    {
        static void Main(string[] args)
        {
            var running = true;
            while (running)
            {
                Console.Write("\nEnter challenge (ex. '160E'):\t");
                var challenge = Console.ReadLine();

                var challengeName = string.Format("DailyProg.Challenge{0}", challenge);
                Console.WriteLine("\n\t***\tRunning: {0}\t***\n", challengeName);

                try
                {
                    var t = Type.GetType(challengeName);
                    var mainMethod = t.GetMethod("MainMethod");
                    mainMethod.Invoke(null, null);
                    Console.WriteLine("\n\t***\t{0} has exited.\t***\n", challengeName);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nError: \"{0}\" not found.", challengeName);
                }

                Console.Write("\nTry another challenge? (y/n):\t");
                if (Console.ReadLine().ToLower() == "n") running = false;
            }
            Console.WriteLine("\n Press any key to exit.");
            Console.ReadKey();
        }
    }
}
