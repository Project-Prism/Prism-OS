using PrismOS.Graphics.GUI.Common;

namespace PrismOS.Graphics.GUI.Progress
{
    public class Progressbar : Base
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
            Canvas.GetCanvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width, Height, Parent.Theme.Background);
            Canvas.GetCanvas.DrawFilledRectangle(Parent.X + X, Parent.Y + Y, Width / Progress, Height, Parent.Theme.Accent);
        }
    }
}