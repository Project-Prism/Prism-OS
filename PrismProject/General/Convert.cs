using System;
using System.Drawing;
using System.Text;

namespace PrismProject.General
{
    static class Convert
    {
        public static Color FromHex(string Hex)
        {
                if (Hex.StartsWith("#"))
                    Hex = Hex.Substring(1);

                if (Hex.Length != 6) throw new Exception("Color not valid");

                return Color.FromArgb(
                    int.Parse(Hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(Hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(Hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber));
        }
    }
}
