namespace PrismOS.ELF.Structure
{
    public enum RelocationType
    {
        R386None = 0, // No relocation
        R38632 = 1, // Symbol + Offset
        R386Pc32 = 2  // Symbol + Offset - Section Offset
    };
}