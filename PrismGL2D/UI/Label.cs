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

        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            Clear(Color.Transparent);
            DrawString(0, 0, Text, Config.Font, Config.GetForeground(false, false));

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)Width - 1, (int)Height - 1, (int)Config.Radius, Config.AccentColor);
            }

            Buffer.DrawImage(X, Y, this, true);
        }
    }
}