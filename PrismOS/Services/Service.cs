namespace PrismOS.Services;

public abstract class Service
{
	internal Service()
	{
		Timer T = new((_) => { if (EnableTicks) { Tick(); } }, null, 55, 0);
	}

	public abstract void Tick();

	public bool EnableTicks;
}