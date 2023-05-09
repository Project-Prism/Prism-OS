namespace PrismAPI.Filesystem.Formats.ELF;

/// <summary>
/// Endian marker enum.
/// </summary>
public enum ELFEndianType
{
    /// <summary>
    /// Invalid file type.
    /// </summary>
    Invalid,
    /// <summary>
    /// Least significant first. (Little Endian)
    /// </summary>
    LSB,
    /// <summary>
    /// Most significant first. (Big Endian)
    /// </summary>
    MSB,
}