using Cosmos.System.Audio.IO;
using Cosmos.HAL.Audio;

namespace PrismAPI.Audio;

/// <summary>
/// A primitive synthesieser class for Prism OS.
/// </summary>
public unsafe static class Synth
{
    /// <summary>
    /// Generates a triangle wave based on the given inputs.
    /// </summary>
    /// <param name="Frequency">The audio frequency of the wave.</param>
    /// <param name="Samples">The number of samples, 48000 @4800hz is 1 second.</param>
    /// <param name="Amplitude">The amplitude/loudness of the audio.</param>
    /// <returns>Triangle wave as an audio stream.</returns>
    public static MemoryAudioStream GetTriangleWave(short Frequency, uint Samples = 48000, float Amplitude = 1f)
    {
        BinaryWriter Writer = new(new MemoryStream());

        // Determine the number of samples per wavelength.
        int SamplesPerWavelength = Convert.ToInt32(Samples / (Frequency / 1));

        // Determine the amplitude step for consecutive samples.
        short AmpStep = Convert.ToInt16(Amplitude * 2 / SamplesPerWavelength);

        // Temporary sample value, added to as we go through the loop.
        short TempSample = (short)-Amplitude;

        for (uint i = 0; i < Samples; i++)
        {
            // Negate ampstep whenever it hits the amplitude boundary.
            if (Math.Abs(TempSample) > Amplitude)
            {
                AmpStep = (short)-AmpStep;
            }

            TempSample += AmpStep;
            Writer.Write(TempSample);
        }

        return new MemoryAudioStream(new(AudioBitDepth.Bits16, 1, true), Samples, ((MemoryStream)Writer.BaseStream).ToArray());
    }

    /// <summary>
    /// Generates a sawtooth wave based on the given inputs.
    /// </summary>
    /// <param name="Frequency">The audio frequency of the wave.</param>
    /// <param name="Samples">The number of samples, 48000 @4800hz is 1 second.</param>
    /// <param name="Amplitude">The amplitude/loudness of the audio.</param>
    /// <returns>Sawtooth wave as an audio stream.</returns>
    public static MemoryAudioStream GetSawtoothWave(short Frequency, uint Samples = 4800, float Amplitude = 1f)
    {
        BinaryWriter Writer = new(new MemoryStream());

        // Determine the number of samples per wavelength.
        int SamplesPerWavelength = Convert.ToInt32(Samples / (Frequency / 1));

        // Determine the amplitude step for consecutive samples.
        short AmpStep = Convert.ToInt16(Amplitude * 2 / SamplesPerWavelength);

        // Total number of samples written so we know when to stop.
        int TotalSamplesWritten = 0;

        while (TotalSamplesWritten < Samples)
        {
            // Temporary sample value, added to as we go through the loop.
            short TempSample = (short)-Amplitude;

            for (uint i = 0; i < SamplesPerWavelength && TotalSamplesWritten < Samples; i++)
            {
                TempSample += AmpStep;
                Writer.Write(TempSample);
                TotalSamplesWritten++;
            }
        }

        Console.WriteLine("Finished");

        return new MemoryAudioStream(new(AudioBitDepth.Bits16, 1, true), Samples, ((MemoryStream)Writer.BaseStream).ToArray());
    }

    /// <summary>
    /// Generates a square wave based on the given inputs.
    /// </summary>
    /// <param name="Frequency">The audio frequency of the wave.</param>
    /// <param name="Samples">The number of samples, 48000 @4800hz is 1 second.</param>
    /// <param name="Amplitude">The amplitude/loudness of the audio.</param>
    /// <returns>Square wave as an audio stream.</returns>
    public static MemoryAudioStream GetSquareWave(short Frequency, uint Samples = 48000, float Amplitude = 1f)
    {
        BinaryWriter Writer = new(new MemoryStream());

        for (uint I = 0; I < Samples; I += 2)
        {
            Writer.Write(Convert.ToInt16(Amplitude * Math.Sign(Math.Sin(Frequency * I))));
        }

        return new MemoryAudioStream(new(AudioBitDepth.Bits16, 1, true), Samples, ((MemoryStream)Writer.BaseStream).ToArray());
    }

    /// <summary>
    /// Generates a sine wave based on the given inputs.
    /// </summary>
    /// <param name="Frequency">The audio frequency of the wave.</param>
    /// <param name="Samples">The number of samples, 48000 @4800hz is 1 second.</param>
    /// <param name="Amplitude">The amplitude/loudness of the audio.</param>
    /// <returns>Sine wave as an audio stream.</returns>
    public static MemoryAudioStream GetSineWave(short Frequency, uint Samples = 48000, float Amplitude = 1f)
    {
        BinaryWriter Writer = new(new MemoryStream());

        for (uint I = 0; I < Samples; I += 2)
        {
            Writer.Write(Convert.ToInt16(Amplitude * Math.Sin(Frequency * I)));
        }

        return new MemoryAudioStream(new(AudioBitDepth.Bits16, 1, true), Samples, ((MemoryStream)Writer.BaseStream).ToArray());
    }
}