using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

/// <summary>
/// Represents the relationship between product models and their descriptions in different cultures.
/// This entity uses a composite key structure consisting of ProductModelId, ProductDescriptionId, and Culture.
/// </summary>
[DataContract]
public class ProductModelProductDescription
{
    /// <summary>
    /// Gets or sets the product model identifier.
    /// Part of the composite primary key.
    /// </summary>
    [DataMember]
    public int ProductModelId { get; set; }

    /// <summary>
    /// Gets or sets the product description identifier.
    /// Part of the composite primary key.
    /// </summary>
    [DataMember]
    public int ProductDescriptionId { get; set; }

    /// <summary>
    /// Gets or sets the culture code for the product description (e.g., "en-US", "fr-FR").
    /// Part of the composite primary key.
    /// </summary>
    [DataMember]
    public string Culture { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the globally unique identifier for this record.
    /// Used for replication and synchronization purposes.
    /// </summary>
    [DataMember]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this record was last modified.
    /// </summary>
    [DataMember]
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the related product description entity.
    /// This navigation property is not serialized for SOAP services.
    /// </summary>
    public ProductDescription? ProductDescription { get; set; }

    /// <summary>
    /// Gets or sets the related product model entity.
    /// This navigation property is not serialized for SOAP services.
    /// </summary>
    public ProductModel? ProductModel { get; set; }
}