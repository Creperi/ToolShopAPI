using ToolShopAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ToolShopAPI.Services;
public class PersonnelService
{
    private readonly IMongoCollection<Personnel> _personnelCollection;

    public PersonnelService(
        IOptions<ToolShopDatabaseSettings> toolShopDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb+srv://elias84631:j9BA8ZkmQu4FEBj9@toolusageapp.yz8x2.mongodb.net/?retryWrites=true&w=majority&appName=ToolUsageApp");

        var mongoDatabase = mongoClient.GetDatabase("ToolShop");

        _personnelCollection = mongoDatabase.GetCollection<Personnel>("Personnel");
    }

    public async Task<List<Personnel>> GetPersonnelList() =>
        await _personnelCollection.Find(_ => true).ToListAsync();

    public async Task<Personnel?> GetPersonnel(string id) =>
        await _personnelCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreatePersonnel(Personnel newPersonnel) =>
        await _personnelCollection.InsertOneAsync(newPersonnel);

    public async Task UpdatePersonnel(string id, Personnel updatedPeronnel) =>
        await _personnelCollection.ReplaceOneAsync(x => x.Id == id, updatedPeronnel);

    public async Task RemovePersonnel(string id) =>
        await _personnelCollection.DeleteOneAsync(x => x.Id == id);
}