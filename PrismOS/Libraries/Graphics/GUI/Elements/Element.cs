using static PrismOS.Libraries.Graphics.GUI.WindowManager;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{
    public abstract class Element
    {
        public delegate void Event(ref Element This, ref Window Parent);
        public int X, Y, Width, Height, Radius;
        public Event OnClick, OnUpdate;
        public bool Clicked, Hovering, Visible = true;

        public abstract void Update(Canvas canvas, Window Parent);
    }
}