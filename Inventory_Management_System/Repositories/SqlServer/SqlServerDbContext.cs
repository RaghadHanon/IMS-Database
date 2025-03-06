using Inventory_Management_System.Configurations;
using Inventory_Management_System.ProductManagement;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Repository.SqlServerRepository;
public class SqlServerDbContext : DbContext
{
    private IConfiguration _databaseConfiguration;
    public SqlServerDbContext(IConfiguration dataBaseConfiguration)
    {
        _databaseConfiguration = dataBaseConfiguration;
    }
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _databaseConfiguration.Configurations.GetSection("SQLServerConnectionString").Value;
        optionsBuilder.UseSqlServer(connectionString);
    }
}