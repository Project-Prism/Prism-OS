namespace PrismProject.Source.Sequence.Boot
{
    class BackgroundWork
    {
        public static int JobsLeft = 2;
        public static void Main()
        {
            Cosmos.System.PCSpeaker.Beep();
            FileSystem.Disk.Start();
            JobsLeft -= 1;
            Network.Network.NetStart(Network.Network.Local, Network.Network.Subnet, Network.Network.Gateway2);
            JobsLeft -= 0;
        }
    }
}
