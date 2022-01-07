using Cosmos.Core;
using Cosmos.Core.IOGroup;
using Cosmos.HAL.Drivers;

namespace PrismOS.UI
{
    // Thanks to zarlo for this
    public class VBEDriverPlus : VBEDriver
    {
        private readonly VBEIOGroup IO;
        public VBEDriverPlus(ushort xres, ushort yres, ushort bpp) : base(xres, yres, bpp)
        {
            IO = Global.BaseIOGroups.VBE;
        }
        public void BufferLessCopyVRAM(int[] aData)
        {
            IO.LinearFrameBuffer.Copy(aData);
        }
    }
}