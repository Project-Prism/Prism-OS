namespace PrismOS.Libraries.Graphics
{
    public class Theme
    {
        public static Theme DefaultLight = new()
        {
            Background = Color.White,
            BackgroundClick = Color.DeepGray,
            BackgroundHover = Color.LightGray,
            BackgroundLight = Color.LightGray,
            Foreground = Color.Black,
            ForegroundClick = Color.White,
            ForegroundHover = Color.Black,
            ForegroundLight = Color.LighterBlack,
            Accent = Color.RubyRed,
            Font = Canvas.Font.Default,
            Radius = 0,
        };
        public static Theme DefaultDark = new()
        {
            Background = Color.LightBlack,
            BackgroundClick = Color.White,
            BackgroundHover = Color.LighterBlack,
            BackgroundLight = Color.LighterBlack,
            Foreground = Color.White,
            ForegroundClick = Color.Black,
            ForegroundHover = Color.White,
            ForegroundLight = Color.LightGray,
            Accent = Color.RubyRed,
            Font = Canvas.Font.Default,
            Radius = 0,
        };        

        public static Theme FromBinary(byte[] Binary)
        {
            System.IO.BinaryReader Reader = new(new System.IO.MemoryStream(Binary));
            return new()
            {
                BackgroundClick = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                ForegroundClick = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                BackgroundHover = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                ForegroundHover = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                BackgroundLight = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                ForegroundLight = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                Background = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                Foreground = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                Accent = new(Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte(), Reader.ReadByte()),
                Font = Canvas.Font.Default,
                Radius = Reader.ReadInt32(),
            };
        }

        public Color BackgroundClick, ForegroundClick;
        public Color BackgroundHover, ForegroundHover;
        public Color BackgroundLight, ForegroundLight;
        public Color Background, Foreground;
        public Color Accent;
        public Canvas.Font Font;
        public int Radius;
    }
}