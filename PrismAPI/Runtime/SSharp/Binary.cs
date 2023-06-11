using PrismAPI.Runtime.SSharp.Structure;

namespace PrismAPI.Runtime.SSharp;

public unsafe class Binary
{
	/// <summary>
	/// Creates a new instance of the <see cref="Binary"/> class.
	/// </summary>
	/// <param name="Binary">Raw executable binary.</param>
	public Binary(byte[] Binary)
	{
		ROM = new(new MemoryStream(Binary));
		IsEnabled = true;
	}

	#region Methods

	/// <summary>
	/// Dumps the data as human-readable text to the console.
	/// </summary>
	public void Dump()
	{
		// Backup and reset position so we can continue later.
		long BPoint = ROM.BaseStream.Position;
		ROM.BaseStream.Position = 0;

		for (; ROM.BaseStream.Position < ROM.BaseStream.Length - 1;)
		{
			// Check instruction type.
			switch ((OPCode)ROM.ReadByte())
			{
				case OPCode.System_Runtime_ThrowException:
					Console.WriteLine($"System_Runtime_ThrowException: {ROM.ReadString()}");
					break;
				case OPCode.System_Console_WriteLine:
					Console.WriteLine($"System_Console_WriteLine: {ROM.ReadString()}");
					break;
				case OPCode.System_Console_Write:
					Console.WriteLine($"System_Console_Write: {ROM.ReadString()}");
					break;
				case OPCode.System_Runtime_Exit:
					Console.WriteLine("System_Runtime_Exit");
					break;
				case OPCode.System_Inline_Jump:
					Console.WriteLine("System_Inline_Jump");
					break;
			}
		}

		// Return to original position.
		ROM.BaseStream.Position = BPoint;
	}

	/// <summary>
	/// Runs next instruction in the executable.
	/// </summary>
	public void Next()
	{
		// Make sure executable can run.
		if (ROM.BaseStream.Position == ROM.BaseStream.Length)
		{
			IsEnabled = false;
		}
		if (!IsEnabled)
		{
			return;
		}

		// Check instruction type.
		switch ((OPCode)ROM.ReadByte())
		{
			case OPCode.System_Runtime_ThrowException:
				Console.WriteLine($"Exception: {ROM.ReadString()}");
				break;
			case OPCode.System_Console_WriteLine:
				Console.WriteLine(ROM.ReadString());
				break;
			case OPCode.System_Console_Write:
				Console.Write(ROM.ReadString());
				break;
			case OPCode.System_Runtime_Exit:
				IsEnabled = false;
				break;
			case OPCode.System_Inline_Jump:
				ROM.BaseStream.Position = (long)ROM.ReadUInt64();
				break;
		}
	}

	#endregion

	#region Fields

	public BinaryReader ROM;
	public bool IsEnabled;

	#endregion
}