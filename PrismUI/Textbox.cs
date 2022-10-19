namespace PrismUI
{
    public class Textbox : Control
    {
        public string Hint = "";

        public override void OnDraw(Control C)
        {
            base.OnDraw(this);

            DrawString((int)(Width / 2), (int)(Height / 2), Text.Length == 0 ? Hint : Text, Config.Font, Config.GetForeground(false, false), true);

            if (HasBorder)
            {
                DrawRectangle(0, 0, Width - 1, Height - 1, Config.Radius, Config.GetForeground(false, false));
            }

            C.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}