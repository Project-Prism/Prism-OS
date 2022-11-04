using Cosmos.Core.Memory;
using Cosmos.Core;

namespace PrismTools
{
	public static unsafe class Syscall
	{
		public static void Call(ref uint* EAX, ref uint* EBX, ref uint* ECX, ref uint* EDX, ref uint* ESI, ref uint* EDI)
		{
			switch ((byte)EAX)
			{
				case 0x0: // Realloc
					EAX = (uint*)Heap.Realloc((byte*)EBX, (uint)EDX);
					break;
				case 0x1: // Alloc
					EAX = (uint*)Heap.Alloc((uint)EBX);
					break;
				case 0x2: // Free
					Heap.Free(EBX);
					break;
				case 0x3: // Copy64
					for (int I = 0; I < (int)(int*)EDX; I++)
					{
						*((long**)EBX)[I] = *((long**)ECX)[I];
					}
					break;
				case 0x4: // Copy32
					MemoryOperations.Copy(EBX, ECX, (int)(int*)EDX);
					break;
				case 0x5: // Copy16
					MemoryOperations.Copy((ushort*)EBX, (ushort*)ECX, (int)(int*)EDX);
					break;
				case 0x6: // Copy8
					MemoryOperations.Copy((byte*)EBX, (byte*)ECX, (int)(int*)EDX);
					break;
				case 0x7: // Fill64
					for (int I = 0; I < (int)(int*)EDX; I++)
					{
						*((long**)EBX)[I] = (long)(long*)ECX;
					}
					break;
				case 0x8: // Fill32
					MemoryOperations.Fill(EBX, (uint)ECX, (int)(int*)EDX);
					break;
				case 0x9: // Fill16
					MemoryOperations.Fill((ushort*)EBX, (ushort)(ushort*)ECX, (int)(int*)EDX);
					break;
				case 0x10: // Fill8
					MemoryOperations.Fill((byte*)EBX, (byte)(byte*)ECX, (int)(int*)EDX);
					break;
				case 0x11: // Prompt
					*(int*)EAX = PrismUI.Window.Frames.Count;
					PrismUI.DialogBox.ShowMessageDialog(GetString((char*)EBX), GetString((char*)ECX));
					break;
				case 0x12: // Print
					Console.WriteLine(GetString((char*)EBX));
					break;
			}
		}

		public static string GetString(char* C)
		{
			string S = "";
			for (int I = 0; C[I] != 0; I++)
			{
				S += C[I];
			}
			return S[..^1];
		}
	}
}