using System;
using System.Drawing;

namespace DailyProg
{
    class Challenge174I
    {
        [STAThread]
        public static void MainMethod()
        {
            while (true)
            {
                Console.WriteLine("Enter a username, or 'q' to quit.");
                var name = Console.ReadLine();
                if (name == "q") break;
                var bmp = GetAvatar(name);
                bmp.Save("avatar.bmp");
            }
        }

        public static Bitmap GetAvatar(string username)
        {
            var l = username.Length;
            var bmp = new Bitmap(l,l);
            for (int i = 0; i < username.Length; i++)
            {
                for (int j = 0; j < username.Length; j++)
                {
                    var temp = username[i] + username[j];
                    var clr = Color.FromArgb(temp/5, temp/5, temp);
                    bmp.SetPixel(i,j, clr);
                }
            }
            return bmp;
        }
    }
}
