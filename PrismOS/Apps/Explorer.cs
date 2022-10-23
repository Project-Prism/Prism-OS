using PrismUI;

namespace PrismOS.Apps
{
	public class Explorer : Frame
	{
		public Explorer() : base(nameof(Explorer))
		{
			if (Program.IsFSReady)
			{
				int YOffset = (int)Config.Scale;
				foreach (string File in Directory.GetFiles("0:\\"))
				{
					Controls.Add(new Button(128, 32)
					{
						Text = File,
						Y = YOffset += 32,
					});
				}
			}
			else
			{
				Controls.Add(new Label("Filesystem not initialized!")
				{
					Y = (int)Config.Scale,
				});
			}
		}
	}
}