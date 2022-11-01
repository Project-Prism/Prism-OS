using Cosmos.System.Audio.IO;
using PrismUI.Controls;
using Cosmos.System;
using PrismAudio;
using PrismUI;

namespace PrismOS.Apps
{
	public class Explorer : Window
	{
		public Explorer() : base(nameof(Explorer))
		{
			if (Program.IsFSReady)
			{
				uint YOffset = 0;
				Text += " - Viewing '0:\\'";
				foreach (string Name in Directory.GetFiles("0:\\"))
				{
					Controls.Add(new Button(128, Config.Scale)
					{
						OnClickEvent = (int X, int Y, MouseState State) =>
						{
							switch (Path.GetExtension(Name))
							{
								case ".bmp":
									_ = new Gallery("0:\\" + Name);
									break;
								case ".txt":
									_ = new Notepad("0:\\" + Name);
									break;
								case ".wav":
									AudioPlayer.Play(MemoryAudioStream.FromWave(File.ReadAllBytes(Name)));
									break;
								default:
									DialogBox.ShowMessageDialog("File load error", $"No app ascociated with the '{Path.GetExtension(Name)}' extention.");
									break;
							}
						},
						HasBorder = false,
						Text = Name,
						Y = (int)(YOffset += Config.Scale),
					});
				}
			}
			else
			{
				Close();
				DialogBox.ShowMessageDialog(
					"Critical Error",
					"Unfortunatly the filesystem could not start.");
			}
		}
	}
}