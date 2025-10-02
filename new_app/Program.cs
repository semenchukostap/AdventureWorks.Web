using AdventureWorks.Web.Models;
using AdventureWorks.Web.Services;
using AdventureWorks.Web.ServiceImplementations;
using Microsoft.EntityFrameworkCore;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Database Context for PostgreSQL
builder.Services.AddDbContext<SampleDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SampleDbContext")));

// Register SOAP Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

// Add SoapCore services
builder.Services.AddSoapCore();

// Add Controllers (for potential REST API endpoints if needed later)
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Configure SOAP Endpoints
app.UseEndpoints(endpoints =>
{
    // Customer SOAP Service
    endpoints.UseSoapEndpoint<ICustomerService>(
        path: "/CustomerService.asmx",
        binding: new System.ServiceModel.BasicHttpBinding(),
        serializer: SoapSerializer.XmlSerializer);

    // Product SOAP Service
    endpoints.UseSoapEndpoint<IProductService>(
        path: "/ProductService.asmx",
        binding: new System.ServiceModel.BasicHttpBinding(),
        serializer: SoapSerializer.XmlSerializer);

    // ProductCategory SOAP Service
    endpoints.UseSoapEndpoint<IProductCategoryService>(
        path: "/ProductCategoryService.asmx",
        binding: new System.ServiceModel.BasicHttpBinding(),
        serializer: SoapSerializer.XmlSerializer);
});

app.Run();
