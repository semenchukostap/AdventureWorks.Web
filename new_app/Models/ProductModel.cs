using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public partial class ProductModel
{
    public ProductModel()
    {
        Products = new HashSet<Product>();
        ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
    }

    [DataMember]
    public int ProductModelId { get; set; }

    [DataMember]
    public string Name { get; set; } = string.Empty;

    [DataMember]
    public string? CatalogDescription { get; set; }

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)
    public ICollection<Product> Products { get; set; }
    public ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
}
