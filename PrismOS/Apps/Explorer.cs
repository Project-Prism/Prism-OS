using PrismUI.Controls;
using Cosmos.System;
using PrismUI;

namespace PrismOS.Apps
{
	public class Explorer : Frame
	{
		public Explorer() : base(nameof(Explorer))
		{
			if (Program.IsFSReady)
			{
				uint YOffset = 0;
				Text += " - Viewing '0:\\'";
				foreach (string File in Directory.GetFiles("0:\\"))
				{
					Controls.Add(new Button(128, Config.Scale)
					{
						OnClickEvent = (int X, int Y, MouseState State) => { _ = new Notepad("0:\\" + File); },
						HasBorder = false,
						Text = File,
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