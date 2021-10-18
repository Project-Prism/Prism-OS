using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace JSONParser
{
    public class CharReader
    {
        public char[] Buffer;
        public int Position;

        public CharReader(string file)
        {
            if (!File.Exists(file)) { Console.WriteLine("Unable to locate file '" + file + "'"); return; }

            Position = 0;
            Buffer = new char[new FileInfo(file).Length];
            Array.Copy(File.ReadAllBytes(file), Buffer, Buffer.Length);

            Console.WriteLine("Loaded file '" + file + "' of size " + Buffer.Length.ToString() + " bytes into char buffer");
        }

        public char Peek()
        {
            if (Position - 1 >= Buffer.Length) { return (char)0; }
            return Buffer[Math.Max(0, Position - 1)];
        }

        public char Next()
        {
            if (!HasMore()) { return (char)0; }
            return Buffer[Position++];
        }

        public void Back()
        {
            Position = Math.Max(0, --Position);
        }

        public bool HasMore()
        {
            if (Position < Buffer.Length) { return true; }
            return Position < Buffer.Length;
        }


    }
}
