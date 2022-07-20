using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Image : Control
    {
        public FrameBuffer Source;

        public override void Update(Window Parent)
        {
            if (Source != null && Visible)
            {
                if (Width != Source.Width || Height != Source.Height)
                {
                    Source = Source.Resize((uint)Width, (uint)Height);
                }
                Parent.FrameBuffer.DrawImage(X, Y, Source);
            }
        }
    }
}