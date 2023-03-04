#if IncludeVMWARE // Only compile with VMWare drivers if specified.

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
			// Setup the FPS counter timer.
			Global.PIT.RegisterTimer(new(() => { _FPS = _Frames; _Frames = 0; }, 1000000000, true));

			// Set up video driver
			(Video = new()).SetMode(Width, Height);
		}

		#region Properties

		/// <summary>
		/// The total height of the canvas in pixels.
		/// </summary>
		public new ushort Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
				Video.SetMode(Width, Height);
			}
		}

		/// <summary>
		/// The total width of the canvas in pixels.
		/// </summary>
		public new ushort Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
				Video.SetMode(Width, Height);
			}
		}

		#endregion

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
			CopyTo((uint*)Video.VideoMemory.Base);
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

#endif