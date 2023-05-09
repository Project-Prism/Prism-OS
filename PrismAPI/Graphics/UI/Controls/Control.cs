using PrismAPI.Graphics;
using PrismAPI.Graphics.UI;

namespace PrismAPI.Graphics.UI.Controls;

/// <summary>
/// The base control class, used to represent a UI control.
/// </summary>
public abstract class Control
{
	/// <summary>
	/// Creates a new instance of the <see cref="Control"/> class. It must be used as a base constructor.
	/// </summary>
	public Control(int X, int Y, ushort Width, ushort Height, ushort Radius)
	{
		this.Radius = Radius;
		this.Height = Height;
		this.Width = Width;
		this.X = X;
		this.Y = Y;
	}

	#region Properties

	/// <summary>
	/// The main image to use for drawing.
	/// The value returned is automatically specified by the control implementation.
	/// </summary>
	public abstract Canvas MainImage { get; }

	/// <summary>
	/// The control's border radius.
	/// </summary>
	public ushort Radius
	{
		get
		{
			return _Radius;
		}
		set
		{
			_Radius = value;
			Render();
		}
	}

	/// <summary>
	/// The control's pixel height.
	/// </summary>
	public ushort Height
	{
		get
		{
			return _Height;
		}
		set
		{
			_Height = value;
			Render();
		}
	}

	/// <summary>
	/// The control's pixel width.
	/// </summary>
	public ushort Width
	{
		get
		{
			return _Width;
		}
		set
		{
			_Width = value;
			Render();
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// This function re-renders the control, Only use when changing values.
	/// </summary>
	public abstract void Render();

	#endregion

	#region Fields

	internal Window? Window;
	private ushort _Radius;
	private ushort _Height;
	private ushort _Width;

	/// <summary>
	/// The control's X position.
	/// </summary>
	public int X;

	/// <summary>
	/// The control's Y position.
	/// </summary>
	public int Y;

	#endregion
}