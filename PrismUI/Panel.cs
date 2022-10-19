namespace PrismUI
{
    public class Panel : Control
    {
        public override void OnDraw(Control C)
        {
            base.OnDraw(this);

            DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(false, false));

            if (HasBorder)
            {
                DrawRectangle(0, 0, Width - 1, Height - 1, Config.Radius, Config.AccentColor);
            }

            C.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}