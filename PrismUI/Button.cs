using PrismGL2D;

namespace PrismUI
{
    public class Button : Control
    {
        public override void OnDrawEvent(Graphics G)
        {
            base.OnDrawEvent(this);

            DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(IsPressed, IsHovering));
            DrawString((int)(Width / 2), (int)(Height / 2), Text, Config.Font, Config.GetForeground(IsPressed, IsHovering), true);

            if (HasBorder)
            {
                DrawRectangle(0, 0, Width - 1, Height - 1, Config.Radius, Config.GetForeground(IsPressed, IsHovering));
            }

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}