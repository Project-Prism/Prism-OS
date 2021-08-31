using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                System2.FileSystem.Format.FormatDisk("0", true);
                System2.FileSystem.Config.StartService();
                Software.Screen0.Start();
            }
            catch (Exception e)
            {
                System2.Extra.CrashHandler.ShowCrashScreen(e);
            }
        }
    }
}