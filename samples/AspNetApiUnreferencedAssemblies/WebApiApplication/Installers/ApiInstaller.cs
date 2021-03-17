using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rootstock;
using Rootstock.Attributes;
using Rootstock.Mvc;

namespace WebApiApplication.Installers
{
    public class ApiInstaller : IRootstockServicesInstaller
    {
        [RootstockPriority(Priority = InstallerPriorities.Last)]
        public void Install(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddRootstockApplicationParts(options =>
                {
                    options.IncludeUnreferencedAssemblies = true;
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApiApplication", Version = "v1"});
            });
        }
    }
}