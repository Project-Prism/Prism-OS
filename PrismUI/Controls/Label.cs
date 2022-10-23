using PrismGL2D;

namespace PrismUI.Controls
{
    public class Label : Control
    {
        public Label(string Text) : base(0, 0)
        {
            HasBackground = false;
            HasBorder = false;
            this.Text = Text;
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                string[] S = value.Split('\n');
                for (int I = 0; I < S.Length; I++)
                {
                    uint TW = Config.Font.MeasureString(S[I]);
                    if (TW > Width)
                    {
                        Width = TW;
                    }
                }
                Height = (uint)(Config.Font.Size * value.Split('\n').Length);
                base.Text = value;
            }
        }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawString(0, 0, Text, Config.Font, Config.ForeColor);

            G.DrawImage(X, Y, this, true);
        }
    }
}