using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Product entity class for AdventureWorks SOAP Web Services.
/// Represents product information with SOAP serialization support.
/// </summary>
[DataContract]
public class Product
{
    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product()
    {
        SalesOrderDetails = new HashSet<SalesOrderDetail>();
    }

    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    [DataMember]
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    [DataMember]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product number (unique identifier).
    /// </summary>
    [DataMember]
    public string ProductNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product color.
    /// </summary>
    [DataMember]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the standard cost of the product.
    /// </summary>
    [DataMember]
    public decimal StandardCost { get; set; }

    /// <summary>
    /// Gets or sets the list price of the product.
    /// </summary>
    [DataMember]
    public decimal ListPrice { get; set; }

    /// <summary>
    /// Gets or sets the size of the product.
    /// </summary>
    [DataMember]
    public string? Size { get; set; }

    /// <summary>
    /// Gets or sets the weight of the product.
    /// </summary>
    [DataMember]
    public decimal? Weight { get; set; }

    /// <summary>
    /// Gets or sets the product category identifier.
    /// </summary>
    [DataMember]
    public int? ProductCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the product model identifier.
    /// </summary>
    [DataMember]
    public int? ProductModelId { get; set; }

    /// <summary>
    /// Gets or sets the date the product became available for sale.
    /// </summary>
    [DataMember]
    public DateTime SellStartDate { get; set; }

    /// <summary>
    /// Gets or sets the date the product was no longer available for sale.
    /// </summary>
    [DataMember]
    public DateTime? SellEndDate { get; set; }

    /// <summary>
    /// Gets or sets the date the product was discontinued.
    /// </summary>
    [DataMember]
    public DateTime? DiscontinuedDate { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail photo of the product.
    /// </summary>
    [DataMember]
    public byte[]? ThumbNailPhoto { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail photo file name.
    /// </summary>
    [DataMember]
    public string? ThumbnailPhotoFileName { get; set; }

    /// <summary>
    /// Gets or sets the row GUID (unique identifier for the row).
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date the record was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties - not serialized in SOAP responses

    /// <summary>
    /// Gets or sets the associated product category.
    /// </summary>
    public ProductCategory? ProductCategory { get; set; }

    /// <summary>
    /// Gets or sets the associated product model.
    /// </summary>
    public ProductModel? ProductModel { get; set; }

    /// <summary>
    /// Gets or sets the collection of sales order details for this product.
    /// </summary>
    public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
}
