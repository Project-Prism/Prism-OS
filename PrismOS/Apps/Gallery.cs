﻿using PrismGL2D.Formats;
using PrismUI.Controls;
using PrismUI;

namespace PrismOS.Apps
{
	public class Gallery : Frame
	{
		public Gallery(string PathTo) : base(nameof(Gallery) + Path.GetFileName(PathTo))
		{
			try
			{
				Image I = new(new Bitmap(File.ReadAllBytes(PathTo)));
				I.Height = Height - Config.Scale;
				I.Width = Width;
				I.Y = (int)Config.Scale;
				Controls.Add(I);
			}
			catch (Exception E)
			{
				Close();
				DialogBox.ShowMessageDialog("Critical Error", $"Unable to load {Path.GetFileName(PathTo)}!\n{E.Message}");
			}
		}
	}
}