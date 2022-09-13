namespace PrismGL2D.UI
{
    public class Textbox : Control
    {
        public string Hint = "";

        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Config.Radius, Config.BackColor);
            DrawString((int)(Width / 2), (int)(Height / 2), Text.Length == 0 ? Hint : Text, Config.Font, Config.ForeColor, true);

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Config.Radius, Config.ForeColor);
            }

            Buffer.DrawImage(X, Y, this, Config.Radius != 0);
        }
    }
}