#if IncludeVBE

using Cosmos.Core;

namespace PrismGraphics.Extentions.VESA
{
	/// <summary>
	/// The VBE canvas extention class.
	/// </summary>
	public class VBECanvas : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="VBECanvas"/> class.
		/// </summary>
		public VBECanvas() : base(VBE.getModeInfo().width, VBE.getModeInfo().height)
		{
			Timer T = new((object? O) => { _FPS = _Frames; _Frames = 0; }, null, 1000, 0);
		}

		#region Methods

		/// <summary>
		/// Coppies the linear buffer to vram.
		/// </summary>
		public unsafe void Update()
		{
			CopyTo((uint*)VBE.getLfbOffset());
			_Frames++;
		}

		/// <summary>
		/// Gets the FPS of the canvas.
		/// </summary>
		/// <returns>Canvas's FPS.</returns>
		public uint GetFPS()
		{
			return _FPS;
		}

		#endregion

		#region Fields

		private uint _Frames;
		private uint _FPS;

		#endregion
	}
}

#endif