namespace PrismOS
{
    public unsafe static class Native
    {
        public enum Registers : byte
        {
            al, ah,
            eax, ebx, ecx, edx,
        }


        public static void Copy(uint* Address, uint* Object, uint Size)
        {
            for (uint I = 0; I < Size; I++)
            {
                *(Address + I) = *(Object + I);
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