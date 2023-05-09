namespace PrismAPI.Filesystem.Filesystems.XFS.Structure;

public enum SuperBlockQuotaFlags : ushort
{
    UQUOTA_ACCT, // User quota accounting is enabled. 
    UQUOTA_ENFD, // User quotas are enforced. 
    UQUOTA_CHKD, // User quotas have been checked and updated on disk. 
    PQUOTA_ACCT, // Project quota accounting is enabled. 
    OQUOTA_ENFD, // Other (group/project) quotas are enforced. 
    OQUOTA_CHKD, // Other (group/project) quotas have been checked. 
    GQUOTA_ACCT, // Group quota accounting is enabled.
}