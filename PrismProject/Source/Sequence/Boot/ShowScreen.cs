using static PrismProject.Source.Graphics.Drawables;
using static PrismProject.Source.Resources.ResourceList;
using System.Drawing;

namespace PrismProject.Source.Sequence.Boot
{
    internal class ShowScreen
    {
        public static void Main()
        {
            DrawText(TextXCenter(14), 450, "Please wait...", Font0, Color.White);
            DrawBMP(UI_Set[0] - (int)BootLogo.Width, UI_Set[1] - (int)BootLogo.Height, BootLogo);
            BootTasks.Main();
            Desktop.Desktop.Main();
        }
    }
}