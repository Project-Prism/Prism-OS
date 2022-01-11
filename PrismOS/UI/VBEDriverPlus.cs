using Cosmos.Core;
using Cosmos.Core.IOGroup;
using Cosmos.HAL.Drivers;

namespace PrismOS.UI
{
    // Thanks to zarlo for this
    public class VBEDriverPlus : VBEDriver
    {
        private readonly VBEIOGroup IO = Global.BaseIOGroups.VBE;
        public VBEDriverPlus(ushort xres, ushort yres, ushort bpp) : base(xres, yres, bpp)
        {
        }
        public void SetVram(int[] Data)
        {
            IO.LinearFrameBuffer.Copy(Data);
        }
    }
}