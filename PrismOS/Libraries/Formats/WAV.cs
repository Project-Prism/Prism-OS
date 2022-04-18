using System;
using System.IO;

namespace PrismOS.Libraries.Formats
{
    public class WAV
    {
        public WAV(byte[] Binary)
        {
            BinaryReader Reader = new(new MemoryStream(Binary));
            if (Reader.ReadChars(4).ToString() != "RIFF")
            {
                return;
            }
            Size = Reader.ReadInt32();
            if (Reader.ReadChars(4).ToString() != "WAVE")
            {
                return;
            }
            if (Reader.ReadChars(4).ToString() != "fmt ")
            {
                return;
            }
            Reader.ReadBytes(4); // Length of format data as listed above??
            Format = Reader.ReadInt16();
            Channels = Reader.ReadInt16();
            SampleRate = Reader.ReadInt32();
            Reader.ReadBytes(4); // (Sample Rate * BitsPerSample * Channels) / 8??
            Reader.ReadByte(); // (BitsPerSample * Channels) / 8.1 - 8 bit mono2 - 8 bit stereo/16 bit mono4 - 16 bit stereo??
            BitsPersample = Reader.ReadByte();
            if (Reader.ReadChars(4).ToString() != "data")
            {
                return;
            }
            int DataLength = Reader.ReadInt32();
            short[] Audio = new short[Binary.Length / 2];
            Buffer.BlockCopy(Binary, 44, Audio, 0, Binary.Length);
            Console.WriteLine("All checks passed!");
        }

        public string FormatString => Format == 1 ? "PCM" : "Unknown";
        public short Format, Channels;
        public int Size, SampleRate;
        public byte BitsPersample;
        public short[] Audio;

        public override string ToString()
        {
            return "Channels: " + Channels + "\nSampleRate: " + SampleRate;
        }
    }
}