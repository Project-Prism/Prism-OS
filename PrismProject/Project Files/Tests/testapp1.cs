using PrismProject.System2.Drawing;
using PrismProject.System2.Drivers;

namespace PrismProject
{
    class Testapp1
    {
        public static WinLib.GuiWindow MainWindow = new WinLib.GuiWindow(Video.SW / 5, Video.SH / 5, Video.SW / 2, Video.SH / 2, "App menu");
        public static void Start()
        {
            MainWindow.AddChild(new WinLib.GuiDiv
            #region Element properties
            (
                30, 54, 70, 144,
                Themes.Divider.Div_Theme
            ));

            #endregion Element properties


            MainWindow.AddChild(new WinLib.GuiButton
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

            MainWindow.AddChild(new WinLib.GuiButton
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
            MainWindow.AddChild(new WinLib.GuiText
            #region Element properties
            (
                32, 118,
                "Enter into textbox",
                System.Drawing.Color.White
            ));

            #endregion Element properties

            MainWindow.AddChild(new WinLib.GuiTextBox
            #region Element properties
            (
                32, 138, 150, 20,
                Themes.Textbox.TB_Border,
                Themes.Textbox.YB_Inner,
                Themes.Window.WindowTextColor
            ));

            #endregion Element properties

            //GuiToggle switches
            MainWindow.AddChild(new WinLib.GuiText
            #region Element properties
            (
                30, 162,
                "Wi-Fi",
                System.Drawing.Color.White
            ));

            #endregion Element properties

            MainWindow.AddChild(new WinLib.GuiSwitch
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

            MainWindow.AddChild(new WinLib.GuiText
            #region Element properties
            (
                30, 190,
                "radio button!",
                System.Drawing.Color.White
            ));

            #endregion Element properties

            MainWindow.AddChild(new WinLib.GuiToggle
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
        }
    }
}
