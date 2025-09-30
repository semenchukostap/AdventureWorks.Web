using AdventureWorks.Web.Models;
using AdventureWorks.Web.Services;
using Microsoft.EntityFrameworkCore;
using SoapCore;
using System.ServiceModel;

// This is a new comment to verify we can access and modify this file
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add database context
builder.Services.AddDbContext<sampledbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sampledbContext")));

// Configure cookie policy
builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Add SOAP Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddSoapCore();

// Enable detailed errors as in the legacy application
builder.WebHost.UseSetting("detailedErrors", "true");
builder.WebHost.CaptureStartupErrors(true);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthorization();

// Configure SOAP endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IProductService>("/soap/ProductService.svc", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
    endpoints.UseSoapEndpoint<ICustomerService>("/soap/CustomerService.svc", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=ProductCategories}/{action=Index}/{id?}");
});

app.Run();