using System;
using System.Collections.Generic;
using System.Drawing;

namespace PrismProject
{
    internal class Desktop
    {
        private static readonly int screenX = Driver.screenX, screenY = Driver.screenY;
        private static readonly G_lib draw = new G_lib();
        public static List<GuiWindow> Windows = new List<GuiWindow>();
        public static BaseGuiElement ActiveElement = null;

        public static void Start()
        {
            var testWindow = new GuiWindow(screenX / 5, screenY / 5, screenX / 2, screenY / 2, "App menu");

            //test power commands
            testWindow.AddChild(new GuiDiv
            #region Element properties
            (
                30, 54, 70, 144,
                Themes.Divider.Div_Theme)
            );
            #endregion
            testWindow.AddChild(new GuiButton
            #region Element properties
            (
                35, 64, 95, 30,
                "Reboot",
                Themes.Button.Button_Text,
                Themes.Button.Button_Theme,
                (self) =>
            {
                Cosmos.System.Power.Reboot();
            })
            );
            #endregion
            testWindow.AddChild(new GuiButton
            #region Element properties
            (
                135, 64, 95, 30,
                "Shut down",
                Themes.Button.Button_Text,
                Themes.Button.Button_Theme,
                (self) => { Cosmos.System.Power.Shutdown(); })
            );
            #endregion

            //Text box
            testWindow.AddChild(new GuiText
            #region Element properties
            (
                32, 118,
                "Enter into textbox",
                Color.White
            ));
            #endregion
            testWindow.AddChild(new GuiTextBox
           #region Element properties
            (
                32, 138, 150, 20,
                Themes.Textbox.TB_Border,
                Themes.Textbox.YB_Inner,
                Themes.Window.Window_Title_Text
            ));
            #endregion

            //GuiToggle switches
            testWindow.AddChild(new GuiText
            #region Element properties
            (
                30, 162,
                "Wi-Fi",
                Color.White
            ));
            #endregion
            testWindow.AddChild(new GuiSwitch
            #region Element properties
            (
                90, 160,
                Themes.Switch.Switch_Theme_Inner,
                Themes.Switch.Switch_Theme,
                true,
                (self) =>
            {
                if (self.status) { self.status = false; }
                if (!self.status) { self.status = true; }
            })
            );
            #endregion
            testWindow.AddChild(new GuiText
            #region Element properties
            (
                30, 190,
                "radio button!",
                Color.White
            ));
            #endregion
            testWindow.AddChild(new GuiToggle
            #region Element properties
            (
                150, 190,
                Themes.R_Button.R_Button_Theme,
                Themes.R_Button.R_Button_Theme_Inner,
                true,
                (self) =>
            {
                if (self.enable)
                {
                    self.enable = false;
                }
                else if (!self.enable)
                {
                    self.enable = true;
                }
            })
            );
            #endregion

            #region Rendering & mouse functions
            Windows.Add(testWindow);
            int clickX = -100, clickY = -100;
            bool clickDown = false;
            Driver.Clear(Themes.Desktop_main);
            while (Kernel.canvasRunning)
            {
                draw.Box(Themes.Window.Window_Theme_Inner, 0, screenY - 30, screenX, screenY / 24);
                draw.Circle(Color.White, 16, screenY - 15, 10);
                foreach (var window in Windows)
                {
                    if (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + screenY / 25)
                    {
                        if (Math.Abs(clickX - Cursor.X) > 4 || Math.Abs(clickY - Cursor.Y) > 4)
                        {
                            window.Dragging(Cursor.X - Cursor.lastX, Cursor.Y - Cursor.lastY);
                        }
                    }

                    window.Render(draw);
                }

                #region Mouse stuff

                if ((Cursor.State & Cosmos.System.MouseState.Left) == Cosmos.System.MouseState.Left)
                {
                    if (!clickDown)
                    {
                        clickX = Cursor.X;
                        clickY = Cursor.Y;
                        clickDown = true;
                    }
                }
                else if (clickDown)
                {
                    if (Math.Abs(clickX - Cursor.X) < 4 && Math.Abs(clickY - Cursor.Y) < 4)
                    {
                        ActiveElement = null;
                        foreach (var window in Windows)
                        {
                            if (clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + window.Height)
                            {
                                window.Click(clickX - window.X, clickY - window.Y - (screenY / 25), 1);
                            }
                        }
                    }
                    clickX = -100;
                    clickY = -100;
                }
                if (ActiveElement != null)
                {
                    if (Cosmos.System.KeyboardManager.TryReadKey(out Cosmos.System.KeyEvent key))
                    { ActiveElement.Key(key); }
                }
                Cursor.Update();

                #endregion Mouse stuff
            }
            #endregion
        }
    }
}