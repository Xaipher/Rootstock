using System.Collections.Generic;
using Weather.DataAccess;

namespace Weather.Data
{
    public class StaticWeatherSummaryRepository : IWeatherSummaryRepository
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        public IEnumerable<string> GetAvailableSummaries()
        {
            return Summaries;
        }
    }
}