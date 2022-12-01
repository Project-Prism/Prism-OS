namespace PrismRuntime.SShell.Structure
{
	public enum ReturnCode
	{
		Unknown = -1,
		Success = 0,
		CommandNotFound = 1,
		CommandExists = 2,
		NotEnoughArgs = 3,
		TooManyArgs = 4,
	}
}