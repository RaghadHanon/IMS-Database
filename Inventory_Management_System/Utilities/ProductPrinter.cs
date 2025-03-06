using Inventory_Management_System.ProductManagement;

namespace Inventory_Management_System.Utility;
public static class ProductPrinter
{
    public static void Print(Product product)
    {
        Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}, {product.Price} {product.Currency}");
    }
}
