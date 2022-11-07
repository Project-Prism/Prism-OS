namespace PrismELF.Structure
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