using System;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Filesystem.FSCore.StartDisk();
                Filesystem.FSCore.WriteFile("0:\\test.json", System.Text.Encoding.ASCII.GetString(Services.Resources.JsonFile), false);
                _System.Threading.Thread.Sleep(1);
                JSONParser.Program.Main();
                _System.Threading.Thread.Sleep(900);
            }
            catch (Exception exc)
            {
                // ToDo: create new error dialog with window manager
                Console.WriteLine(exc.Message);
            }
        }
    }
}