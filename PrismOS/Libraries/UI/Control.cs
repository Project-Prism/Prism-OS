using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.UI.Types;
using System.Collections.Generic;
using Cosmos.System;
using Cosmos.Core;
using System;

namespace PrismOS.Libraries.UI
{
    public abstract class Control : IDisposable
    {
        public Control()
        {
            OnClickEvents = new();
            OnDrawEvents = new();
            OnKeyEvents = new();

            Theme = Theme.Default;
            HasBorder = true;
        }

        public List<Action> OnClickEvents { get; set; }
        public List<Action> OnDrawEvents { get; set; }
        public List<Action> OnKeyEvents { get; set; }
        public FrameBuffer Buffer { get; set; }
        public Theme Theme { get; set; }

        public bool IsHovering { get; set; }
        public bool IsPressed { get; set; }
        public bool HasBorder { get; set; }
        public bool IsHidden { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public uint Height
        {
            get
            {
                return Buffer.Height;
            }
            set
            {
                if (Width != 0)
                {
                    Buffer = new(Width, value);
                }
            }
        }
        public uint Width
        {
            get
            {
                return Buffer.Width;
            }
            set
            {
                if (Height != 0)
                {
                    Buffer = new(value, Height);
                }
            }
        }

        public abstract void OnClick(int X, int Y, MouseState State);
        public abstract void OnDraw(FrameBuffer Buffer);
        public abstract void OnKeyPress(KeyEvent Key);

        public void Dispose()
        {
            GCImplementation.Free(OnClickEvents);
            GCImplementation.Free(OnDrawEvents);
            GCImplementation.Free(OnKeyEvents);
            GCImplementation.Free(this);
            GC.SuppressFinalize(this);
        }
    }
}