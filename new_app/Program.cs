using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;
using SoapCore;
using AdventureWorks.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services (formerly ConfigureServices in Startup.cs)
builder.Services.AddControllersWithViews();

// Database Context Configuration
builder.Services.AddDbContext<sampledbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sampledbContext")));

// Cookie Policy Configuration
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// SOAP Services Configuration
// Register services that will be exposed as SOAP endpoints
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddSoapCore();

var app = builder.Build();

// Configure middleware pipeline (formerly Configure in Startup.cs)
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

// MVC Route Configuration (from legacy Startup.cs)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductCategories}/{action=Index}/{id?}");

// SOAP Endpoint Configuration (NEW for .NET 8)
app.UseSoapEndpoint<ICustomerService>("/Services/CustomerService.asmx", new SoapEncoderOptions
{
    MessageVersion = System.ServiceModel.Channels.MessageVersion.Soap11,
    WriteEncoding = System.Text.Encoding.UTF8
});

app.Run();
