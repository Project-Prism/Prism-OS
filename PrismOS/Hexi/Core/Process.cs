using System;
using System.IO;
using static PrismOS.Hexi.Core.Binary;

namespace PrismOS.Hexi.Core
{
    public class Process : IDisposable
    {
        public Process(string FromFile)
        {
            ROM = File.ReadAllBytes(FromFile);
        }

        private readonly byte[] ROM;
        private int ROMIndex;

        public string ProcessName = "";
        public string Output = "";

        public void Tick()
        {
            if (ROMIndex >= ROM.Length)
            {
                Dispose();
                return;
            }

            switch(ROM[ROMIndex++])
            {
                case (byte)OPCodes.Write:
                    Output += GetString(ROM, ref ROMIndex);
                    break;
                case (byte)OPCodes.WriteLine:
                    Output += GetString(ROM, ref ROMIndex) + '\n';
                    break;
                case (byte)OPCodes.Clear:
                    Output = "";
                    break;
                case (byte)OPCodes.Jump:
                    ROMIndex = ROM[ROMIndex];
                    break;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}