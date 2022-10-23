using Cosmos.System;

namespace PrismUI
{
	public static class DialogBox
	{
		public static Frame ShowMessageDialog(string Title, string Message)
		{
			Label L = new(Message)
			{
				Y = (int)Control.Config.Scale,
			};
			Frame F = new(L.Width + Control.Config.Scale * 2, L.Height + (uint)(Control.Config.Scale * 3.5), Title);
			L.Y = (int)((F.Height / 2) - (L.Height / 2));
			L.X = (int)((F.Width / 2) - (L.Width / 2));
			F.Controls.Add(new Button(128, 32)
			{
				X = (int)(F.Width - 128 - (Control.Config.Scale / 2)),
				Y = (int)(F.Height - 32 - (Control.Config.Scale / 2)),
				Text = "OK",
				OnClickEvent = (int X, int Y, MouseState State) => { F.Close(); },
			});
			F.Controls.Add(L);
			return F;
		}
	}
}