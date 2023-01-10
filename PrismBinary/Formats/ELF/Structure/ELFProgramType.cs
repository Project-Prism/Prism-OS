namespace PrismBinary.Formats.ELF.Structure
{
	public enum ELFProgramType : uint
	{
		Null,
		Load,
		Dynamic,
		Interperit,
		Note,
		SHLibrary,
		PHeader,
		TLS,
	}
}