using PrismGL2D;

namespace PrismUI.Controls
{
    public class ImageBox : Control
    {
        public ImageBox(uint Width, uint Height) : base(Width, Height)
		{
            Source = new(Width, Height);
            HasBackground = false;
            HasBorder = false;
        }
        public ImageBox(Image Source) : base(Source.Width, Source.Height)
		{
            this.Source = Source;
            HasBackground = false;
            HasBorder = false;
		}

        public Image Source { get; set; }

        internal unsafe override void OnDraw(Graphics G)
        {
            // Possibly not needed
            // base.OnDraw(G);

            if (Source != null)
            {
                if (Source.Width != Width || Source.Height != Height)
				{
                    Source = (Image)Source.Scale(Width, Height);
				}

                G.DrawImage(X, Y, Source, false);
            }
        }
    }
}