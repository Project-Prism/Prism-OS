using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System.Audio;
using PrismTools;

namespace PrismAudio
{
	/// <summary>
	/// Audio player class for playing audio.
	/// </summary>
	public static class AudioPlayer
	{
		#region Methods

		/// <summary>
		/// Plays an audio stream, which can be loaded from any supported format.
		/// Idealy should be 16-bit @ 48000hz to prevent slowdowns.
		/// </summary>
		public static void Play(AudioStream Stream)
		{
			try
			{
				// Add the audio stream to the mixer 'now-playing' list.
				Mixer.Streams.Add(Stream);
				AM.Enable();
			}
			catch (Exception E)
			{
				// Catch the exception if one happens.
				Debugger.Log(E.Message, Debugger.Severity.Warning);
			}
		}

		#endregion

		#region Fields

		/// <summary>
		/// Debugger.
		/// </summary>
		private static Debugger Debugger { get; set; } = new("Audio");

		/// <summary>
		/// Audio mixer.
		/// </summary>
		public static AudioMixer Mixer { get; set; } = new();

		/// <summary>
		/// Audio manager.
		/// </summary>
		private static AudioManager AM { get; set; } = new()
		{
			Output = AC97.Initialize(4096),
			Stream = Mixer,
			Enabled = true,
		};

		#endregion
	}
}