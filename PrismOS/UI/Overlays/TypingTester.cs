using System.Drawing;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace PrismOS.UI.Overlays
{
    public static class TypingTester
    {
        private static string Input = "";
        public static void Tick()
        {
            if (Cosmos.System.KeyboardManager.KeyAvailable)
            {
                var Key = Cosmos.System.KeyboardManager.ReadKey();

                if (Key.Key == Cosmos.System.ConsoleKeyEx.Backspace)
                {
                    if (Input.Length != 0)
                    {
                        Input = Input.Remove(Input.Length - 1);
                    }
                }
                else if (Key.Key == Cosmos.System.ConsoleKeyEx.Tab)
                {
                    Input += "  ";
                }
                else
                {
                    Input += Key.KeyChar;
                }
            }

            Canvas.GetCanvas.DrawFilledRectangle((Canvas.GetCanvas.Width / 2) - 200, (Canvas.GetCanvas.Height / 2) - 10, 400, 20, Color.White);
            Canvas.GetCanvas.DrawString(Canvas.GetCanvas.Width / 2, Canvas.GetCanvas.Height / 2, Default, Input, Color.Black);
        }
    }
}
