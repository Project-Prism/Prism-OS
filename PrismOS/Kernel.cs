using PrismOS.Graphics;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using System.Drawing;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Canvas X = new(1280, 720);
            // Window manager freezes os :(
            //int p = 0;
            //Graphics.GUI.Containers.Window W1 = new(100, 100, 500, 500, 0, "", new(Color.White, Color.DarkSlateBlue, Color.Blue, Color.White));
            //W1.Children.Add(new Graphics.GUI.Progress.Progressbar(30, 30, 10, 200, p));
            
            while (true)
            {
                X.Clear(Color.DarkSlateGray);
                Graphics.Overlays.FPS.Draw();
                X.Update();
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