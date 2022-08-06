using PrismOS.Libraries.Graphics;
using Cosmos.System;
using System;

namespace PrismOS.Libraries.UI
{
    public class Switch : Control
    {
        public bool Enabled { get; set; }


        public override void OnClick(int X, int Y, MouseState State)
        {
            Enabled = !Enabled;
            foreach (Action A in OnClickEvents)
            {
                A();
            }
        }

        public override void OnKeyPress(KeyEvent Key)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.DrawFilledRectangle(1, 1, (int)(Width - 2), (int)(Height - 2), (int)Theme.Radius, Theme.Background);
            this.Buffer.DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Theme.Radius, Theme.Accent);
            this.Buffer.DrawFilledRectangle((int)(Enabled ? 2 : Width / 2 + 2), 0, (int)(Width / 2), (int)(Width - 2), (int)Theme.Radius, Theme.Accent);

            Buffer.DrawImage(X, Y, this.Buffer, Theme.Radius != 0);
        }
    }
}