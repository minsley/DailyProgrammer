using System;

namespace DailyProg
{
    class Challenge164E
    {
        public static void MainMethod()
        {
            Console.WriteLine("Running Challenge164E.hs via GHCi (must have ghci installed).");

            const string strCmdText = @"..\..\Challenge164E.hs";
            System.Diagnostics.Process.Start("ghci.exe", strCmdText);
        }
    }
}
