namespace PrismGL2D.UI
{
    public class Panel : Control
    {
        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Config.Radius, Config.GetBackground(false, false));

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)Width - 1, (int)Height - 1, (int)Config.Radius, Config.AccentColor);
            }

            Buffer.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}