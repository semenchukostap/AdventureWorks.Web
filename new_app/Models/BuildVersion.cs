using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class BuildVersion
{
    [DataMember]
    public byte SystemInformationId { get; set; }

    [DataMember]
    public string DatabaseVersion { get; set; } = string.Empty;

    [DataMember]
    public DateTime VersionDate { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }
}
