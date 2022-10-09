using Cosmos.Core;

namespace PrismGL2D.Extentions
{
	public class VBECanvas : Graphics
	{
		public VBECanvas() : base(VBE.getModeInfo().width, VBE.getModeInfo().height) { }

		public unsafe void Update()
		{
			CopyTo((uint*)VBE.getLfbOffset());
		}
	}
}