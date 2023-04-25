using PrismGraphics.Extentions.VMWare;
using PrismGraphics.Extentions.VESA;
using Cosmos.System;
using Cosmos.Core.Multiboot;

namespace PrismGraphics.Extentions;

/// <summary>
/// The generic display interface. Used to abstract driver classes and get display output.
/// </summary>
public abstract class Display : Graphics
{
	public Display(ushort Width, ushort Height) : base(Width, Height) { }

	#region Methods

	/// <summary>
	/// Gets a display output, the best mode is automatically chosen.
	/// </summary>
	/// <param name="Width"></param>
	/// <param name="Height"></param>
	/// <returns></returns>
	public static Display GetDisplay(ushort Width, ushort Height)
	{
		if (Multiboot2.IsVBEAvailable)
		{
			return new VBECanvas();
		}
		if (VMTools.IsVMWare)
		{
			return new SVGAIICanvas(Width, Height);
		}

		throw new NotImplementedException("No display is available!");
	}

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
	public abstract uint GetFPS();

	#endregion
}