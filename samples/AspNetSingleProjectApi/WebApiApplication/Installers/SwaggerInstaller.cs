using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rootstock;
using Rootstock.Attributes;

namespace WebApiApplication.Installers
{
    [RootstockPriority(Priority = InstallerPriorities.Last)]
    public class SwaggerInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApiApplication", Version = "v1"});
            });
        }
    }
}