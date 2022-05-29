using System;

namespace PrismOS.Libraries.Graphics.GUI
{
    public abstract class Element
    {
        public bool Clicked, Hovering, Visible = true;
        public int X, Y, Width, Height, Radius;
        public Action OnClick, OnUpdate;
        public Theme Theme;

        public abstract void Update(Canvas Canvas, Window Parent);
    }
}