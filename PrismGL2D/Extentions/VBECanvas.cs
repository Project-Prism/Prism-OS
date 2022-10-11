using Cosmos.Core;

namespace PrismGL2D.Extentions
{
	/// <summary>
	/// The VBE canvas extention class.
	/// </summary>
	public class VBECanvas : Graphics
	{
		/// <summary>
		/// Creates a new instance of the <see cref="VBECanvas"/> class.
		/// </summary>
		public VBECanvas() : base(VBE.getModeInfo().width, VBE.getModeInfo().height) { }

		/// <summary>
		/// Coppies the linear buffer to vram.
		/// </summary>
		public unsafe void Update()
		{
			CopyTo((uint*)VBE.getLfbOffset());
		}
	}
}