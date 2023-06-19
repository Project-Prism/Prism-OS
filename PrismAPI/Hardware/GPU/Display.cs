using PrismAPI.Hardware.GPU.VMWare;
using PrismAPI.Hardware.GPU.Vesa;
using Cosmos.Core.Multiboot;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.Hardware.GPU;

/// <summary>
/// The generic display interface. Used to abstract driver classes and get display output.
/// </summary>
public abstract class Display : Canvas
{
	#region Constructors

	/// <summary>
	/// A generic constructor used to initialize the FPS counter.
	/// </summary>
	/// <param name="Width">The Width of the display.</param>
	/// <param name="Height">The Height of the display.</param>
	internal Display(ushort Width, ushort Height) : base(Width, Height)
	{
		// Setup the FPS counter timer.
		Timer T = new((_) => { _FPS = _Frames; _Frames = 0; }, null, 1000, 0);

		// Set up the mouse manager.
		MouseManager.ScreenHeight = Height;
		MouseManager.ScreenWidth = Width;
	}

	#endregion

	#region Properties

	/// <summary>
	/// A toggle telling whether the display is enabled or not.
	/// </summary>
	public abstract bool IsEnabled { get; set; }

	#endregion

	#region Methods

	/// <summary>
	/// Gets a display output, the best mode is automatically chosen.
	/// The Width and Height arguments may not always be used.
	/// </summary>
	/// <param name="Width">The requested Width of the display.</param>
	/// <param name="Height">The requested Height of the display.</param>
	/// <returns>An instance of the display class.</returns>
	public static Display GetDisplay(ushort Width, ushort Height)
	{
		if (VMTools.IsVMWare)
		{
			return new SVGAIICanvas(Width, Height);
		}
		if (Multiboot2.IsVBEAvailable)
		{
			return new VBECanvas();
		}

		throw new NotImplementedException("No display is available!");
	}

	/// <summary>
	/// Sets the position of the hardware accelerated cursor on the screen.
	/// Please note that this may not work on all display methods.
	/// </summary>
	/// <param name="X">The X-axis position.</param>
	/// <param name="Y">The Y-axis position.</param>
	public abstract void SetCursor(uint X, uint Y, bool IsVisible);

	/// <summary>
	/// Sets the image of the hardware accelerated cursor.
	/// Please note that this may not work on all display methods.
	/// </summary>
	/// <param name="Cursor">The image to use as the cursor.</param>
	public abstract void DefineCursor(Canvas Cursor);

	/// <summary>
	/// Gets the display driver's name.
	/// </summary>
	/// <returns>the display name.</returns>
	public abstract string GetName();

	/// <summary>
	/// Coppies the second buffer to the primary display buffer.
	/// </summary>
	public abstract void Update();

	/// <summary>
	/// Gets the FPS measurment of the display.
	/// </summary>
	/// <returns>The FPS as a uint number.</returns>
	public uint GetFPS()
	{
		return _FPS;
	}

	#endregion

	#region Fields

	/// <summary>
	/// The internal frame counter, used for FPS calculation.
	/// </summary>
	internal uint _Frames;

	/// <summary>
	/// The internal FPS value, returned from <see cref="GetFPS()"/>.
	/// </summary>
	internal uint _FPS;

	#endregion
}