namespace PrismRuntime.Formats.ELF;

public enum ELFSectionType : uint
{
    Null,
    ProgramBits,
    SymbolTable,
    StringTable,
    RelocationAddress,
    Hash,
    Dynamic,
    Note,
    NoBits,
    Relocation,
    SHLibrary,
    DynamicSymbol,
}