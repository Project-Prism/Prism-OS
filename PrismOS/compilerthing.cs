using System;
using System.IO;

namespace PrismOS
{
    public static class compilerthing
    {
        public static void Main()
        {
            File.WriteAllBytes("0:\\Compiled.0x", Compile(Console.ReadLine()));
            Run(File.ReadAllBytes("0:\\Compiled.0x"));
        }

        public enum Errors
        {
            UnknownCommand,
        }
        public enum Codes : int
        {
            Terminate = 0x00000000, // Tells the runner when it should stop reading data, such as text.
            Write = 0x00000001 // Tells the runner to print out the following bytes to the console as a char until it reaches a Terminate byte
        }

        public static byte[] Compile(string aFile)
        {
            int Index = 0;
            byte[] Bytes = new byte[500];

            foreach (string Line in File.ReadAllLines(aFile)) // Enumerate over each line
            {
                string[] Items = Line.Split("=>");
                switch (Items[0].ToLower())
                {
                    #region Write
                    case "write":
                        Bytes[Index++] = (byte)Codes.Write;
                        foreach (char Char in Items[1])
                        {
                            Bytes[Index++] = (byte)Char;
                        }
                        Bytes[Index++] = (byte)Codes.Terminate;
                        break;
                    #endregion Write
                    default:
                        throw new Exception("Compiler error: " + nameof(Errors.UnknownCommand));
                }
            }

            return Bytes; // Done compiling
        }

        public static void Run(byte[] Bytes)
        {
            int Index = 0;
            while (true)
            {
                switch (Bytes[Index])
                {
                    case 0x001:
                        Index++;
                        while (Bytes[Index] != 0x000)
                        {
                            Console.Write((char)Bytes[Index++]);
                        }
                        break;
                }
            }
        }
    }
}