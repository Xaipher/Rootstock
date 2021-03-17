using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rootstock;

namespace Weather.Installers
{
    public class WeatherInstaller : IRootstockServicesInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.TryAddScoped<TemperatureService>();
            services.TryAddScoped<SummaryService>();
            services.TryAddScoped<WeatherForecastService>();
        }
    }
}