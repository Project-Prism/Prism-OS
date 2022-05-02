namespace PrismOS.Libraries.Utilities
{
    public static unsafe class CLib
    {
        // Non pointer array coppies
        public static void Copy(uint Pointer, byte[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(byte*)(Pointer + I) = Buffer[I];
            }
        }
        public static void Copy(uint Pointer, short[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(short*)(Pointer + (I * 2)) = Buffer[I];
            }
        }
        public static void Copy(uint Pointer, int[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(int*)(Pointer + (I * 4)) = Buffer[I];
            }
        }
        public static void Copy(uint Pointer, long[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(long*)(Pointer + (I * 8)) = Buffer[I];
            }
        }

        // Pointer array coppiespublic static void Copy(uint* Pointer, byte[] Buffer)
        public static void Copy(uint Pointer, byte*[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(byte**)(Pointer + I) = Buffer[I];
            }
        }
        public static void Copy(uint Pointer, short*[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(short**)(Pointer + (I * 2)) = Buffer[I];
            }
        }
        public static void Copy(uint Pointer, int*[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(int**)(Pointer + (I * 4)) = Buffer[I];
            }
        }
        public static void Copy(uint Pointer, long*[] Buffer)
        {
            for (int I = 0; I < Buffer.Length; I++)
            {
                *(long**)(Pointer + (I * 8)) = Buffer[I];
            }
        }

        // Wrapper to make it more rememberable that these exist in cosmos
        public static void Free(object Object)
        {
            Cosmos.Core.GCImplementation.Free(Object);
        }
        public static void Malloc(uint Size)
        {
            Cosmos.Core.GCImplementation.AllocNewObject(Size);
        }
    }
}