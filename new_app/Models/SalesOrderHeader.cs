using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents a sales order header entity for SOAP service operations.
/// Contains order-level information and related navigation properties.
/// </summary>
[DataContract]
public class SalesOrderHeader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SalesOrderHeader"/> class.
    /// Sets up the collection of sales order details.
    /// </summary>
    public SalesOrderHeader()
    {
        SalesOrderDetails = new HashSet<SalesOrderDetail>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the sales order.
    /// Primary key for the SalesOrderHeader entity.
    /// </summary>
    [DataMember]
    public int SalesOrderId { get; set; }

    /// <summary>
    /// Gets or sets the revision number of the sales order.
    /// Incremented each time the order is modified.
    /// </summary>
    [DataMember]
    public byte RevisionNumber { get; set; }

    /// <summary>
    /// Gets or sets the date the order was placed.
    /// </summary>
    [DataMember]
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Gets or sets the date the order is due to the customer.
    /// </summary>
    [DataMember]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the date the order was shipped to the customer.
    /// Null if not yet shipped.
    /// </summary>
    [DataMember]
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// Gets or sets the order status code.
    /// 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled
    /// </summary>
    [DataMember]
    public byte Status { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order was placed online.
    /// True = Order placed by customer online; False = Order placed by sales person.
    /// </summary>
    [DataMember]
    public bool OnlineOrderFlag { get; set; }

    /// <summary>
    /// Gets or sets the unique sales order number.
    /// </summary>
    [DataMember]
    public string SalesOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer purchase order number reference.
    /// </summary>
    [DataMember]
    public string? PurchaseOrderNumber { get; set; }

    /// <summary>
    /// Gets or sets the financial account number for the customer.
    /// </summary>
    [DataMember]
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets the customer identifier.
    /// Foreign key to Customer.
    /// </summary>
    [DataMember]
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the shipping address.
    /// Foreign key to Address.
    /// </summary>
    [DataMember]
    public int? ShipToAddressId { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the billing address.
    /// Foreign key to Address.
    /// </summary>
    [DataMember]
    public int? BillToAddressId { get; set; }

    /// <summary>
    /// Gets or sets the shipping method.
    /// </summary>
    [DataMember]
    public string ShipMethod { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the credit card approval code.
    /// </summary>
    [DataMember]
    public string? CreditCardApprovalCode { get; set; }

    /// <summary>
    /// Gets or sets the subtotal amount before tax and freight.
    /// </summary>
    [DataMember]
    public decimal SubTotal { get; set; }

    /// <summary>
    /// Gets or sets the tax amount.
    /// </summary>
    [DataMember]
    public decimal TaxAmt { get; set; }

    /// <summary>
    /// Gets or sets the shipping cost.
    /// </summary>
    [DataMember]
    public decimal Freight { get; set; }

    /// <summary>
    /// Gets or sets the total due amount.
    /// Computed as SubTotal + TaxAmt + Freight.
    /// </summary>
    [DataMember]
    public decimal TotalDue { get; set; }

    /// <summary>
    /// Gets or sets additional comments about the order.
    /// </summary>
    [DataMember]
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier used for replication.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time the record was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the billing address navigation property.
    /// Not serialized for SOAP operations.
    /// </summary>
    public Address? BillToAddress { get; set; }

    /// <summary>
    /// Gets or sets the customer navigation property.
    /// Not serialized for SOAP operations.
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets or sets the shipping address navigation property.
    /// Not serialized for SOAP operations.
    /// </summary>
    public Address? ShipToAddress { get; set; }

    /// <summary>
    /// Gets or sets the collection of sales order details for this order.
    /// Not serialized for SOAP operations.
    /// </summary>
    public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
}