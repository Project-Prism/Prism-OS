using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio;
using PrismTools;

namespace PrismAudio;

/// <summary>
/// Audio player class for playing audio.
/// </summary>
public static class AudioPlayer
{
	/// <summary>
	/// Initializes the audio manager uppon app running.
	/// </summary>
	static AudioPlayer()
	{
		// Initialize objects.
		Debugger = new("Audio");
		Mixer = new();
		AM = new();

		// Attempt to load the audio driver.
		try
		{
			Debugger.WritePartial("Initializing audio...");

			// Assign values and initialize audio player.
			AM.Output = AC97.Initialize(4096);
			AM.Stream = Mixer;
			AM.Enable();

			// Inform that audio player is done initializing.
			Debugger.Finalize(Severity.Success);
			IsAvailable = true;
		}
		catch
		{
			Debugger.Finalize(Severity.Fail);
		}
	}

	#region Methods

	/// <summary>
	/// Plays an audio stream, which can be loaded from any supported format.
	/// Idealy should be 16-bit @ 48000hz to prevent slowdowns.
	/// </summary>
	public static void Play(AudioStream Stream)
	{
		// Add the audio stream to the mixer 'now-playing' list.
		Mixer.Streams.Add(Stream);
	}

	#endregion

	#region Fields

	private static readonly Debugger Debugger;
	public static readonly AudioMixer Mixer;
	private static readonly AudioManager AM;
	public static readonly bool IsAvailable;

	#endregion
}