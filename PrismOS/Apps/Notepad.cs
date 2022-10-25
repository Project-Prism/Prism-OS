using PrismUI.Controls;
using PrismUI;

namespace PrismOS.Apps
{
	public class Notepad : Frame
	{
		public Notepad(string PathTo) : base($"{nameof(Notepad)} - { Path.GetFileName(PathTo)} (read-only)")
		{
			try
			{
				TextBox = new(Width, Height - Config.Scale);
				TextBox.Y = (int)Config.Scale;
				TextBox.Text = File.ReadAllText(PathTo);
				Controls.Add(TextBox);
			}
			catch (Exception E)
			{
				Close();
				DialogBox.ShowMessageDialog("Critical Error", "Unable to load file!\n" + E.Message);
			}
		}

		public TextBox TextBox;
	}
}