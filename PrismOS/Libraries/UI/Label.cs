using Cosmos.System;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Label : Control
    {
        public Label()
        {
            HasBorder = false;
        }

        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                string[] S = value.Split('\n');
                for (int I = 0; I < S.Length; I++)
                {
                    uint TW = Font.Default.MeasureString(S[I]);
                    if (TW > Width)
                    {
                        Width = TW;
                    }
                }
                Height = (uint)(Font.Default.Size * value.Split('\n').Length);
                _Text = value;
            }
        }
        private string _Text;

        public override void OnClick(int X, int Y, MouseState State)
        {
        }

        public override void OnDraw(FrameBuffer Buffer)
        {
            this.Buffer.Clear(Theme.Background);
            this.Buffer.DrawString(0, 0, Text, Font.Default, Theme.Text);

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