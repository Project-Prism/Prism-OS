using PrismGL2D;

namespace PrismUI
{
	public class Button : Control
    {
        public Button(uint Width, uint Height) : base(Width, Height) { }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(IsPressed, IsHovering));
            DrawString((int)(Width / 2), (int)(Height / 2), Text, Config.Font, Config.GetForeground(IsPressed, IsHovering), true);

            if (HasBorder)
            {
                G.DrawRectangle(X - 1, Y - 1, Width + 2, Height + 2, Config.Radius, Config.GetForeground(false, false));
            }

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}