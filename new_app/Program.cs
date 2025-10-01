using Microsoft.EntityFrameworkCore;
using SoapCore;
using System.ServiceModel.Channels;
using System.Text;
using AdventureWorks.Soap.Data;
using AdventureWorks.Soap.Services;
using AdventureWorks.Soap.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Configure Database Context with PostgreSQL
builder.Services.AddDbContext<AdventureWorksContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("AdventureWorksContext"),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__efmigrationshistory", "saleslt")
    )
);

// Register SOAP Services (Scoped lifetime for database context compatibility)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

// Add SoapCore services
builder.Services.AddSoapCore();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Configure SOAP endpoints with explicit routing
app.UseEndpoints(endpoints =>
{
    // ProductService SOAP endpoint
    endpoints.UseSoapEndpoint<IProductService>(
        "/ProductService.asmx",
        new SoapEncoderOptions
        {
            MessageVersion = MessageVersion.Soap11,
            WriteEncoding = Encoding.UTF8
        },
        SoapSerializer.DataContractSerializer
    );

    // CustomerService SOAP endpoint
    endpoints.UseSoapEndpoint<ICustomerService>(
        "/CustomerService.asmx",
        new SoapEncoderOptions
        {
            MessageVersion = MessageVersion.Soap11,
            WriteEncoding = Encoding.UTF8
        },
        SoapSerializer.DataContractSerializer
    );

    // ProductCategoryService SOAP endpoint
    endpoints.UseSoapEndpoint<IProductCategoryService>(
        "/ProductCategoryService.asmx",
        new SoapEncoderOptions
        {
            MessageVersion = MessageVersion.Soap11,
            WriteEncoding = Encoding.UTF8
        },
        SoapSerializer.DataContractSerializer
    );
});

app.Run();
