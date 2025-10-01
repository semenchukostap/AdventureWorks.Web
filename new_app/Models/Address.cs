using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents a physical address entity for customers and orders.
/// </summary>
[DataContract]
public class Address
{
    /// <summary>
    /// Initializes a new instance of the Address class.
    /// </summary>
    public Address()
    {
        CustomerAddresses = new HashSet<CustomerAddress>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the address.
    /// </summary>
    [DataMember]
    public int AddressId { get; set; }

    /// <summary>
    /// Gets or sets the first line of the address.
    /// </summary>
    [DataMember]
    public string AddressLine1 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional second line of the address.
    /// </summary>
    [DataMember]
    public string? AddressLine2 { get; set; }

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    [DataMember]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state or province name.
    /// </summary>
    [DataMember]
    public string StateProvince { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the country or region name.
    /// </summary>
    [DataMember]
    public string CountryRegion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    [DataMember]
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the globally unique identifier for the address.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time the address was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)

    /// <summary>
    /// Gets or sets the collection of customer addresses associated with this address.
    /// </summary>
    public ICollection<CustomerAddress> CustomerAddresses { get; set; }
}
