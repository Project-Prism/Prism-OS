using Cosmos.HAL.Drivers.PCI.Audio;
using Cosmos.System.Audio;
using PrismTools;

namespace PrismAudio
{
	public static class AudioPlayer
	{
		private static Debugger Debugger { get; set; } = new("Audio");
		public static AudioMixer Mixer { get; set; } = new();
		private static AudioManager AM { get; set; } = new()
		{
			Output = AC97.Initialize(4096),
			Stream = Mixer,
			Enabled = true,
		};

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