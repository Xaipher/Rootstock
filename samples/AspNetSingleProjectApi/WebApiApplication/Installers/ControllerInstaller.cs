using Microsoft.Extensions.DependencyInjection;
using Rootstock;
using Rootstock.Attributes;

namespace WebApiApplication.Installers
{
    [RootstockPriority(Priority = InstallerPriorities.First)]
    public class ControllerInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}