using Cosmos.Core;

namespace PrismGraphics.Extentions
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
			Cosmos.HAL.Global.PIT.RegisterTimer(new(() => { _FPS = _Frames; _Frames = 0; }, 1000000000, true));
		}

		#region Methods

		/// <summary>
		/// Coppies the linear buffer to vram.
		/// </summary>
		public unsafe void Update()
		{
			_Frames++;
			CopyTo((uint*)VBE.getLfbOffset());
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