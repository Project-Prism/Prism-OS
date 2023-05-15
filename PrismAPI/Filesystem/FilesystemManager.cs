using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using Cosmos.HAL.BlockDevice;
using PrismAPI.Tools;

namespace PrismAPI.Filesystem;

/// <summary>
/// The Prism OS file syste manager, used to interact with disks.
/// </summary>
public static class FilesystemManager
{
	/// <summary>
	/// Statically initializes this class when it is first accessed (lazy loading).
	/// Should run when <see cref="Init()"/> is ran.
	/// </summary>
	static FilesystemManager()
	{
		// Initialize the debugger and VFS.
		Debugger = new("Filesystem");
		VFS = new();
	}

	#region Methods

	/// <summary>
	/// Formats the specified disk with a single partition.
	/// </summary>
	/// <param name="Disk">The disk, must be specified. Main disk is typically 0.</param>
	/// <param name="Format">The format to use, only "FAT32" is currently supported.</param>
	public static void Format(int Disk, string Format)
	{
		// Write format status indicating formatting has begun.
		Debugger.WriteFull($@"Formatting disk {Disk}...", Severity.Info);

		// Select the proper disk to format.
		Disk SelectedDisk = VFS.Disks[Disk];

		// Delete all partitions.
		SelectedDisk.Clear();

		// Create MBR and Primary partitions.
		SelectedDisk.CreatePartition(512);
		SelectedDisk.CreatePartition((SelectedDisk.Size - 512) / 1048576);

		// Create MBR helper instance and write info to the disk.
		MBR Helper = new(SelectedDisk.Host);
		Helper.CreateMBR(SelectedDisk.Host);
		Helper.WritePartitionInformation(SelectedDisk.Partitions[0].Host, 0);

		// Format the primary partition as the requested format.
		SelectedDisk.FormatPartition(1, Format, true);
	}

	/// <summary>
	/// Method to initialize all the filesystems for the disks connected to the machine.
	/// </summary>
	public static void Init()
	{
		try
		{
			Debugger.WritePartial("Initializing FS...");
			VFSManager.RegisterVFS(VFS, false, false);
			Debugger.Finalize(Severity.Success);
		}
		catch
		{
			Debugger.Finalize(Severity.Fail);
		}
	}

	#endregion

	#region Fields

	public static Debugger Debugger;
	public static CosmosVFS VFS;

	#endregion
}