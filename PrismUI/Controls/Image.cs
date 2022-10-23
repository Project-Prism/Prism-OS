using PrismGL2D;

namespace PrismUI.Controls
{
    public class Image : Control
    {
        public Image(uint Width, uint Height) : base(Width, Height)
		{
            Source = new(Width, Height);
            HasBackground = false;
            HasBorder = false;
        }
        public Image(Graphics Source) : base(Source.Width, Source.Height)
		{
            this.Source = Source;
            HasBackground = false;
            HasBorder = false;
		}

        public Graphics Source { get; set; }

        internal unsafe override void OnDraw(Graphics G)
        {
            // Possibly not needed
            // base.OnDraw(G);

            if (Source != null)
            {
                if (Source.Width != Width || Source.Height != Height)
				{
                    Source = Source.Scale(Width, Height);
				}

                G.DrawImage(X, Y, Source, false);
            }
        }
    }
}