using PrismRuntime.GLang.Structure;
using PrismGL2D;

namespace PrismRuntime.GLang
{
	public unsafe class Executable
	{
		public Executable(byte[] Binary)
		{
			Reader = new(new MemoryStream(Binary));
			Canvas = new(0, 0);
			IsEnabled = true;
		}

		#region Methods

		public void Next()
		{
			if (IsEnabled)
			{
				uint X;
				uint Y;
				uint Width;
				uint Height;
				uint Color;

				switch ((OPCode)Reader.ReadByte())
				{
					case OPCode.SetMode:
						Width = Reader.ReadUInt32();
						Height = Reader.ReadUInt32();
						Canvas.Width = Width;
						Canvas.Height = Height;
						break;
					case OPCode.Clear:
						Canvas.Clear(Reader.ReadUInt32());
						break;
					case OPCode.Draw:
						switch ((DrawMode)Reader.ReadByte())
						{
							case DrawMode.Rectangle:
								X = Reader.ReadUInt32();
								Y = Reader.ReadUInt32();
								Width = Reader.ReadUInt32();
								Height = Reader.ReadUInt32();
								Color = Reader.ReadUInt32();
								Canvas.DrawRectangle((int)X, (int)Y, Width, Height, 0, Color);
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

		public BinaryReader Reader;
		public Graphics Canvas;
		public bool IsEnabled;

		#endregion
	}
}