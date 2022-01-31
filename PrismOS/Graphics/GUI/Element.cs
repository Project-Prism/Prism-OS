namespace PrismOS.Graphics.GUI
{
    public class Element
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public string Text { get; set; }
        public Window Parent { get; set; }
        public bool Focused { get; set; }

        public void Draw()
        {
            Parent.Screen.DrawFilledRectangle(X, Y, Width, Height, Parent.Theme.Background);
        }
    }
}