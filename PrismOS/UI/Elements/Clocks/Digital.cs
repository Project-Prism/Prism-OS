using static Cosmos.System.Graphics.Fonts.PCScreenFont;

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

        System.DateTime LT = System.DateTime.Now;
        bool ShowCollon = true;
        string Time = "";

        public new void Draw()
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