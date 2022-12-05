using Cosmos.HAL;

namespace PrismTools
{
	public class EventTimer
	{
		public EventTimer(Action CallBack, uint UPS)
		{
			Global.PIT.RegisterTimer(new(CallBack, (ulong)(UPS * 266666.667), true));
		}
	}
}