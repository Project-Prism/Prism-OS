using System.Diagnostics;

namespace Prism
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Libraries.UI.Framework.UI.Window X = new(Libraries.UI.Framework.Width / 2, Libraries.UI.Framework.Height / 2, 400, 400, "", null);
            X.Children.Add(new Libraries.UI.Framework.UI.Button(50, 50, 7, X));
            while (true)
            {
                X.Width++;
                X.Height++;
                X.Draw();
            }
        }
    }
}