using Cosmos.System;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Label : Control
    {
        public string Text;

        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.Clear(Theme.Background);
            this.Buffer.DrawString(0, 0, Text, Font.Default, Theme.Text);

            Buffer.DrawImage(X, Y, this.Buffer, false);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
        }
    }
}