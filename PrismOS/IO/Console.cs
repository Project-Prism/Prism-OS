using System;
using C = Cosmos.System;

namespace PrismOS.IO
{
    public static class Console
    {
        private static C.Console GetConsole { get; } = new(new Cosmos.HAL.TextScreen());
        public static int X { get; set; } = 0;
        public static int Y { get; set; } = 0;

        public static void Write(char Char)
        {
            GetConsole.Write((byte)Char);
            X++;
            if(Char == '\n') { Y++; }
        }
        public static void Write(char Char, ConsoleColor Color)
        {
            ConsoleColor xColor = GetConsole.Foreground;
            GetConsole.Foreground = Color;
            Write(Char);
            GetConsole.Foreground = xColor;
        }
        public static void Write(char Char, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            ConsoleColor xColor = GetConsole.Foreground;
            ConsoleColor yColor = GetConsole.Background;
            GetConsole.Foreground = ForeColor;
            GetConsole.Background = BackColor;
            Write(Char);
            GetConsole.Foreground = xColor;
            GetConsole.Background = yColor;
        }
        public static void Write(string String)
        {
            char[] Chars = String.ToCharArray();

            for (int i = 0; i < String.Length; i++)
            {
                Write(Chars[i]);
            }
        }
        public static void Write(string String, ConsoleColor Color)
        {
            ConsoleColor xColor = GetConsole.Foreground;
            GetConsole.Foreground = Color;

            char[] Chars = String.ToCharArray();

            for (int i = 0; i < String.Length; i++)
            {
                Write(Chars[i]);
            }

            GetConsole.Foreground = xColor;
        }
        public static void Write(string String, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            ConsoleColor xColor = GetConsole.Foreground;
            ConsoleColor yColor = GetConsole.Background;
            GetConsole.Foreground = ForeColor;
            GetConsole.Background = BackColor;

            char[] Chars = String.ToCharArray();

            for (int i = 0; i < String.Length; i++)
            {
                Write(Chars[i]);
            }

            GetConsole.Foreground = xColor;
            GetConsole.Background = yColor;
        }

        public static void WriteLine(char Char)
        {
            Write(Char + "\n");
        }
        public static void WriteLine(char Char, ConsoleColor Color)
        {
            Write(Char + "\n", Color);
        }
        public static void WriteLine(char Char, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            Write(Char + "\n", ForeColor, BackColor);
        }
        public static void WriteLine(string String)
        {
            Write(String + "\n");
        }
        public static void WriteLine(string String, ConsoleColor Color)
        {
            Write(String + "\n", Color);
        }
        public static void WriteLine(string String, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            Write(String + "\n", ForeColor, BackColor);
        }

        public static char Read()
        {
            C.KeyboardManager.TryReadKey(out C.KeyEvent Key);
            return Key.KeyChar;
        }

        public static string ReadLine()
        {
            char[] Chars = Array.Empty<char>();
            const bool Reading = true;
            for (int index = 0; Reading; index++)
            {
                char Char = Read();
                if (Char == '\n')
                {
                    return Chars.ToString();
                }
                Chars[index] = Char;
            }
        }

        public static void Clear()
        {
            GetConsole.Clear();
        }

        public static void Beep() => C.PCSpeaker.Beep();
    }
}