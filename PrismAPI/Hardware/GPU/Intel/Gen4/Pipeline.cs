namespace PrismAPI.Hardware.GPU.Intel.Gen4;

public static class Pipeline
{
	/// <summary>
	/// Get the pipeline select command.
	/// </summary>
	/// <param name="Pipeline">0 == 3D, 1 == Media</param>
	/// <returns>A 3D pipeline select command.</returns>
	public static uint GetSelectCommand(bool Pipeline)
	{
		if (Pipeline)
		{
			return 0b_0011_0010_0000_1000_0000_0000_0000_0001;
		}
		else
		{
			return 0b_0011_0010_0000_1000_0000_0000_0000_0000;
		}
	}

	public static uint GetURBFenceOPCode(bool CSRealloc, bool VFERealloc, bool SFRealloc, bool ClipRealloc, bool GSRealloc, bool VSRealloc)
	{
		uint Value = 0b_1100_0000_0000_0000_0000_0001;

		if (CSRealloc)
		{
			Value |= 0b_0000_0000_0000_0001_0000_0000;
		}
		if (VFERealloc)
		{
			Value |= 0b_0000_0000_0000_0000_1000_0000;
		}
		if (SFRealloc)
		{
			Value |= 0b_0000_0000_0000_0000_0100_0000;
		}
		if (ClipRealloc)
		{
			Value |= 0b_0000_0000_0000_0000_0010_0000;
		}
		if (GSRealloc)
		{
			Value |= 0b_0000_0000_0000_0000_0000_1000;
		}
		if (VSRealloc)
		{
			Value |= 0b_0000_0000_0000_0000_0000_0100;
		}

		return Value;
	}
}