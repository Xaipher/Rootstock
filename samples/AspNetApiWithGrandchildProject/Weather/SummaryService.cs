using System;
using System.Linq;
using Weather.DataAccess;

namespace Weather
{
    public class SummaryService
    {
        private readonly IWeatherSummaryRepository _weatherSummaryRepository;
        
        private readonly Random _rng = new ();

        public SummaryService(IWeatherSummaryRepository weatherSummaryRepository)
        {
            _weatherSummaryRepository = weatherSummaryRepository;
        }

        public string GetCurrentWeatherSummary()
        {
            var summaries = _weatherSummaryRepository.GetAvailableSummaries().ToArray();
            
            return summaries[_rng.Next(summaries.Length)];
        }
    }
}