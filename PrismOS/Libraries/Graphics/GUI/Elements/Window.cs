using System.Collections.Generic;

namespace PrismOS.Libraries.Graphics.GUI.Elements
{

    public class Window
    {
        public int X, Y, Width, Height, Radius;
        public List<Element> Elements = new();
        public bool Visible = true, Draggable = true, TitleVisible = true, Moving;
        public string Text;
        public int IX, IY;
    }
}