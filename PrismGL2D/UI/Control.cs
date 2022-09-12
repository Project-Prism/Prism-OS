using Cosmos.System;
using Cosmos.Core;

namespace PrismGL2D.UI
{
    public class Control : Graphics, IDisposable
    {
        public Control() : base(0, 0)
        {
            OnClickEvents = new();
            OnDrawEvents = new();
            OnKeyEvents = new();
            InputFeed = string.Empty;
            Text = string.Empty;
            Font = Font.Empty;

            Theme = Theme.Default;
        }

		#region Events

		public List<Action<int, int, MouseState>> OnClickEvents { get; set; }
        public List<Action<ConsoleKeyInfo>> OnKeyEvents { get; set; }
        public List<Action<Graphics>> OnDrawEvents { get; set; }

		#endregion

		#region Fields

		public string InputFeed { get; set; }
        public string Text { get; set; }
        public Theme Theme { get; set; }
        public Font Font { get; set; }

        public bool IsHovering { get; set; }
        public bool IsPressed { get; set; }
        public bool HasBorder { get; set; }
        public bool IsHidden { get; set; }
        public int Y { get; set; }
        public int X { get; set; }

		#endregion

		public virtual void OnClickEvent(int X, int Y, MouseState State)
		{
            for (int I = 0; I < OnClickEvents.Count; I++)
			{
                OnClickEvents[I](X, Y, State);
			}
        }
        public virtual void OnKeyEvent(ConsoleKeyInfo Key)
        {
            InputFeed += Key.KeyChar;

            for (int I = 0; I < OnKeyEvents.Count; I++)
            {
                OnKeyEvents[I](Key);
            }
        }
        public virtual void OnDrawEvent(Graphics Graphics)
        {
            // Need to call Base.OnDrawEvent for this to fire.

            for (int I = 0; I < OnDrawEvents.Count; I++)
			{
                OnDrawEvents[I](Graphics);
			}
        }

        public new void Dispose()
        {
            GCImplementation.Free(OnClickEvents);
            GCImplementation.Free(OnDrawEvents);
            GCImplementation.Free(OnKeyEvents);
            GC.SuppressFinalize(this);
        }
    }
}