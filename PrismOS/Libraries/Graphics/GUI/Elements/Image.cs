using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public class Image : Element
    {
        public Formats.Image Source;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Source != null)
            {
                if (Width == default)
                {
                    Width = Source.Width;
                }
                if (Height == default)
                {
                    Height = Source.Height;
                }

                Canvas.DrawImage(Parent.X + X, Parent.Y = Y, Width, Height, Source);
            }
        }
    }
}