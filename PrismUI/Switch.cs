using Cosmos.System;

namespace PrismUI
{
    public class Switch : Control
    {
        public bool Toggled { get; set; }

        public override void OnClick(int X, int Y, MouseState State)
        {
            IsEnabled = !IsEnabled;
            base.OnClick(X, Y, State);
        }

        public override void OnDraw(Control C)
        {
            base.OnDraw(this);

            DrawFilledRectangle(1, 1, Width - 2, Height - 2, Config.Radius, Config.GetBackground(false, false));
            DrawFilledRectangle((int)(Toggled ? 2 : Width / 2 + 2), 0, (Width / 2), (Width - 2), Config.Radius, Config.AccentColor);

            if (HasBorder)
            {
                DrawRectangle(0, 0, Width - 1, Height - 1, Config.Radius, Config.AccentColor);
            }

            C.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}