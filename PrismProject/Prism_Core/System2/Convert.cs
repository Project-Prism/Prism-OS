using System.Drawing;

namespace PrismProject.Prism_Core.System2
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

        public abstract Color[] ToColorArray();

        public static Color[] ToColorArray(string[] Input)
        {
            Color[] ColorArray = new Color[] { };
            int place = 0;
            foreach (string RGB in Input)
            {
                ColorArray[place++] = Color.FromArgb(System.Convert.ToInt32(RGB));
            }
            return ColorArray;
        }


        public static int KMtoMile(int KM)
        {
            return KM / (int)1.609344;
        }
    }
}
