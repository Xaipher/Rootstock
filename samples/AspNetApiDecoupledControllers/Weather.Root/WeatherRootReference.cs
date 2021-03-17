using Weather.Controllers.Installers;
using Weather.Data.Installers;
using Weather.Installers;

namespace Weather.Root
{
    public class WeatherRootReference
    {
#pragma warning disable 169
        private WeatherLogicRoot _logic;
        private WeatherDataRoot _data;
        private WeatherControllerRoot _controller;
#pragma warning restore 169
    }
}