using Azure;
using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Bind configuration
var azureConfig = builder.Configuration.GetSection("AzureTable");
builder.Services.Configure<AzureTableOptions>(azureConfig);

// Register TableServiceClient or TableClient factory
builder.Services.AddSingleton<TableClient>(sp =>
{
    var cfg = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<AzureTableOptions>>().Value;
    string tableName = cfg.TableName ?? "cosmicworks-products";
    if (cfg.UseConnectionString && !string.IsNullOrEmpty(cfg.StorageConnectionString))
    {
        var serviceClient = new TableServiceClient(cfg.StorageConnectionString);
        var client = serviceClient.GetTableClient(tableName);
        client.CreateIfNotExists();
        return client;
    }
    else if (!string.IsNullOrEmpty(cfg.TableServiceEndpoint))
    {
        var serviceClient = new TableServiceClient(new Uri(cfg.TableServiceEndpoint), new DefaultAzureCredential());
        var client = serviceClient.GetTableClient(tableName);
        client.CreateIfNotExists();
        return client;
    }
    else
    {
        throw new InvalidOperationException("No valid Azure Table configuration provided.");
    }
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Table}/{action=Index}/{id?}"
);

app.Run();

public record AzureTableOptions
{
    public bool UseConnectionString { get; init; } = true;
    public string? StorageConnectionString { get; init; }
    public string? TableServiceEndpoint { get; init; }
    public string? TableName { get; init; } = "cosmicworks-products";
}
