using System.Numerics;

namespace PrismGraphics.Rasterizer
{
    /// <summary>
    /// The <see cref="Light"/> class, used to represent a light's type, position, rotation, and color.
    /// </summary>
    public class Light
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Light"/> class.
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Rotation"></param>
        /// <param name="Color"></param>
        /// <param name="Type"></param>
        public Light(Vector3 Position, Vector3 Rotation, LightTypes Type, Color Color)
        {
            this.Position = Position;
            this.Rotation = Rotation;
            this.Color = Color;
            this.Type = Type;
        }

		#region Fields

		public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public LightTypes Type { get; set; }
        public Color Color { get; set; }

		#endregion
	}
}