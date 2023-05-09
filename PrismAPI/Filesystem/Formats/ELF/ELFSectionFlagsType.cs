namespace PrismAPI.Filesystem.Formats.ELF;

[Flags]
public enum ELFSectionFlagsType : uint
{
    None,
    Write,
    Allocate,
    Executable,
}