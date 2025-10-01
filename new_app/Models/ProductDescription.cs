using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents a product description entity for SOAP web services.
/// Contains descriptive information about products in the AdventureWorks system.
/// </summary>
[DataContract]
public class ProductDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductDescription"/> class.
    /// </summary>
    public ProductDescription()
    {
        ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the product description.
    /// </summary>
    [DataMember]
    public int ProductDescriptionId { get; set; }

    /// <summary>
    /// Gets or sets the description text for the product.
    /// </summary>
    [DataMember]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the globally unique identifier (GUID) for the row.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product description was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of product model product descriptions.
    /// This navigation property is not serialized for SOAP services.
    /// </summary>
    public ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
}