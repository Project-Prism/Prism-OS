using PrismOS.Libraries.Graphics;
using Cosmos.System;
using System;

namespace PrismOS.Libraries.UI
{
    public class Button : Control
    {
        public string Text { get; set; }

        public override void OnClick(int X, int Y, MouseState State)
        {
            foreach (Action A in OnClickEvents)
            {
                A();
            }
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.GetBackground(IsPressed, IsHovering));
            this.Buffer.DrawString((int)(Width / 2), (int)(Height / 2), Text, Font.Default, Theme.GetText(IsPressed, IsHovering), true);
            this.Buffer.DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Theme.Radius, Theme.Foreground);

            Buffer.DrawImage(X, Y, this.Buffer, false);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
        }
    }
}