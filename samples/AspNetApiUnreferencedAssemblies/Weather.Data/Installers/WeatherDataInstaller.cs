using Microsoft.Extensions.DependencyInjection;
using Rootstock;
using Rootstock.Attributes;
using Weather.DataAccess;
using Weather.Installers;

namespace Weather.Data.Installers
{
    [RootstockPriority(Priority = WeatherInstallerPriorities.Data)]
    public class WeatherDataInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped<IWeatherSummaryRepository, StaticWeatherSummaryRepository>();
        }
    }
}