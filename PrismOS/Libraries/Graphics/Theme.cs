namespace PrismOS.Libraries.Graphics
{
    public class Theme
    {
        public static Theme DefaultLight = new()
        {
            Background = Color.White,
            BackgroundClick = Color.DeepGray,
            BackgroundHover = Color.LightGray,
            Foreground = Color.Black,
            ForegroundClick = Color.White,
            ForegroundHover = Color.Black,
            Accent = Color.RubyRed,
            ForegroundHint = Color.LightGray,
        };
        public static Theme DefaultDark = new()
        {
            Background = Color.LightBlack,
            BackgroundClick = Color.White,
            BackgroundHover = Color.LighterBlack,
            Foreground = Color.White,
            ForegroundClick = Color.Black,
            ForegroundHover = Color.White,
            Accent = Color.RubyRed,
            ForegroundHint = Color.LightGray,
        };        

        public Color Background, Foreground;
        public Color BackgroundClick, ForegroundClick;
        public Color BackgroundHover, ForegroundHover;
        public Color Accent, ForegroundHint;
    }
}