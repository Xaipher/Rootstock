using System;

namespace Weather
{
    public class SummaryService
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Random _rng = new ();

        public string GetCurrentWeatherSummary() => Summaries[_rng.Next(Summaries.Length)];
    }
}