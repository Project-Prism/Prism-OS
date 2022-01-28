using PrismOS.Graphics;
using static PrismOS.Hexi.Main;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
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
        }
    }
}