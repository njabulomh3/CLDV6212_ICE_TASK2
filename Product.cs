using Azure;
using Azure.Data.Tables;
using System;

namespace AzureTableMvc.Models;

public record Product : ITableEntity
{
    // ITableEntity requires PartitionKey, RowKey, ETag, Timestamp
    public required string PartitionKey { get; set; }
    public required string RowKey { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool Clearance { get; set; }

    public ETag ETag { get; set; } = ETag.All;
    public DateTimeOffset? Timestamp { get; set; }
}
