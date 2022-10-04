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

        public unsafe override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            if (Source != null)
            {
                Buffer.DrawImage(X, Y, Source, false);
            }
        }
    }
}