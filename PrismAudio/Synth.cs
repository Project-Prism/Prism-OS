using Cosmos.System.Audio.IO;
using Cosmos.HAL.Audio;

namespace PrismAudio
{
	public unsafe static class Synth
	{
		public static MemoryAudioStream GetSineWave(short Frequency, uint Samples, float Amplitude = 1.0f)
		{
			short[] Audio1 = new short[Samples];
			byte[] Audio2 = new byte[Samples * 2];

			for (uint I = 0; I < Samples; I++)
			{
				Audio1[I] = Convert.ToInt16(Amplitude * Math.Sin(Frequency * I));
			}

			Buffer.BlockCopy(Audio1, 0, Audio2, 0, (int)(Samples * 2));
			return new MemoryAudioStream(new(AudioBitDepth.Bits16, 1, true), 48000, Audio2);
		}
	}
}
