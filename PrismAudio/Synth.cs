using Cosmos.System.Audio.IO;
using Cosmos.HAL.Audio;

namespace PrismAudio
{
	/// <summary>
	/// A primitive synthesieser class for Prism OS.
	/// </summary>
	public unsafe static class Synth
	{
		/// <summary>
		/// Generates a sine wave based on the given inputs.
		/// </summary>
		/// <param name="Frequency">The audio frequency of the sine wave.</param>
		/// <param name="Samples">The number of samples, 48000 @4800hz is 1 second.</param>
		/// <param name="Amplitude">The amplitude/loudness of the audio.</param>
		/// <returns>Sine wave as an audio stream.</returns>
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