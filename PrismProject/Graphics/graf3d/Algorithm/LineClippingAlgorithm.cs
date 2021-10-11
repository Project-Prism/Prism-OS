using System;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Algorithm
{
    /// <summary>
    ///     Algorytmy obcinania odcinka do prostokątnego okna.
    ///     Dzięki interfejsowi można łatwo zmieniać stosowany algorytm bez znaczących modyfikacji kodu.
    /// </summary>
    public interface IClippingAlgorithm
    {
        /// <summary>
        ///     Ustawia ograniczający prostokąt, poza którym odcinki będą obcinane.
        /// </summary>
        void SetBoundingRectangle(Vector2 begin, Vector2 end);

        /// <summary>
        ///     Przycina odcinek do ograniczającego go prostokąta.
        ///     Zwraca TRUE, jeśli odcinek ma część znajdującą się na ekranie.
        ///     Zwraca FALSE, jeśli całość odcinka jest poza ekranem.
        /// </summary>
        bool ClipLine(ref Vector2 begin, ref Vector2 end);
    }

    /// <summary>
    ///     Algorytm Lianga-Barsky’ego.
    ///     Ten algorytm jest bardziej wydajny niż algorytm Cohena-Sutherlanda w przypadku
    ///     gdy zachodzi konieczność przycięcia odcinka. Pomysłem w algorytmie Lianga-Barsky'ego
    ///     jest wykonywanie tylu porównań, na ile jest to możliwe przed właściwym obliczaniem
    ///     końców przyciętego odcinka.
    ///     Materiały:
    ///     https://pl.wikipedia.org/wiki/Algorytm_Lianga-Barsky%E2%80%99ego
    ///     http://www.skytopia.com/project/articles/compsci/clipping.html
    /// </summary>
    public class LiangBarskyClipping : ClippingAlgorithm, IClippingAlgorithm
    {
        private float _t0;
        private float _t1;

        public bool ClipLine(ref Vector2 begin, ref Vector2 end)
        {
            var delta = end - begin;
            _t0 = 0;
            _t1 = 1;

            if (!Clip(-delta.X, -ClipMin.X + begin.X)) return false; // Lewa krawędź ekranu.
            if (!Clip(delta.X, ClipMax.X - begin.X)) return false; // Prawa krawędź ekranu.
            if (!Clip(-delta.Y, -ClipMin.Y + begin.Y)) return false; // Dolna krawędź ekranu.
            if (!Clip(delta.Y, ClipMax.Y - begin.Y)) return false; // Górna krawędź ekranu.

            if (_t1 < 1)
            {
                end = begin + delta*_t1;
            }
            if (_t0 > 0)
            {
                begin += delta*_t0;
            }

            return true;
        }

        private bool Clip(float p, float q)
        {
            if (Math.Abs(p) < float.Epsilon)
            {
                // Równoległy odcinek poza granicami.
                if (q < 0) return false;
            }
            else
            {
                var r = q/p;

                if (p < 0)
                {
                    if (r > _t1) return false; // Nie rysuj w ogóle.
                    if (r > _t0) _t0 = r; // Odcinek został przycięty.
                }
                else
                {
                    if (r < _t0) return false; // Nie rysuj w ogóle.
                    if (r < _t1) _t1 = r; // Odcinek został przycięty.
                }
            }
            return true;
        }
    }

    public abstract class ClippingAlgorithm
    {
        protected Vector2 ClipMax;
        protected Vector2 ClipMin;

        public void SetBoundingRectangle(Vector2 begin, Vector2 end)
        {
            ClipMin = begin;
            ClipMax = end;
        }
    }
}