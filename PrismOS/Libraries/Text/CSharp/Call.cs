namespace PrismOS.Libraries.Text.CSharp
{
    public class Call
    {
        public Call()
        {
            Attributes = new()
            {
                Name = "",
                Arguments = new(),
            };
        }

        public Attributes Attributes { get; set; }
    }
}