using System;
using System.Drawing;

namespace PrismProject
{
	class GuiSysInfo : BaseGuiElement
	{
		Color TextColour;
		public GuiSysInfo(int x, int y, BaseGuiElement parent = null) : base(x, y, 300, 50, parent)
		{
			TextColour = Desktop.Text;
		}

		internal override void Render(drawable draw, int offset_x, int offset_y)
		{
			draw.Text(TextColour, Driver.font, "Used memory: " + Memory.Used + " MB (" + Memory.Used_percent + "%)", X + offset_x, Y + offset_y);
			draw.Text(TextColour, Driver.font, "Total memory: " + Memory.Total + " MB", X + offset_x, Y + offset_y + 15);
			draw.Text(TextColour, Driver.font, "Free memory: " + Memory.Free + " MB (" + Memory.Free_percent + "%)", X + offset_x, Y + offset_y + 30);

		}
	}
}
