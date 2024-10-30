using Inventory_Management_System.ProductManagement;
using MongoDB.Driver;

namespace Inventory_Management_System.Repository.MongoDbRepository;
public class MongoDbRepository : IProductRepository
{
    private readonly MongoDbContext _context;
    private static int _currentMaxId;

    public MongoDbRepository(MongoDbContext context)
    {
        _context = context;
        _currentMaxId = _context.Products.Find(FilterDefinition<Product>.Empty)
                                 .SortByDescending(p => p.Id)
                                 .FirstOrDefault()?.Id ?? 0;
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        product.Id = ++_currentMaxId;
        _context.Products.InsertOne(product);
    }

    public void DeleteProduct(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        var result = _context.Products.DeleteOne(p => p.Name == name);
        if (result.DeletedCount == 0)
            throw new InvalidOperationException($"No product found with name '{name}'.");
    }

    public Product GetProductByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        return _context.Products.Find(p => p.Name == name).FirstOrDefault();
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products
            .Find(FilterDefinition<Product>.Empty)
            .SortBy(p => p.Name)
            .ThenBy(p => p.Price)
            .ToList();
    }

    public void UpdateProduct(string name, string? newName = null, decimal? newPrice = null, Currency? newCurrency = null, int? newQuantity = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        var update = Builders<Product>.Update.Combine(
            newName != null ? Builders<Product>.Update.Set(p => p.Name, newName) : null,
            newPrice.HasValue ? Builders<Product>.Update.Set(p => p.Price, newPrice.Value) : null,
            newCurrency.HasValue ? Builders<Product>.Update.Set(p => p.Currency, newCurrency.Value) : null,
            newQuantity.HasValue ? Builders<Product>.Update.Set(p => p.Quantity, newQuantity.Value) : null
        );
        var result = _context.Products.UpdateOne(p => p.Name == name, update);
        if (result.ModifiedCount == 0)
            throw new InvalidOperationException($"No product found with name '{name}' to update.");
    }
}
