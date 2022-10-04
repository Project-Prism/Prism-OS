using Cosmos.HAL;

namespace PrismOS.Extentions
{
	public static class VMTools
	{
		public static bool IsVirtualBox => GetIsVirtualBox();
		public static bool IsVMWare => GetIsVMWare();
		public static bool IsQEMU => GetIsQEMU();

		private static bool GetIsVirtualBox()
		{
			for (int I = 0; I < PCI.Count; I++)
			{
				if (PCI.Devices[I].VendorID == 0x80EE)
				{
					return true;
				}
			}
			return false;
		}
		private static bool GetIsVMWare()
		{
			for (int I = 0; I < PCI.Count; I++)
			{
				if (PCI.Devices[I].VendorID == 0x15ad)
				{
					return true;
				}
			}
			return false;
		}
		private static bool GetIsQEMU()
		{
			for (int I = 0; I < PCI.Count; I++)
			{
				if (PCI.Devices[I].VendorID == 0x1af4)
				{
					return true;
				}
			}
			return false;
		}
	}
}