using IL2CPU.API.Attribs;
using XSharp.Assembler;
using System;

namespace PrismOS.Libraries.Memory
{
    public unsafe class MemoryOperations
    {
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
        public static uint* Allocate(uint Size)
        {
            return (uint*)Cosmos.Core.GCImplementation.AllocNewObject(Size);
        }
        public static void Free(object O)
        {
            Cosmos.Core.GCImplementation.Free(O);
        }
        public static void Free(uint* P)
        {
            Cosmos.Core.Memory.Heap.Free(P);
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
    }
}