using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using ToolShopAPI.Models;

namespace ToolShopAPI.Services;

public class ToolsService
{
    private readonly IMongoCollection<Tool> _toolsCollection;

    public ToolsService(
        IOptions<ToolShopDatabaseSettings> toolShopDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb+srv://elias84631:j9BA8ZkmQu4FEBj9@toolusageapp.yz8x2.mongodb.net/?retryWrites=true&w=majority&appName=ToolUsageApp");

        var mongoDatabase = mongoClient.GetDatabase("ToolShop");

        _toolsCollection = mongoDatabase.GetCollection<Tool>("Tools");
    }

    public async Task<List<Tool>> GetTools() =>
        await _toolsCollection.Find(_ => true).ToListAsync();

    public async Task<Tool?> GetTool(string id) =>
        await _toolsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateTool(Tool newTool) =>
        await _toolsCollection.InsertOneAsync(newTool);

    public async Task UpdateTool(string id, Tool updatedTool) =>
        await _toolsCollection.ReplaceOneAsync(x => x.Id == id, updatedTool);

    public async Task RemoveTool(string id) =>
        await _toolsCollection.DeleteOneAsync(x => x.Id == id);
}