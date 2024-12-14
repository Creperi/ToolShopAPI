using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolShopAPI.Models;
public class Tool
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime AdditionDate { get; set; }
    public byte[] ImagePath { get; set; }
    public string BrandName { get; private set; }
    [BsonElement("DateAdded")]
    private DateTime DateAdded { get; set; }
    public void DeleteTools(List<Tool> tools)
    {
        tools.Clear();
    }
    public void setDateAdded()
    {
        DateAdded = DateTime.Now;
    }

    public Tool(string name, string brandName, byte[] imagePath)
    {
        Name = name;
        BrandName = brandName;
        ImagePath = imagePath;
        AdditionDate = DateTime.UtcNow;
    }

}