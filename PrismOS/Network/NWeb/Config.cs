namespace PrismOS.Network.NWeb
{
    // [HeaderLength][Version][0x0][Path] - Get
    // [HeaderLength][Version][0x1][PathLength][Path][DataLength][Data] - Post
    public enum Methods : byte
    {
        Get = 0x0,
        Post = 0x1,
    }
}
