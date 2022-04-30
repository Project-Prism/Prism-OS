using System.IO;

namespace PrismOS.Libraries.Utilities
{
    public unsafe static class Binary
    {
        public static byte[] ObjectToByte(object Object)
        {
            uint* Pointer = Cosmos.Core.GCImplementation.GetPointer(Object);
            int Size = *((*(int**)Pointer) + 8);
            System.Console.WriteLine("Reported size: " + Size);
            BinaryReader Reader = new(new UnmanagedMemoryStream((byte*)Pointer, Size));
            Reader.BaseStream.Position = 12; // Ignore unwanted data (type, obj type, size)
            return Reader.ReadBytes(Size);
        }
    }
}