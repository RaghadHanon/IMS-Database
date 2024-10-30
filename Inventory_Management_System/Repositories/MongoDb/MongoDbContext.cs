using Inventory_Management_System.Configurations;
using Inventory_Management_System.ProductManagement;
using MongoDB.Driver;

namespace Inventory_Management_System.Repository.MongoDbRepository;
public class MongoDbContext
{
    private IConfiguration _databaseConfiguration;
    public MongoDbContext(IConfiguration dataBaseConfiguration)
    {
        _databaseConfiguration = dataBaseConfiguration;
        Products = SetProducts();
    }
    public IMongoCollection<Product> Products { get; set; }
    private IMongoCollection<Product> SetProducts()
    {
        var connectionString = _databaseConfiguration.Configurations.GetSection("MongoDBConnectionString").Value;
        var client = new MongoClient(connectionString);
        var databaseName = _databaseConfiguration.Configurations.GetSection("DatabaseName").Value;
        var database = client.GetDatabase(databaseName);
        return database.GetCollection<Product>("Products");
    }
}
