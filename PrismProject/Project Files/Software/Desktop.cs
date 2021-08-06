using System;
using System.Collections.Generic;
using System.Drawing;
using PrismProject.System2.Drawing;

namespace PrismProject.Software
{
    internal class Desktop
    {
        #region UIGen-setup
        private static readonly GLib drawing = new GLib();
        private static readonly int SW = Drivers.Video.SW, SH = Drivers.Video.SH;
        public static List<UIGen.GuiWindow> Windows = new List<UIGen.GuiWindow>();
        public static UIGen.BaseGuiElement ActiveElement = null;
        #endregion

        public static void Start()
        {
            var testWindow = new UIGen.GuiWindow(SW/5, SH/5, SW/2, SH/2, "App menu");

            //test power commands
            testWindow.AddChild(new UIGen.GuiDiv
            #region Element properties
            (
                30, 54, 70, 144,
                Themes.Divider.Div_Theme
            ));

            #endregion Element properties

            testWindow.AddChild(new UIGen.GuiButton
            #region Element properties
            (
                35, 64, 75, 23,
                "Reboot",
                Themes.Button.Button_Text,
                Themes.Button.Button_Theme,
                (self) =>
                {
                    Cosmos.System.Power.Reboot();
                })
            );

            #endregion Element properties

            testWindow.AddChild(new UIGen.GuiButton
            #region Element properties
            (
                145, 64, 75, 23,
                "prw-off",
                Themes.Button.Button_Text,
                Themes.Button.Button_Theme,
                (self) => { Cosmos.System.Power.Shutdown(); })
            );

            #endregion Element properties

            //Text box
            testWindow.AddChild(new UIGen.GuiText
            #region Element properties
            (
                32, 118,
                "Enter into textbox",
                Color.White
            ));

            #endregion Element properties

            testWindow.AddChild(new UIGen.GuiTextBox
            #region Element properties
            (
                32, 138, 150, 20,
                Themes.Textbox.TB_Border,
                Themes.Textbox.YB_Inner,
                Themes.Window.WindowTextColor
            ));

            #endregion Element properties

            //GuiToggle switches
            testWindow.AddChild(new UIGen.GuiText
            #region Element properties
            (
                30, 162,
                "Wi-Fi",
                Color.White
            ));

            #endregion Element properties

            testWindow.AddChild(new UIGen.GuiSwitch
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

            #endregion Element properties

            testWindow.AddChild(new UIGen.GuiText
            #region Element properties
            (
                30, 190,
                "radio button!",
                Color.White
            ));

            #endregion Element properties

            testWindow.AddChild(new UIGen.GuiToggle
            #region Element properties
            (
                150, 190,
                Themes.RadioButton.R_Button_Theme,
                Themes.RadioButton.R_Button_Theme_Inner,
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

            #endregion Element properties

            #region Rendering & mouse functions

            Windows.Add(testWindow);
            int clickX = -100, clickY = -100;
            bool clickDown = false;
            Drivers.Video.Screen.Clear(Themes.desktop);

            while (true)
            {
                foreach (var window in Windows)
                {
                    if (clickDown && clickX > window.X && clickX < window.X + window.Width && clickY > window.Y && clickY < window.Y + SH / 25)
                    {
                        if (Math.Abs(clickX - Mouse.X) > 4 || Math.Abs(clickY - Mouse.Y) > 4)
                        {
                            window.Dragging(Mouse.X - Mouse.lastX, Mouse.Y - Mouse.lastY);
                        }
                    }

                    window.Render(drawing);
                }

                #region Mouse stuff

                if ((Mouse.State & Cosmos.System.MouseState.Left) == Cosmos.System.MouseState.Left)
                {
                    if (!clickDown)
                    {
                        clickX = Mouse.X;
                        clickY = Mouse.Y;
                        clickDown = true;
                    }
                }
                else if (clickDown)
                {
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
                }
                if (ActiveElement != null)
                {
                    if (Cosmos.System.KeyboardManager.TryReadKey(out Cosmos.System.KeyEvent key))
                    { ActiveElement.Key(key); }
                }
                Mouse.Update();

                #endregion Mouse stuff
            }

            #endregion Rendering & mouse functions
        }
    }
}