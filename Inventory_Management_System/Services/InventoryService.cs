using Inventory_Management_System.Factories;
using Inventory_Management_System.ProductManagement;
using Inventory_Management_System.Repository;
using MongoDB.Driver;

namespace Inventory_Management_System.InventoryManagement;
public class InventoryService
{
    private readonly IProductRepository _productRepository;
    public InventoryService(DatabaseType databaseType)
    {
        _productRepository = RepositoryFactory.CreateRepository(databaseType);
    }

    public void AddProduct(Product product)
    {
        _productRepository.AddProduct(product);
    }

    public List<Product> GetAllProducts()
    {
        var products = _productRepository.GetAllProducts();
        if (!products.Any())
            Console.WriteLine("\nNo products in inventory.");
        
        return products;
    }

    public void UpdateProduct(string name, string? newName = null, decimal? newPrice = null, Currency? newCurrency = null, int? newQuantity = null)
    {
        var product = _productRepository.GetProductByName(name);
        if (product == null)
        {
            Console.WriteLine($"\nProduct '{name}' not found.");
            return;
        }

        _productRepository.UpdateProduct(name, newName, newPrice, newCurrency, newQuantity);
    }

    public Product GetProductByName(string name)
    {
        var product = _productRepository.GetProductByName(name); ;
        if (product == null)
            Console.WriteLine($"\nProduct '{name}' not found.");

        return product;
    }

    public void DeleteProduct(string name)
    {
        var product = _productRepository.GetProductByName(name);
        if (product == null)
        {
            Console.WriteLine($"Product '{name}' not found.");
            return;
        }

        _productRepository.DeleteProduct(name);
    }
}
