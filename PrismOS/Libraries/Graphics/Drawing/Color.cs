using System;

namespace PrismOS.Libraries.Graphics.Drawing
{
    public struct Color : IDisposable
    {
        public Color(int A, int R, int G, int B)
        {
            this.A = A;
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public Color(int R, int G, int B)
        {
            A = 255;
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public Color(string Hex)
        {
            Hex = Hex.Replace("#", "");
            byte R = byte.Parse("0x" + Hex[1].ToString() + Hex[2].ToString());
            byte G = byte.Parse("0x" + Hex[3].ToString() + Hex[4].ToString());
            byte B = byte.Parse("0x" + Hex[5].ToString() + Hex[6].ToString());

            A = 255;
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public Color(int ARGB)
        {
            A = (int)((ARGB & 0xFF000000) >> 24);
            R = (ARGB & 0x00FF0000) >> 16;
            G = (ARGB & 0x0000FF00) >> 8;
            B = (ARGB & 0x000000FF) >> 0;
        }

        public int ARGB => ((R & 0x0ff) << 16) | ((G & 0x0ff) << 8) | (B & 0x0ff);
        public int A, R, G, B;

        public void Blend(Color NewColor)
        {
            R = ((NewColor.A * NewColor.R) + ((256 - NewColor.A) * R)) >> 8;
            G = ((NewColor.A * NewColor.G) + ((256 - NewColor.A) * G)) >> 8;
            B = ((NewColor.A * NewColor.B) + ((256 - NewColor.A) * B)) >> 8;
        }

        public int ToArgb()
        {
            return ARGB;
        }

        public void Dispose()
        {
            Cosmos.Core.GCImplementation.Free(this);
        }

        #region Colors
        public static readonly Color White = new(255, 255, 255, 255);
        public static readonly Color Black = new(255, 0, 0, 0);
        public static readonly Color Red = new(255, 255, 0, 0);
        public static readonly Color Green = new(255, 0, 255, 0);
        public static readonly Color Blue = new(255, 0, 0, 255);
        public static readonly Color CoolGreen = new(255, 54, 94, 53);
        public static readonly Color HotPink = new(255, 230, 62, 109);
        public static readonly Color UbuntuPurple = new(255, 66, 5, 22);
        public static readonly Color GoogleBlue = new(255, 66, 133, 244);
        public static readonly Color GoogleGreen = new(255, 52, 168, 83);
        public static readonly Color GoogleYellow = new(255, 251, 188, 5);
        public static readonly Color GoogleRed = new(255, 234, 67, 53);
        public static readonly Color DeepOrange = new(255, 255, 64, 0);
        public static readonly Color RubyRed = new(255, 204, 52, 45);
        public static readonly Color Transparent = new(0, 0, 0, 0);
        public static readonly Color StackOverflowOrange = new(255, 244, 128, 36);
        public static readonly Color StackOverflowBlack = new(255, 34, 36, 38);
        public static readonly Color StackOverflowWhite = new(255, 188, 187, 187);
        public static readonly Color DeepGray = new(255, 25, 25, 25);
        public static readonly Color LightGray = new(255, 125, 125, 125);
        public static readonly Color SuperOrange = new(255, 255, 99, 71);
        public static readonly Color FakeGrassGreen = new(255, 60, 179, 113);
        public static readonly Color DeepBlue = new(255, 51, 47, 208);
        #endregion

        public static class SystemColors
        {
            public static Color BackGround = StackOverflowBlack;
            public static Color ForeGround = GoogleBlue;
            public static Color ContentText = White;
            public static Color TitleText = Black;
            public static Color Button = GoogleBlue;
            public static Color ButtonHighlight = new(255, 77, 144, 255);
            public static Color ButtonClick = DeepBlue;
        }
    }
}