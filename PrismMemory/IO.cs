using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace PrismMemory
{
	public unsafe static class IO
    {
        public static uint* GetPointer(object Object)
        {
            return Cosmos.Core.GCImplementation.GetPointer(Object);
        }

        public static uint* Allocate(uint Size)
		{
            return (uint*)Cosmos.Core.GCImplementation.AllocNewObject(Size);
		}

        public static void Free(object Object)
		{
            Cosmos.Core.GCImplementation.Free(Object);
		}
        public static void Free(uint* Pointer)
		{
            Cosmos.Core.Memory.Heap.Free(Pointer);
		}

		[PlugMethod(Assembler = typeof(AsmCopyUint))]
        public static void Copy(uint* Destination, uint* Source, uint Length)
        {
            throw new NotImplementedException();
        }
        [PlugMethod(Assembler = typeof(AsmSetUint))]
        public static void Fill(uint* Destination, uint Value, uint Length)
        {
            throw new NotImplementedException();
        }

        [PlugMethod(Assembler = typeof(AsmCopyBytes))]
        public static void Copy(byte* Destination, byte* Source, uint Length)
        {
            throw new NotImplementedException();
        }
        [PlugMethod(Assembler = typeof(AsmSetByte))]
        public static void Fill(byte* Destination, byte Value, uint Length)
        {
            throw new NotImplementedException();
        }

        #region Plugs

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
