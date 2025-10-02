using System;

namespace AdventureWorks.Web.Models;

/// <summary>
/// Junction table entity representing the many-to-many relationship between ProductModel and ProductDescription.
/// Supports culture-specific product descriptions for different languages/locales.
/// Composite key: (ProductModelId, ProductDescriptionId, Culture)
/// </summary>
public partial class ProductModelProductDescription
{
    /// <summary>
    /// Foreign key to ProductModel. Part of composite primary key.
    /// </summary>
    public int ProductModelId { get; set; }

    /// <summary>
    /// Foreign key to ProductDescription. Part of composite primary key.
    /// </summary>
    public int ProductDescriptionId { get; set; }

    /// <summary>
    /// Culture/language code (e.g., "en-US", "fr-FR"). Part of composite primary key.
    /// </summary>
    public string Culture { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier for this record.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Last modified date/time for this record.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Navigation property to the associated ProductDescription.
    /// </summary>
    public virtual ProductDescription ProductDescription { get; set; } = null!;

    /// <summary>
    /// Navigation property to the associated ProductModel.
    /// </summary>
    public virtual ProductModel ProductModel { get; set; } = null!;
}
