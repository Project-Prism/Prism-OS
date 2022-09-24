namespace PrismGL2D.Formats.Generated
{
	public class PerlinNoise : Graphics
	{
		public PerlinNoise(uint Width, uint Height, int Seed, int Octaves = 1) : base(Width, Height)
		{
		}

		public unsafe float[] GetNoise1D(float Scale, int Seed, int Octaves)
		{
			Octaves %= 8; // Wrap around, max of 8.

			float[] NoiseSeed = new float[Width];
			float[] Noise1D = new float[Width];
			float ScaleAccumulate = 1.0f;
			Random Random = new(Seed);

			for (int X = 0; X < Width; X++)
			{
				NoiseSeed[X] = (float)Random.NextDouble();
				float Noise = 0.0f;

				for (int O = 0; O < Octaves; O++)
				{
					int Pitch = (int)(Width >> O);
					int Sample1 = X / Pitch * Pitch;
					int Sample2 = (Sample1 + Pitch) % (int)Width;

					float Blend = (float)(X - Sample1) / Pitch;
					float Sample = (1.0f - Blend) * NoiseSeed[Sample1] + Blend * NoiseSeed[Sample2];

					Noise += Sample * Scale;
					ScaleAccumulate += Scale;
					Scale /= 2;
				}

				Noise1D[X] = Noise / ScaleAccumulate;
			}

			return Noise1D;
		}
	}
}