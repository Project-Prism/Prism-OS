namespace PrismFilesystem.Formats.BIN;

/// <summary>
/// The bin executable class, used to represent and jump to bin programs.
/// </summary>
public unsafe class BINFile
{
	/// <summary>
	/// Creates a new instance of the <see cref="BINFile"/> class.
	/// </summary>
	/// <param name="Binary">The raw bytes of the binary program.</param>
	public BINFile(byte[] Binary)
	{
		fixed (byte* P = Binary)
		{
			// This assigns the delegate pointer to the entry point of the program.
			// It is a delegate as it can be called by just using Main as a method.
			Main = (delegate* unmanaged<void>)P;
		}
	}

	#region Fields

	public readonly delegate* unmanaged<void> Main;

	#endregion
}