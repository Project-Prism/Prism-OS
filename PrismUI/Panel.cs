using PrismGL2D;

namespace PrismUI
{
    public class Panel : Control
    {
        public override void OnDrawEvent(Graphics G)
        {
            base.OnDrawEvent(this);

            DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(false, false));

            if (HasBorder)
            {
                DrawRectangle(0, 0, Width - 1, Height - 1, Config.Radius, Config.AccentColor);
            }

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}