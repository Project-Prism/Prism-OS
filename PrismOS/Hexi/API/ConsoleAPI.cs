using System;
using System.Text;
using PrismOS.Hexi.Misc;

namespace PrismOS.Hexi.API
{
    public static class ConsoleAPI
    {
        public static void Write(Executable exe, byte[] args)
        {
            Console.Write(Encoding.ASCII.GetString(exe.Memory, args[0], args[1]));
        }

        public static void WriteLine(Executable exe, byte[] args)
        {
            Console.WriteLine(Encoding.ASCII.GetString(exe.Memory, args[0], args[1]));
        }
    }
}
