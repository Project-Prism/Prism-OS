using graf3d.Engine.Structure;

namespace graf3d.Engine.Abstractions
{
    public interface IBufferedBitmap
    {
        /// <summary>
        ///     Stosunek szerokości do wysokości bitmapy.
        /// </summary>
        float AspectRatio { get; }

        /// <summary>
        ///     Szerokość bitmapy w piskelach.
        /// </summary>
        int PixelWidth { get; }

        /// <summary>
        ///     Wysokość bitmapy w piskelach.
        /// </summary>
        int PixelHeight { get; }

        /// <summary>
        ///     Rysuje piksel w buforowanej ramce, uprzednio sprawdzając czy mieści się w granicach.
        /// </summary>
        void DrawPoint(int x, int y, Color32 color);

        /// <summary>
        ///     Wypełnia cały bufor jednolitym kolorem.
        /// </summary>
        void Clear(Color32 color);

        /// <summary>
        ///     Metoda wywoływana, gdy klatka animacji jest już w całości wyrenderowana.
        /// </summary>
        void Present();
    }
}