using PrismOS.Libraries;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Graphics.GUI;

namespace PrismOS.Libraries.Applications
{
    public class AppTemplate1 : Runtime.Application
    {
        public Textbox TextBox1 = new();
        public Button Button1 = new();
        public Window Window = new();
        public Label Label1 = new();
        public Panel Panel1 = new();

        public override void OnCreate()
        {
            // Main window
            Window.X = 256;
            Window.Y = 256;
            Window.Width = 400;
            Window.Height = 175;
            Window.Radius = 0;
            Window.Theme = Theme.DefaultDark;
            Window.Text = "Form1";

            // Main panel
            Panel1.X = 0;
            Panel1.Y = 0;
            Panel1.Width = Window.Width;
            Panel1.Height = Window.Height;
            Panel1.Radius = 0;
            Panel1.Theme = Theme.DefaultDark;
            Window.Elements.Add(Panel1);

            // Label1
            Label1.X = 12;
            Label1.Y = 9;
            Label1.Text = "label1";
            Label1.Theme = Theme.DefaultDark;
            Window.Elements.Add(Label1);

            // TextBox1
            TextBox1.X = 12;
            TextBox1.Y = 27;
            TextBox1.Width = 100;
            TextBox1.Height = 23;
            TextBox1.Hint = "Start typing...";
            TextBox1.Theme = Theme.DefaultDark;
            Window.Elements.Add(TextBox1);

            // Button1
            Button1.X = 12;
            Button1.Y = 56;
            Button1.Width = 100;
            Button1.Height = 23;
            Button1.Text = "Button1";
            Button1.Theme = Theme.DefaultDark;
            Button1.OnClick = new System.Action(() => { Button1_Click(); });
            Window.Elements.Add(Button1);

            Runtime.Windows.Add(Window);
        }

        public override void OnDestroy()
        {

        }

        public override void OnUpdate()
        {

        }

        private void Button1_Click()
        {
            Label1.Text = TextBox1.Text;
        }
    }
}