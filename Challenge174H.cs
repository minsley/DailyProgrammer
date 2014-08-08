using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DailyProg
{
	class Challenge174H
	{

		/*
		 * Basic idea:
		 * 1. Get the top-most point (1).
		 * 2. Draw lines to all other points. 
		 * 3. Keep line to point right of (1) with min slope-to-xaxis angle (2).
		 * 4. Draw lines to all other points.
		 * 5. Keep line to point (3) with min angle to (1,2) 
		 * 6. Repeat 4-5 until back to (1).
		 * 
		 */


		[STAThread]
		public static void MainMethod()
		{
			var points = GetRandomPoints(12);
			

		}

		public static List<Point> GetRandomPoints(int n)
		{
			var rand = new Random();
			var points = new List<Point>();
			while (points.Count < n)
			{
				var newPoint = new Point(rand.Next(0,100), rand.Next(0,100));
				if(!points.Contains(newPoint)) points.Add(newPoint);
			}
			return points;
		}

		public class Point
		{
			public int X { get; set; }
			public int Y { get; set; }

			public Point(int x, int y)
			{
				X = x;
				Y = y;
			}
		}

		public class Line
		{
			public Point PointA { get; set; }
			public Point PointB { get; set; }
			public int Slope { get; set; }

			public Line(Point A, Point B)
			{
				PointA = A;
				PointB = B;
			}

			public double AngleTo(Line l)
			{
				return 0;
			}
		}
	}
}
