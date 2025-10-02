namespace AdventureWorks.Web.Models;

public partial class SalesOrderDetail
{
    public int SalesOrderId { get; set; }
    public int SalesOrderDetailId { get; set; }
    public short OrderQty { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitPriceDiscount { get; set; }
    
    /// <summary>
    /// Computed column configured in DbContext
    /// </summary>
    public decimal LineTotal { get; set; }
    
    public Guid Rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public virtual Product Product { get; set; } = null!;
    public virtual SalesOrderHeader SalesOrder { get; set; } = null!;
}