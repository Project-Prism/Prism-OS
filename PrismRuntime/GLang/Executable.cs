using PrismRuntime.GLang.Structure;
using PrismGL2D;

namespace PrismRuntime.GLang
{
	public unsafe class Executable
	{
		public Executable(byte[] Binary)
		{
			fixed (byte* P = Binary)
			{
				this.Binary = P;
			}
			Canvas = new(0, 0);
			IsEnabled = true;
		}

		#region Methods

		public void Next()
		{
			if (IsEnabled)
			{
				switch ((OPCode)Binary[0]++)
				{
					case OPCode.Clear:
						Canvas.Clear(((uint*)Binary)[0]++);
						break;
					case OPCode.Draw:
						switch ((DrawMode)Binary[0]++)
						{
							case DrawMode.Rectangle:
								break;
							case DrawMode.Triangle:
								break;
							case DrawMode.Cirlce:
								break;
							case DrawMode.Pixel:
								break;
							case DrawMode.Line:
								break;
						}
						break;
					case OPCode.Exit:
						IsEnabled = false;
						break;
				}
			}
		}

		#endregion

		#region Fields

		public Graphics Canvas;
		public bool IsEnabled;
		public byte* Binary;

		#endregion
	}
}