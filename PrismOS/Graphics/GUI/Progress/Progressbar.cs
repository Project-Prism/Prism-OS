using PrismOS.Graphics.Utilities;

namespace PrismOS.Graphics.GUI.Progress
{
    public class Progressbar : Element
    {
        public Progressbar(int X, int Y, int Height, int Width, int Progress)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.Progress = Progress;
        }

        public int Progress { get; set; }

        public new void Draw()
        {
            Parent.Screen.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Parent.Theme.Background);
            Parent.Screen.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width / Progress, Height, Parent.Theme.Accent);
        }
    }
}