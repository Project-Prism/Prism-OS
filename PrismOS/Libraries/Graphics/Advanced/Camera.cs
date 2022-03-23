using PrismOS.Libraries.Numerics;

namespace PrismOS.Libraries.Graphics.Advanced
{
    public class Camera
    {
        public Camera(Vector3<float> Position, Vector3<float> Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;
        }

        public Vector3<float> Position, Rotation;
    }
}