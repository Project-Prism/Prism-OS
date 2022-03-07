using PrismOS.Libraries.Graphics;
using System.Drawing;

namespace PrismOS.Apps
{
    public class Menus
    {
        public void Update1(string Error, Canvas Canvas)
        {
            Canvas.Clear(Color.ForestGreen);
            Canvas.DrawString((Canvas.Width / 2) - (Canvas.Font.Default.Width * Error.Length / 2), (Canvas.Height / 2) - (Canvas.Font.Default.Height * Error.Split('\n').Length / 2), Error, Color.White);
            Canvas.Update();
            while (true) { }
        }
    }
}