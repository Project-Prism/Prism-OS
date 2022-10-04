using static Cosmos.Core.INTs;
using IL2CPU.API.Attribs;

namespace PrismOS.Plugs
{
    [Plug(Target = typeof(Cosmos.Core.INTs))]
    public class INTs
    {
        public static void HandleException(uint aEIP, string aDescription, string aName, ref IRQContext ctx, uint lastKnownAddressValue = 0)
        {
            const string xHex = "0123456789ABCDEF";
            string LastKnownAddress = "";
            string CTXInterupt = "";

            CTXInterupt += xHex[(int)((ctx.Interrupt >> 4) & 0xF)].ToString();
            CTXInterupt += xHex[(int)(ctx.Interrupt & 0xF)].ToString();

            if (lastKnownAddressValue != 0)
            {
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 28) & 0xF)];
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 24) & 0xF)];
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 20) & 0xF)];
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 16) & 0xF)];
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 12) & 0xF)];
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 8) & 0xF)];
                LastKnownAddress += xHex[(int)((lastKnownAddressValue >> 4) & 0xF)];
                LastKnownAddress += xHex[(int)(lastKnownAddressValue & 0xF)];
            }

            _ = new PrismGL2D.UI.Frame("Critical kernel error")
            {
                Controls = new()
                {
                    new PrismGL2D.UI.Label()
                    {
                        Text = $"{aName}\n{aDescription}\nLast known address: {LastKnownAddress}\nCTX Interupt: {CTXInterupt}",
					},
				},
            };
        }
    }
}