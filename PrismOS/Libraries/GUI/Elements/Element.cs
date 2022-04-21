using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.GUI.Elements
{
    public abstract class Element
    {
        public delegate void Event(ref Element This);
        public int X, Y, Width, Height, Radius;
        public bool Clicked, Hovering, Visible;
        public Event OnClick, OnUpdate;

        public abstract void Update(Canvas canvas);
    }
}