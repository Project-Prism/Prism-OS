using PrismAPI.UI.Controls;
using PrismAPI.UI.Config;
using Cosmos.System;

namespace PrismAPI.UI;

public static class DialogBox
{
	public static Window ShowMessageDialog(string Title, string Message)
	{
		ushort Height = (ushort)(MouseManager.ScreenHeight / 4);
		ushort Width = (ushort)(MouseManager.ScreenWidth / 3);
		int X = (ushort)((MouseManager.ScreenWidth / 2) + (Height / 2));
		int Y = (ushort)((MouseManager.ScreenWidth / 2) + (Width / 2));

		Window Temp = new(X, Y, Width, Height, Title)
		{
			Controls =
			{
				new Button(Width - 128, Height - 64, 128, 64, 0, "OK", ThemeStyle.Holo),
				new Label(15, 15, Message),
			},
		};

		Temp.Controls[^1].OnClick = (int _, int _, MouseState _) => WindowManager.Windows.Remove(Temp);
		WindowManager.Windows.Add(Temp);

		return Temp;
	}
}