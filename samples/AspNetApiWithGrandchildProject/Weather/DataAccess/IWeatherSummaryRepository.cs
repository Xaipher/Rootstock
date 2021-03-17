using System.Collections.Generic;

namespace Weather.DataAccess
{
    public interface IWeatherSummaryRepository
    {
        IEnumerable<string> GetAvailableSummaries();
    }
}