# Azure Table Storage MVC Sample

This repository contains a minimal ASP.NET Core MVC application demonstrating how to write and read entities to Azure Table Storage using the `Azure.Data.Tables` SDK.

## Contents
- `AzureTableMvc.csproj` - project file (target net8.0)
- `Program.cs` - app startup and DI
- `appsettings.json` - configuration (put your Azure Table endpoint or connection string here)
- `Controllers/TableController.cs` - simple controller to create, list entities
- `Models/Product.cs` - example entity implementing `ITableEntity`
- `Views/Table/Index.cshtml` - view to list products
- `Views/Table/Create.cshtml` - small form to create a product

## Prerequisites
- .NET 8 SDK (or change TargetFramework in csproj to your installed SDK version)
- Azure.Data.Tables NuGet package
- An Azure Table endpoint (Azure Storage account table service endpoint) or Azure Cosmos DB Table endpoint and proper credentials

## How to run
1. Edit `appsettings.json`:
   - Either set `UseConnectionString` to true and fill `StorageConnectionString`, or
   - Set `UseConnectionString` to false and ensure `TableServiceEndpoint` is set and your environment supports `DefaultAzureCredential` (e.g., VS sign-in or managed identity).
2. From a terminal in project folder:
   ```
   dotnet restore
   dotnet run
   ```
3. Open `https://localhost:7180/Table` (port may vary) to view the sample.

## Notes
- This sample is intentionally minimal for learning. In production, do not store connection strings in plain text.
- The sample uses `Azure.Data.Tables` and demonstrates `TableServiceClient`, `TableClient`, `UpsertEntityAsync`, and `QueryAsync`.
