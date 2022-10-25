namespace PrismELF.Structure
{
    [Flags]
    public enum SectionAttributes
    {
        Write = 0x01,
        Alloc = 0x02,
        Executable = 0x4
    }
}