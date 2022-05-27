namespace PrismOS.Libraries.Graphics.GUI
{
    public class Image : Element
    {
        public Formats.Image Source;

        public override void Update(Canvas Canvas, Window Parent)
        {
            if (Visible && Parent.Visible && Source != null)
            {
                if (Width == default || Height == default)
                {
                    Width = Parent.Width;
                    Height = Parent.Height;
                }

                Canvas.DrawImage(Parent.X + X, Parent.Y + Y, Width, Height, Source);
            }
        }
    }
}