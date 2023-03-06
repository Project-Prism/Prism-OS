using PrismGraphics;

namespace PrismUI.Controls
{
	/// <summary>
	/// The base control class, used to represent a UI control.
	/// </summary>
	public abstract class Control
	{
		/// <summary>
		/// Creates a new instance of the <see cref="Control"/> class. It must be used as a base constructor.
		/// </summary>
		public Control(ushort Width, ushort Height, ushort Radius)
		{
			this.Radius = Radius;
			this.Height = Height;
			this.Width = Width;
		}

		#region Properties

		/// <summary>
		/// The main image to use for drawing.
		/// The value returned is automatically specified by the control implementation.
		/// </summary>
		public abstract Graphics MainImage { get; }

		#endregion

		#region Methods

		/// <summary>
		/// This function re-renders the control, Only use when changing values.
		/// </summary>
		public abstract void Render();

		#endregion

		#region Fields

		public ushort Radius;
		public ushort Height;
		public ushort Width;
		public int X;
		public int Y;

		#endregion
	}
}