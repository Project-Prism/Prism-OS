#if IncludeVBE

using Cosmos.Core.Multiboot;

namespace PrismGraphics.Extentions.VESA;

/// <summary>
/// The VBE canvas extention class.
/// </summary>
public unsafe class VBECanvas : Display
{
	/// <summary>
	/// Creates a new instance of the <see cref="VBECanvas"/> class.
	/// </summary>
	public VBECanvas() : base((ushort)Multiboot2.Framebuffer->Width, (ushort)Multiboot2.Framebuffer->Height)
	{
		Timer T = new((O) => { _FPS = _Frames; _Frames = 0; }, null, 1000, 0);
	}

	#region Methods

	public override string GetName()
	{
		return nameof(VBECanvas);
	}

	public override unsafe void Update()
	{
		CopyTo((uint*)Multiboot2.Framebuffer->Address);
		_Frames++;
	}

	public override uint GetFPS()
	{
		return _FPS;
	}

	#endregion

	#region Fields

	private uint _Frames;
	private uint _FPS;

	#endregion
}

#endif