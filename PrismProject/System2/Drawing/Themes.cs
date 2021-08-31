using System.Drawing;

namespace PrismProject.System2.Drawing
{
    /// <summary>
    /// Themes for the system's UI
    /// </summary>
    internal class Themes
    {
        public struct Window
        {
            public static Color WindowTitleColor = Color.FromArgb(240,240,240);
            public static Color WindowUnfocusTitleColor = Color.FromArgb(0, 152, 28);
            public static Color WindowSplash = Color.FromArgb(224,224,224);
            public static Color WindowTextColor = Color.White;
            public static Color WindowShadow = Color.Black;
        }
        public struct Button
        {//button colors
            public static Color Button_Border = Color.FromArgb(0, 202, 78); //Default color
            public static Color Button_Disabled = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color Button_Color = Color.FromArgb(40, 40, 45); //inner background color
            public static Color Button_Text = Color.White; //text color
        }
        public struct RadioButton
        {//radio button colors
            public static Color R_Button_Theme = Color.FromArgb(0, 202, 78); //Default color
            public static Color R_Button_Theme_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color R_Button_Theme_Inner = Color.FromArgb(40, 40, 45); //inner background color
        }
        public struct Switch
        {//switch colors
            public static Color Switch_Theme = Color.FromArgb(0, 202, 78); //Default color
            public static Color Switch_Theme_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color Switch_Theme_Inner = Color.FromArgb(40, 40, 45); //inner background color
            public static Color Switch_Nub = Color.White; //switch nub color
        }
        public struct Divider
        {//divider colors
            public static Color Div_Theme = Color.FromArgb(0, 202, 78); //Default color
        }
        public struct Textbox
        {//text box colors
            public static Color TB_Border = Color.FromArgb(0, 202, 78); //Default color
            public static Color TB_Border_Disable = Color.FromArgb(0, 152, 28); //color to use instead when disabled
            public static Color YB_Inner = Color.FromArgb(40, 40, 45); //inner background color
        }

        public static Color desktop = Color.FromArgb(45, 45, 45);
    }
}
