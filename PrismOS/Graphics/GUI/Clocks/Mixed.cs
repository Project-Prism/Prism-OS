using PrismOS.Graphics.GUI.Common;
using PrismOS.Graphics.GUI.Containers;

namespace PrismOS.Graphics.GUI.Clocks
{
    public class Mixed : Base
    {
        public Mixed(int X, int Y, int Radius, Window Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Radius = Radius;
            this.Parent = Parent;
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

            Canvas.GetCanvas.DrawFilledCircle(Parent.X + X, Parent.Y + Y, Radius + 45, Parent.Theme.Background);
            Canvas.GetCanvas.DrawString(Parent.X + X, Parent.Y + Y - 50, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, Time, Color: Parent.Theme.Accent);

            Canvas.GetCanvas.DrawAngledLine( Parent.X + X, Parent.Y + Y, System.DateTime.Now.Hour, Radius, Parent.Theme.Foreground);
            Canvas.GetCanvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, System.DateTime.Now.Minute, Radius + 20, Parent.Theme.Foreground);
            Canvas.GetCanvas.DrawAngledLine(Parent.X + X, Parent.Y + Y, System.DateTime.Now.Second, Radius + 40, Parent.Theme.Accent);
        }
    }
}