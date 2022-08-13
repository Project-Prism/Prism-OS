using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class ImageButton : Button
    {
        public FrameBuffer Source { get; set; }

        public override void OnDraw(FrameBuffer Buffer)
        {
            if (Source == null)
            {
                if (Source.Width != Width || Source.Height != Height)
                {
                    Source = Source.Resize(Width, Height);
                }

                this.Buffer.DrawImage(0, 0, Source, true);

                Buffer.DrawImage(X, Y, this.Buffer, true);
            }
        }
    }
}