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

                int SX = (Width / 2) - Text.Length * Parent.Theme.Font.Width / 2;
                int SY = (Height / 2) - (Text.Split('\n').Length * Parent.Theme.Font.Height / 2);

                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Parent.Theme.Radius, BG);
                FrameBuffer.DrawString(SX, SY, Text, Parent.Theme.Font, FG);
                FrameBuffer.DrawRectangle(0, 0, Width, Height, Parent.Theme.Radius, Parent.Theme.Foreground);

                Parent.FrameBuffer.DrawImage(X, Y, FrameBuffer);
            }
        }
    }
}