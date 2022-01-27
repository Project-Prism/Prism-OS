namespace PrismOS.UI.Elements.Clocks
{
    public class Mixed : Element
    {
        public Mixed(int X, int Y, int Radius, Element Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Radius = Radius;
            this.Parent = Parent;
            Canvas = Parent.Canvas;
        }

        System.DateTime LT = System.DateTime.Now;
        bool ShowCollon = true;
        private string Time = "";

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

            Canvas.DrawFilledCircle(Parent.X + X, Parent.Y + Y, Radius + 45, Background);
            Canvas.DrawString(Parent.X + X, Parent.Y + Y - 50, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Time, Color: Accent);

            Canvas.DrawAngledLine( Parent.X + X, Parent.Y + Y, System.DateTime.Now.Hour, Radius, Foreground);
            Canvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, System.DateTime.Now.Minute, Radius + 20, Foreground);
            Canvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, System.DateTime.Now.Second, Radius + 40, Accent);
        }
    }
}