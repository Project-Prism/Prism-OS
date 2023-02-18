using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.HAL;

namespace PrismGraphics.Extentions.VMWare
{
	public unsafe class SVGAIICanvas : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="SVGAIICanvas"/> class.
		/// </summary>
		/// <param name="Width">Total width (in pixels) of the canvas.</param>
		/// <param name="Height">Total height (int pixels) of the canvas.</param>
		public SVGAIICanvas(ushort Width, ushort Height) : base(Width, Height)
		{
			// Setup the VMWare driver & set it's mode.
			(Video = new()).SetMode(Width, Height, 32);

			// Setup the FPS counter timer.
			Global.PIT.RegisterTimer(new(() => { _FPS = _Frames; _Frames = 0; }, 1000000000, true));
		}

		#region Methods

		/// <summary>
		/// Gets the FPS of the canvas.
		/// </summary>
		/// <returns>Canvas's FPS.</returns>
		public uint GetFPS()
		{
			return _FPS;
		}

		/// <summary>
		/// Coppies the linear buffer to vram.
		/// </summary>
		public void Update()
		{
			Buffer.MemoryCopy(Internal, (uint*)Video.VideoMemory.Base, Size * 4, Size * 4);
			Video.Update(0, 0, Width, Height);
			_Frames++;
		}

		#endregion

		#region Fields

		private readonly VMWareSVGAII Video;
		private uint _Frames;
		private uint _FPS;

		#endregion
	}
}