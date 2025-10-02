namespace AdventureWorks.Web.Models;

public partial class Address
{
    public Address()
    {
        CustomerAddress = new HashSet<CustomerAddress>();
        SalesOrderHeaderBillToAddress = new HashSet<SalesOrderHeader>();
        SalesOrderHeaderShipToAddress = new HashSet<SalesOrderHeader>();
    }

    public int AddressId { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string StateProvince { get; set; } = string.Empty;
    public string CountryRegion { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public Guid Rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddress { get; set; }
    public virtual ICollection<SalesOrderHeader> SalesOrderHeaderBillToAddress { get; set; }
    public virtual ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddress { get; set; }
}