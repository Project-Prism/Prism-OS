namespace PrismELF.Structure
{
    public enum SymbolBinding
    {
        Local = 0, // Local scope
        Global = 1, // Global scope
        Weak = 2  // Weak, (ie. __attribute__((weak)))
    }
}