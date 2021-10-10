using System;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Algorithm
{
    /// <summary>
    ///     Algorytmy rasteryzacji odcinka.
    ///     Dzięki interfejsowi można łatwo zmieniać stosowany algorytm bez znaczących modyfikacji kodu.
    /// </summary>
    public interface ILineDrawingAlgorithm
    {
        void DrawLine(Vector2 begin, Vector2 end, Action<int, int> drawAction);
    }

    /// <summary>
    ///     Ten algorytm znajduje punkt leżący po środku odcinka łączącego dwa punkty, a następnie rysuje tam piksel.
    ///     Następnie wykonuje tę samą operację dla dwóch odcinków powstałych z podzielenia większego odcinka na pół.
    ///     Jest to algorytm rekursywny, dlatego nie stosuję go w projekcie.
    /// </summary>
    public class ResursiveMidpoint : ILineDrawingAlgorithm
    {
        public void DrawLine(Vector2 begin, Vector2 end, Action<int, int> drawAction)
        {
            var delta = end - begin;
            var dist = delta.Length();

            // Jeżeli odstęp między punktami wynosi mniej niż 2px kończymy działanie.
            if (dist < 2) return;

            // Szukamy punktu środkowego między punktem początkowym i końcowym.
            var mid = begin + delta*0.5f;

            // Rysujemy punkt na ekranie.
            drawAction.Invoke((int) mid.X, (int) mid.Y);

            // Rekursywnie wywołujemy algorytm dla odcinka pomiędzy środkiem i początkiem
            // oraz środkiem i końcem.
            DrawLine(begin, mid, drawAction);
            DrawLine(mid, end, drawAction);
        }
    }

    /// <summary>
    ///     Efektywny algorytm rasteryzacji odcinka, który nie jest rekursywny i jest znacznie
    ///     szybszy od algorytmu dzielącego odcinek na pół.
    ///     https://pl.wikipedia.org/wiki/Algorytm_Bresenhama
    /// </summary>
    public class Bresenham : ILineDrawingAlgorithm
    {
        public void DrawLine(Vector2 begin, Vector2 end, Action<int, int> drawAction)
        {
            var x = (int) begin.X;
            var y = (int) begin.Y;

            var x2 = (int) end.X;
            var y2 = (int) end.Y;

            // Definiujemy różnice i błąd.
            var dx = Math.Abs(x2 - x);
            var dy = Math.Abs(y2 - y);
            var sx = x < x2 ? 1 : -1;
            var sy = y < y2 ? 1 : -1;
            var err = dx - dy;

            // Ustawiamy pierwsze współrzędne.
            drawAction.Invoke(x, y);

            while (!(x == x2 && y == y2))
            {
                var e2 = err << 1;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }

                // Ustawiamy współrzędne.
                drawAction.Invoke(x, y);
            }
        }
    }
}