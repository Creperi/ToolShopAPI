namespace ToolShopAPI.Models;

public class ToolShopDatabaseSettings
{
    public string DbConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ToolsCollectionName { get; set; } = null!;
    public string BrandsCollectionName { get; set; } = null!;
    public string PersonnelCollectionName { get; set; } = null!;
}