using PrismUI.Structure;
using Cosmos.System;
using Cosmos.Core;
using PrismGL2D;

namespace PrismUI.Controls
{
    public class Control : Graphics, IDisposable
    {
        public Control(uint Width, uint Height) : base(Width, Height)
        {
            OnClickEvent = (int X, int Y, MouseState State) => { };
            OnKeyEvent = (ConsoleKeyInfo Key) => { };
            OnDrawEvent = (Graphics G) => { };

            Controls = new();

            HasBackground = true;
            CanInteract = true;
            IsEnabled = true;
            HasBorder = true;
            CanType = true;
            Text = string.Empty;
        }

        public static class Config
        {
            /// <summary>
            /// Background color for the control.
            /// </summary>
            public static Color BackColorClick = Color.LightGray, BackColorHover = Color.LighterBlack, BackColor = Color.Black;
            /// <summary>
            /// Foreground color for the control.
            /// </summary>
            public static Color ForeColorClick = Color.Black, ForeColorHover = Color.White, ForeColor = Color.White;
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
                if (GetBackground(C).A == 255 && Radius == 0)
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
            public static Color GetBackground(Control C)
            {
                return C.ClickState switch
                {
                    ClickState.Neutral => BackColor,
                    ClickState.Hovering => BackColorHover,
                    ClickState.Clicked => BackColorClick,
                    _ => Color.Black,
                };
            }
            /// <summary>
            /// Gets the correct color based on the mouse status.
            /// </summary>
            /// <param name="Click"></param>
            /// <param name="Hover"></param>
            /// <returns>The correct foreground color.</returns>
            public static Color GetForeground(Control C)
            {
				return C.ClickState switch
				{
					ClickState.Neutral => ForeColor,
					ClickState.Hovering => ForeColorHover,
					ClickState.Clicked => ForeColorClick,
					_ => Color.Black,
				};
			}
        }

        #region Methods

        internal virtual void OnClick(int X, int Y, MouseState State)
        {
            OnClickEvent?.Invoke(X, Y, State);
        }
        internal virtual void OnKey(ConsoleKeyInfo Key)
        {
            OnKeyEvent?.Invoke(Key);
        }
        internal virtual void OnDraw(Graphics G)
        {
            // Clear
            if (HasBackground)
            {
                if (Config.Radius == 0)
                {
                    Clear(Config.GetBackground(this));
                }
                else
                {
                    Clear(Color.Transparent);
                    DrawFilledRectangle(0, 0, Width, Height, Config.Radius, Config.GetBackground(this));
                }
            }
            else
            {
                Clear(Color.Transparent);
            }

            OnDrawEvent?.Invoke(this);
            // Update sub-elements
            for (int I = 0; I < Controls.Count; I++)
            {
                if (Controls[I].IsEnabled)
                {
                    Controls[I].OnDraw(this);
                }
            }
            if (HasBorder)
            {
                G.DrawRectangle(X - 1, Y - 1, Width + 1, Height + 1, Config.Radius, Config.ForeColor);
            }
        }

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
        /// The click status of the control.
        /// </summary>
        public ClickState ClickState;
        /// <summary>
        /// Check to see if the control should have a background drawn.
        /// </summary>
        public bool HasBackground;
        /// <summary>
        /// Check to see if the control can be ineteracted with.
        /// </summary>
        public bool CanInteract;
        /// <summary>
        /// The visibility of the control;
        /// </summary>
        public bool IsEnabled;
        /// <summary>
        /// Check to see if the control should have a border.
        /// </summary>
        public bool HasBorder;
        /// <summary>
        /// Check to see if the element can be typed into.
        /// </summary>
        public bool CanType;
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

        public Action<int, int, MouseState> OnClickEvent;
        public Action<ConsoleKeyInfo> OnKeyEvent;
        public Action<Graphics> OnDrawEvent;

        #endregion

        public new void Dispose()
        {
            GCImplementation.Free(OnClickEvent);
            GCImplementation.Free(OnDrawEvent);
            GCImplementation.Free(OnKeyEvent);
            GC.SuppressFinalize(this);
        }
    }
}