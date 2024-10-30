using Inventory_Management_System.InventoryManagement;
using Inventory_Management_System.ProductManagement;
using Inventory_Management_System.Repository;

namespace Inventory_Management_System.Utility;
public class ConsoleUI
{
    private readonly InventoryService _inventory;
    public ConsoleUI(DatabaseType databaseType)
    {
        _inventory = new InventoryService(databaseType);
    }

    internal void ShowMainMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n---------------------- Inventory Management System ----------------------\n");
            ViewAllProducts();

            Console.WriteLine("\n1. Add a product");
            Console.WriteLine("2. View all products");
            Console.WriteLine("3. Edit a product");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("5. Search for a product");
            Console.WriteLine("6. Exit");
            Console.Write("Please select an option (1-6): ");

            switch (Console.ReadLine())
            {
                case "1": AddProduct(); break;
                case "2": ViewAllProducts(); break;
                case "3": EditProduct(); break;
                case "4": DeleteProduct(); break;
                case "5": SearchProduct(); break;
                case "6": exit = true; break;
                default: Console.WriteLine("Invalid option. Please try again.\n"); break;
            }
        }
    }

    public void AddProduct()
    {
        Console.Write("Enter product name (more than 2 characters): ");
        var name = Console.ReadLine();

        Console.Write("Enter product price (non-negative value): ");
        if (!decimal.TryParse(Console.ReadLine(), out var price) || price < 0)
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        Console.Write("Enter currency (Dollar/Euro/Pound): ");
        if (!Enum.TryParse<Currency>(Console.ReadLine(), true, out var currency))
        {
            Console.WriteLine("Invalid currency.");
            return;
        }

        Console.Write("Enter product quantity (non-negative): ");
        if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        var product = new Product { Name = name, Quantity = quantity, Price = price, Currency = currency };
        _inventory.AddProduct(product);
    }

    public void ViewAllProducts()
    {
        var products = _inventory.GetAllProducts();
        foreach (var product in products)
            ProductPrinter.Print(product);

        Console.WriteLine();
    }

    public void EditProduct()
    {
        ViewAllProducts();
        Console.Write("Enter the name of the product you want to edit: ");
        var name = Console.ReadLine();

        Console.Write("Enter new product name (or press Enter to skip): ");
        string? newName = Console.ReadLine();
        newName = string.IsNullOrWhiteSpace(newName) ? null : newName;

        Console.Write("Enter new product price (or press Enter to skip): ");
        decimal? newPrice = ReadDecimal();

        Console.Write("Enter new currency (Dollar/Euro/Pound or press Enter to skip): ");
        Currency? newCurrency = ReadCurrency();

        Console.Write("Enter new product quantity (or press Enter to skip): ");
        int? newQuantity = ReadInteger();

        _inventory.UpdateProduct(name, newName, newPrice, newCurrency, newQuantity);
    }

    public void DeleteProduct()
    {
        Console.Write("Enter the name of the product you want to delete: ");
        var name = Console.ReadLine();
        _inventory.DeleteProduct(name);
    }

    public void SearchProduct()
    {
        Console.Write("Enter the name of the product you want to search for: ");
        var name = Console.ReadLine();
        var product = _inventory.GetProductByName(name);
        if (product != null)
            ProductPrinter.Print(product);
    }

    private static decimal? ReadDecimal()
    {
        var input = Console.ReadLine();
        return decimal.TryParse(input, out var value) && value >= 0 ? value : (decimal?)null;
    }

    private static int? ReadInteger()
    {
        var input = Console.ReadLine();
        return int.TryParse(input, out var value) && value >= 0 ? value : (int?)null;
    }

    private static Currency? ReadCurrency()
    {
        var input = Console.ReadLine();
        return Enum.TryParse(input, true, out Currency currency) ? currency : (Currency?)null;
    }

    public void ExitApplication()
    {
        Console.WriteLine("Exiting the application.");
    }
}
