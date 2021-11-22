using System.Drawing;

namespace Prism.Libraries.UI
{
    public static class Theme
    {
        public static void Load(int[] Theme)
        {
            Button.Background = Color.FromArgb(Theme[0]);
            Button.Foreground = Color.FromArgb(Theme[1]);
            Button.Text = Color.FromArgb(Theme[2]);

            Label.Text = Color.FromArgb(Theme[3]);
            Label.Background = Color.FromArgb(Theme[4]);
        }

        public static class Button
        {
            public static Color Background { get; set; } = Color.FromArgb(25, 25, 45);
            public static Color Foreground { get; set; } = Color.Yellow;
            public static Color Text { get; set; } = Color.White;
        }
        public static class Label
        {
            public static Color Background { get; set; } = Color.FromArgb(255, 0, 0, 0);
            public static Color Text { get; set; } = Color.White;
        }
    }
}