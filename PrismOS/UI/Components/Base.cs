using System.Collections.Generic;
using System.Drawing;

namespace PrismOS.UI.Components
{
    public abstract class Base
    {
        public abstract int X
        {
            get;
            set;
        }
        public abstract int Y
        {
            get;
            set;
        }
        public abstract int Width
        {
            get;
            set;
        }
        public abstract int Height
        {
            get;
            set;
        }
        public abstract Color ForeGround
        {
            get;
            set;
        }
        public abstract Color BackGround
        {
            get;
            set;
        }
        public abstract Base Parent
        {
            get;
            set;
        }
        public abstract List<Base> Children
        {
            get;
            set;
        }

        public abstract void Draw();
    }
}