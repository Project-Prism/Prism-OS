using System.Numerics;

namespace PrismAPI.Graphics.Physics;

public class Gravity
{
	public Gravity()
	{
		Velocity = Vector3.Zero;
	}

	#region Methods

	public void Next()
	{
		Velocity += Force - new Vector3(Mass);
		Velocity.Z -= 9.80665f;

		Speed = (Velocity.X + Velocity.Y + Velocity.Z) / 3;
	}

	#endregion

	#region Fields

	public Vector3 Velocity;
	public Vector3 Force;
	public float Speed;
	public float Mass;

	#endregion
}