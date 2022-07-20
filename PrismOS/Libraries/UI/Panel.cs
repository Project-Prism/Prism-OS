using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Panel : Control
    {
        public override void Update(Window Parent)
        {
            if (Visible)
            {
                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Parent.Theme.Radius, Parent.Theme.Background);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer);
            }
        }
    }
}