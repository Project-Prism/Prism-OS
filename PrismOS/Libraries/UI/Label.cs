using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Label : Control
    {
        public string Text;

        public override void Update(Window Parent)
        {
            if (Visible)
            {
                FrameBuffer.DrawString(Width / 2, Height / 2, Text, Parent.Theme.Font, Parent.Theme.Foreground, true);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer);
            }

        }
    }
}