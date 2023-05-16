using Cosmos.Core;
using Cosmos.HAL;

namespace PrismAPI.Hardware.GPU.Intel.Skylake;

public class MMIO
{
	#region Constructors

	public MMIO(PCIDevice Device)
	{
		this.Device = Device;
	}

	#endregion

	#region Methods

	public uint GetMMIO(MMIOUnit Unit, uint Offset)
	{
		return IOPort.Read32((int)(Device.BAR0 + (uint)Unit + Offset));
	}

	public void SetMMIO(MMIOUnit Unit, uint Offset, uint Value)
	{
		IOPort.Write32((int)(Device.BAR0 + (uint)Unit + Offset), Value);
	}

	public ushort GetMMIO(MMIOUnit Unit, ushort Offset)
	{
		return IOPort.Read16((int)(Device.BAR0 + (uint)Unit + Offset));
	}

	public void SetMMIO(MMIOUnit Unit, uint Offset, ushort Value)
	{
		IOPort.Write16((int)(Device.BAR0 + (uint)Unit + Offset), Value);
	}

	#endregion

	#region Fields

	public readonly PCIDevice Device;

	#endregion
}