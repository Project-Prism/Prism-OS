using System.Drawing;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace PrismOS.UI.Elements
{
    public class Textbox : Element
    {
        public Textbox(int X, int Y, int Width, int Height, Element Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Parent = Parent;
            // incomplete lol
        }

        public new void Draw()
        {
            if (Cosmos.System.KeyboardManager.KeyAvailable && Focused)
            {
                var Key = Cosmos.System.KeyboardManager.ReadKey();

                if (Key.Key == Cosmos.System.ConsoleKeyEx.Backspace)
                {
                    if (Text.Length != 0)
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                }
                else if (Key.Key == Cosmos.System.ConsoleKeyEx.Tab)
                {
                    Text += "  ";
                }
                else
                {
                    Text += Key.KeyChar;
                }
            }

            Canvas.GetCanvas.DrawFilledRectangle(X - 200, Y - 10, Width, Height, Foreground);
            Canvas.GetCanvas.DrawString(X, Y, Default, Text, Color.White);
        }
    }
}
