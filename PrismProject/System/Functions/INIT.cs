namespace PrismProject
{
    internal class INIT
    {
        public static void Run()
        {
            Networking.DHCP();
            Filesystem.Init();
            Cmds.Init();
        }
    }
}