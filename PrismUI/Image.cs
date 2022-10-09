using PrismGL2D;

namespace PrismUI
{
    public class Image : Control
    {
        public Image()
		{
            Source = new(0, 0);
		}

        public Graphics Source { get; set; }

        public unsafe override void OnDrawEvent(Control C)
        {
            base.OnDrawEvent(this);

            if (Source != null)
            {
                C.DrawImage(X, Y, Source, false);
            }
        }
    }
}