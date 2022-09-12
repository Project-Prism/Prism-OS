namespace PrismGL2D.UI
{
    public class Button : Control
    {
        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            DrawFilledRectangle(0, 0, (int)Width, (int)Height, (int)Theme.Radius, Theme.GetBackground(IsPressed, IsHovering));
            DrawString((int)(Width / 2), (int)(Height / 2), Text, Font, Theme.GetText(IsPressed, IsHovering), true);

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)(Width - 1), (int)(Height - 1), (int)Theme.Radius, Theme.Foreground);
            }

            Buffer.DrawImage(X, Y, this, Theme.Radius != 0);
        }
    }
}