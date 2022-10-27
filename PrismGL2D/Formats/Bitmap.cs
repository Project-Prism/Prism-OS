namespace PrismGL2D.Formats
{
    /// <summary>
    /// Bitmap image class.
    /// </summary>
    public unsafe class Bitmap : Graphics
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Bitmap"/> class.
        /// </summary>
        /// <param name="Binary">Raw binary of a bitmap file.</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotImplementedException">Thrown if ColorDepth mode not implemented.</exception>
        public Bitmap(byte[] Binary) : base(0, 0)
        {
            Cosmos.System.Graphics.Bitmap BMP = new(Binary);
            Height = BMP.Height;
            Width = BMP.Width;

            fixed (int* PTR = BMP.rawData)
            {
                Internal = (uint*)PTR;
            }
        }

        /// <summary>
        /// Validate that a set of bytes is a valid bitmap file.
        /// </summary>
        /// <param name="Binary">Binary to test.</param>
        /// <returns>Validity of binary.</returns>
        public static bool Validate(byte[] Binary)
        {
            return Binary[0] == 'B' && Binary[1] == 'M';
        }
    }
}