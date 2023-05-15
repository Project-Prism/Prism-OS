namespace PrismAPI.Tools;

/// <summary>
/// The <see cref="ByteFormatter"/> class. Used to format byte counts into the requested format.
/// </summary>
public static class ByteFormatter
{
	#region Constants

	public const ulong BytesPerTerrabye = BytesPerGigabyte * 1000;
	public const ulong BytesPerGigabyte = BytesPerMegabyte * 1000;
	public const ulong BytesPerMegabyte = BytesPerKilobyte * 1000;
	public const ulong BytesPerKilobyte = 1024;

	#endregion

	#region Methods

	public static ulong GetTerabytes(ulong Bytes) => Bytes / BytesPerTerrabye;

	public static ulong GetGigabytes(ulong Bytes) => Bytes / BytesPerGigabyte;

    public static ulong GetMegaBytes(ulong Bytes) => Bytes / BytesPerMegabyte;

	public static ulong GetKilobytes(ulong Bytes) => Bytes / BytesPerKilobyte;

	#endregion
}