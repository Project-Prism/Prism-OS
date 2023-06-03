using System.Runtime.InteropServices;
using PrismAPI.Tools.Diagnostics;
using Cosmos.Core;
using TinyMath;

namespace PrismAPI.Runtime.SystemCall;

public static unsafe class Handler
{
    #region Methods

    /// <summary>
    /// Initialize system calls.
    /// </summary>
    public static void Init()
    {
        Debugger.WritePartial("Initializing syscalls...");
        INTs.SetIRQMaskState(0x80, false);
        INTs.SetIrqHandler(0x80, SystemCall);
        Debugger.Finalize(Severity.Success);
    }

    /// <summary>
    /// The system-call event for ELF files.
    /// </summary>
    /// <param name="Context">The details about the interupt.</param>
    public static void SystemCall(ref INTs.IRQContext Context /*, ref Executable CallingBinary*/)
    {
        Console.WriteLine("System Call!!!!");

        // We need to add the ability to get an exact reference to the calling binary here
        // That way we can check it's permission and see what it can and cannot do.
        // We need to add a method that prompts the user to enter a password to grant a program
        // a higher permission level, and we should use this in core apps. Even though they
        // can just do the action anyway due to them being core apps, it will be good so
        // the user knows it is going to be doing special access.

        switch ((Kind)Context.EAX)
        {
            #region Console

            case Kind.System_Console_WriteLine_string:
                Console.WriteLine(GetString((char*)Context.EBX));
                break;
            case Kind.System_Console_Write_string:
                Console.Write(GetString((char*)Context.EBX));
                break;
            case Kind.System_Console_Clear:
                Console.Clear();
                break;
            case Kind.System_Console_SetColor:
                Console.ForegroundColor = (ConsoleColor)Context.EBX;
                Console.BackgroundColor = (ConsoleColor)Context.ECX;
                break;
            case Kind.System_Console_ResetColor:
                Console.ResetColor();
                break;

            #endregion

            #region Memory

            case Kind.System_Runtime_InteropServices_NativeMemory_Realloc: // Realloc
                Context.EAX = (uint)NativeMemory.Realloc((byte*)Context.EBX, Context.EDX);
                break;

            case Kind.System_Runtime_InteropServices_NativeMemory_Alloc: // Alloc
                Context.EAX = (uint)NativeMemory.Alloc(Context.EBX);
                break;

            case Kind.System_Runtime_InteropServices_NativeMemory_Free: // Free
                NativeMemory.Free((uint*)Context.EBX);
                break;

            case Kind.System_Buffer_MemoryCopy64: // Copy64
                System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX * 8, Context.EDX * 8);
                break;

            case Kind.System_Buffer_MemoryCopy32: // Copy32
                System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX * 4, Context.EDX * 4);
                break;

            case Kind.System_Buffer_MemoryCopy16: // Copy16
                System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX * 2, Context.EDX * 2);
                break;

            case Kind.System_Buffer_MemoryCopy8: // Copy8
                System.Buffer.MemoryCopy((void*)Context.EBX, (void*)Context.ECX, Context.EDX, Context.EDX);
                break;

            case Kind.Core_Fill64: // Fill64
                for (int I = 0; I < (int)(int*)Context.EDX; I++)
                {
                    *((long**)Context.EBX)[I] = (long)(long*)Context.ECX;
                }
                break;

            case Kind.Core_Fill32: // Fill32
                MemoryOperations.Fill((uint*)Context.EBX, Context.ECX, (int)(int*)Context.EDX);
                break;

            case Kind.Core_Fill16: // Fill16
                MemoryOperations.Fill((ushort*)Context.EBX, (ushort)(ushort*)Context.ECX, (int)(int*)Context.EDX);
                break;

            case Kind.Core_Fill8: // Fill8
                MemoryOperations.Fill((byte*)Context.EBX, (byte)(byte*)Context.ECX, (int)(int*)Context.EDX);
                break;

            #endregion

            #region Files

            case Kind.System_IO_File_ReadAllBytes_string:
                fixed (byte* PTR = File.ReadAllBytes(GetString((char*)Context.EBX)))
                {
                    Context.EAX = (uint)new FileInfo(GetString((char*)Context.EBX)).Length;
                    MemoryOperations.Copy((uint*)Context.ECX, (uint*)PTR, (int)Context.EAX);
                }
                break;

            case Kind.System_IO_File_WriteeAllBytes_string_bytes:
                byte[] Buffer = new byte[Context.EDX];
                fixed (byte* PTR = Buffer)
                {
                    MemoryOperations.Copy(PTR, (byte*)Context.ECX, Buffer.Length);
                }
                File.WriteAllBytes(GetString((char*)Context.EBX), Buffer);
                break;

            case Kind.System_IO_Directory_Delete:
                Directory.Delete(GetString((char*)Context.EBX));
                break;

            case Kind.System_IO_File_Delete:
                File.Delete(GetString((char*)Context.EBX));
                break;

            case Kind.System_IO_Directory_Create:
                Directory.CreateDirectory(GetString((char*)Context.EBX));
                break;

            case Kind.System_IO_File_Create:
                File.Create(GetString((char*)Context.EBX));
                break;

            case Kind.System_IO_Directory_Exists:
                Context.EAX = (uint)(Directory.Exists(GetString((char*)Context.EBX)) ? 1 : 0);
                break;

            case Kind.System_IO_File_Exists:
                Context.EAX = (uint)(File.Exists(GetString((char*)Context.EBX)) ? 1 : 0);
                break;

            #endregion

            #region Core

            case Kind.Core_Set_Permissions:
                // If CallingBinary.Permissions == Kernel
                // CallingBinary.Permissions = Requested permissions
                // Else ask user permission
                // If answer is yes or correct password is in...
                // CallingBinary.Permissions = Requested permissions
                // Else, do nothing and return.
                break;
            case Kind.Core_Get_Permissions:
                // Return CallingBinary.Permissions
                break;
            case Kind.Core_Math_Eval:
                fixed (char* C = SyntaxParser.Evaluate(GetString((char*)Context.EBX)).ToString())
                {
                    Context.EAX = (uint)C;
                }
                break;

            #endregion

            default:
                Debugger.WriteFull($"Unimplemented systemcall: {(int)Context.EAX}", Severity.Fail);
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