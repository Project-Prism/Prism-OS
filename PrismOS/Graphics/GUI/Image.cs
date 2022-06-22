namespace PrismOS.Graphics.GUI
{
    public class Image : Element
    {
        public Formats.Image Source;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Source != null)
            {
                if (Size.Width == default || Size.Height == default)
                {
                    Size.Width = Parent.Size.Width;
                    Size.Height = Parent.Size.Height;
                }

                Canvas.DrawImage(
                    Parent.Position.X + Position.X,
                    Parent.Position.Y + Position.Y,
                    Size.Width,
                    Size.Height,
                    Source);
            }
        }
    }
}