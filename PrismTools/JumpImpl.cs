using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace PrismTools
{
	public static class JumpImpl
	{
		[PlugMethod(Assembler = typeof(JumpImplAsm))]
		public static void JumpFar(uint Address) => throw new NotImplementedException();

		private class JumpImplAsm : AssemblerMethod
		{
			public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
			{
				_ = new LiteralAssemblerCode("jmp far [esp+4]");
			}
		}
	}
}