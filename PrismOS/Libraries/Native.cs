namespace PrismOS.Libraries
{
    public unsafe static class Native
    {
        public enum Registers : byte
        {
            al, ah,
            eax, ebx, ecx, edx,
        }


        public static void Copy64(byte Source, byte Destination)
        {
            for (int I = 0; I < sizeof(long); I++)
            {
                *(byte*)(Destination + I) = *(byte*)(Source + I);
            }
        }
        public static void Copy32(byte Source, byte Destination)
        {
            for (int I = 0; I < sizeof(int); I++)
            {
                *(byte*)(Destination + I) = *(byte*)(Source + I);
            }
        }
        public static void Copy16(byte Source, byte Destination)
        {
            for (int I = 0; I < sizeof(short); I++)
            {
                *(byte*)(Destination + I) = *(byte*)(Source + I);
            }
        }
        public static void Copy8(byte Source, byte Destination)
        {
            *(byte*)Destination = *(byte*)Source;
        }
        public static void Copy(byte Register, object Value)
        {
            byte[] Binary = (byte[])Value;
            for (int I = 0; I < Binary.Length; I++)
            {
                *(byte*)(Register + I) = Binary[I];
            }
        }

        public static void Interupt(byte Address)
        {
            delegate* unmanaged<void> Method = (delegate* unmanaged<void>)Address;
            Method();
        }

        public static void Execute(byte[] Binary)
        {
            fixed (byte* PTR = Binary)
            {
                delegate* unmanaged<void> Method = (delegate* unmanaged<void>)PTR;
                Method();
            }
        }
    }
}