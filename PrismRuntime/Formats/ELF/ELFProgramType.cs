namespace PrismRuntime.Formats.ELF;

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