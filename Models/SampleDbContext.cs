using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Web.Models;

public partial class SampleDbContext : DbContext
{
    public SampleDbContext()
    {
    }

    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Address { get; set; }
    public virtual DbSet<BuildVersion> BuildVersion { get; set; }
    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
    public virtual DbSet<ErrorLog> ErrorLog { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<ProductCategory> ProductCategory { get; set; }
    public virtual DbSet<ProductDescription> ProductDescription { get; set; }
    public virtual DbSet<ProductModel> ProductModel { get; set; }
    public virtual DbSet<ProductModelProductDescription> ProductModelProductDescription { get; set; }
    public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
    public virtual DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Address Entity Configuration
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("address", "saleslt");

            entity.HasKey(e => e.AddressId);

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_address_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.StateProvince);

            entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion });

            entity.Property(e => e.AddressId).HasColumnName("addressid");
            entity.Property(e => e.AddressLine1).HasColumnName("addressline1").HasMaxLength(60).IsRequired();
            entity.Property(e => e.AddressLine2).HasColumnName("addressline2").HasMaxLength(60);
            entity.Property(e => e.City).HasColumnName("city").HasMaxLength(30).IsRequired();
            entity.Property(e => e.CountryRegion).HasColumnName("countryregion").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.PostalCode).HasColumnName("postalcode").HasMaxLength(15).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.StateProvince).HasColumnName("stateprovince").HasMaxLength(50).IsRequired();
        });

        // BuildVersion Entity Configuration
        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.ToTable("buildversion", "dbo");

            entity.HasKey(e => e.SystemInformationId);

            entity.Property(e => e.SystemInformationId).HasColumnName("systeminformationid").ValueGeneratedOnAdd();
            entity.Property(e => e.DatabaseVersion).HasColumnName("database version").HasMaxLength(25).IsRequired();
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.VersionDate).HasColumnName("versiondate");
        });

        // Customer Entity Configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer", "saleslt");

            entity.HasKey(e => e.CustomerId);

            entity.HasIndex(e => e.EmailAddress);

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_customer_rowguid")
                .IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.CompanyName).HasColumnName("companyname").HasMaxLength(128);
            entity.Property(e => e.EmailAddress).HasColumnName("emailaddress").HasMaxLength(50);
            entity.Property(e => e.FirstName).HasColumnName("firstname").HasMaxLength(50).IsRequired();
            entity.Property(e => e.LastName).HasColumnName("lastname").HasMaxLength(50).IsRequired();
            entity.Property(e => e.MiddleName).HasColumnName("middlename").HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.NameStyle).HasColumnName("namestyle");
            entity.Property(e => e.PasswordHash).HasColumnName("passwordhash").HasMaxLength(128).IsRequired();
            entity.Property(e => e.PasswordSalt).HasColumnName("passwordsalt").HasMaxLength(10).IsRequired();
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(25);
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.SalesPerson).HasColumnName("salesperson").HasMaxLength(256);
            entity.Property(e => e.Suffix).HasColumnName("suffix").HasMaxLength(10);
            entity.Property(e => e.Title).HasColumnName("title").HasMaxLength(8);
        });

        // CustomerAddress Entity Configuration
        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.ToTable("customeraddress", "saleslt");

            entity.HasKey(e => new { e.CustomerId, e.AddressId });

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_customeraddress_rowguid")
                .IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.AddressId).HasColumnName("addressid");
            entity.Property(e => e.AddressType).HasColumnName("addresstype").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Address)
                .WithMany(p => p.CustomerAddress)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerAddress)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // ErrorLog Entity Configuration
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("errorlog", "dbo");

            entity.HasKey(e => e.ErrorLogId);

            entity.Property(e => e.ErrorLogId).HasColumnName("errorlogid");
            entity.Property(e => e.ErrorMessage).HasColumnName("errormessage").HasMaxLength(4000).IsRequired();
            entity.Property(e => e.ErrorProcedure).HasColumnName("errorprocedure").HasMaxLength(126);
            entity.Property(e => e.ErrorTime).HasColumnName("errortime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UserName).HasColumnName("username").HasMaxLength(128).IsRequired();
            entity.Property(e => e.ErrorNumber).HasColumnName("errornumber");
            entity.Property(e => e.ErrorSeverity).HasColumnName("errorseverity");
            entity.Property(e => e.ErrorState).HasColumnName("errorstate");
            entity.Property(e => e.ErrorLine).HasColumnName("errorline");
        });

        // Product Entity Configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product", "saleslt");

            entity.HasKey(e => e.ProductId);

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("ak_product_name")
                .IsUnique();

            entity.HasIndex(e => e.ProductNumber)
                .HasDatabaseName("ak_product_productnumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_product_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.Color).HasColumnName("color").HasMaxLength(15);
            entity.Property(e => e.DiscontinuedDate).HasColumnName("discontinueddate");
            entity.Property(e => e.ListPrice).HasColumnName("listprice").HasColumnType("numeric(19,4)");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ProductCategoryId).HasColumnName("productcategoryid");
            entity.Property(e => e.ProductModelId).HasColumnName("productmodelid");
            entity.Property(e => e.ProductNumber).HasColumnName("productnumber").HasMaxLength(25).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.SellEndDate).HasColumnName("sellenddate");
            entity.Property(e => e.SellStartDate).HasColumnName("sellstartdate");
            entity.Property(e => e.Size).HasColumnName("size").HasMaxLength(5);
            entity.Property(e => e.StandardCost).HasColumnName("standardcost").HasColumnType("numeric(19,4)");
            entity.Property(e => e.ThumbnailPhotoFileName).HasColumnName("thumbnailphotofilename").HasMaxLength(50);
            entity.Property(e => e.ThumbNailPhoto).HasColumnName("thumbnailphoto");
            entity.Property(e => e.Weight).HasColumnName("weight").HasColumnType("numeric(8,2)");

            entity.HasOne(d => d.ProductCategory)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ProductCategoryId);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ProductModelId);
        });

        // ProductCategory Entity Configuration
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("productcategory", "saleslt");

            entity.HasKey(e => e.ProductCategoryId);

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("ak_productcategory_name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_productcategory_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductCategoryId).HasColumnName("productcategoryid");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ParentProductCategoryId).HasColumnName("parentproductcategoryid");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.ParentProductCategory)
                .WithMany(p => p.InverseParentProductCategory)
                .HasForeignKey(d => d.ParentProductCategoryId)
                .HasConstraintName("fk_productcategory_productcategory_parentproductcategoryid_productcategoryid");
        });

        // ProductDescription Entity Configuration
        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.ToTable("productdescription", "saleslt");

            entity.HasKey(e => e.ProductDescriptionId);

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_productdescription_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductDescriptionId).HasColumnName("productdescriptionid");
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(400).IsRequired();
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
        });

        // ProductModel Entity Configuration
        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("productmodel", "saleslt");

            entity.HasKey(e => e.ProductModelId);

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("ak_productmodel_name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_productmodel_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("productmodelid");
            entity.Property(e => e.CatalogDescription).HasColumnName("catalogdescription").HasColumnType("xml");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
        });

        // ProductModelProductDescription Entity Configuration
        modelBuilder.Entity<ProductModelProductDescription>(entity =>
        {
            entity.ToTable("productmodelproductdescription", "saleslt");

            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture });

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_productmodelproductdescription_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("productmodelid");
            entity.Property(e => e.ProductDescriptionId).HasColumnName("productdescriptionid");
            entity.Property(e => e.Culture).HasColumnName("culture").HasMaxLength(6).IsRequired();
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.ProductDescription)
                .WithMany(p => p.ProductModelProductDescription)
                .HasForeignKey(d => d.ProductDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.ProductModelProductDescription)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // SalesOrderDetail Entity Configuration
        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.ToTable("salesorderdetail", "saleslt");

            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId });

            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_salesorderdetail_rowguid")
                .IsUnique();

            entity.Property(e => e.SalesOrderId).HasColumnName("salesorderid");
            entity.Property(e => e.SalesOrderDetailId).HasColumnName("salesorderdetailid").ValueGeneratedOnAdd();
            entity.Property(e => e.LineTotal).HasColumnName("linetotal").HasColumnType("numeric(38,6)")
                .HasComputedColumnSql("((unitprice * ((1.0)::numeric - unitpricediscount)) * (orderqty)::numeric)", stored: true);
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.OrderQty).HasColumnName("orderqty");
            entity.Property(e => e.ProductId).HasColumnName("productid");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.UnitPrice).HasColumnName("unitprice").HasColumnType("numeric(19,4)");
            entity.Property(e => e.UnitPriceDiscount).HasColumnName("unitpricediscount").HasColumnType("numeric(19,4)");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.SalesOrderDetail)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.SalesOrderDetail)
                .HasForeignKey(d => d.SalesOrderId);
        });

        // SalesOrderHeader Entity Configuration
        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.ToTable("salesorderheader", "saleslt");

            entity.HasKey(e => e.SalesOrderId);

            entity.HasIndex(e => e.CustomerId);

            entity.HasIndex(e => e.Rowguid)
                .HasDatabaseName("ak_salesorderheader_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.SalesOrderNumber)
                .HasDatabaseName("ak_salesorderheader_salesordernumber")
                .IsUnique();

            entity.Property(e => e.SalesOrderId).HasColumnName("salesorderid");
            entity.Property(e => e.AccountNumber).HasColumnName("accountnumber").HasMaxLength(15);
            entity.Property(e => e.BillToAddressId).HasColumnName("billtoaddressid");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreditCardApprovalCode).HasColumnName("creditcardapprovalcode").HasMaxLength(15);
            entity.Property(e => e.CustomerId).HasColumnName("customerid");
            entity.Property(e => e.DueDate).HasColumnName("duedate");
            entity.Property(e => e.Freight).HasColumnName("freight").HasColumnType("numeric(19,4)").HasDefaultValueSql("0.00");
            entity.Property(e => e.ModifiedDate).HasColumnName("modifieddate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.OnlineOrderFlag).HasColumnName("onlineorderflag").HasDefaultValueSql("true");
            entity.Property(e => e.OrderDate).HasColumnName("orderdate").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.PurchaseOrderNumber).HasColumnName("purchaseordernumber").HasMaxLength(25);
            entity.Property(e => e.RevisionNumber).HasColumnName("revisionnumber");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid").HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.SalesOrderNumber).HasColumnName("salesordernumber").HasMaxLength(25).IsRequired();
            entity.Property(e => e.ShipDate).HasColumnName("shipdate");
            entity.Property(e => e.ShipMethod).HasColumnName("shipmethod").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ShipToAddressId).HasColumnName("shiptoaddressid");
            entity.Property(e => e.Status).HasColumnName("status").HasDefaultValueSql("1");
            entity.Property(e => e.SubTotal).HasColumnName("subtotal").HasColumnType("numeric(19,4)").HasDefaultValueSql("0.00");
            entity.Property(e => e.TaxAmt).HasColumnName("taxamt").HasColumnType("numeric(19,4)").HasDefaultValueSql("0.00");
            entity.Property(e => e.TotalDue).HasColumnName("totaldue").HasColumnType("numeric(19,4)")
                .HasComputedColumnSql("((subtotal + taxamt) + freight)", stored: true);

            entity.HasOne(d => d.BillToAddress)
                .WithMany(p => p.SalesOrderHeaderBillToAddress)
                .HasForeignKey(d => d.BillToAddressId)
                .HasConstraintName("fk_salesorderheader_address_billto_addressid");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.SalesOrderHeader)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipToAddress)
                .WithMany(p => p.SalesOrderHeaderShipToAddress)
                .HasForeignKey(d => d.ShipToAddressId)
                .HasConstraintName("fk_salesorderheader_address_shipto_addressid");
        });
    }
}