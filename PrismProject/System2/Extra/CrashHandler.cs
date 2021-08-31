using System;

namespace PrismProject.System2.Extra
{
    internal class CrashHandler
    {
        /// <summary>
        /// Shows a crash screen.
        /// </summary>
        /// <param name="e"></param>
        public static void ShowCrashScreen(Exception e)
        {
            Extended.Console.WriteColoredLine("A critical error has ocurred and the system crashed.\nAditional information:\n", ConsoleColor.Red);
            Console.WriteLine("Exception occurred: " + e.Message);
            Console.WriteLine("\nPlease report this error to developers in an issue. we are sorry that this has happened.\n\n" +
                " What would you like to do?\n\n" +
                "____________________________\n" +
                "| 1 | Attempt to recover   |\n" +
                "| 2 | Restart (ram wipe)   |\n" +
                "============================");
            switch (System.Convert.ToInt32(System.Console.ReadLine()))
            {
                case 1:
                    Software.Screen0.Start();
                    break;
                case 2:
                    Hardware.CPU.Power.Reboot();
                    break;
            }
        }
    }
}
