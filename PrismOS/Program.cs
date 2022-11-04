using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Cosmos.Core.Memory;
using Cosmos.System;
using PrismOS.Apps;
using Cosmos.Core;
using PrismTools;
using PrismUI;

namespace PrismOS
{
	public unsafe class Program : Kernel
	{
		public static Debugger Debugger;
		public static Desktop Desktop;
		public static bool IsNETReady;
		public static bool IsFSReady;

		protected override void BeforeRun()
		{
			Debugger = new("Kernel");
			Desktop = new();

			DialogBox.ShowMessageDialog("Welcome", "Welcome to prism OS!\nIf you are wondering...\nThe seal in the background is named shawn.");
			Debugger.Log("Kernel initialized", Debugger.Severity.Ok);

			try
			{
				_ = new DHCPClient().SendDiscoverPacket();
				Debugger.Log("Initialized networking", Debugger.Severity.Ok);
				IsNETReady = true;
			}
			catch
			{
				Debugger.Log("Unable to initialize networking!", Debugger.Severity.Warning);
			}
			try
			{
				VFSManager.RegisterVFS(new CosmosVFS(), false, false);
				Debugger.Log("Initialized filesystem", Debugger.Severity.Ok);
				IsFSReady = true;
			}
			catch
			{
				Debugger.Log("Unable to initialize filesystem!", Debugger.Severity.Warning);
			}
		}
		protected override void Run()
		{
			Desktop.Update();
		}

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
					*(int*)EAX = Window.Frames.Count;
					DialogBox.ShowMessageDialog(StringEx.GetString((char*)EBX), StringEx.GetString((char*)ECX));
					break;
				case 0x12: // Print
					System.Console.WriteLine(StringEx.GetString((char*)EBX));
					break;
			}
		}
	}
}