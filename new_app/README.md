# AdventureWorks SOAP Web Services - .NET 8

## Overview

This project represents a complete migration from **ASP.NET Core 2.1 MVC** to a modern **.NET 8 SOAP Web Services** application. The legacy AdventureWorks Web App, which was built using Model-View-Controller architecture with Razor views, has been transformed into a pure SOAP-based web service implementation using SoapCore middleware.

The migration includes:
- Framework upgrade from .NET Core 2.1 to .NET 8
- Architecture transformation from MVC to SOAP Web Services
- Database migration from SQL Server to PostgreSQL
- Modernized hosting model using minimal APIs
- Complete removal of UI layer in favor of service-oriented architecture

---

## Technology Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| **.NET** | 8.0 | Target framework (LTS release) |
| **SoapCore** | 1.1.0+ | SOAP protocol implementation for ASP.NET Core |
| **Entity Framework Core** | 8.0.x | Object-Relational Mapping (ORM) |
| **PostgreSQL** | Latest | Primary database |
| **Npgsql** | 8.0.x | PostgreSQL provider for EF Core |
| **System.ServiceModel** | 8.0.x | WCF primitives for SOAP contracts |

---

## Prerequisites

Before running this application, ensure you have the following installed:

### Required Software
- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **PostgreSQL Database** - Access to the AdventureWorks PostgreSQL instance
- **SOAP Testing Tools** (any of the following):
  - [SoapUI](https://www.soapui.org/) - Recommended for comprehensive SOAP testing
  - [Postman](https://www.postman.com/) - Supports SOAP requests
  - [Curl](https://curl.se/) - Command-line SOAP requests

### Development Tools (Optional)
- **Visual Studio 2022** (v17.8+) or **Visual Studio Code**
- **pgAdmin** or **DBeaver** - For PostgreSQL database management
- **Entity Framework Core Tools** - Install globally:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## Configuration

### Database Connection String

The application connects to a PostgreSQL database hosted on AWS RDS. Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "AdventureWorksContext": "Host=adventureworks2022lt.cluster-cxi86kcku4iz.us-east-1.rds.amazonaws.com;Port=5432;Database=postgres;Username=postgres;Password=postgres;SearchPath=saleslt"
  }
}
```

**Important Configuration Notes:**
- `SearchPath=saleslt` - Specifies the PostgreSQL schema containing AdventureWorks tables
- All table and column names use lowercase convention per PostgreSQL best practices
- The database uses the `saleslt` schema (lowercase) instead of `SalesLT` from SQL Server

### Environment-Specific Settings

#### Development (appsettings.Development.json)
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

#### Production
For production deployments:
1. Use environment variables for sensitive data
2. Store connection strings in Azure Key Vault or AWS Secrets Manager
3. Set `ASPNETCORE_ENVIRONMENT=Production`

---

## Running the Application

### Development Mode

```bash
# Navigate to the project directory
cd new_app

# Restore NuGet packages
dotnet restore

# Run the application
dotnet run
```

The application will start on:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`

### Production Build

```bash
# Publish the application
dotnet publish -c Release -o ./publish

# Navigate to publish directory
cd publish

# Run the published application
dotnet AdventureWorks.Soap.dll
```

### Docker Deployment (Optional)

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AdventureWorks.Soap.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AdventureWorks.Soap.dll"]
```

---

## Available SOAP Services

### 1. ProductService

**Endpoint**: `https://localhost:5001/ProductService.asmx`  
**WSDL**: `https://localhost:5001/ProductService.asmx?wsdl`

#### Operations

| Operation | Description | Parameters | Returns |
|-----------|-------------|------------|---------|
| `GetAllProductsAsync` | Retrieve all products | None | `List<Product>` |
| `GetProductByIdAsync` | Get single product by ID | `int id` | `Product` (nullable) |
| `CreateProductAsync` | Create new product | `Product product` | `Product` |
| `UpdateProductAsync` | Update existing product | `int id, Product product` | `bool` |
| `DeleteProductAsync` | Delete product by ID | `int id` | `bool` |

**Sample SOAP Request (GetAllProductsAsync)**:
```xml
POST /ProductService.asmx HTTP/1.1
Host: localhost:5001
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://tempuri.org/IProductService/GetAllProductsAsync"

<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAllProductsAsync xmlns="http://tempuri.org/" />
  </soap:Body>
</soap:Envelope>
```

---

### 2. CustomerService

**Endpoint**: `https://localhost:5001/CustomerService.asmx`  
**WSDL**: `https://localhost:5001/CustomerService.asmx?wsdl`

#### Operations

| Operation | Description | Parameters | Returns |
|-----------|-------------|------------|---------|
| `GetAllCustomersAsync` | Retrieve all customers | None | `List<Customer>` |
| `GetCustomerByIdAsync` | Get single customer by ID | `int id` | `Customer` (nullable) |
| `CreateCustomerAsync` | Create new customer | `Customer customer` | `Customer` |
| `UpdateCustomerAsync` | Update existing customer | `int id, Customer customer` | `bool` |
| `DeleteCustomerAsync` | Delete customer by ID | `int id` | `bool` |

**Sample SOAP Request (GetCustomerByIdAsync)**:
```xml
POST /CustomerService.asmx HTTP/1.1
Host: localhost:5001
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://tempuri.org/ICustomerService/GetCustomerByIdAsync"

<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetCustomerByIdAsync xmlns="http://tempuri.org/">
      <id>1</id>
    </GetCustomerByIdAsync>
  </soap:Body>
</soap:Envelope>
```

---

### 3. ProductCategoryService

**Endpoint**: `https://localhost:5001/ProductCategoryService.asmx`  
**WSDL**: `https://localhost:5001/ProductCategoryService.asmx?wsdl`

#### Operations

| Operation | Description | Parameters | Returns |
|-----------|-------------|------------|---------|
| `GetAllProductCategoriesAsync` | Retrieve all categories | None | `List<ProductCategory>` |
| `GetProductCategoryByIdAsync` | Get single category by ID | `int id` | `ProductCategory` (nullable) |
| `CreateProductCategoryAsync` | Create new category | `ProductCategory productCategory` | `ProductCategory` |
| `UpdateProductCategoryAsync` | Update existing category | `int id, ProductCategory productCategory` | `bool` |
| `DeleteProductCategoryAsync` | Delete category by ID | `int id` | `bool` |

**Sample SOAP Request (CreateProductCategoryAsync)**:
```xml
POST /ProductCategoryService.asmx HTTP/1.1
Host: localhost:5001
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://tempuri.org/IProductCategoryService/CreateProductCategoryAsync"

<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <CreateProductCategoryAsync xmlns="http://tempuri.org/">
      <productCategory>
        <Name>Bikes</Name>
        <ParentProductCategoryId>1</ParentProductCategoryId>
      </productCategory>
    </CreateProductCategoryAsync>
  </soap:Body>
</soap:Envelope>
```

---

## Database Migrations

### Create a New Migration

```bash
# Navigate to project directory
cd new_app

# Create migration
dotnet ef migrations add <MigrationName> --context AdventureWorksContext

# Example:
dotnet ef migrations add InitialCreate --context AdventureWorksContext
```

### Apply Migrations to Database

```bash
# Update database to latest migration
dotnet ef database update --context AdventureWorksContext

# Update to specific migration
dotnet ef database update <MigrationName> --context AdventureWorksContext

# Rollback to previous migration
dotnet ef database update <PreviousMigrationName> --context AdventureWorksContext
```

### Remove Last Migration

```bash
dotnet ef migrations remove --context AdventureWorksContext
```

### Generate SQL Script

```bash
# Generate SQL for all migrations
dotnet ef migrations script --context AdventureWorksContext

# Generate SQL for specific migration range
dotnet ef migrations script <FromMigration> <ToMigration> --context AdventureWorksContext
```

**Migration Notes**:
- All migrations target PostgreSQL database
- Migrations use `__efmigrationshistory` table in `saleslt` schema
- Table and column names are lowercase per PostgreSQL conventions

---

## Testing with SoapUI

### Step 1: Import WSDL

1. Open SoapUI
2. Click **File** → **New SOAP Project**
3. Enter Project Name: `AdventureWorks SOAP Services`
4. Enter Initial WSDL: `https://localhost:5001/ProductService.asmx?wsdl`
5. Click **OK**

SoapUI will automatically:
- Parse the WSDL
- Generate sample requests for all operations
- Create test cases

### Step 2: Configure SSL Certificate (Development)

For development with self-signed certificates:

1. Go to **File** → **Preferences** → **SSL Settings**
2. Disable SSL certificate validation (development only)

### Step 3: Execute Operations

1. Expand service → Expand operation (e.g., `GetAllProductsAsync`)
2. Double-click **Request 1**
3. Click green play button to send request
4. View response in right panel

### Step 4: Test All Services

Repeat import process for:
- `https://localhost:5001/CustomerService.asmx?wsdl`
- `https://localhost:5001/ProductCategoryService.asmx?wsdl`

### Step 5: Automated Testing (Optional)

Create test suites in SoapUI:
1. Right-click project → **Generate TestSuite**
2. Select operations to test
3. Run entire test suite with one click

---

## Project Structure

```
new_app/
├── Data/
│   └── AdventureWorksContext.cs          # EF Core DbContext with PostgreSQL config
│
├── Models/                                # Entity models (all with [DataContract])
│   ├── Address.cs
│   ├── BuildVersion.cs
│   ├── Customer.cs
│   ├── CustomerAddress.cs
│   ├── ErrorLog.cs
│   ├── Product.cs
│   ├── ProductCategory.cs
│   ├── ProductDescription.cs
│   ├── ProductModel.cs
│   ├── ProductModelProductDescription.cs
│   ├── SalesOrderDetail.cs
│   └── SalesOrderHeader.cs
│
├── Services/
│   ├── Contracts/                         # SOAP service interfaces
│   │   ├── IProductService.cs            # [ServiceContract] interface
│   │   ├── ICustomerService.cs
│   │   └── IProductCategoryService.cs
│   │
│   ├── ProductService.cs                  # Service implementations
│   ├── CustomerService.cs
│   └── ProductCategoryService.cs
│
├── Migrations/                            # EF Core migrations (auto-generated)
│   ├── <timestamp>_InitialCreate.cs
│   ├── <timestamp>_InitialCreate.Designer.cs
│   └── AdventureWorksContextModelSnapshot.cs
│
├── Properties/
│   └── launchSettings.json               # Launch profiles and URLs
│
├── appsettings.json                       # Application configuration
├── appsettings.Development.json          # Development-specific config
├── Program.cs                             # Application entry point (minimal hosting)
├── AdventureWorks.Soap.csproj            # Project file (.NET 8 SDK-style)
└── README.md                              # This file
```

### Key Files Explained

| File | Purpose |
|------|---------|
| `Program.cs` | Application startup, dependency injection, middleware configuration, SOAP endpoint registration |
| `AdventureWorksContext.cs` | EF Core database context with entity configurations for PostgreSQL |
| `IProductService.cs` | SOAP service contract with [ServiceContract] and [OperationContract] attributes |
| `ProductService.cs` | Business logic implementation for Product operations |
| `appsettings.json` | Connection strings, logging configuration, application settings |

---

## Migration Notes

### Changes from Legacy Application

This project represents a complete modernization from the legacy ASP.NET Core 2.1 MVC application:

#### 1. Framework Upgrade
- **From**: .NET Core 2.1 (End of Life since August 21, 2021)
- **To**: .NET 8.0 (LTS release, supported until November 2026)
- **Benefits**: 
  - Improved performance (up to 30% faster)
  - Enhanced security features
  - Modern C# 12 language features
  - Native AOT support
  - Better cloud-native capabilities

#### 2. Architecture Change: MVC → SOAP Web Services
- **Removed**: 
  - All MVC Controllers (`HomeController`, `ProductsController`, etc.)
  - All Razor Views (`Views/` folder)
  - Client-side assets (`wwwroot/` folder)
  - View models and ViewBag/ViewData patterns
  
- **Added**:
  - SOAP service contracts with `[ServiceContract]` attributes
  - SOAP service implementations with `[OperationContract]` methods
  - SoapCore middleware for SOAP protocol handling
  - WSDL auto-generation
  - DataContract serialization

#### 3. Database Migration: SQL Server → PostgreSQL
- **From**: SQL Server with `SalesLT` schema (mixed case)
- **To**: PostgreSQL with `saleslt` schema (lowercase)
- **Schema Changes**:
  - All table names converted to lowercase (`Product` → `product`)
  - All column names converted to lowercase (`ProductID` → `productid`)
  - Connection string changed from SQL Server format to PostgreSQL
  - Schema explicitly set to `saleslt` in all queries

#### 4. Hosting Model: Startup.cs → Program.cs
- **From**: Separate `Startup.cs` with `ConfigureServices` and `Configure` methods
- **To**: Unified `Program.cs` using minimal hosting model
- **Changes**:
  - `IWebHostBuilder` replaced with `WebApplicationBuilder`
  - Explicit service registration via `builder.Services`
  - Middleware pipeline via `app.Use*` methods
  - Top-level statements (no `Main` method)

#### 5. UI Removal
- **Removed Components**:
  - All Razor views and layouts
  - Bootstrap, jQuery, and client-side libraries
  - CSS and JavaScript files
  - Cookie consent and privacy pages
  - HTML forms and validation
  
- **Impact**: Application is now headless - accessible only via SOAP clients

#### 6. Configuration Changes
- `Startup.cs` → Consolidated into `Program.cs`
- `web.config` → Not needed (removed)
- `AssemblyInfo.cs` → Not needed in SDK-style projects
- MVC-specific middleware removed (UseStaticFiles, UseRouting for MVC)

---

## Breaking Changes

### ⚠️ Important: Client Applications Must Be Updated

The following breaking changes require updates to any applications consuming AdventureWorks services:

#### 1. HTTP Endpoints Replaced with SOAP Operations

| Legacy (MVC) | New (SOAP) | Change Required |
|--------------|------------|-----------------|
| `GET /Products` | `POST /ProductService.asmx` with `GetAllProductsAsync` | Change from HTTP GET to SOAP POST with XML envelope |
| `GET /Products/Details/5` | `POST /ProductService.asmx` with `GetProductByIdAsync(5)` | Change from route parameter to SOAP parameter |
| `POST /Products/Create` | `POST /ProductService.asmx` with `CreateProductAsync` | Change from form POST to SOAP operation |
| `POST /Products/Edit/5` | `POST /ProductService.asmx` with `UpdateProductAsync(5, product)` | Change from form POST to SOAP operation |
| `POST /Products/Delete/5` | `POST /ProductService.asmx` with `DeleteProductAsync(5)` | Change from form POST to SOAP operation |

**Action Required**: Update all HTTP clients to use SOAP protocol with proper XML envelopes.

#### 2. Response Format: HTML/JSON → SOAP XML

- **Legacy**: HTML views or JSON responses
- **New**: SOAP XML responses only
- **Action Required**: Update response parsers to handle SOAP XML format

#### 3. Browser UI Removed

- **Legacy**: Full web interface accessible in browser
- **New**: No browser UI - SOAP services only
- **Action Required**: 
  - Use SOAP client tools (SoapUI, Postman)
  - Or build custom UI application that consumes SOAP services

#### 4. Database Schema Changes

- **Table Names**: `Product` → `product`, `Customer` → `customer`
- **Column Names**: `ProductID` → `productid`, `FirstName` → `firstname`
- **Schema**: `SalesLT` → `saleslt`
- **Action Required**: If accessing database directly, update all queries to use lowercase identifiers

#### 5. Connection String Format

**Legacy (SQL Server)**:
```
Server=tcp:server.database.windows.net,1433;Database=AdventureWorks;
```

**New (PostgreSQL)**:
```
Host=server.postgres.database.azure.com;Port=5432;Database=postgres;SearchPath=saleslt
```

**Action Required**: Update connection strings in all configuration files and deployment scripts.

#### 6. Authentication Changes (If Applicable)

- **Legacy**: Cookie-based authentication with MVC
- **New**: Must implement WS-Security or HTTP Basic Auth for SOAP
- **Action Required**: Add authentication headers to SOAP requests

---

## Troubleshooting

### Database Connection Issues

#### Problem: Cannot connect to PostgreSQL database

**Symptoms**:
```
Npgsql.NpgsqlException: Failed to connect to [host]:5432
```

**Solutions**:
1. **Verify PostgreSQL server is running**:
   ```bash
   # Check if server is accessible
   telnet adventureworks2022lt.cluster-cxi86kcku4iz.us-east-1.rds.amazonaws.com 5432
   ```

2. **Check connection string in `appsettings.json`**:
   - Verify host, port, database name
   - Ensure `SearchPath=saleslt` is included
   - Validate username and password

3. **Verify schema exists**:
   ```sql
   SELECT schema_name FROM information_schema.schemata WHERE schema_name = 'saleslt';
   ```

4. **Check firewall rules**:
   - Ensure port 5432 is open
   - Verify security group rules (if using AWS RDS)

5. **Test connection with pgAdmin or psql**:
   ```bash
   psql -h adventureworks2022lt.cluster-cxi86kcku4iz.us-east-1.rds.amazonaws.com -p 5432 -U postgres -d postgres
   ```

#### Problem: Tables not found

**Symptoms**:
```
Npgsql.NpgsqlException: relation "product" does not exist
```

**Solutions**:
1. **Verify schema in connection string**:
   ```json
   "SearchPath=saleslt"
   ```

2. **Run migrations**:
   ```bash
   dotnet ef database update --context AdventureWorksContext
   ```

3. **Check if tables exist in correct schema**:
   ```sql
   SELECT table_schema, table_name FROM information_schema.tables 
   WHERE table_schema = 'saleslt';
   ```

---

### SOAP Client Errors

#### Problem: SOAP request returns 400 Bad Request

**Symptoms**:
```
HTTP/1.1 400 Bad Request
```

**Solutions**:
1. **Verify Content-Type header**:
   ```
   Content-Type: text/xml; charset=utf-8
   ```

2. **Check SOAPAction header matches operation**:
   ```
   SOAPAction: "http://tempuri.org/IProductService/GetAllProductsAsync"
   ```

3. **Validate XML structure**:
   - Ensure proper SOAP envelope
   - Check namespace declarations
   - Verify operation name spelling

4. **Use WSDL to generate request**:
   - Import WSDL into SoapUI
   - Use auto-generated request templates

#### Problem: SOAP request returns 404 Not Found

**Symptoms**:
```
HTTP/1.1 404 Not Found
```

**Solutions**:
1. **Verify endpoint URL**:
   - Correct: `https://localhost:5001/ProductService.asmx`
   - Incorrect: `https://localhost:5001/api/ProductService`

2. **Check application is running**:
   ```bash
   dotnet run
   ```

3. **Verify WSDL is accessible**:
   ```
   https://localhost:5001/ProductService.asmx?wsdl
   ```

#### Problem: SSL/TLS certificate errors

**Symptoms**:
```
SSL certificate problem: self signed certificate
```

**Solutions**:
1. **Development**: Disable certificate validation in SOAP client
   - SoapUI: Preferences → SSL Settings → Disable validation
   - Postman: Settings → General → SSL certificate verification OFF

2. **Production**: Install valid SSL certificate
   ```bash
   # Example with Let's Encrypt
   sudo certbot --nginx -d yourdomain.com
   ```

3. **Trust development certificate**:
   ```bash
   dotnet dev-certs https --trust
   ```

#### Problem: Deserialization errors

**Symptoms**:
```
Cannot deserialize the current JSON object
```

**Solutions**:
1. **Ensure DataContract attributes on models**:
   ```csharp
   [DataContract]
   public class Product { }
   ```

2. **Check DataMember attributes on properties**:
   ```csharp
   [DataMember]
   public int ProductId { get; set; }
   ```

3. **Use DataContractSerializer in SoapCore**:
   ```csharp
   endpoints.UseSoapEndpoint<IProductService>(
       "/ProductService.asmx", 
       new SoapEncoderOptions(), 
       SoapSerializer.DataContractSerializer
   );
   ```

---

### Performance Issues

#### Problem: Slow SOAP responses

**Solutions**:
1. **Enable response caching**:
   ```csharp
   builder.Services.AddResponseCaching();
   app.UseResponseCaching();
   ```

2. **Optimize database queries**:
   ```csharp
   // Use AsNoTracking for read-only queries
   await _context.Products.AsNoTracking().ToListAsync();
   ```

3. **Add database indexes**:
   ```sql
   CREATE INDEX idx_product_name ON saleslt.product(name);
   ```

4. **Enable connection pooling** (already configured in Npgsql by default)

---

## License

This project is licensed under the **MIT License** - inherited from the source project.

```
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## Additional Resources

### Documentation
- [.NET 8 Documentation](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [SoapCore GitHub Repository](https://github.com/DigDes/SoapCore)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Npgsql Documentation](https://www.npgsql.org/doc/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)

### Migration Guides
- [Migrate from .NET Core 2.1 to 8.0](https://docs.microsoft.com/en-us/aspnet/core/migration/21-to-60)
- [WCF to SoapCore Migration](https://github.com/DigDes/SoapCore/wiki/Migration-from-WCF)
- [SQL Server to PostgreSQL Migration](https://www.postgresql.org/docs/current/sql-syntax.html)

### Community
- [Stack Overflow - SoapCore Tag](https://stackoverflow.com/questions/tagged/soapcore)
- [.NET Discord Community](https://aka.ms/dotnet-discord)
- [PostgreSQL Mailing Lists](https://www.postgresql.org/list/)

---

## Support

For issues, questions, or contributions related to this migration:

1. **Check existing documentation** in this README
2. **Review troubleshooting section** above
3. **Consult official documentation** linked in Additional Resources
4. **Open an issue** in the project repository (if applicable)

---

## Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | 2024 | Initial migration from ASP.NET Core 2.1 MVC to .NET 8 SOAP Web Services |

---

**Last Updated**: 2024  
**Migrated From**: AdventureWorks.Web (ASP.NET Core 2.1 MVC)  
**Target Framework**: .NET 8.0
