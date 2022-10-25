namespace PrismOS.ELF.Structure
{
    public enum SymbolType
    {
        None = 0, // No type
        Object = 1, // Variables, arrays, etc.
        Function = 2,  // Methods or functions
        Common = 5
    }
}