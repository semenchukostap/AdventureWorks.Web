using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class ProductDescription
{
    public ProductDescription()
    {
        ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
    }

    [DataMember]
    public int ProductDescriptionId { get; set; }

    [DataMember]
    public string Description { get; set; } = string.Empty;

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation property (not serialized for SOAP)
    public ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
}
