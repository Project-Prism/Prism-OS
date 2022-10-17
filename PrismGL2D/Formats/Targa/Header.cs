using System.Runtime.InteropServices;

namespace PrismGL2D.Formats.Targa
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Header
	{
		public char Magic1;						    // must be zero
		public char ColorMap;						// must be zero
		public char Encoding;						// must be 2
		public short CMaporig, CMaplen, CMapent;	// must be zero
		public short X;								// must be zero
		public short Y;								// image's height
		public short Height;						// image's height
		public short Width;							// image's width
		public char ColorDepth;						// must be 32
		public char PixelType;				        // must be 40
	}
}