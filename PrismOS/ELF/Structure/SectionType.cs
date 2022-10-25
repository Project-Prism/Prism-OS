namespace PrismOS.ELF.Structure
{
    public enum SectionType
    {
        None = 0,
        ProgramInformation = 1,
        SymbolTable = 2,
        StringTable = 3,
        RelocationAddend = 4,
        NotPresentInFile = 8,
        Relocation = 9,
    }
}