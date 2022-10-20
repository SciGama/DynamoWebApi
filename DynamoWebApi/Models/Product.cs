using Newtonsoft.Json;

namespace DynamoWebApi.Models;

public record Product(
    [JsonProperty(PropertyName = "id")]
    string id,
    
    [JsonProperty(PropertyName = "category")]
    string category,
    
    [JsonProperty(PropertyName = "nameb")]
    string nameb,
    
    [JsonProperty(PropertyName = "quantity")]
    int quantity,
    
    [JsonProperty(PropertyName = "sale")]
    bool sale
);