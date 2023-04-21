using PrismFilesystem.Formats.CSVG.Lexer;
using PrismGraphics;

namespace PrismFilesystem.Formats.CSVG;

public static class CSVG
{
	public static Graphics ParseCSVG(ushort Width, ushort Height, string Input)
	{
		Graphics Main = new(Width, Height);
		Bundler Bunder = new(Input);

		return Main;
	}
}