using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.Resource.Images
{
    public static unsafe class BMP
    {
        public static FrameBuffer FromBitmap(byte[] Binary)
        {
            Cosmos.System.Graphics.Bitmap BMP = new(Binary);
            fixed (uint* PTR = (uint[])(object)BMP.rawData)
            {
                return new FrameBuffer(BMP.Width, BMP.Height) { Internal = PTR };
            }
        }
    }
}