namespace PrismFilesystem.Formats.ELF.Structure
{
    public enum ELFType : ushort
    {
        None,
        Relocatable,
        Executable,
        Dynamic,
        Core,
    }
}