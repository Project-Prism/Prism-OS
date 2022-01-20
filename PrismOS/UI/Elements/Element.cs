using System.Collections.Generic;
using System.Drawing;

namespace PrismOS.UI.Elements
{
    public abstract class Element
    {
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract int Radius { get; set; }
        public abstract Color Foreground { get; set; }
        public abstract Color Background { get; set; }
        public abstract Color Accent { get; set; }
        public abstract Element Parent { get; set; }
        public abstract List<Element> Children { get; set; }
        public abstract Canvas Canvas { get; set; }

        public abstract void Draw();
    }
}