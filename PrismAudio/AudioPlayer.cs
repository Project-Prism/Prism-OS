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
    		/// <summary>
    		/// Debugger
    		/// </summary>
		private static Debugger Debugger { get; set; } = new("Audio");
		
		
    		/// <summary>
    		/// Audio mixer
    		/// </summary>
		public static AudioMixer Mixer { get; set; } = new();
		
		
    		/// <summary>
    		/// Audio manager
    		/// </summary>
		private static AudioManager AM { get; set; } = new()
		{
			Output = AC97.Initialize(4096),
			Stream = Mixer,
			Enabled = true,
		};

    		/// <summary>
    		/// Plays a wave file imported from ManifestResourceStream attribute.
    		/// </summary>
		public static void Play(AudioStream Stream)
		{
			try
			{
				Mixer.Streams.Add(Stream);
			}
			catch (Exception E)
			{
				Debugger.Log(E.Message, Debugger.Severity.Warning);
			}
		}
		
		/// <summary>
    		/// Update the audio manager
    		/// </summary>
		public static void OnUpdate()
		{
			if (AM.Stream.Depleted)
			{
				if (AM.Enabled)
				{
					AM.Disable();
				}
			}
			else
			{
				if (!AM.Enabled)
				{
					AM.Enable();
				}
			}
		}
	}
}
