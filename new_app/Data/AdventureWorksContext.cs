using Microsoft.EntityFrameworkCore;
using AdventureWorks.Soap.Models;

namespace AdventureWorks.Soap.Data;

/// <summary>
/// Entity Framework Core DbContext for AdventureWorks database.
/// Configured for PostgreSQL with lowercase table and column names in the 'saleslt' schema.
/// </summary>
public class AdventureWorksContext : DbContext
{
    public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
        : base(options)
    {
    }

    // DbSet properties for all entities
    public virtual DbSet<Address> Addresses { get; set; } = null!;
    public virtual DbSet<BuildVersion> BuildVersions { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
    public virtual DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; } = null!;
    public virtual DbSet<ProductModel> ProductModels { get; set; } = null!;
    public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = null!;
    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; } = null!;
    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set default schema to 'saleslt' for PostgreSQL
        modelBuilder.HasDefaultSchema("saleslt");

        // Configure Address entity
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("address");
            entity.HasKey(e => e.AddressId);

            entity.Property(e => e.AddressId).HasColumnName("addressid");
            entity.Property(e => e.AddressLine1).HasColumnName("addressline1").HasMaxLength(60).IsRequired();
            entity.Property(e => e.AddressLine2).HasColumnName("addressline2").HasMaxLength(60);
            entity.Property(e => e.City).HasColumnName("city").HasMaxLength(30).IsRequired();
            entity.Property(e => e.StateProvince).HasColumnName("stateprovince").HasMaxLength(50).IsRequired();
            entity.Property(e => e.CountryRegion).HasColumnName("countryregion").HasMaxLength(50).IsRequired();
            entity.Property(e => e.PostalCode).HasColumnName("postalcode").HasMaxLength(15).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
        });

        // Configure BuildVersion entity
        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.ToTable("buildversion");
            entity.HasKey(e => e.SystemInformationId);

            entity.Property(e => e.SystemInformationId).HasColumnName("systeminformationid");
            entity.Property(e => e.DatabaseVersion).HasColumnName("database version").HasMaxLength(25).IsRequired();
            entity.Property(e => e.VersionDate).HasColumnName("versiondate");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
        });

        // Configure Customer entity
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.NameStyle).HasColumnName("namestyle");
            entity.Property(e => e.Title).HasColumnName("title").HasMaxLength(8);
            entity.Property(e => e.FirstName).HasColumnName("firstname").HasMaxLength(50).IsRequired();
            entity.Property(e => e.MiddleName).HasColumnName("middlename").HasMaxLength(50);
            entity.Property(e => e.LastName).HasColumnName("lastname").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Suffix).HasColumnName("suffix").HasMaxLength(10);
            entity.Property(e => e.CompanyName).HasColumnName("companyname").HasMaxLength(128);
            entity.Property(e => e.SalesPerson).HasColumnName("salesperson").HasMaxLength(256);
            entity.Property(e => e.EmailAddress).HasColumnName("emailaddress").HasMaxLength(50);
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(25);
            entity.Property(e => e.PasswordHash).HasColumnName("passwordhash").HasMaxLength(128).IsRequired();
            entity.Property(e => e.PasswordSalt).HasColumnName("passwordsalt").HasMaxLength(10).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
        });

        // Configure CustomerAddress entity
        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.ToTable("customeraddress");
            entity.HasKey(e => new { e.CustomerId, e.AddressId });

            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.AddressId).HasColumnName("addressid");
            entity.Property(e => e.AddressType).HasColumnName("addresstype").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Relationships
            entity.HasOne(d => d.Address)
                .WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Configure ErrorLog entity
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("errorlog");
            entity.HasKey(e => e.ErrorLogId);

            entity.Property(e => e.ErrorLogId).HasColumnName("errorlogid");
            entity.Property(e => e.ErrorTime).HasColumnName("errortime");
            entity.Property(e => e.UserName).HasColumnName("username").HasMaxLength(128).IsRequired();
            entity.Property(e => e.ErrorNumber).HasColumnName("errornumber");
            entity.Property(e => e.ErrorSeverity).HasColumnName("errorseverity");
            entity.Property(e => e.ErrorState).HasColumnName("errorstate");
            entity.Property(e => e.ErrorProcedure).HasColumnName("errorprocedure").HasMaxLength(126);
            entity.Property(e => e.ErrorLine).HasColumnName("errorline");
            entity.Property(e => e.ErrorMessage).HasColumnName("errormessage").HasMaxLength(4000).IsRequired();
        });

        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ProductNumber).HasColumnName("productnumber").HasMaxLength(25).IsRequired();
            entity.Property(e => e.Color).HasColumnName("color").HasMaxLength(15);
            entity.Property(e => e.StandardCost).HasColumnName("standardcost").HasColumnType("decimal(19,4)");
            entity.Property(e => e.ListPrice).HasColumnName("listprice").HasColumnType("decimal(19,4)");
            entity.Property(e => e.Size).HasColumnName("size").HasMaxLength(5);
            entity.Property(e => e.Weight).HasColumnName("weight").HasColumnType("decimal(8,2)");
            entity.Property(e => e.ProductCategoryId).HasColumnName("productcategoryid");
            entity.Property(e => e.ProductModelId).HasColumnName("productmodelid");
            entity.Property(e => e.SellStartDate).HasColumnName("sellstartdate");
            entity.Property(e => e.SellEndDate).HasColumnName("sellenddate");
            entity.Property(e => e.DiscontinuedDate).HasColumnName("discontinueddate");
            entity.Property(e => e.ThumbNailPhoto).HasColumnName("thumbnailphoto");
            entity.Property(e => e.ThumbnailPhotoFileName).HasColumnName("thumbnailphotofilename").HasMaxLength(50);
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Relationships
            entity.HasOne(d => d.ProductCategory)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductModelId);
        });

        // Configure ProductCategory entity
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("productcategory");
            entity.HasKey(e => e.ProductCategoryId);

            entity.Property(e => e.ProductCategoryId).HasColumnName("productcategoryid");
            entity.Property(e => e.ParentProductCategoryId).HasColumnName("parentproductcategoryid");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Self-referencing relationship
            entity.HasOne(d => d.ParentProductCategory)
                .WithMany(p => p.InverseParentProductCategories)
                .HasForeignKey(d => d.ParentProductCategoryId);
        });

        // Configure ProductDescription entity
        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.ToTable("productdescription");
            entity.HasKey(e => e.ProductDescriptionId);

            entity.Property(e => e.ProductDescriptionId).HasColumnName("productdescriptionid");
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(400).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
        });

        // Configure ProductModel entity
        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("productmodel");
            entity.HasKey(e => e.ProductModelId);

            entity.Property(e => e.ProductModelId).HasColumnName("productmodelid");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            entity.Property(e => e.CatalogDescription).HasColumnName("catalogdescription");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");
        });

        // Configure ProductModelProductDescription entity
        modelBuilder.Entity<ProductModelProductDescription>(entity =>
        {
            entity.ToTable("productmodelproductdescription");
            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture });

            entity.Property(e => e.ProductModelId).HasColumnName("productmodelid");
            entity.Property(e => e.ProductDescriptionId).HasColumnName("productdescriptionid");
            entity.Property(e => e.Culture).HasColumnName("culture").HasMaxLength(6).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Relationships
            entity.HasOne(d => d.ProductDescription)
                .WithMany(p => p.ProductModelProductDescriptions)
                .HasForeignKey(d => d.ProductDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.ProductModelProductDescriptions)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Configure SalesOrderDetail entity
        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.ToTable("salesorderdetail");
            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId });

            entity.Property(e => e.SalesOrderId).HasColumnName("salesorderid");
            entity.Property(e => e.SalesOrderDetailId).HasColumnName("salesorderdetailid");
            entity.Property(e => e.OrderQty).HasColumnName("orderqty");
            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.UnitPrice).HasColumnName("unitprice").HasColumnType("decimal(19,4)");
            entity.Property(e => e.UnitPriceDiscount).HasColumnName("unitpricediscount").HasColumnType("decimal(19,4)");
            entity.Property(e => e.LineTotal).HasColumnName("linetotal").HasColumnType("decimal(38,6)");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Relationships
            entity.HasOne(d => d.Product)
                .WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.SalesOrderId);
        });

        // Configure SalesOrderHeader entity
        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.ToTable("salesorderheader");
            entity.HasKey(e => e.SalesOrderId);

            entity.Property(e => e.SalesOrderId).HasColumnName("salesorderid");
            entity.Property(e => e.RevisionNumber).HasColumnName("revisionnumber");
            entity.Property(e => e.OrderDate).HasColumnName("orderdate");
            entity.Property(e => e.DueDate).HasColumnName("duedate");
            entity.Property(e => e.ShipDate).HasColumnName("shipdate");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.OnlineOrderFlag).HasColumnName("onlineorderflag");
            entity.Property(e => e.SalesOrderNumber).HasColumnName("salesordernumber").HasMaxLength(25).IsRequired();
            entity.Property(e => e.PurchaseOrderNumber).HasColumnName("purchaseordernumber").HasMaxLength(25);
            entity.Property(e => e.AccountNumber).HasColumnName("accountnumber").HasMaxLength(15);
            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.ShipToAddressId).HasColumnName("shiptoaddressid");
            entity.Property(e => e.BillToAddressId).HasColumnName("billtoaddressid");
            entity.Property(e => e.ShipMethod).HasColumnName("shipmethod").HasMaxLength(50).IsRequired();
            entity.Property(e => e.CreditCardApprovalCode).HasColumnName("creditcardapprovalcode").HasMaxLength(15);
            entity.Property(e => e.SubTotal).HasColumnName("subtotal").HasColumnType("decimal(19,4)");
            entity.Property(e => e.TaxAmt).HasColumnName("taxamt").HasColumnType("decimal(19,4)");
            entity.Property(e => e.Freight).HasColumnName("freight").HasColumnType("decimal(19,4)");
            entity.Property(e => e.TotalDue).HasColumnName("totaldue").HasColumnType("decimal(19,4)");
            entity.Property(e => e.Comment).HasColumnName("comment").HasMaxLength(1000);
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate");

            // Relationships
            entity.HasOne(d => d.BillToAddress)
                .WithMany(p => p.SalesOrderHeadersBillTo)
                .HasForeignKey(d => d.BillToAddressId);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipToAddress)
                .WithMany(p => p.SalesOrderHeadersShipTo)
                .HasForeignKey(d => d.ShipToAddressId);
        });
    }
}