namespace PrismAPI.Graphics;

public class Gaussian : Canvas
{
	#region  Constructors

	public Gaussian(Canvas Source, int Radial) : base(Source.Width, Source.Height)
	{
		int[] A = new int[Source.Size];
		int[] R = new int[Source.Size];
		int[] G = new int[Source.Size];
		int[] B = new int[Source.Size];

		for (uint I = 0; I < Source.Size; I++)
		{
			A[I] = (int)Source[I].A;
			R[I] = (int)Source[I].R;
			G[I] = (int)Source[I].G;
			B[I] = (int)Source[I].B;
		}

		var newAlpha = new int[Source.Size];
		var newRed = new int[Source.Size];
		var newGreen = new int[Source.Size];
		var newBlue = new int[Source.Size];

		GaussBlur4(A, newAlpha, Radial);
		GaussBlur4(R, newRed, Radial);
		GaussBlur4(G, newGreen, Radial);
		GaussBlur4(B, newBlue, Radial);

		for (uint I = 0; I < Source.Size; I++)
		{
			if (newAlpha[I] > 255) newAlpha[I] = 255;
			if (newRed[I] > 255) newRed[I] = 255;
			if (newGreen[I] > 255) newGreen[I] = 255;
			if (newBlue[I] > 255) newBlue[I] = 255;

			if (newAlpha[I] < 0) newAlpha[I] = 0;
			if (newRed[I] < 0) newRed[I] = 0;
			if (newGreen[I] < 0) newGreen[I] = 0;
			if (newBlue[I] < 0) newBlue[I] = 0;

			this[I] = new(newAlpha[I], newRed[I], newGreen[I], newBlue[I]);
		}
	}

	#endregion

	#region Methods

	private void GaussBlur4(int[] source, int[] dest, int r)
	{
		var bxs = BoxesForGauss(r, 3);
		BoxBlur4(source, dest, Width, Height, (bxs[0] - 1) / 2);
		BoxBlur4(dest, source, Width, Height, (bxs[1] - 1) / 2);
		BoxBlur4(source, dest, Width, Height, (bxs[2] - 1) / 2);
	}

	private int[] BoxesForGauss(int sigma, int n)
	{
		var wIdeal = Math.Sqrt((12 * sigma * sigma / n) + 1);
		var wl = (int)Math.Floor(wIdeal);
		if (wl % 2 == 0) wl--;
		var wu = wl + 2;

		var mIdeal = (double)(12 * sigma * sigma - n * wl * wl - 4 * n * wl - 3 * n) / (-4 * wl - 4);
		var m = Math.Round(mIdeal);

		var sizes = new List<int>();
		for (var i = 0; i < n; i++) sizes.Add(i < m ? wl : wu);
		return sizes.ToArray();
	}

	private void BoxBlur4(int[] source, int[] dest, int w, int h, int r)
	{
		for (var i = 0; i < source.Length; i++) dest[i] = source[i];
		BoxBlurH4(dest, source, w, h, r);
		BoxBlurT4(source, dest, w, h, r);
	}

	private void BoxBlurH4(int[] source, int[] dest, int w, int h, int r)
	{
		var iar = (double)1 / (r + r + 1);
		for (int I = 0; I < h; I++)
		{
			var ti = I * w;
			var li = ti;
			var ri = ti + r;
			var fv = source[ti];
			var lv = source[ti + w - 1];
			var val = (r + 1) * fv;
			for (var j = 0; j < r; j++) val += source[ti + j];
			for (var j = 0; j <= r; j++)
			{
				val += source[ri++] - fv;
				dest[ti++] = (int)Math.Round(val * iar);
			}
			for (var j = r + 1; j < w - r; j++)
			{
				val += source[ri++] - dest[li++];
				dest[ti++] = (int)Math.Round(val * iar);
			}
			for (var j = w - r; j < w; j++)
			{
				val += lv - source[li++];
				dest[ti++] = (int)Math.Round(val * iar);
			}
		}
	}

	private void BoxBlurT4(int[] source, int[] dest, int w, int h, int r)
	{
		var iar = (double)1 / (r + r + 1);
		for (int I = 0; I < w; I++)
		{
			var ti = I;
			var li = ti;
			var ri = ti + r * w;
			var fv = source[ti];
			var lv = source[ti + w * (h - 1)];
			var val = (r + 1) * fv;
			for (var j = 0; j < r; j++) val += source[ti + j * w];
			for (var j = 0; j <= r; j++)
			{
				val += source[ri] - fv;
				dest[ti] = (int)Math.Round(val * iar);
				ri += w;
				ti += w;
			}
			for (var j = r + 1; j < h - r; j++)
			{
				val += source[ri] - source[li];
				dest[ti] = (int)Math.Round(val * iar);
				li += w;
				ri += w;
				ti += w;
			}
			for (var j = h - r; j < h; j++)
			{
				val += lv - source[li];
				dest[ti] = (int)Math.Round(val * iar);
				li += w;
				ti += w;
			}
		}
	}

	#endregion
}