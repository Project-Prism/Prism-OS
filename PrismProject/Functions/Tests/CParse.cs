using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PrismProject.Functions.Tests
{
    class CParse
    {
        public static Dictionary<string[], Color[]> Themes = new Dictionary<string[], Color[]>();
        public static void Add(string ThemeData)
        {
            string[] Info = ThemeData.Replace("[H]", "").Split("[/H]")[0].Split(", "); // Get theme information
            string[] Data = ThemeData.Split("\n[THEME]")[1].Replace("\n[/THEME]", "").Split(";\n"); // Get theme daata
            Color[] ColorArray = new Color[] { };
            int place = 0;

            foreach (string data in Data)
            {
                if (data.StartsWith("FromArgb."))
                {
                    ColorArray[place] = Color.FromArgb(Convert.ToInt32(data.Replace("ARGB[ ", "").Replace(" ]", "").Split("; ")[place++]));
                }
                if (data.StartsWith("Color"))
                {
                    ColorArray[place] = Color.FromName(data.Split("Color.")[0].Split(", ")[place++]);
                }
            }
            Themes.Add(Info, ColorArray);
        }
    }
}
