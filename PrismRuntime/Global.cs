using Cosmos.Core.Memory;
using Cosmos.Core;
using PrismTools;

namespace PrismRuntime
{
	public static unsafe class Global
	{
		public static void SystemCall(ref uint EAX, ref uint EBX, ref uint ECX, ref uint EDX, ref uint ESI, ref uint EDI)
		{
			switch (EAX)
			{
				#region Console

				case 0x00: // WriteLine
					Console.WriteLine(GetString((char*)EBX));
					break;
				case 0x01: // Write
					Console.Write(GetString((char*)EBX));
					break;
				case 0x02: // Clear
					Console.Clear();
					break;
				case 0x03: // Set Color
					Console.ForegroundColor = (ConsoleColor)EBX;
					Console.BackgroundColor = (ConsoleColor)ECX;
					break;
				case 0x04: // Reset Color
					Console.ResetColor();
					break;

				#endregion

				#region Memory

				case 0x05: // Realloc
					EAX = (uint)Heap.Realloc((byte*)EBX, EDX);
					break;

				case 0x06: // Alloc
					EAX = (uint)Heap.Alloc(EBX);
					break;

				case 0x07: // Free
					Heap.Free((uint*)EBX);
					break;

				case 0x08: // Set
					*(uint*)EBX = ECX;
					break;

				case 0x09: // Get
					EAX = *(uint*)EBX;
					break;

				case 0x10: // Copy64
					for (int I = 0; I < (int)(int*)EDX; I++)
					{
						*((long**)EBX)[I] = *((long**)ECX)[I];
					}
					break;

				case 0x11: // Copy32
					MemoryOperations.Copy((uint*)EBX, (uint*)ECX, (int)(int*)EDX);
					break;

				case 0x12: // Copy16
					MemoryOperations.Copy((ushort*)EBX, (ushort*)ECX, (int)(int*)EDX);
					break;

				case 0x13: // Copy8
					MemoryOperations.Copy((byte*)EBX, (byte*)ECX, (int)(int*)EDX);
					break;

				case 0x14: // Fill64
					for (int I = 0; I < (int)(int*)EDX; I++)
					{
						*((long**)EBX)[I] = (long)(long*)ECX;
					}
					break;

				case 0x15: // Fill32
					MemoryOperations.Fill((uint*)EBX, ECX, (int)(int*)EDX);
					break;

				case 0x16: // Fill16
					MemoryOperations.Fill((ushort*)EBX, (ushort)(ushort*)ECX, (int)(int*)EDX);
					break;

				case 0x17: // Fill8
					MemoryOperations.Fill((byte*)EBX, (byte)(byte*)ECX, (int)(int*)EDX);
					break;

				#endregion

				#region Files

				case 0x18: // Read
					fixed (byte* PTR = File.ReadAllBytes(GetString((char*)EBX)))
					{
						EAX = (uint)new FileInfo(GetString((char*)EBX)).Length;
						MemoryOperations.Copy((uint*)ECX, (uint*)PTR, (int)EAX);
					}
					break;

				case 0x19: // Write
					byte[] Buffer = new byte[EDX];
					fixed (byte* PTR = Buffer)
					{
						MemoryOperations.Copy(PTR, (byte*)ECX, Buffer.Length);
					}
					File.WriteAllBytes(GetString((char*)EBX), Buffer);
					break;

				case 0x20: // Remove
					try
					{
						if (ECX == 0)
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

				case 0x21: // Create
					if (ECX == 0)
					{
						Directory.CreateDirectory(GetString((char*)EBX));
					}
					else
					{
						File.Create(GetString((char*)EBX));
					}
					break;

				case 0x22: // Exists
					if (Directory.Exists(GetString((char*)EBX)))
					{
						EAX = 1;
					}
					else if (File.Exists(GetString((char*)EBX)))
					{
						EAX = 2;
					}
					else
					{
						EAX = 0;
					}
					break;

				case 0x23: // Size
					EAX = (uint)new FileInfo(GetString((char*)EBX)).Length;
					break;

				#endregion

				#region Eval

				case 0x24:
					fixed (char* C = MathEx.Eval(GetString((char*)EBX)))
					{
						EAX = (uint)C;
					}
					break;

					#endregion
			}
		}

		internal static string GetString(char* PTR)
		{
			return new string(PTR)[..^1];
		}
	}
}