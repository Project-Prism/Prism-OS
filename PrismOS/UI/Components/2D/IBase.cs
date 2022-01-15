namespace PrismOS.UI.Components
{
    public interface IBase
    {
        void Draw();
        void ClickDown();
        void ClickUp();
        void Hover();
        void Destroy();
        byte[] ToBytes();
    }
}
