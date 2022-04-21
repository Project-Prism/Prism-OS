using PrismOS.Libraries.Formats;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.GUI.Elements
{
    public class Image : Element
    {
        public BMP Source;

        public override void Update(Canvas Canvas)
        {
            if (Visible && Source != null)
            {
                if (Width == default)
                {
                    Width = (int)Source.Width;
                }
                if (Height == default)
                {
                    Height = (int)Source.Height;
                }

                Canvas.DrawBitmap(X, Y, Width, Height, Source);
            }
        }
    }
}