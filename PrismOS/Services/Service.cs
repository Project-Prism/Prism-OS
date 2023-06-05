namespace PrismOS.Services;

public abstract class Service
{
	public Service()
	{
		Timer T = new((object? O) => { if (EnableTicks) { Tick(); } }, null, 55, 0);
	}

	public abstract void Tick();

	public bool EnableTicks;
}