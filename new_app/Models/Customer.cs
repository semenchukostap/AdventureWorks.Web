using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class Customer
{
    public Customer()
    {
        CustomerAddresses = new HashSet<CustomerAddress>();
        SalesOrderHeaders = new HashSet<SalesOrderHeader>();
    }

    [DataMember]
    public int CustomerId { get; set; }

    [DataMember]
    public bool NameStyle { get; set; }

    [DataMember]
    public string? Title { get; set; }

    [DataMember]
    public string FirstName { get; set; } = string.Empty;

    [DataMember]
    public string? MiddleName { get; set; }

    [DataMember]
    public string LastName { get; set; } = string.Empty;

    [DataMember]
    public string? Suffix { get; set; }

    [DataMember]
    public string? CompanyName { get; set; }

    [DataMember]
    public string? SalesPerson { get; set; }

    [DataMember]
    public string? EmailAddress { get; set; }

    [DataMember]
    public string? Phone { get; set; }

    [DataMember]
    public string PasswordHash { get; set; } = string.Empty;

    [DataMember]
    public string PasswordSalt { get; set; } = string.Empty;

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)
    public ICollection<CustomerAddress> CustomerAddresses { get; set; }
    public ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
}
