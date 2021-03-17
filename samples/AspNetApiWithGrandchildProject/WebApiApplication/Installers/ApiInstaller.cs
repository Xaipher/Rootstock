using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rootstock;
using Rootstock.Attributes;

namespace WebApiApplication.Installers
{
    public class ApiInstaller : IRootstockServicesInstaller
    {
        [RootstockPriority(Priority = InstallerPriorities.Last)]
        public void Install(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApiApplication", Version = "v1"});
            });
        }
    }
}