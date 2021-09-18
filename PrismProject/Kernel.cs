namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Source.Sequence.Boot.ShowScreen.Main();
            }
            catch (System.Exception CMSG)
            {
                Source.Sequence.Crash.ShowScreen.Main(CMSG);
            }
        }
    }
}