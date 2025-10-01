using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class SalesOrderHeader
{
    public SalesOrderHeader()
    {
        SalesOrderDetails = new HashSet<SalesOrderDetail>();
    }

    [DataMember]
    public int SalesOrderId { get; set; }

    [DataMember]
    public byte RevisionNumber { get; set; }

    [DataMember]
    public DateTime OrderDate { get; set; }

    [DataMember]
    public DateTime DueDate { get; set; }

    [DataMember]
    public DateTime? ShipDate { get; set; }

    [DataMember]
    public byte Status { get; set; }

    [DataMember]
    public bool OnlineOrderFlag { get; set; }

    [DataMember]
    public string SalesOrderNumber { get; set; } = string.Empty;

    [DataMember]
    public string? PurchaseOrderNumber { get; set; }

    [DataMember]
    public string? AccountNumber { get; set; }

    [DataMember]
    public int CustomerId { get; set; }

    [DataMember]
    public int? ShipToAddressId { get; set; }

    [DataMember]
    public int? BillToAddressId { get; set; }

    [DataMember]
    public string ShipMethod { get; set; } = string.Empty;

    [DataMember]
    public string? CreditCardApprovalCode { get; set; }

    [DataMember]
    public decimal SubTotal { get; set; }

    [DataMember]
    public decimal TaxAmt { get; set; }

    [DataMember]
    public decimal Freight { get; set; }

    [DataMember]
    public decimal TotalDue { get; set; }

    [DataMember]
    public string? Comment { get; set; }

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)
    public Address? BillToAddress { get; set; }
    public Customer? Customer { get; set; }
    public Address? ShipToAddress { get; set; }
    public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
}
