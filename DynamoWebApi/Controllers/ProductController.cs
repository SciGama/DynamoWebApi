using Amazon.DynamoDBv2.Model;
using Azure.Core;
using DynamoWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace DynamoWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    
    private CosmosClient _client;

    private Database _database;

    private Container _container;
    // GET
    public ProductController()
    {
        _client = new(
            "https://cosmosfirst.documents.azure.com:443/", 
            "Rje6puybZ3PoxjFJ7cLhsQ6qJS3y36zYBHYnO9MpN5HDhPptu9kaHH0LrEtPmvB3Eq3eW0ZfYARNAiLEIsL84Q==",
            new CosmosClientOptions()
        );
        
        _database = _client.GetDatabase(
            id: "adventureworks"
        );
        
        _container = _database.GetContainer("products");
    }
    
    [HttpGet(Name = "GetProducts")]
    public async Task<List<Product>> GetProducts()
    {
        QueryDefinition query = new QueryDefinition("select * from Products");

        using FeedIterator<Product> feed = _container.GetItemQueryIterator<Product>(
            queryDefinition: query
        );

        var products = new List<Product>();
        while (feed.HasMoreResults)
        {
            FeedResponse<Product> response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                Console.WriteLine($"Found item:\t{item.nameb}");
                products.Add(item);
            }
        }

        return products;
    }
    
    [HttpPost]
    public async Task PostProduct(Product product)
    {
        Guid g = Guid.NewGuid();
        Product newItem = new(
            id: g.ToString(),
            category: g.ToString(),
            nameb: product.nameb,
            quantity: product.quantity,
            sale: product.sale
        );

        Product createdItem = await _container.CreateItemAsync(newItem, new PartitionKey(newItem.id));
        
    }
}