using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rootstock
{
    internal class NullRootstockInstaller : IRootstockInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            // Intentionally left empty.
        }
    }
}