namespace PrismOS.Libraries.Assembly
{
    public unsafe static class Assembly
    {
        public static void Move64(byte Register, long Value)
        {
            *(long*)Register = Value;
        }
        public static void Move32(byte Register, int Value)
        {
            *(int*)Register = Value;
        }
        public static void Move16(byte Register, short Value)
        {
            *(short*)Register = Value;
        }
        public static void Move8(byte Register, byte Value)
        {
            *(byte*)Register = Value;
        }

        public static void Copy64(byte Source, byte Destination)
        {
            *(long*)Destination = *(long*)Source;
        }
        public static void Copy32(byte Source, byte Destination)
        {
            *(int*)Destination = *(int*)Source;
        }
        public static void Copy16(byte Source, byte Destination)
        {
            *(short*)Destination = *(short*)Source;
        }
        public static void Copy8(byte Source, byte Destination)
        {
            *(byte*)Destination = *(byte*)Source;
        }
    }
}