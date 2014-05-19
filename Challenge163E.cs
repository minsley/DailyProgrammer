using System;

namespace DailyProg
{
    class Challenge163E
    {
        public static void MainMethod()
        {
            var die = new Random();
            var rollCounts = new int[6,7];

            for (var i = 0; i < 6; i++)
            {
                rollCounts[i, 0] = (int)Math.Pow(10, i + 1);
                for (var j = 0; j < rollCounts[i,0]; j++)
                {
                    rollCounts[i, die.Next(1, 7)]++;
                }
            }

            Console.WriteLine("#Rolls\t1s\t2s\t3s\t4s\t5s\t6s");       
            Console.WriteLine("======================================================");
            for (var k = 0; k < 6; k++)
            {
                Console.Write(rollCounts[k,0] + "\t");
                for (var l = 1; l < 7; l++)
                {
                    Console.Write("{0:F2}%\t", (float)rollCounts[k,l]*100/rollCounts[k,0]);
                }
                Console.WriteLine();
            }
        }
   } 
}
