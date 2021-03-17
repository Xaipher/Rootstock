# Rootstock
A library to provide a decoupled and distributed composition root to .NET Core 3.1 and .NET 5 applications.

## Installation
*NuGet packages pending*

## Features
- Discovery of dependency injection registratration across multiple assemblies
    - Support for *referenced* and *unreferenced* assemblies
- Grouping of installers
- Setting installer group priority
- Setting installer priority within a group
- Discovery and registration of Application Parts for distributed Api Controllers
- Integrated with ILogger

## Usage
See the available samples to learn about different ways to consume the Rootstock library.

### Basic Usage

1. Create an installer

    Implement the IRootstockServicesInstaller interface. Here is an example of extracting the default configuration from a new .NET 5 Web Api project's Startup.cs.

    ```csharp
    // ApiInstaller.cs
    public class ApiInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApplication", Version = "v1"});
            });
        }
    }
    ```
2. Update the Startup.cs ConfigureServices method
   ```csharp
   // Startup.cs
   public void ConfigureServices(IServiceCollection services)
   {
       services.AddRootstock(Configuration);
   }
   ```

   And that's it!

   In this basic usage only one installer is created. However, any number of installers can be made to localize any common dependency registration.

### Accessing Configuration

To access the configration use implement either the IRootstockConfigurationInstaller or the IRootstockInstaller

```csharp
// ConfigurationInstaller.cs
public class ConfigurationInstaller : IRootstockConfigurationInstaller
{
    public void Install(IConfiguration configuration)
    {
        // Handle configuration
    }
}
```

or

```csharp
// Installer.cs
public class Installer : IRootstockInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        // Handle services and configuration
    }
}
```

## Inspiration
The inspriation for this project comes from Nick Chapsas and the YouTube video [Dependency injection and clean service registration](https://youtu.be/ESdvXlrG9zQ)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)