namespace AdventureWorks.Web.Models;

public partial class BuildVersion
{
    public byte SystemInformationId { get; set; }
    public string DatabaseVersion { get; set; } = string.Empty;
    public DateTime VersionDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
