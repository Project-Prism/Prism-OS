using Arc;
using System;
using static PrismOS.Storage.Framework;
using Cosmos.System.Graphics;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        public static ArcFile Config { get; } = new(Drives.CD_Drive + ":\\Config.arc");
        protected override void Run()
        {

            new UI.Framework.Image(
                400, 300,
                new Bitmap(Read(Drives.CD_Drive + Config.Read("Logo"), DataTypes.Bytes)), null).Draw();

            while (true)
            {
            }
        }
    }
}