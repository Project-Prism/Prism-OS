using Cosmos.System;
using PrismGL2D;

namespace PrismUI.Controls.Buttons
{
	public class DropDown : Button
	{
		public DropDown(string[] Items) : base(128, 32)
		{
			List = new(Items);
			OnClickEvent = (int X, int Y, MouseState State) =>
			{
				List.IsEnabled = !List.IsEnabled;
			};
			OnDrawEvent = (Graphics G) =>
			{
				if (List.IsEnabled)
				{
					Text = List.Index == -1 ? "Not selected." : List.Controls[List.Index].Text;
				}
			};
		}

		public ListBox List;
	}
}