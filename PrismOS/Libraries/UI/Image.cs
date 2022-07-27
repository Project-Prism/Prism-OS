using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Image : Control
    {
        public override void Update(Window Parent)
        {
            if (FrameBuffer != null && Visible)
            {
                if (Width != FrameBuffer.Width || Height != FrameBuffer.Height)
                {
                    FrameBuffer = FrameBuffer.Resize((uint)Width, (uint)Height);
                }
                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer, true);
            }
        }
    }
}