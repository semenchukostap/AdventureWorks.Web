using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Web.Models
{
    public partial class Product
    {
        public Product()
        {
            SalesOrderDetail = new HashSet<SalesOrderDetail>();
        }

        public int ProductId { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;
        
        [Required]
        public string ProductNumber { get; set; } = null!;
        
        public string? Color { get; set; }
        
        public decimal StandardCost { get; set; }
        
        public decimal ListPrice { get; set; }
        
        public string? Size { get; set; }
        
        public decimal? Weight { get; set; }
        
        public int? ProductCategoryId { get; set; }
        
        public int? ProductModelId { get; set; }
        
        public DateTime SellStartDate { get; set; }
        
        public DateTime? SellEndDate { get; set; }
        
        public DateTime? DiscontinuedDate { get; set; }
        
        public byte[]? ThumbNailPhoto { get; set; }
        
        public string? ThumbnailPhotoFileName { get; set; }
        
        public Guid Rowguid { get; set; }
        
        public DateTime ModifiedDate { get; set; }

        public ProductCategory? ProductCategory { get; set; }
        public ProductModel? ProductModel { get; set; }
        public ICollection<SalesOrderDetail> SalesOrderDetail { get; set; } = null!;
    }
}