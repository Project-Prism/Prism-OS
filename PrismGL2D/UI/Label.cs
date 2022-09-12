namespace PrismGL2D.UI
{
    public class Label : Control
    {
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
                    uint TW = Font.MeasureString(S[I]);
                    if (TW > Width)
                    {
                        Width = TW;
                    }
                }
                Height = (uint)(Font.Size * value.Split('\n').Length);
                base.Text = value;
            }
        }

        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            Clear(Theme.Background);
            DrawString(0, 0, Text, Font, Theme.Text);

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)Width - 1, (int)Height - 1, (int)Theme.Radius, Theme.Accent);
            }

            Buffer.DrawImage(X, Y, this, false);
        }
    }
}