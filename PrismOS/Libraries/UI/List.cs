using System.Collections.Generic;
using PrismOS.Libraries.Graphics;
using Cosmos.System;

namespace PrismOS.Libraries.UI
{
    public class ListBox <T> : Control where T : Control
    {
        public ListBox()
        {
            List = new();
        }

        public List<T> List { get; set; }
        public int Scroll { get; set; }

        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.Clear(Theme.Background);

            int UsedY = -Scroll;
            for (int I = 0; I < List.Count; I++)
            {
                UsedY += (int)List[I].Height;
                List[I].Y = UsedY;
                List[I].OnDraw(this.Buffer);
            }

            if (HasBorder)
            {
                this.Buffer.DrawRectangle(0, 0, (int)Width - 1, (int)Height - 1, (int)Theme.Radius, Theme.Accent);
            }

            Buffer.DrawImage(X, Y, this.Buffer, false);
        }

        public override void OnKeyPress(KeyEvent Key)
        {
        }
    }
}