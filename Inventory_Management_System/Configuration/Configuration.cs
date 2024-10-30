using Microsoft.Extensions.Configuration;

namespace Inventory_Management_System.Configurations;
public class Configuration : IConfiguration
{
    public IConfigurationRoot Configurations { get; }
    public Configuration()
    {
        Configurations = SetConfiguration();
    }

    public IConfigurationRoot SetConfiguration()
    {
        return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("AppSetting.json")
                    .Build();
    }
}