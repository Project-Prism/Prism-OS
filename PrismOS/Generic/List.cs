namespace PrismOS.Generic
{
    public class List<T> : System.Collections.Generic.List<T>
    {
        public void MoveToFirst(T Object)
        {
            Insert(0, Object);
            Remove(Object);
        }
    }
}