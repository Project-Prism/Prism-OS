using System.Drawing;

namespace PrismProject.Source.Graphics
{
    internal class ThemesEngine
    {
        public static Color[] Parse(byte[] Input)
        {
            Color[] a = new Color[] { };
            int b = 0;
            foreach (byte data in Input)
            {
                a[b++] = Color.FromArgb(data);
            }
            return a;
        }
    }
}