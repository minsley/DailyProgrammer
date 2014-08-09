

using System;

namespace DailyProg
{
    class Challenge163H
    {
        public static void MainMethod()
        {
            var line1 = new Line           
            {
                Label = 'A',
                x1 = -2.5,
                y1 = 0.5,
                x2 = 3.5,
                y2 = .5
            };

            var line2 = new Line
            {
                Label = 'B',
                x1 = -2.23,
                y1 = 99.99,
                x2 = -2.10,
                y2 = -56.23
            };

            var line3 = new Line
            {
                Label = 'C',
                x1 = 100.1,
                y1 = 1000.34,
                x2 = 2000.23,
                y2 = 2100.23
            };
            
            Console.WriteLine(FindXIntercept(line1, line2) + " " + FindYIntercept(line1, line2));
            Console.WriteLine(FindXIntercept(line1, line3) + " " + FindYIntercept(line1, line3));
        }

        public static double FindXIntercept(Line l1, Line l2)
        {
            return ((l1.x1 * l1.y2 - l1.y1 * l1.x2) * (l2.x1 - l2.x2) - (l1.x1 - l1.x2) * (l2.x1 * l2.y2 - l2.y1 * l2.x2))
                   / ((l1.x1 - l1.x2) * (l2.y1 - l2.y2) - (l1.y1 - l1.y2) * (l2.x1 - l2.x2));
        }

        public static double FindYIntercept(Line l1, Line l2)
        {
            return ((l1.x1 * l1.y2 - l1.y1 * l1.x2) * (l2.y1 - l2.y2) - (l1.y1 - l1.y2) * (l2.x1 * l2.y2 - l2.y1 * l2.x2))
                    / ((l1.x1 - l1.x2) * (l2.y1 - l2.y2) - (l1.y1 - l1.y2) * (l2.x1 - l2.x2));
        }

        public class Line
        {
            public char Label;
            public double x1;
            public double y1;
            public double x2;
            public double y2;
        }
    }
}
