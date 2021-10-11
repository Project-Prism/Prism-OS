using graf3d.Engine.Abstractions;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Oświetlenie
{
    /// <summary>
    ///     Klasa implementująca mapowanie normalnych.
    ///     Każdy komponent piksela mapy normalnych (czerwony, zielony, niebieski) zawiera informacje o odpowiednich
    ///     długościach X, Y, Z wektora normalnego.
    ///     Materiały źródłowe:
    ///     https://pl.wikipedia.org/wiki/Mapowanie_normalnych
    ///     http://blog.digitaltutors.com/bump-normal-and-displacement-maps/
    /// </summary>
    public class NormalMap
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Vector3[,] _map;

        /// <summary>
        ///     Tworzy mapę normalnych z bitmapy danej w argumencie.
        /// </summary>
        public NormalMap(IReadOnlyBitmap bmp)
        {
            _width = bmp.Width;
            _height = bmp.Height;

            _map = new Vector3[_width, _height];

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    var pix = bmp.GetPixel(x, y);
                    _map[x, y] = new Vector3(
                        2*(pix.R/(float) byte.MaxValue) - 1,
                        2*(pix.G/(float) byte.MaxValue) - 1,
                        2*(pix.B/(float) byte.MaxValue) - 1
                        );
                }
            }
        }

        /// <summary>
        ///     Zwraca wektor normalny dla danych współrzędnych na mapie.
        /// </summary>
        /// <returns></returns>
        public Vector3 this[int x, int y] => _map[x%_width, y%_height];
    }
}