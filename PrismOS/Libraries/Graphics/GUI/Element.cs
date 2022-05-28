namespace PrismOS.Libraries.Graphics.GUI
{
    public abstract class Element
    {
        public delegate void Event(Element This, Window Parent);
        public int X, Y, Width, Height, Radius;
        public Event OnClick, OnUpdate;
        public bool Clicked, Hovering, Visible = true;
        public Theme Theme;

        public abstract void Update(Canvas Canvas, Window Parent);
    }
}