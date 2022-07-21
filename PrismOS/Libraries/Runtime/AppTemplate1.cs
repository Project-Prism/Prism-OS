using PrismOS.Libraries.UI;

namespace PrismOS.Libraries.Runtime
{
    public class AppTemplate1 : Application
    {
        public Textbox TextBox1 = new();
        public Button Button1 = new();
        public Button Button2 = new();
        public Window Window = new();
        public Label Label1 = new();

        public override void OnCreate()
        {
            // Main window
            Window.X = 50;
            Window.Y = 50;
            Window.Width = 200;
            Window.Height = 200;
            Window.Theme = Theme.DefaultDark;
            Window.Text = "Form1";

            // Label1
            Label1.X = 12;
            Label1.Y = 29;
            Label1.Width = 38;
            Label1.Height = 15;
            Label1.Text = "label1";

            // TextBox1
            TextBox1.X = 12;
            TextBox1.Y = 47;
            TextBox1.Width = 100;
            TextBox1.Height = 23;

            // Button1
            Button1.X = 12;
            Button1.Y = 76;
            Button1.Width = 100;
            Button1.Height = 23;
            Button1.Text = "button1";
            Button1.OnClick = new System.Action(() => { Button1_Click(); });

            // Button2
            Button2.X = 12;
            Button2.Y = 105;
            Button2.Width = 100;
            Button2.Height = 23;
            Button2.Text = "button2";
            Button2.OnClick = new System.Action(() => { Button2_Click(); });

            Window.Elements.Add(Button2);
            Window.Elements.Add(Button1);
            Window.Elements.Add(TextBox1);
            Window.Elements.Add(Label1);
            Window.Windows.Add(Window);
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