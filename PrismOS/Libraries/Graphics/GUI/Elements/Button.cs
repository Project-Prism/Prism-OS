using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Button : Element
    {
        public string Text;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible)
            {
                if (Clicked)
                {
                    Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Color.Black);
                    Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Color.White);
                }
                else if (Hovering)
                {
                    Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Color.LightGray);
                    Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Color.Black);
                }
                else
                {
                    Canvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Radius, Color.White);
                    Canvas.DrawString(Parent.X + X, Parent.Y + Y, Text, Color.Black);
                }
            }
        }
    }
}