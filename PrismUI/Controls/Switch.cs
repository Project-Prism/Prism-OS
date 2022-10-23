using Cosmos.System;
using PrismGL2D;

namespace PrismUI.Controls
{
    public class Switch : Control
    {
        public Switch() : base(Config.Scale * 2, Config.Scale)
		{
            OnClickEvent = (int X, int Y, MouseState State) => { Toggled = !Toggled; };
		}

        public bool Toggled { get; set; }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawFilledRectangle((int)(Toggled ? Width / 2 : 0), 0, Width / 2, Height, Config.Radius, Config.AccentColor);

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}