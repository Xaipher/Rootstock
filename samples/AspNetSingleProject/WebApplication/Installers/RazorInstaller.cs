using Microsoft.Extensions.DependencyInjection;
using Rootstock;

namespace WebApplication.Installers
{
    public class RazorInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddRazorPages();
        }
    }
}