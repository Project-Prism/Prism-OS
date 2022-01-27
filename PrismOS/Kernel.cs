using static PrismOS.Hexi.Main;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            var c = new UI.Canvas(1280, 720);
            while (true)
            {
                c.Clear(System.Drawing.Color.DarkSlateGray);
                UI.Overlays.SystemInfo.Tick();
                UI.Overlays.TypingTester.Tick();
                c.Update();
            }

            /*
            Storage.VFS.InitVFS();
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