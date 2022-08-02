using System.Collections.Generic;
using Cosmos.System.Audio.IO;
using System.IO;

namespace PrismOS.Libraries.Graphics.Parsing
{
    public class TVF
    {
        public TVF(byte[] Binary)
        {
            BinaryReader R = new(new MemoryStream(Binary));

            if (R.ReadUInt32() != 0x69420)
            {
                throw new System.NotSupportedException("Unknown File Type Or Unsuported Format!");
            }
            Frames = new();

            uint FrameCount = R.ReadUInt32();
            uint AudioSize = R.ReadUInt32();
            uint VideoSize = R.ReadUInt32();
            uint FPS = R.ReadUInt32();

            for (int I = 0; I < FrameCount; I++)
            {
                Frames.Add(BMP.FromBitmap(R.ReadBytes((int)VideoSize)));
            }
            Audio = MemoryAudioStream.FromWave(R.ReadBytes((int)AudioSize));

            Cosmos.HAL.Global.PIT.RegisterTimer(new(() =>
            {
                if (Position < Frames.Count)
                {
                    Frame = Frames[(int)Position++];
                }
            }, 250000000, true));
        }

        public List<FrameBuffer> Frames { get; set; }
        public FrameBuffer Frame { get; internal set; }
        public MemoryAudioStream Audio { get; set; }
        public uint Position { get; set; }
    }
}