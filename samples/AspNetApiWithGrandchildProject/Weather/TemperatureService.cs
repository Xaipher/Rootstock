using System;

namespace Weather
{
    public class TemperatureService
    {
        private readonly Random _rng = new();

        public int GetRandomTemperatureBetween(int low, int high)
        {
            return _rng.Next(-low, high);
        }
    }
}