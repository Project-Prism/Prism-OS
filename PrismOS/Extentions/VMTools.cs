using Cosmos.HAL;

namespace PrismOS.Extentions
{
	public static class VMTools
	{
		public static bool IsVirtualBox => GetIsVirtualBox();

		private static bool GetIsVirtualBox()
		{
			if (PCI.Exists((VendorID)0x80EE, (DeviceID)0xCAFE))
			{
				return true;
			}
			return false;
		}
	}
}