using System.Collections.Generic;

namespace PrismOS.Hexi
{
    public static class Runtime
    {
        public static List<Program> Programs { get; } = new();

        public static class Codes
        {
            public enum Console : byte
            {
                Print = 0x0000, // [Code][Memory Index][Length]
            }
            public enum Program : byte
            {
                Start = 0x0001, // [Code][PathToFile]
                Stop = 0x0002, // [Code]
                Jump = 0x0003, // [Code][Value]
            }
            public enum Memory
            {
                Allocate = 0x0004, // [Code][Value]
                Set = 0x0005, // [Code][Index][Length][Data]
            }
            public enum Graphics
            {
                SetPixel = 0x0007, // [Code][X][Y][argb]
                GetPixel = 0x0008, // [Code][X][Y][Memory index]
            }
        }

        public static void Tick()
        {
            foreach (Program Prog in Programs)
            {
                Prog.Tick();
            }
        }
    }
}