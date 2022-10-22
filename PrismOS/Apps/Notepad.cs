using PrismUI;

namespace PrismOS.Apps
{
	public class Notepad : Frame
	{
		public Notepad() : base(300, 200, "Notepad")
		{
			TextBox1 = new(Width, Height);

			Controls.Add(TextBox1);
		}

		public TextBox TextBox1;
	}
}