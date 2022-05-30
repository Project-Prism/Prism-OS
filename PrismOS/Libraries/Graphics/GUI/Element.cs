using System;

namespace PrismOS.Libraries.Graphics.GUI
{
    public abstract class Element
    {
        public Action OnClick = () => { }, OnUpdate = () => { };
        public bool Clicked, Hovering, Visible = true;
        public int X, Y, Width, Height, Radius;
        public Theme Theme;

        public abstract void Update(Canvas Canvas, Window Parent);
    }
}