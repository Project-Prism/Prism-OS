using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.HAL;

namespace PrismGraphics.Extentions.VMWare
{
	public unsafe class SVGAIICanvas : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="SVGAIICanvas"/> class.
		/// The video driver is automatically set up in the Height and Width properties.
		/// </summary>
		/// <param name="Width">Total width (in pixels) of the canvas.</param>
		/// <param name="Height">Total height (int pixels) of the canvas.</param>
		public SVGAIICanvas(ushort Width, ushort Height) : base(Width, Height)
		{
			// Setup the FPS counter timer.
			Global.PIT.RegisterTimer(new(() => { _FPS = _Frames; _Frames = 0; }, 1000000000, true));
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
				// Set up the video driver if it is null.
				if (Video == null)
				{
					Video = new();
				}

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
				// Set up the video driver if it is null.
				if (Video == null)
				{
					Video = new();
				}

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
			// Don't do anything if there is no video device.
			if (Video == null)
			{
				return;
			}

			Buffer.MemoryCopy(Internal, (uint*)Video.VideoMemory.Base, Size * 4, Size * 4);
			Video.Update(0, 0, Width, Height);
			_Frames++;
		}

		#endregion

		#region Fields

		private VMWareSVGAII? Video;
		private uint _Frames;
		private uint _FPS;

		#endregion
	}
}