using PrismGL2D;

namespace PrismUI
{
    public class Panel : Control
    {
        public Panel(uint Width, uint Height) : base(Width, Height) { }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(false, false));

            if (HasBorder)
            {
                G.DrawRectangle(X - 1, Y - 1, Width + 2, Height + 2, Config.Radius, Config.GetForeground(false, false));
            }

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}