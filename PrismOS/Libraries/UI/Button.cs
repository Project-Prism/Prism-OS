using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Button : Control
    {
        public string Text;

        public override void Update(Window Parent)
        {
            if (Visible)
            {
                Color BG, FG;
                if (Pressed)
                {
                    BG = Parent.Theme.BackgroundClick;
                    FG = Parent.Theme.ForegroundClick;
                }
                else if (Hover)
                {
                    BG = Parent.Theme.BackgroundHover;
                    FG = Parent.Theme.ForegroundHover;
                }
                else
                {
                    BG = Parent.Theme.Background;
                    FG = Parent.Theme.Foreground;
                }

                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Parent.Theme.Radius, BG);
                FrameBuffer.DrawString(Width / 2, Height / 2, Text, Parent.Theme.Font, FG, 2, true);
                FrameBuffer.DrawRectangle(0, 0, Width - 1, Height - 1, Parent.Theme.Radius, Parent.Theme.Foreground);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer, false);
            }
        }
    }
}