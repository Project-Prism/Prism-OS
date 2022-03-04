using PrismOS.Graphics;
using System.Drawing;

namespace PrismOS.Tests
{
    public static class SineWaveVisualiser
    {
        public static double Freq = 40.0;

        public static void Tick(Canvas Canvas)
        {
            double Amp = 0.25 * Canvas.Height;
            Canvas.DrawFilledRectangle(300, 300, 200, 50, Color.DarkSlateGray);
            Canvas.DrawString(300, 300, "Sine wave demo (" + Freq + " HZ)", Color.White);
            Canvas.DrawBitmap(300, 300 + 15, ImageTools.GetSine(200, 50 - 15, Color.White, Freq));
            
            if (Cosmos.System.KeyboardManager.TryReadKey(out var Key))
            {
                if (Cosmos.System.KeyboardManager.ControlPressed)
                {

                    switch (Key.Key)
                    {
                        case Cosmos.System.ConsoleKeyEx.NumPlus:
                            Freq += 10;
                            break;
                        case Cosmos.System.ConsoleKeyEx.NumMinus:
                            Freq -= 10;
                            break;
                    }
                }
                else
                {
                    switch (Key.Key)
                    {
                        case Cosmos.System.ConsoleKeyEx.NumPlus:
                            Freq++;
                            break;
                        case Cosmos.System.ConsoleKeyEx.NumMinus:
                            Freq--;
                            break;
                    }
                }
            }
        }
    }
}