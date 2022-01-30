using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using PrismOS.Graphics.GUI.Containers;
using PrismOS.Graphics.GUI.Common;

namespace PrismOS.Graphics.GUI.Clocks
{
    public class Digital : Base
    {
        public Digital(int X, int Y, Window Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Parent = Parent;
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
            Canvas.GetCanvas.DrawString(
                (Canvas.GetCanvas.Width / 2) - (Default.Width * Time.Length / 2),
                (Canvas.GetCanvas.Height / 2) - (Default.Height / 2), Default, Time,
                Parent.Theme.Accent);
        }
    }
}