using System.Runtime.Serialization;

namespace AdventureWorks.Soap.Models;

[DataContract]
public class ProductCategory
{
    public ProductCategory()
    {
        InverseParentProductCategory = new HashSet<ProductCategory>();
        Products = new HashSet<Product>();
    }

    [DataMember]
    public int ProductCategoryId { get; set; }

    [DataMember]
    public int? ParentProductCategoryId { get; set; }

    [DataMember]
    public string Name { get; set; } = string.Empty;

    [DataMember]
    public Guid Rowguid { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }

    // Navigation properties (not serialized for SOAP)
    public ProductCategory? ParentProductCategory { get; set; }
    public ICollection<ProductCategory> InverseParentProductCategory { get; set; }
    public ICollection<Product> Products { get; set; }
}
