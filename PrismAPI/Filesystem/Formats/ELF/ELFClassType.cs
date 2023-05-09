namespace PrismAPI.Filesystem.Formats.ELF;

/// <summary>
/// ELF file class descriptors.
/// </summary>
public enum ELFClassType
{
    /// <summary>
    /// Invalid file type.
    /// </summary>
    Invalid,
    /// <summary>
    /// 32-bit file.
    /// </summary>
    Class32,
    /// <summary>
    /// 63-bit file.
    /// </summary>
    Class64,
}