namespace PrismFilesystem.Formats.ELF.Structure
{
    [Flags]
    public enum ELFSectionFlagsType : uint
    {
        None,
        Write,
        Allocate,
        Executable,
    }
}