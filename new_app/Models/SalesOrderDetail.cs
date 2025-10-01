using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents a sales order detail entity containing line item information for sales orders.
/// Migrated from AdventureWorks.Web.Models to support .NET 8 SOAP Web Services.
/// </summary>
[DataContract]
public class SalesOrderDetail
{
    /// <summary>
    /// Gets or sets the sales order identifier. Part of the composite primary key.
    /// </summary>
    [DataMember]
    public int SalesOrderId { get; set; }

    /// <summary>
    /// Gets or sets the sales order detail identifier. Part of the composite primary key.
    /// </summary>
    [DataMember]
    public int SalesOrderDetailId { get; set; }

    /// <summary>
    /// Gets or sets the quantity ordered.
    /// </summary>
    [DataMember]
    public short OrderQty { get; set; }

    /// <summary>
    /// Gets or sets the product identifier for the ordered item.
    /// </summary>
    [DataMember]
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the unit price for the product at the time of the order.
    /// </summary>
    [DataMember]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage applied to the unit price.
    /// </summary>
    [DataMember]
    public decimal UnitPriceDiscount { get; set; }

    /// <summary>
    /// Gets or sets the calculated line total (UnitPrice * OrderQty - discount).
    /// </summary>
    [DataMember]
    public decimal LineTotal { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the row.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the record was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties - not serialized for SOAP transmission
    
    /// <summary>
    /// Gets or sets the product associated with this order detail.
    /// Navigation property - not included in SOAP serialization.
    /// </summary>
    public Product? Product { get; set; }

    /// <summary>
    /// Gets or sets the sales order header associated with this detail.
    /// Navigation property - not included in SOAP serialization.
    /// </summary>
    public SalesOrderHeader? SalesOrder { get; set; }
}
