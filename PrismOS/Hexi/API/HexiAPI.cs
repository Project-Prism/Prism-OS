using PrismOS.Hexi.Misc;

namespace PrismOS.Hexi.API
{
    public static class HexiAPI
    {
        public static void Compile(Executable exe, byte[] args)
        {
            string Input = System.Text.Encoding.UTF8.GetString(args, 1, args[0]);
            string Output = System.Text.Encoding.UTF8.GetString(args, Input.Length + 3, args[Input.Length + 2]);

            Main.Compiler.Compile(Input, Output);
        }
    }
}
