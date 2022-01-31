using PrismOS.Generic;
using PrismOS.Graphics.Utilities;

namespace PrismOS.Graphics.GUI
{
    public class Window
    {
        public Window(int X, int Y, int Width, int Height, int Radius, string Text, Theme Theme)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Radius = Radius;
            this.Text = Text;
            this.Theme = Theme;
            Screen = new(Width, Height);
            Children = new();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public string Text { get; set; }
        public VScreen Screen { get; set; }
        public Theme Theme { get; set; }
        public List<Element> Children { get; set; }

        public void Draw()
        {
            Screen.DrawFilledRectangle(X, Y, Width, Height, Theme.Background);
            Screen.DrawFilledRectangle(X, Y, Width, 50, Theme.Foreground);

            foreach (Element Object in Children)
            {
                Object.Draw();
            }
        }
    }
}