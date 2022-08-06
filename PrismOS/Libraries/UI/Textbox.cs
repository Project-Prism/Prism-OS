using Cosmos.System;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Textbox : Control
    {
        public string Hint = "";
        public string Text = "";

        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.Background);
            this.Buffer.DrawString((int)(Width / 2), (int)(Height / 2), (Text.Length == 0 ? Hint : Text), Font.Default, Theme.Text, true);
            this.Buffer.DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Theme.Radius, Theme.Foreground);

            Buffer.DrawImage(X, Y, this.Buffer, false);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
            Text += Key.KeyChar;
        }
    }
}