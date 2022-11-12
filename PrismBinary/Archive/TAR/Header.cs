using System.Runtime.InteropServices;

namespace PrismBinary.Archive.TAR
{
    /// <summary>
    /// The header file for posix tar files.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Header
    {
        // GNU Compatible
        public fixed byte Name[100];
        public fixed byte Mode[8];
        public fixed byte OwnerUID[8];
        public fixed byte GroupUID[8];
        public fixed byte Size[12];
        public fixed byte LastModifyTime[12];
        public fixed byte HRChecksum[8];
        public fixed byte TypeFlag[1];
        public fixed byte LFName[100];

        // Posix Extention
        public fixed byte LinkName[100];
        public fixed byte Magic[6];
        public fixed byte Version[2];
        public fixed byte UName[32];
        public fixed byte GName[32];
        public fixed byte DevMajor[8];
        public fixed byte DevMinor[8];
        public fixed byte Prefix[155];
    }
}