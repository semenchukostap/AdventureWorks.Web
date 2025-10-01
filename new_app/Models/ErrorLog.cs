using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class ErrorLog
{
    [DataMember]
    public int ErrorLogId { get; set; }

    [DataMember]
    public DateTime ErrorTime { get; set; }

    [DataMember]
    public string UserName { get; set; } = string.Empty;

    [DataMember]
    public int ErrorNumber { get; set; }

    [DataMember]
    public int? ErrorSeverity { get; set; }

    [DataMember]
    public int? ErrorState { get; set; }

    [DataMember]
    public string? ErrorProcedure { get; set; }

    [DataMember]
    public int? ErrorLine { get; set; }

    [DataMember]
    public string ErrorMessage { get; set; } = string.Empty;
}
