using PrismOS.Graphics.Utilities;
using PrismOS.Graphics.GUI;
using System.Drawing;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Theme Theme1 = new(Color.White, Color.DarkSlateBlue, Color.Blue, Color.White);
            WindowManager WM = new(1280, 720);
            WM.Windows.Add(new(100, 100, 500, 500, 0, "", Theme1));
            WM.Windows[0].Children.Add(new Graphics.GUI.Progress.Progressbar(30, 30, 10, 200, 50));

            while (true)
            {
                WM.Draw();
            }

            /*
            Storage.VFS.InitVFS();

            foreach (string String in System.IO.Directory.GetFiles("0:\\"))
            {
                System.Console.WriteLine(String);
            }

            System.Console.WriteLine("Compiling...");
            Compiler.Compile("0:\\IN.HEX", "0:\\OUT.H");
            System.Console.WriteLine("Running...");
            Runtime.RunProgram("0:\\OUT.H");

            while (true)
            {
                Runtime.Tick();
            }
            */
        }
    }
}