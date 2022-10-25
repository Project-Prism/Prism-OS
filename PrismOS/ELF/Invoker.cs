using Cosmos.Core.Memory;
using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace PrismOS.ELF
{
	public static unsafe class Invoker
	{
		public static uint Offset;
		public static uint eax, ebx, ecx, edx, esi, edi, esp, ebp;
		public static uint* Stack = (uint*)Heap.Alloc(1024);

		public static void Dump()
		{
			Console.WriteLine($"eax:{eax}, ebx:{ebx}, ecx:{ecx}, edx:{edx}, esi:{esi}, edi:{edi}, esp: {esp}, ebp: {ebp}");
			for (int i = 0; i < 512; i++)
			{
				Console.Write(((byte*)Stack)[i] + " ");
			}
		}

		[PlugMethod(Assembler = typeof(PlugStoreState))]
		public static void StoreState()
		{
			eax = 0;
			ebx = 0;
			ecx = 0;
			edx = 0;
			esi = 0;
			edi = 0;
			esp = 0;
			ebp = 0;
		}

		[PlugMethod(Assembler = typeof(PlugLoadState))]
		public static void LoadState() { }

		[PlugMethod(Assembler = typeof(PlugCall))]
		public static void CallCode() { }
	}

	[Plug(Target = typeof(Invoker))]
	public class PlugStoreState : AssemblerMethod
	{
		public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
		{
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_eax], eax");
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_ebx], ebx");
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_ecx], ecx");
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_edx], edx");
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_edi], edi");
		}
	}

	[Plug(Target = typeof(Invoker))]
	public class PlugLoadState : AssemblerMethod
	{
		public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
		{
			_ = new LiteralAssemblerCode("mov eax, [static_field__PrismOS_ELF_Invoker_eax]");
			_ = new LiteralAssemblerCode("mov ebx, [static_field__PrismOS_ELF_Invoker_ebx]");
			_ = new LiteralAssemblerCode("mov ecx, [static_field__PrismOS_ELF_Invoker_ecx]");
			_ = new LiteralAssemblerCode("mov edx, [static_field__PrismOS_ELF_Invoker_edx]");
			_ = new LiteralAssemblerCode("mov edi, [static_field__PrismOS_ELF_Invoker_edi]");
		}
	}

	[Plug(Target = typeof(Invoker))]
	public class PlugCall : AssemblerMethod
	{
		public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
		{
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_esp], esp");
			_ = new LiteralAssemblerCode("mov [static_field__PrismOS_ELF_Invoker_ebp], ebp");

			_ = new LiteralAssemblerCode("mov eax, [static_field__PrismOS_ELF_Invoker_Stack]");
			_ = new LiteralAssemblerCode("add eax, 50");
			_ = new LiteralAssemblerCode("mov esp, eax");
			_ = new LiteralAssemblerCode("mov ebp, eax");
			_ = new LiteralAssemblerCode("mov eax, [static_field__PrismOS_ELF_Invoker_Offset]");
			_ = new LiteralAssemblerCode("Call eax");

			_ = new LiteralAssemblerCode("mov ecx, [static_field__PrismOS_ELF_Invoker_Stack]");
			_ = new LiteralAssemblerCode("mov dword [ecx], eax");

			_ = new LiteralAssemblerCode("mov esp, [static_field__PrismOS_ELF_Invoker_esp]");
			_ = new LiteralAssemblerCode("mov ebp, [static_field__PrismOS_ELF_Invoker_ebp]");
		}
	}
}