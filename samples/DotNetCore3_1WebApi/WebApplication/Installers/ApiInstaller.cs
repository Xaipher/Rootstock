using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rootstock;

namespace WebApplication.Installers
{
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
}