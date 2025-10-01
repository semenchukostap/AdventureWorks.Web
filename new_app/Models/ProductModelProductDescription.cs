using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class ProductModelProductDescription
{
    [DataMember]
    public int ProductModelId { get; set; }

    [DataMember]
    public int ProductDescriptionId { get; set; }

    [DataMember]
    public string Culture { get; set; } = string.Empty;

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)
    public ProductDescription? ProductDescription { get; set; }
    public ProductModel? ProductModel { get; set; }
}
