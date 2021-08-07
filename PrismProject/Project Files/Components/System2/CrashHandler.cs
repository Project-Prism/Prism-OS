using System;

namespace PrismProject.System2
{
    internal class CrashHandler
    {
        /// <summary>
        /// Shows a crash screen.
        /// </summary>
        /// <param name="e"></param>
        public static void ShowCrashScreen(Exception e)
        {
            Console.WriteColoredLine("A critical error has ocurred and the system crashed.\nAditional information:\n", ConsoleColor.Red);
            System.Console.WriteLine("Exception occurred: " + e.Message);
            System.Console.WriteLine("\nPlease report this error to developers in an issue. we are sorry that this has happened.\n\n" +
                " What would you like to do?\n\n" +
                "____________________________\n" +
                "| 1 | Attempt to recover   |\n" +
                "| 2 | Restart (ram wipe)   |\n" +
                "| 3 | Restart to terminal  |\n" +
                "============================");
            switch (Convert.ToInt32(System.Console.ReadLine()))
            {
                case 1:
                    Software.Cmds.Start();
                    break;
                case 2:
                    Hardware.CPU.Power.Reboot();
                    break;
                case 3:
                    Software.Cmds.Start();
                    break;
            }
        }
    }
}
