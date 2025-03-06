using Inventory_Management_System.ProductManagement;

namespace Inventory_Management_System.Repository;
public interface IProductRepository
{
    void AddProduct(Product product);
    void DeleteProduct(string name);
    List<Product> GetAllProducts();
    Product GetProductByName(string name);
    void UpdateProduct(string name, string? newName = null, decimal? newPrice = null, Currency? newCurrency = null, int? newQuantity = null);
}