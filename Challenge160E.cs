using System;
using System.Linq;

namespace DailyProg
{
    class Challenge160E
    {
        public static void MainMethod()
        {
            var sides = new double[3];
            var angles = new double[3];

            Console.WriteLine("*** Triangle Solver ***");
            Console.Write("#hints >> ");
            var hintsGiven = Convert.ToInt16(Console.ReadLine());

            for (var i = 0; i < hintsGiven; i++)
            {
                Console.Write("Hint #{0} >> ", i+1);
                var input = Console.ReadLine();
                if (input != null)
                {
                    var val = Convert.ToDouble(input.Substring(2));
                    switch (input[0])
                    {
                        case 'a':
                            sides[0] = val;
                            break;
                        case 'b':
                            sides[1] = val;
                            break;
                        case 'c':
                            sides[2] = val;
                            break;
                        case 'A':
                            angles[0] = val * (Math.PI / 180);
                            break;
                        case 'B':
                            angles[1] = val * (Math.PI / 180);
                            break;
                        case 'C':
                            angles[2] = val * (Math.PI / 180);
                            break;
                    }
                }
            }

            SolveMissingElements(ref sides, ref angles);

            Console.WriteLine("\n Results: ");
            Console.WriteLine("a={0}", sides[0]);
            Console.WriteLine("b={0}", sides[1]);
            Console.WriteLine("c={0}", sides[2]);
            Console.WriteLine("A={0}", angles[0] * (180 / Math.PI));
            Console.WriteLine("B={0}", angles[1] * (180 / Math.PI));
            Console.WriteLine("C={0}", angles[2] * (180 / Math.PI));
        }

        public static void SolveMissingElements(ref double[] sides, ref double[] angles)
        {
            var len = sides.Count();
            for (var i = 0; i < len; i++)
            {
                // SSS
                if (sides[i] > 0 && sides[(i + 1) % len] > 0 && sides[(i + 2) % len] > 0)
                {
                    angles[i] = SolveSss(sides[i], sides[(i + 1) % len], sides[(i + 2) % len]);
                    angles[(i + 1) % len] = SolveSss(sides[(i + 1) % len], sides[i], sides[(i + 2) % len]);
                    angles[(i + 2) % len] = SolveSss(sides[(i + 2) % len], sides[(i + 1) % len], sides[i]);
                    return;
                }
                // ASA
                if (angles[i] > 0 && sides[(i + 2) % len] > 0 && angles[(i + 1) % len] > 0)
                {
                    angles[(i + 2) % len] = Math.PI - angles[i] - angles[(i + 1) % len];
                    sides[i] = SolveAsa(angles[i], sides[(i + 2) % len], angles[(i + 1) % len]);
                    sides[(i + 1) % len] = SolveAsa(angles[(i + 1) % len], sides[(i + 2) % len], angles[i]);
                    return;
                }
                // SAS
                if (sides[i] > 0 && angles[(i + 2) % len] > 0 && sides[(i + 1) % len] > 0)
                {
                    sides[(i + 2) % 3] = Math.Sqrt(Math.Pow(sides[i], 2) + Math.Pow(sides[(i + 1) % len], 2) - 2 * sides[i] * sides[(i + 1) % len] * Math.Cos(angles[(i + 2) % len]));
                    angles[i] = SolveSas(sides[i], angles[(i + 2)%3], sides[(i + 1)%3]);
                    angles[(i + 1) % 3] = SolveSas(sides[(i + 1) % 3], angles[(i + 2) % 3], sides[i]);
                    return;
                }
                // AAS
                if(angles[i] > 0 && angles[(i + 1) % len] > 0 && sides[i] > 0)
                {
                    angles[(i + 2) % len] = Math.PI - angles[i] - angles[(i + 1) % len];
                    sides[(i + 1) % len] = sides[i] * Math.Sin(angles[(i + 1)%len]) / Math.Sin(angles[i]);
                    sides[(i + 2) % len] = sides[(i + 1) % len] * Math.Cos(angles[i]) + sides[i] * Math.Cos(angles[(i + 1) % len]);
                    return;
                }
            }
        }

        public static double SolveSss(double a, double b, double c)
        {
            double A = Math.Acos( (Math.Pow(b, 2) + Math.Pow(c, 2) - Math.Pow(a, 2)) / (2 * b * c) );
            return A;
        }

        public static double SolveAsa(double A, double c, double B)
        {
            double a = Math.Sin(A)*c/Math.Sin(Math.PI - A - B);
            return a;
        }

        public static double SolveSas(double a, double B, double c)
        {
            double A = Math.Asin(
                    a * Math.Sin(B) 
                    / 
                    Math.Sqrt(Math.Pow(a,2) + Math.Pow(c,2) - 2 * a * c * Math.Cos(B))
                );
            return A;
        }
    }
}
