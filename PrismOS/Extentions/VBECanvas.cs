using Cosmos.Core;
using PrismGL2D;

namespace PrismOS.Extentions
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