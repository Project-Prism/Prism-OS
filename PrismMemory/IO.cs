using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace PrismMemory
{
	public unsafe static class IO
    {
		#region 32 bit

		[PlugMethod(Assembler = typeof(AsmCopyUint))]
        public static void Copy32(uint* Destination, uint* Source, uint Length)
        {
            throw new NotImplementedException();
        }
        [PlugMethod(Assembler = typeof(AsmSetUint))]
        public static void Fill32(uint* Destination, uint Value, uint Length)
        {
            throw new NotImplementedException();
        }

        public class AsmCopyUint : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                _ = new LiteralAssemblerCode("mov esi, [esp+12]");
                _ = new LiteralAssemblerCode("mov edi, [esp+16]");
                _ = new LiteralAssemblerCode("cld");
                _ = new LiteralAssemblerCode("mov ecx, [esp+8]");
                _ = new LiteralAssemblerCode("rep movsd");
            }
        }
        public class AsmSetUint : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                _ = new LiteralAssemblerCode("mov eax, [esp+12]");
                _ = new LiteralAssemblerCode("mov edi, [esp+16]");
                _ = new LiteralAssemblerCode("cld");
                _ = new LiteralAssemblerCode("mov ecx, [esp+8]");
                _ = new LiteralAssemblerCode("rep stosd");
            }
        }

        #endregion

        #region 8 bit

        [PlugMethod(Assembler = typeof(AsmCopyBytes))]
        public static void Copy8(byte* Destination, byte* Source, uint Length)
        {
            throw new NotImplementedException();
        }
        [PlugMethod(Assembler = typeof(AsmSetByte))]
        public static void Fill8(byte* Destination, byte Value, uint Length)
        {
            throw new NotImplementedException();
        }

        public class AsmCopyBytes : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                _ = new LiteralAssemblerCode("mov esi, [esp+12]");
                _ = new LiteralAssemblerCode("mov edi, [esp+16]");
                _ = new LiteralAssemblerCode("cld");
                _ = new LiteralAssemblerCode("mov ecx, [esp+8]");
                _ = new LiteralAssemblerCode("rep movsb");
            }
        }
        public class AsmSetByte : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                _ = new LiteralAssemblerCode("mov al, [esp+12]");
                _ = new LiteralAssemblerCode("mov edi, [esp+16]");
                _ = new LiteralAssemblerCode("cld");
                _ = new LiteralAssemblerCode("mov ecx, [esp+8]");
                _ = new LiteralAssemblerCode("rep stosb");
            }
        }

        #endregion
    }
}
