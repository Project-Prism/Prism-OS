namespace PrismGL2D.Formats
{
    public unsafe class Bitmap : Graphics
    {
        public Bitmap(byte[] Binary) : base(1, 1)
        {
            Cosmos.System.Graphics.Bitmap BMP = new(Binary);
            Height = BMP.Height;
            Width = BMP.Width;

            fixed (uint* PTR = (uint[])(object)BMP.rawData)
            {
                Internal = PTR;
            }
        }

        public static bool Validate(byte[] Binary)
        {
            return Binary[0] == 'B' && Binary[1] == 'M';
        }
    }
}