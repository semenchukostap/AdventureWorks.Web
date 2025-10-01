using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents a customer entity in the AdventureWorks system.
/// </summary>
[DataContract]
public class Customer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Customer"/> class.
    /// </summary>
    public Customer()
    {
        CustomerAddresses = new HashSet<CustomerAddress>();
        SalesOrderHeaders = new HashSet<SalesOrderHeader>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    [DataMember]
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the name style is Western or Eastern.
    /// False indicates Western style (FirstName LastName), True indicates Eastern style (LastName FirstName).
    /// </summary>
    [DataMember]
    public bool NameStyle { get; set; }

    /// <summary>
    /// Gets or sets the customer's title (e.g., Mr., Ms., Dr.).
    /// </summary>
    [DataMember]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the customer's first name.
    /// </summary>
    [DataMember]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's middle name or initial.
    /// </summary>
    [DataMember]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the customer's last name.
    /// </summary>
    [DataMember]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's name suffix (e.g., Jr., Sr., III).
    /// </summary>
    [DataMember]
    public string? Suffix { get; set; }

    /// <summary>
    /// Gets or sets the name of the company the customer is associated with.
    /// </summary>
    [DataMember]
    public string? CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the sales person assigned to this customer.
    /// </summary>
    [DataMember]
    public string? SalesPerson { get; set; }

    /// <summary>
    /// Gets or sets the customer's email address.
    /// </summary>
    [DataMember]
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the customer's phone number.
    /// </summary>
    [DataMember]
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the hashed password for the customer's account.
    /// </summary>
    [DataMember]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the salt value used for password hashing.
    /// </summary>
    [DataMember]
    public string PasswordSalt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the globally unique identifier for the customer record.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the customer record was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of addresses associated with this customer.
    /// This navigation property is not serialized for SOAP.
    /// </summary>
    public ICollection<CustomerAddress> CustomerAddresses { get; set; }

    /// <summary>
    /// Gets or sets the collection of sales order headers associated with this customer.
    /// This navigation property is not serialized for SOAP.
    /// </summary>
    public ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
}