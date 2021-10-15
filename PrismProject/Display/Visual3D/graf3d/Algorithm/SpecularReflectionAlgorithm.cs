using System;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Algorithm
{
    public interface ISpecularReflectionAlgorithm
    {
        /// <summary>
        ///     Specular to światło odbite, tworzące "błyszczący punkt", który jest widoczny na gładkich powierzchniach.
        ///     Wielkość i rozkład tego odbicia zależy od współczynnika i algorytmu który zastosujemy. Odbicie jest dynamiczne i
        ///     jego widoczność zależy od kąta pod którym obserwujemy obiekt.
        /// </summary>
        /// <param name="normal">
        ///     Wektor normalny do powierzchni modelu (normal vector).
        /// </param>
        /// <param name="observerDir">
        ///     Kierunek obserwacji (observer direction).
        /// </param>
        /// <param name="light">
        ///     Kierunek padania światła (incoming light direction).
        /// </param>
        /// <param name="power">
        ///     Współczynnik odbicia.
        /// </param>
        float Specular(Vector3 normal, Vector3 observerDir, Vector3 light, float power);
    }

    /// <summary>
    ///     Odbicie Phonga. Nieużywane.
    /// </summary>
    public class PhongSpecularAlgorithm : ISpecularReflectionAlgorithm
    {
        public float Specular(Vector3 normal, Vector3 observerDir, Vector3 light, float power = 200)
        {
            var refLightDir = (normal*2 - light).Normalize();
            var dot = Vector3.Dot(observerDir, refLightDir).Saturate();
            var sf = (float) Math.Pow(dot, power);
            return sf;
        }
    }

    /// <summary>
    ///     Odbicie Blinna-Phonga.
    /// </summary>
    public class BlinnPhongSpecularAlgorithm : ISpecularReflectionAlgorithm
    {
        public float Specular(Vector3 normal, Vector3 view, Vector3 light, float power = 200)
        {
            var halfway = ((light + view)*0.5f).Normalize();
            var dot = Vector3.Dot(normal, halfway).Saturate();
            var sf = (float) Math.Pow(dot, power);
            return sf;
        }
    }
}