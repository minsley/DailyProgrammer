using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Cryptography;

namespace DailyProg
{
    class Challenge174I
    {
        public static void MainMethod()
        {
            const int scaleFactor = 10;

            while (true)
            {
                Console.WriteLine("Enter a username, or 'q' to quit.");
                var name = Console.ReadLine();
                if (name == "q") break;
                var bmp = ResizeBitmap(GetAvatar(name),20 * scaleFactor,20 * scaleFactor);
                bmp.Save("avatar.bmp");
            }
        }

        public static Bitmap GetAvatar(string username)
        {
            var nameBytes = System.Text.Encoding.ASCII.GetBytes(username);

            var sh = SHA1.Create();
            var h1 = sh.ComputeHash(nameBytes);
            var h2 = sh.ComputeHash(h1);
            var h3 = sh.ComputeHash(h2);

            var bmp = new Bitmap(20,20);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var clr = Color.FromArgb(byte.MaxValue, h1[i + j], h2[i + j], h3[i + j]);
                    bmp.SetPixel(i, j, clr);
                    bmp.SetPixel(19 - i, j, clr);
                    bmp.SetPixel(i, 19 - j, clr);
                    bmp.SetPixel(19 - i, 19 - j, clr);
                }
            }
            return bmp;
        }

        // http://stackoverflow.com/a/12522782/1771697
        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(sourceBMP, 0, 0, width, height);
            }
            return result;
        }
    }
}
