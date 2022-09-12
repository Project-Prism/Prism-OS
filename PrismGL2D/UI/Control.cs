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
            Enabled = true;
            Feed = string.Empty;
            Text = string.Empty;
            Theme = Theme.Default;
            Font = Font.Empty;
        }

        #region Methods

        /// <summary>
        /// Shows the control.
        /// </summary>
        public void Show()
        {
            Enabled = true;
        }
        
        /// <summary>
        /// Hides the control.
        /// </summary>
        public void Hide()
		{
            Enabled = false;
		}

        #endregion

        #region Fields

        /// <summary>
        /// Check to see if the mouse is hovering over the element.
        /// </summary>
        public bool IsHovering;
        /// <summary>
        /// Check to see if the OnClickEvent needs to be fired.
        /// </summary>
        public bool IsPressed;
        /// <summary>
        /// Check to see if the control should have a border.
        /// </summary>
        public bool HasBorder;
        /// <summary>
        /// Visibility control.
        /// </summary>
        public bool Enabled;
        /// <summary>
        /// The char feed from the keyboard. (input)
        /// </summary>
        public string Feed;
        /// <summary>
        /// The text value of the control, if it uses it.
        /// </summary>
        public string Text;
        /// <summary>
        /// The theme used for the control.
        /// </summary>
        public Theme Theme;
        /// <summary>
        /// The font used for the control.
        /// </summary>
        public Font Font;
        /// <summary>
        /// The X position of the control.
        /// </summary>
        public int X;
        /// <summary>
        /// The Y position of the control.
        /// </summary>
        public int Y;

        #endregion

        #region Events

        public List<Action<int, int, MouseState>> OnClickEvents { get; set; }
        public List<Action<ConsoleKeyInfo>> OnKeyEvents { get; set; }
        public List<Action<Graphics>> OnDrawEvents { get; set; }

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
            Feed += Key.KeyChar;

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