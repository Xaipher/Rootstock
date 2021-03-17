using System;
using System.Collections.Generic;
using System.Linq;

namespace Weather
{
    public class WeatherForecastService
    {
        private readonly TemperatureService _temperatureService;
        private readonly SummaryService _summaryService;
        
        public WeatherForecastService(TemperatureService temperatureService, SummaryService summaryService)
        {
            _temperatureService = temperatureService;
            _summaryService = summaryService;
        }

        public IEnumerable<WeatherForecast> GetCurrentForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = _temperatureService.GetRandomTemperatureBetween(-20, 55),
                    Summary = _summaryService.GetCurrentWeatherSummary()
                })
                .ToArray();
        }
    }
}