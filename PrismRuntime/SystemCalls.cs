using Cosmos.Core.Memory;
using Cosmos.Core;
using PrismTools;
using TinyMath;

namespace PrismRuntime
{
	public static unsafe class SystemCalls
	{
		#region Methods

		/// <summary>
		/// Initialize system calls.
		/// </summary>
		public static void Init()
		{
			INTs.SetIRQMaskState(16, false);
			INTs.SetIrqHandler(16, SystemCall);
			Debugger.Success("Initialized system calls!");
		}

		/// <summary>
		/// The system-call event for ELF files.
		/// </summary>
		/// <param name="Context">The details about the interupt.</param>
		public static void SystemCall(ref INTs.IRQContext Context)
		{
			Console.WriteLine("System Call!!!!");

			if (Context.Interrupt != 16)
			{
				return;
			}

			switch (Context.EAX)
			{
				#region Console

				case 0x00: // WriteLine
					Console.WriteLine(GetString((char*)Context.EBX));
					break;
				case 0x01: // Write
					Console.Write(GetString((char*)Context.EBX));
					break;
				case 0x02: // Clear
					Console.Clear();
					break;
				case 0x03: // Set Color
					Console.ForegroundColor = (ConsoleColor)Context.EBX;
					Console.BackgroundColor = (ConsoleColor)Context.ECX;
					break;
				case 0x04: // Reset Color
					Console.ResetColor();
					break;

				#endregion

				#region Memory

				case 0x05: // Realloc
					Context.EAX = (uint)Heap.Realloc((byte*)Context.EBX, Context.EDX);
					break;

				case 0x06: // Alloc
					Context.EAX = (uint)Heap.Alloc(Context.EBX);
					break;

				case 0x07: // Free
					Heap.Free((uint*)Context.EBX);
					break;

				case 0x08: // Set
					*(uint*)Context.EBX = Context.ECX;
					break;

				case 0x09: // Get
					Context.EAX = *(uint*)Context.EBX;
					break;

				case 0x10: // Copy64
					System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX * 8, Context.EDX * 8);
					break;

				case 0x11: // Copy32
					System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX * 4, Context.EDX * 4);
					break;

				case 0x12: // Copy16
					System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX * 2, Context.EDX * 2);
					break;

				case 0x13: // Copy8
					System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX, Context.EDX);
					break;

				case 0x14: // Fill64
					for (int I = 0; I < (int)(int*)Context.EDX; I++)
					{
						*((long**)Context.EBX)[I] = (long)(long*)Context.ECX;
					}
					break;

				case 0x15: // Fill32
					MemoryOperations.Fill((uint*)Context.EBX, Context.ECX, (int)(int*)Context.EDX);
					break;

				case 0x16: // Fill16
					MemoryOperations.Fill((ushort*)Context.EBX, (ushort)(ushort*)Context.ECX, (int)(int*)Context.EDX);
					break;

				case 0x17: // Fill8
					MemoryOperations.Fill((byte*)Context.EBX, (byte)(byte*)Context.ECX, (int)(int*)Context.EDX);
					break;

				#endregion

				#region Files

				case 0x18: // Read
					fixed (byte* PTR = File.ReadAllBytes(GetString((char*)Context.EBX)))
					{
						Context.EAX = (uint)new FileInfo(GetString((char*)Context.EBX)).Length;
						MemoryOperations.Copy((uint*)Context.ECX, (uint*)PTR, (int)Context.EAX);
					}
					break;

				case 0x19: // Write
					byte[] Buffer = new byte[Context.EDX];
					fixed (byte* PTR = Buffer)
					{
						MemoryOperations.Copy(PTR, (byte*)Context.ECX, Buffer.Length);
					}
					File.WriteAllBytes(GetString((char*)Context.EBX), Buffer);
					break;

				case 0x20: // Remove
					try
					{
						if (Context.ECX == 0)
						{
							Directory.Delete(GetString((char*)Context.EBX));
						}
						else
						{
							File.Delete(GetString((char*)Context.EBX));
						}
					}
					catch { }
					break;

				case 0x21: // Create
					if (Context.ECX == 0)
					{
						Directory.CreateDirectory(GetString((char*)Context.EBX));
					}
					else
					{
						File.Create(GetString((char*)Context.EBX));
					}
					break;

				case 0x22: // Exists
					if (Directory.Exists(GetString((char*)Context.EBX)))
					{
						Context.EAX = 1;
					}
					else if (File.Exists(GetString((char*)Context.EBX)))
					{
						Context.EAX = 2;
					}
					else
					{
						Context.EAX = 0;
					}
					break;

				case 0x23: // Size
					Context.EAX = (uint)new FileInfo(GetString((char*)Context.EBX)).Length;
					break;

				#endregion

				#region Eval

				case 0x24:
					fixed (char* C = SyntaxParser.Evaluate(GetString((char*)Context.EBX)).ToString())
					{
						Context.EAX = (uint)C;
					}
					break;

				#endregion

				default:
					Debugger.Warn($"Unimplemented systemcall: 0x{Context.EAX}");
					break;
			}
		}

		/// <summary>
		/// Reads a string from a char*, it is different that new() from string.
		/// </summary>
		/// <param name="PTR">Pointer to string.</param>
		/// <returns>String.</returns>]
		internal static string GetString(char* PTR)
		{
			return new string(PTR)[..^1];
		}

		#endregion

		#region Fields

		public static Debugger Debugger { get; set; } = new("Runtime");

		#endregion
	}
}