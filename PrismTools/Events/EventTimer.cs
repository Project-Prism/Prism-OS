using Cosmos.HAL;

namespace PrismTools.Events
{
	public class EventTimer
	{
		/// <summary>
		/// Creates a new instance of the <see cref="EventTimer"/> class.
		/// </summary>
		/// <param name="CallBack">Event to call every update.</param>
		/// <param name="UPS">The number of updates per second.</param>
		public EventTimer(Action CallBack, uint UPS)
		{
			Global.PIT.RegisterTimer(new(CallBack, (ulong)(UPS * 266666.667), true));
		}
	}
}