namespace PrismAPI.Filesystem.Filesystems.XFS.Structure;

public enum SuperBlockFlags : byte
{
    VERSION_ATTRBIT,// Set if any inode have extended attributes.
    VERSION_NLINKBIT, // Set if any inodes use 32-bit di_nlink values. 
    VERSION_QUOTABIT, // Set if quotas are enabled on the filesystem. This also brings in the various quota fields in the superblock. 
    VERSION_ALIGNBIT, // Set if sb_inoalignmt is used. 
    VERSION_DALIGNBIT, // Set if sb_unit and sb_width are used. 
    VERSION_SHAREDBIT, // Set if sb_shared_vn is used. 
    VERSION_LOGV2BIT, // Set if version 2 journaling logs are used. 
    VERSION_SECTORBIT, // Set if sb_sectsize is not 512. 
    VERSION_EXTFLGBIT, // Unwritten extents are used. This is always set today. 
    VERSION_DIRV2BIT, // Version 2 directories are used. This is always set today. 
    VERSION_MOREBITSBIT, // Set if the sb_features2 field in the superblock contains more flags.
}