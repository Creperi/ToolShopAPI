using ToolShopAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ToolShopAPI.Services;
public class BrandService   
{
    private readonly IMongoCollection<Brand> _brandsCollection;

    public BrandService(
        IOptions<ToolShopDatabaseSettings> toolShopDatabaseSettings)
    {
        var mongoClient = new MongoClient("mongodb+srv://elias84631:j9BA8ZkmQu4FEBj9@toolusageapp.yz8x2.mongodb.net/?retryWrites=true&w=majority&appName=ToolUsageApp");
        //"mongodb+srv://elias84631:j9BA8ZkmQu4FEBj9@toolusageapp.yz8x2.mongodb.net/?retryWrites=true&w=majority&appName=ToolUsageApp"

        var mongoDatabase = mongoClient.GetDatabase("ToolShop");

        _brandsCollection = mongoDatabase.GetCollection<Brand>("Brands");
    }

    public async Task<List<Brand>> GetBrands() =>
        
        await _brandsCollection.Find(_ => true).ToListAsync();

    public async Task<Brand?> GetBrand(string id) =>
        await _brandsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateBrand(Brand newBrand) =>
        await _brandsCollection.InsertOneAsync(newBrand);

    public async Task UpdateBrand(string id, Brand updatedBrand) =>
        await _brandsCollection.ReplaceOneAsync(x => x.Id == id, updatedBrand);

    public async Task RemoveBrand(string id) =>
        await _brandsCollection.DeleteOneAsync(x => x.Id == id);
}