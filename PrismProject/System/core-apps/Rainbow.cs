using System;
using System.Drawing;

namespace PrismProject.System.core_apps
{
    class Rainbow
    {
        public static Color Rainbows(float progress)
        {
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }

        public static void Display()
        {
            int prg = 0;
            while (prg != 100)
            {
                prg++;
                var clr = Rainbows(prg);
                Console.WriteLine(clr);
                if (prg == 99)
                {
                    prg = 0;
                }
            }
        }
    }
}
