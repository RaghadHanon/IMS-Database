using Inventory_Management_System.ProductManagement;

namespace Inventory_Management_System.Repository.SqlServerRepository;
public class SqlServerDbRepository : IProductRepository
{
    private readonly SqlServerDbContext _context;
    public SqlServerDbRepository(SqlServerDbContext context)
    {
        _context = context;
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(string name)
    {
        var product = GetProductByName(name);
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public Product GetProductByName(string name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));

        return _context.Products.FirstOrDefault(x => x.Name == name);
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Price)
            .ToList();
    }

    public void UpdateProduct(string name, string? newName = null, decimal? newPrice = null, Currency? newCurrency = null, int? newQuantity = null)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));

        var product = GetProductByName(name);
        if (product == null)
            throw new InvalidOperationException($"Product with name '{name}' not found.");

        if (newName != null)
            product.Name = newName;

        if (newPrice.HasValue)
            product.Price = newPrice.Value;

        if (newCurrency.HasValue)
            product.Currency = newCurrency.Value;

        if (newQuantity.HasValue)
            product.Quantity = newQuantity.Value;

        _context.SaveChanges();
    }
}
