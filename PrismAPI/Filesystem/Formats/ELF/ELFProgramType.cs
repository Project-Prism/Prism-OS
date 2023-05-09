namespace PrismAPI.Filesystem.Formats.ELF;

public enum ELFProgramType : uint
{
    Null,
    Load,
    Dynamic,
    Interperit,
    Note,
    SHLibrary,
    PHeader,
    TLS,
}