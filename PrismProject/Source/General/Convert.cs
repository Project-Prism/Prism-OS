using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrismProject.Source.General
{
    abstract class Convert
    {
        public abstract byte[] ToByteArray();

        public static byte[] ToByteArray(string Input)
        {
            byte[] ByteArray = new byte[] { };
            int place = 0;
            foreach (string Char in Input.Split(""))
            {
                ByteArray[place++] = System.Convert.ToByte(Char);

            }
            return ByteArray;
        }

        public static byte[] ToByteArray(int[] Input)
        {
            byte[] ByteArray = new byte[] { };
            int place = 0;
            foreach (int Char in Input)
            {
                ByteArray[place++] = System.Convert.ToByte(Char);
            }
            return ByteArray;
        }

        public abstract Color ToColor();

        public static Color ToColor(int Input)
        {
            return Color.FromArgb(Input);
        }

        public static Color ToColor(string Input)
        {
            return Color.FromArgb(System.Convert.ToInt32(Input));
        }
    }
}
