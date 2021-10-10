using System.Collections.Generic;
using graf3d.Engine.Oświetlenie;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Component
{
    /// <summary>
    ///     Klasa reprezentująca trójwymiarową scenę.
    /// </summary>
    public class Scene
    {
        /// <summary>
        ///     Lista świateł oświetlających scenę. Używane tylko do zadania 2.
        /// </summary>
        public readonly List<Light> Lights = new List<Light>();

        /// <summary>
        ///     Implementacja kamery, która obserwuje scenę.
        /// </summary>
        public Camera Camera;

        /// <summary>
        ///     Implementacja materiału, z którego wykonane są przedmioty na scenie. Używane tylko do zadania 2.
        /// </summary>
        public Material Material;

        /// <summary>
        ///     Lista modeli występujących na scenie.
        /// </summary>
        public List<Mesh> Meshes = new List<Mesh>();

        /// <summary>
        ///     Klasa obliczająca wektor normalny i współrzędnych. Używane tylko do zadania 2.
        /// </summary>
        public ISurfaceShader SurfaceShader;
    }
}