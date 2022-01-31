using static Cosmos.System.Graphics.Fonts.PCScreenFont;
using PrismOS.Graphics.Utilities;

namespace PrismOS.Graphics.GUI.Text
{
    public class Textbox : Element
    {
        public Textbox(int X, int Y, int Width, int Height, Window Parent)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Parent = Parent;
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

            Parent.Screen.DrawFilledRectangle(Parent.X + X - 200, Parent.Y + Y - 10, Width, Height, Parent.Theme.Foreground);
            Parent.Screen.DrawString(Parent.X + X, Parent.Y + Y, Default, Text, Parent.Theme.Text);
        }
    }
}
