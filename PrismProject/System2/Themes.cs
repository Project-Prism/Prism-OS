using System.Collections.Generic;
using System.Drawing;

namespace PrismProject.System2
{
    class Themes
    {
        public static Dictionary<string, BlankTheme> ThemeList = new Dictionary<string, BlankTheme>();
        public struct BlankTheme
        {
            public Color[] Window;
            public Color[] ProgressBar;
        }

        public static void AddTheme(string FullPath, string Name)
        {
            BlankTheme NewBlankTheme = new BlankTheme();
            string Data = IO.Disk.ReadFile(FullPath);
            string[] RawThemeData = Data.Split("\n");
            NewBlankTheme.Window = Convert.ToColorArray(RawThemeData[0].Split(" : "));
            NewBlankTheme.ProgressBar = Convert.ToColorArray(RawThemeData[1].Split(" : "));
            ThemeList.Add(Name, NewBlankTheme);
        }

        public static BlankTheme GetTheme(string Name)
        {
            BlankTheme NewTheme;
            ThemeList.TryGetValue(Name, out NewTheme);
            return NewTheme;
        }
    }
}
