namespace PrismOS.Libraries.Utilities
{
    public unsafe static class Native
    {
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