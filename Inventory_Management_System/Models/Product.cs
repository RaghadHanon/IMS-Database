using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.ProductManagement;
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    [EnumDataType(typeof(Currency))]
    [DefaultValue(Currency.Dollar)]
    public Currency Currency { get; set; }
}
