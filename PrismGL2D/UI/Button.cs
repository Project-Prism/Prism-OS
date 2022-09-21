namespace PrismGL2D.UI
{
    public class Button : Control
    {
        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Config.Radius, Config.GetBackground(IsPressed, IsHovering));
            DrawString((int)(Width / 2), (int)(Height / 2), Text, Config.Font, Config.GetForeground(IsPressed, IsHovering), true);

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Config.Radius, Config.GetForeground(IsPressed, IsHovering));
            }

            Buffer.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}