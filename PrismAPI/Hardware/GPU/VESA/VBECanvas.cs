using Cosmos.Core.Multiboot;
using PrismAPI.Graphics;

namespace PrismAPI.Hardware.GPU.Vesa;

/// <summary>
/// The VBE canvas extention class.
/// </summary>
public unsafe class VBECanvas : Display
{
    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="VBECanvas"/> class.
    /// </summary>
    public VBECanvas() : base((ushort)Multiboot2.Framebuffer->Width, (ushort)Multiboot2.Framebuffer->Height) { }

    #endregion

    #region Methods

    public override void DefineCursor(Canvas Cursor)
    {
        throw new NotSupportedException("VBE does not offer hardware-accelerated cursor support.");
    }

    public override void SetCursor(uint X, uint Y, bool IsVisible)
    {
        throw new NotSupportedException("VBE does not offer hardware-accelerated cursor support.");
    }
    public override void SetMode(ushort Width, ushort Height)
    {
        throw new NotImplementedException("VBE does not offer to set mode.");
    }

    public override string GetName()
    {
        return nameof(VBECanvas);
    }

    public override void Update()
    {
        CopyTo((uint*)Multiboot2.Framebuffer->Address);
        _Frames++;
    }

    public override void Disable()
    {
        throw new NotImplementedException("VBE does not offer to disable the display.");
    }
    #endregion
}