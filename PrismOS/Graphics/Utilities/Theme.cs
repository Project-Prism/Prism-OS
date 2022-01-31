using System.Drawing;

namespace PrismOS.Graphics.Utilities
{
    /// <summary>
    /// A super basic theme engine
    /// </summary>
    public struct Theme
    {
        public Theme(Color Background, Color Foreground, Color Accent, Color Text)
        {
            this.Background = Background;
            this.Foreground = Foreground;
            this.Accent = Accent;
            this.Text = Text;
        }

        public static Theme GetTheme { get; set; }
        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public Color Accent { get; set; }
        public Color Text { get; set; }
    }
}
