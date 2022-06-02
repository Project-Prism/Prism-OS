using PrismOS.Libraries;
using PrismOS.Libraries.Graphics;
using PrismOS.Libraries.Graphics.GUI;

namespace PrismOS.Libraries.Applications
{
    public class AppTemplate1 : Runtime.Application
    {
        public Textbox TextBox1 = new();
        public Button Button1 = new();
        public Button Button2 = new();
        public Window Window = new();
        public Label Label1 = new();

        public override void OnCreate()
        {
            // Main window
            Window.Position = new(256, 256);
            Window.Size = new(400, 175);
            Window.Theme = Theme.DefaultDark;
            Window.Text = "Form1";

            // Label1
            Label1.Position = new(12, 9);
            Label1.Size = new(38, 15);
            Label1.Text = "label1";
            Label1.Center = true;

            // TextBox1
            TextBox1.Position = new(12, 27);
            TextBox1.Size = new(100, 23);

            // Button1
            Button1.Position = new(12, 56);
            Button1.Size = new(100, 23);
            Button1.Text = "button1";
            Button1.OnClick = new System.Action(() => { Button1_Click(); });

            // Button2
            Button2.Position = new(12, 85);
            Button2.Size = new(100, 23);
            Button2.Text = "button2";
            Button2.OnClick = new System.Action(() => { Button2_Click(); });

            Window.Elements.Add(Button2);
            Window.Elements.Add(Button1);
            Window.Elements.Add(TextBox1);
            Window.Elements.Add(Label1);
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

        private void Button2_Click()
        {
            TextBox1.Text = "";
        }
    }
}