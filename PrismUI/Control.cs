using Cosmos.System;
using Cosmos.Core;
using PrismGL2D;

namespace PrismUI
{
    public class Control : Graphics, IDisposable
    {
        public Control() : base(0, 0)
        {
            OnClickEvents = new();
            OnDrawEvents = new();
            OnKeyEvents = new();

            Controls = new();
            HasBackground = true;
            CanInteract = true;
            IsEnabled = true;
            HasBorder = true;
            Feed = string.Empty;
            Text = string.Empty;
        }

        public static class Config
        {
            /// <summary>
            /// Background color for the control.
            /// </summary>
            private static Color BackColorClick = Color.LightGray, BackColorHover = Color.LighterBlack, BackColor = Color.Black;
            /// <summary>
            /// Foreground color for the control.
            /// </summary>
            private static Color ForeColorClick = Color.Black, ForeColorHover = Color.White, ForeColor = Color.White;
            /// <summary>
            /// Accent color for the control.
            /// </summary>
            public static Color AccentColor = Color.UbuntuPurple;
            /// <summary>
            /// The global radius factor.
            /// </summary>
            public static uint Radius = 0;
            /// <summary>
            /// The global scaling factor.
            /// </summary>
            public static uint Scale = 35;
            /// <summary>
            /// The global default font.
            /// </summary>
            public static Font Font = Font.Fallback;

            /// <summary>
            /// Check to see if the element should contain alpha transparency at the last copy to the main buffer.
            /// </summary>
            /// <param name="C"></param>
            /// <returns>true or false.</returns>
            public static bool ShouldContainAlpha(Control C)
			{
                if (GetBackground(C.IsPressed, C.IsHovering).A == 255 && Radius == 0)
				{
                    return false;
				}
                return true;
			}

            /// <summary>
            /// Gets the correct color based on the mouse status.
            /// </summary>
            /// <param name="Click"></param>
            /// <param name="Hover"></param>
            /// <returns>Correct background color.</returns>
            public static Color GetBackground(bool Click, bool Hover)
            {
                if (Hover)
                {
                    if (Click)
                    {
                        return BackColorClick;
                    }
                    else
                    {
                        return BackColorHover;
                    }
                }
                return BackColor;
            }
            /// <summary>
            /// Gets the correct color based on the mouse status.
            /// </summary>
            /// <param name="Click"></param>
            /// <param name="Hover"></param>
            /// <returns>The correct foreground color.</returns>
            public static Color GetForeground(bool Click, bool Hover)
            {
                if (Hover)
                {
                    if (Click)
                    {
                        return ForeColorClick;
                    }
                    else
                    {
                        return ForeColorHover;
                    }
                }
                return ForeColor;
            }
        }

        #region Methods

        /// <summary>
        /// Shows the control.
        /// </summary>
        public virtual void Show()
        {
            IsEnabled = true;
        }
        
        /// <summary>
        /// Hides the control.
        /// </summary>
        public virtual void Hide()
		{
            IsEnabled = false;
		}

        #endregion

        #region Fields

        /// <summary>
        /// A list containing all sub-elements for the control.
        /// </summary>
        public List<Control> Controls;
        /// <summary>
        /// Check to see if the control should have a background drawn.
        /// </summary>
        public bool HasBackground;
        /// <summary>
        /// Check to see if the control can be ineteracted with.
        /// </summary>
        public bool CanInteract;
        /// <summary>
        /// Check to see if the mouse is hovering over the element.
        /// </summary>
        public bool IsHovering;
        /// <summary>
        /// Check to see if the OnClickEvent needs to be fired.
        /// </summary>
        public bool IsPressed;
        /// <summary>
        /// The visibility of the control;
        /// </summary>
        public bool IsEnabled;
        /// <summary>
        /// The char feed from the keyboard. (input)
        /// </summary>
        internal string Feed;
        /// <summary>
        /// Check to see if the control should have a border.
        /// </summary>
        public bool HasBorder;
        /// <summary>
        /// The text value of the control, if it uses it.
        /// </summary>
        public string Text;
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
        public List<Action<Control>> OnDrawEvents { get; set; }

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
        public virtual void OnDrawEvent(Control C)
        {
            // Fire events
            for (int I = 0; I < OnDrawEvents.Count; I++)
			{
                OnDrawEvents[I](this);
			}

            // Clear
            if (HasBackground)
			{
                if (Config.Radius == 0)
				{
                    Clear(Config.GetBackground(IsPressed, IsHovering));
				}
                else
				{
                    Clear(Color.Transparent);
                    DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(IsPressed, IsHovering));
				}
			}
            else
			{
                Clear(Color.Transparent);
            }

            // Update sub-elements
            for (int I = 0; I < Controls.Count; I++)
            {
                Controls[I].OnDrawEvent(this);
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