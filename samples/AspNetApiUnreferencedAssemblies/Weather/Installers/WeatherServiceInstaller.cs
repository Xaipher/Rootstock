using Microsoft.Extensions.DependencyInjection;
using Rootstock;
using Rootstock.Attributes;

namespace Weather.Installers
{
    [RootstockPriority(Priority = WeatherInstallerPriorities.Logic)]
    public class WeatherServiceInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped<TemperatureService>();
            services.AddScoped<SummaryService>();
            services.AddScoped<WeatherForecastService>();
        }
    }
}