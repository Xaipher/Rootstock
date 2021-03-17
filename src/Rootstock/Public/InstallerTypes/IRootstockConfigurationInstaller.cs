using Microsoft.Extensions.Configuration;

namespace Rootstock
{
    public interface IRootstockConfigurationInstaller : IRootstockInstallerMarker
    {
        void Install(IConfiguration configuration);
    }
}