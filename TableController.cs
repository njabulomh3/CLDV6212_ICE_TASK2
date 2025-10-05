using Azure;
using Azure.Data.Tables;
using AzureTableMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureTableMvc.Controllers;

public class TableController : Controller
{
    private readonly TableClient _tableClient;

    public TableController(TableClient tableClient)
    {
        _tableClient = tableClient;
    }

    public async Task<IActionResult> Index()
    {
        var results = new List<Product>();
        await foreach (Product p in _tableClient.QueryAsync<Product>())
        {
            results.Add(p);
        }
        return View(results);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model)
    {
        if (!ModelState.IsValid) return View(model);

        // Use category as PartitionKey and a GUID as RowKey
        model.PartitionKey ??= "default-partition";
        model.RowKey = Guid.NewGuid().ToString();

        await _tableClient.UpsertEntityAsync<Product>(model, TableUpdateMode.Replace);

        return RedirectToAction(nameof(Index));
    }
}
