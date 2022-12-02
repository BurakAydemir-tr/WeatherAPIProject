using Entities.Concrete.JsonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.OpenWeatherServices
{
    public interface IOpenWeatherService
    {
        Task<List<Geolocation>> GetCoordinateByCity(string location);

        Task<WeatherForecastData> GetCurrentWeatherByLocation(double lat, double lon);
    }
}
