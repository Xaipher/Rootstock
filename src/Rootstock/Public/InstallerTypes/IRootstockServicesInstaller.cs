using Microsoft.Extensions.DependencyInjection;

namespace Rootstock
{
    public interface IRootstockServicesInstaller : IRootstockInstallerMarker
    {
        void Install(IServiceCollection services);
    }
}