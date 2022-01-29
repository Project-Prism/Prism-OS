using PrismOS.Generic;
using PrismOS.Graphics.GUI.Common;

namespace PrismOS.Graphics.GUI.Containers
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
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public string Text { get; set; }
        public Theme Theme { get; set; }
        public List<Base> Children { get; set; }
        public bool Focused { get; set; }

        public void Draw()
        {
            Canvas.GetCanvas.DrawFilledRectangle(X, Y, Width, Height, Theme.Background);
            Canvas.GetCanvas.DrawFilledRectangle(X, Y, Width, 50, Theme.Foreground);

            foreach (Base Object in Children)
            {
                Object.Draw();
            }
        }
    }
}