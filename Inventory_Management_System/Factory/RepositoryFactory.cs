using Inventory_Management_System.Configurations;
using Inventory_Management_System.Repository;
using Inventory_Management_System.Repository.MongoDbRepository;
using Inventory_Management_System.Repository.SqlServerRepository;

namespace Inventory_Management_System.Factories;
public class RepositoryFactory
{
    public static IConfiguration Configuration = new Configuration();
    public static IProductRepository CreateRepository(DatabaseType databaseType)
    {
        switch (databaseType)
        {
            case DatabaseType.MongoDB:
                var mongoDbContext = new MongoDbContext(Configuration);
                return new MongoDbRepository(mongoDbContext);

            case DatabaseType.SqlServer:
                var sqlServerContext = new SqlServerDbContext(Configuration);
                return new SqlServerDbRepository(sqlServerContext);

            default:
                throw new ArgumentException($"Unsupported DatabaseType: {databaseType}");
        }
    }
}
