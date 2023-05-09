namespace PrismAPI.Filesystem.Formats.ELF;

public enum ELFType : ushort
{
    None,
    Relocatable,
    Executable,
    Dynamic,
    Core,
}