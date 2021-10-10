namespace graf3d.Engine.Abstractions
{
    public interface IReadOnlyBitmap
    {
        int Width { get; }
        int Height { get; }
        IColor GetPixel(int i, int i1);
    }
}