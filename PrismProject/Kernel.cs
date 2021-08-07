using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                System2.VFS.Format("0", true);
                System2.VFS.StartService();
                Software.Cmds.Start();
            }
            catch (Exception e)
            {
                System2.CrashHandler.ShowCrashScreen(e);
            }
        }
    }
}