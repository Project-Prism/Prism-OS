namespace PrismAPI.Runtime.SystemCall;

public enum Kind : uint
{
    System_Console_WriteLine_string,
    System_Console_Write_string,
    System_Console_ResetColor,
    System_Console_SetColor,
    System_Console_Clear,

    System_Runtime_InteropServices_NativeMemory_Realloc,
    System_Runtime_InteropServices_NativeMemory_Alloc,
    System_Runtime_InteropServices_NativeMemory_Free,

    System_Buffer_MemoryCopy64,
    System_Buffer_MemoryCopy32,
    System_Buffer_MemoryCopy16,
    System_Buffer_MemoryCopy8,

    System_IO_File_WriteeAllBytes_string_bytes,
    System_IO_File_ReadAllBytes_string,
    System_IO_Directory_Create,
    System_IO_Directory_Delete,
    System_IO_Directory_Exists,
    System_IO_File_Create,
    System_IO_File_Delete,
    System_IO_File_Exists,

    Core_Set_Permissions,
    Core_Get_Permissions,
    Core_Math_Eval,
    Core_Fill64,
    Core_Fill32,
    Core_Fill16,
    Core_Fill8,
}