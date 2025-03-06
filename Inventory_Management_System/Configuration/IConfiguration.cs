using Microsoft.Extensions.Configuration;

namespace Inventory_Management_System.Configurations;
public interface IConfiguration
{
    IConfigurationRoot Configurations { get; }
    IConfigurationRoot SetConfiguration();
}