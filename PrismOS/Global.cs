using Cosmos.Core.Memory;
using Cosmos.Core;
using PrismUI;

namespace PrismOS
{
	public static unsafe class Global
	{
		public static void SystemCall(ref uint* EAX, ref uint* EBX, ref uint* ECX, ref uint* EDX, ref uint* ESI, ref uint* EDI)
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
				case 0x11: // Print
					Console.WriteLine(GetString((char*)EBX));
					break;
				case 0x12: // Read
					fixed (byte* PTR = File.ReadAllBytes(GetString((char*)EBX)))
					{
						EAX = (uint*)(uint)new FileInfo(GetString((char*)EBX)).Length;
						MemoryOperations.Copy(ECX, (uint*)PTR, (int)EAX);
					}
					break;
				case 0x13: // Write
					byte[] Buffer = new byte[(uint)EDX];
					fixed (byte* PTR = Buffer)
					{
						MemoryOperations.Copy(PTR, (byte*)ECX, Buffer.Length);
					}
					File.WriteAllBytes(GetString((char*)EBX), Buffer);
					break;
				case 0x14: // Delete
					try
					{
						if ((uint)ECX == 0)
						{
							Directory.Delete(GetString((char*)EBX));
						}
						else
						{
							File.Delete(GetString((char*)EBX));
						}
					}
					catch { }
					break;
				case 0x15:
					if ((uint)ECX == 0)
					{
						Directory.CreateDirectory(GetString((char*)EBX));
					}
					else
					{
						File.Create(GetString((char*)EBX));
					}
					break;
				case 0x16:
					if (Directory.Exists(GetString((char*)EBX)))
					{
						EAX = (uint*)1;
					}
					if (File.Exists(GetString((char*)EBX)))
					{
						EAX = (uint*)2;
					}
					else
					{
						EAX = (uint*)0;
					}
					break;
				case 0x17:
					DialogBox.ShowMessageDialog(
						GetString((char*)EBX),
						GetString((char*)ECX));
					break;
			}
		}

		private static string GetString(char* PTR)
		{
			return new string(PTR)[..^1];
		}
	}
}