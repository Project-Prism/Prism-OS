using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using PrismTools;

namespace PrismFilesystem
{
	public static class FilesystemManager
	{
		#region Methods

		/// <summary>
		/// Method to initialize all the filesystems for the disks connected to the machine.
		/// </summary>
		public static void Init()
		{
			try
			{
				VFSManager.RegisterVFS(new CosmosVFS(), false, false);
				Debugger.Success("Initialized the filesystem!");
			}
			catch
			{
				Debugger.Warn("Unable to initialize the filesystem!");
			}
		}

		#endregion

		#region Fields

		public static Debugger Debugger { get; set; } = new("Filesystem");

		#endregion
	}
}