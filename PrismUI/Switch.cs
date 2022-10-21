using Cosmos.System;
using PrismGL2D;

namespace PrismUI
{
    public class Switch : Control
    {
        public Switch() : base(Config.Scale * 2, Config.Scale)
		{
            Toggled = false;
		}

        public bool Toggled { get; set; }

        internal override void OnClick(int X, int Y, MouseState State)
        {
            IsEnabled = !IsEnabled;
            base.OnClick(X, Y, State);
        }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawFilledRectangle(1, 1, Width - 2, Height - 2, Config.Radius, Config.GetBackground(false, false));
            DrawFilledRectangle((int)(Toggled ? 2 : Width / 2 + 2), 0, Width / 2, Width - 2, Config.Radius, Config.AccentColor);


            if (HasBorder)
            {
                G.DrawRectangle(X - 1, Y - 1, Width + 2, Height + 2, Config.Radius, Config.GetForeground(false, false));
            }

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}