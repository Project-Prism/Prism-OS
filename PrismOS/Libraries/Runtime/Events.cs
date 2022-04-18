using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;
using Cosmos.System;

namespace PrismOS.Libraries.Runtime
{
    public static class Events
    {
        public static List<KeyboardEvent> OnKeyUp = new();
        public static List<KeyboardEvent> OnKeyDown = new();
        public static List<MouseEvent> OnClickDown = new();
        public static List<MouseEvent> OnClickUp = new();
        private static bool IsClicked = false;
        private static bool IsPressed = false;

        public delegate void KeyboardEvent(ConsoleKeyEx Key);
        public delegate void MouseEvent(int X, int Y);

        public static void Update()
        {
            if (!IsClicked && Mouse.MouseState == MouseState.Left)
            {
                IsClicked = true;
                foreach (MouseEvent Event in OnClickDown)
                    Event.Invoke((int)Mouse.X, (int)Mouse.Y);
            }
            else if (IsClicked && Mouse.MouseState != MouseState.Left)
            {
                IsClicked = false;
                foreach (MouseEvent Event in OnClickUp)
                    Event.Invoke((int)Mouse.X, (int)Mouse.Y);
            }

            if (!IsPressed && KeyboardManager.TryReadKey(out KeyEvent Key))
            {
                IsPressed = true;
                foreach (KeyboardEvent Event in OnKeyDown)
                    Event.Invoke(Key.Key);
            }
            if (IsPressed && !KeyboardManager.TryReadKey(out Key))
            {
                IsPressed = false;
                foreach (KeyboardEvent Event in OnKeyUp)
                    Event.Invoke(Key.Key);
            }
        }
    }
}