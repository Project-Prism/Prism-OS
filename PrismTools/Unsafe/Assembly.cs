using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace PrismTools.Unsafe
{
	public static class Assembly
	{
		/// <summary>
		/// An implementation for far-jump in C#.
		/// Used for ELF files.
		/// </summary>
		/// <param name="Address">Address we want to jump to.</param>
		/// <exception cref="NotImplementedException">Thrown on not-plugged error.</exception>
		[PlugMethod(Assembler = typeof(JumpFarImplAsm))]
		public static void JumpFar(uint Address) => throw new NotImplementedException();

		/// <summary>
		/// An implementation for jump in C#.
		/// </summary>
		/// <param name="Address">Address we want to jump to.</param>
		/// <exception cref="NotImplementedException">Thrown on not-plugged error.</exception>
		[PlugMethod(Assembler = typeof(JumpImplAsm))]
		public static void Jump(uint Address) => throw new NotImplementedException();

		private class JumpFarImplAsm : AssemblerMethod
		{
			public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
			{
				_ = new LiteralAssemblerCode("jmp far [esp+4]");
			}
		}
		private class JumpImplAsm : AssemblerMethod
		{
			public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
			{
				_ = new LiteralAssemblerCode("jmp [esp+4]");
			}
		}
	}
}