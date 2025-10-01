using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents a product model in the AdventureWorks database.
/// Contains information about product models and their associated descriptions.
/// </summary>
[DataContract]
public class ProductModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductModel"/> class.
    /// </summary>
    public ProductModel()
    {
        Products = new HashSet<Product>();
        ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the product model.
    /// </summary>
    [DataMember]
    public int ProductModelId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product model.
    /// </summary>
    [DataMember]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the catalog description in XML format.
    /// This is an optional field that may contain detailed product information.
    /// </summary>
    [DataMember]
    public string? CatalogDescription { get; set; }

    /// <summary>
    /// Gets or sets the globally unique identifier (GUID) for the product model row.
    /// Used for merge replication and external system references.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product model record was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of products associated with this product model.
    /// This navigation property is not serialized for SOAP services.
    /// </summary>
    public ICollection<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the collection of product model descriptions in various cultures.
    /// This navigation property is not serialized for SOAP services.
    /// </summary>
    public ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
}