using System.Drawing;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Graphics.Canvas c = new(1280, 720);
            int A = 0;
            while (true)
            {
                c.Clear();
                c.DrawLine(50, 50, 70, 70, Color.White);
                c.DrawString(10, 10, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, "Testing text", Color.White);
                c.Update();
                if (A != 360)
                {
                    A++;
                }
                else
                {
                    A = 0;
                }
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