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
                FrameBuffer.Clear(Parent.Theme.Background);
                FrameBuffer.DrawString(0, 0, Text, Parent.Theme.Font, Parent.Theme.Foreground);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer);
            }

        }
    }
}