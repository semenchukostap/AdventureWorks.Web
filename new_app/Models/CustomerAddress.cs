using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class CustomerAddress
{
    [DataMember]
    public int CustomerId { get; set; }

    [DataMember]
    public int AddressId { get; set; }

    [DataMember]
    public string AddressType { get; set; } = string.Empty;

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)
    public Address? Address { get; set; }
    public Customer? Customer { get; set; }
}
