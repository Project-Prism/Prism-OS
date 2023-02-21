using System.Numerics;

namespace PrismGraphics.Rasterizer
{
    /// <summary>
    /// The camera class, only used to represent location and rotation of a camera.
    /// </summary>
    public class Camera
    {
        public Vector3 Position = new(0, 0, 0);
        public Vector3 Rotation = new(0, 0, 0);
    }
}