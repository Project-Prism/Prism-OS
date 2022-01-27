using System.Collections.Generic;
using System.Drawing;

namespace PrismOS.UI.Elements
{
    public class Element
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public string Text { get; set; }
        public Color Foreground { get; set; } // add actual theme stuff later xd
        public Color Background { get; set; }
        public Color Accent { get; set; }
        public Element Parent { get; set; }
        public List<Element> Children { get; set; }
        public Canvas Canvas { get; set; }
        public bool Focused { get; set; }

        public void Draw()
        {
        }
    }
}