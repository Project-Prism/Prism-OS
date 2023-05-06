namespace PrismTools;

/// <summary>
/// The <see cref="ByteFormatter"/> class. Used to format byte counts into the requested format.
/// </summary>
public static class ByteFormatter
{
	public static ulong GetTerabytes(ulong Bytes) => Bytes / (1024 ^ 8);

	public static ulong GetGigabytes(ulong Bytes) => Bytes / (1024 ^ 4);

	public static ulong GetMegaBytes(ulong Bytes) => Bytes / (1024 ^ 2);

	public static ulong GetKilobytes(ulong Bytes) => Bytes / 1024;
}