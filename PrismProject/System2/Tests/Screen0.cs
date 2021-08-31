using System;
using System.Collections.Generic;
using PrismProject.System2.Drivers;
using PrismProject.System2.Drawing;
using System.Drawing;

namespace PrismProject.Software
{
    internal class Screen0
    {
        #region UIGen-setup
        private static readonly UILib drawing = new UILib();
        private static readonly int SH = Video.SH;
        public static List<WinLib.GuiWindow> Windows = new List<WinLib.GuiWindow>();
        public static WinLib.BaseGuiElement ActiveElement = null;
        public static WinLib.GuiWindow MainWindow = new WinLib.GuiWindow(Video.SW / 4, Video.SH / 4, Video.SW / 2, Video.SH / 2, "Setup");
        #endregion

        public static void Start()
        {
            MainWindow.AddChild(new WinLib.GuiButton
            #region Config
            (
                MainWindow.X*3, 24, 24, 24,
                "Reboot",
                Themes.Button.Button_Text,
                Themes.Button.Button_Color,
                (self) => { Cosmos.System.Power.Reboot(); })
            );

            #endregion Config

            MainWindow.AddChild(new WinLib.GuiIcon
            #region Config
            (
                MainWindow.X+MainWindow.Width-16, MainWindow.Y+MainWindow.Height-16,
                16, 16,
                "cross",
                Color.Navy )
            );
            #endregion

            Windows.Add(MainWindow);

            int clickX = -100, clickY = -100;
            bool clickDown = false;
            Video.Screen.Clear(Color.FromArgb(24,24,34));

            while (true)
            {
                foreach (var window in Windows)
                {
                    switch (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + SH / 25)
                    {
                        case true:
                            switch (Math.Abs(clickX - Mouse.X) > 4 || Math.Abs(clickY - Mouse.Y) > 4)
                            {
                                case true:
                                    window.Dragging(Mouse.X - Mouse.lastX, Mouse.Y - Mouse.lastY);
                                    break;
                            }
                            break;
                    }
                    window.Render(drawing);
                }

                switch ((Mouse.State & Cosmos.System.MouseState.Left) == Cosmos.System.MouseState.Left)
                {
                    case true:
                        switch (clickDown)
                        {
                            case false:
                                clickX = Mouse.X;
                                clickY = Mouse.Y;
                                clickDown = true;
                                break;

                            case true:
                                if (Math.Abs(clickX - Mouse.X) < 4 && Math.Abs(clickY - Mouse.Y) < 4)
                                {
                                    ActiveElement = null;
                                    foreach (var window in Windows)
                                    {
                                        if (clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + window.Height)
                                        {
                                            window.Click(clickX - window.X, clickY - window.Y - (SH / 25), 1);
                                        }
                                    }
                                }
                                clickX = -100;
                                clickY = -100;
                                break;
                        }
                        break;
                }
                switch (ActiveElement == null)
                {
                    case false:
                        switch (Cosmos.System.KeyboardManager.TryReadKey(out Cosmos.System.KeyEvent key))
                        {
                            case true:
                                { ActiveElement.Key(key); }
                                break;
                        }
                        break;
                }
                Mouse.Update();
            }
        }
    }
}