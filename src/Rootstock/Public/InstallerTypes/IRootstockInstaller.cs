using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rootstock
{
    public interface IRootstockInstaller : IRootstockInstallerMarker
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}