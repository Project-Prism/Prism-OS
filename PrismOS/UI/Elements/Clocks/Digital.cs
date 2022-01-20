using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using System.Collections.Generic;
using System.Drawing;

namespace PrismOS.UI.Elements.Clocks
{
    public class Digital : Element
    {
        public Digital(int X, int Y, Element Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Parent = Parent;
            Canvas = Parent.Canvas;
        }

        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }
        public override int Radius { get; set; }
        public override Color Foreground { get; set; }
        public override Color Background { get; set; }
        public override Color Accent { get; set; }
        public override Element Parent { get; set; }
        public override List<Element> Children { get; set; }
        public override Canvas Canvas { get; set; }

        #region Clock properties
        System.DateTime LT = System.DateTime.Now;
        bool ShowCollon = true;
        string Time = "";
        #endregion

        public override void Draw()
        {
            if (System.DateTime.Now.Second != LT.Second)
            {
                if (ShowCollon)
                {
                    Time = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
                    ShowCollon = false;
                }
                else
                {
                    Time = System.DateTime.Now.Hour + " " + System.DateTime.Now.Minute;
                    ShowCollon = true;
                }
                LT = System.DateTime.Now;
            }
            Canvas.DrawString(
                (Canvas.Width / 2) - (Default.Width * Time.Length / 2),
                (Canvas.Height / 2) - (Default.Height / 2), Default, Time,
                Accent);
        }
    }
}