using Entities.Concrete.JsonEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.OpenWeatherServices
{
    public class OpenWeatherManager : IOpenWeatherService
    {
        public IConfiguration Configuration { get; }
        private readonly IHttpClientFactory _factory;

        public OpenWeatherManager(IConfiguration configuration, IHttpClientFactory factory)
        {
            Configuration = configuration;
            _factory = factory;
        }
        public async Task<List<Geolocation>> GetCoordinateByCity(string location)
        {
            if (location != null)
            {
                string baseUrl = Configuration["OpenWeatherAPI:GeocodingAPI"];
                string apiKey = Configuration["OpenWeatherAPI:ApiKey"];
                string url = $"{baseUrl}{location}&appid={apiKey}";

                var geolocationModel = await GetDataFromAPI<List<Geolocation>>(url);

                return geolocationModel;

            }
            throw new Exception("Şehir bilgisinin girilmesi gerekir.");

        }

        public async Task<WeatherForecastData> GetCurrentWeatherByLocation(double lat, double lon)
        {
            if (lat != null && lon != null)
            {
                string baseUrl = Configuration["OpenWeatherAPI:WeatherAPI"];
                string apiKey = Configuration["OpenWeatherAPI:ApiKey"];
                string url = $"{baseUrl}?lat={lat}&lon={lon}&units=metric&appid={apiKey}";

                var weatherForecast = await GetDataFromAPI<WeatherForecastData>(url);

                return weatherForecast;
            }
            throw new Exception("Coğrafi konum bilgileri eksik girilmiş.");

        }

        private async Task<T> GetDataFromAPI<T>(string url)
        {
            try
            {
                var client = _factory.CreateClient();
                var response = client.GetAsync(url).Result;
                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true,
                };
                T Tdata = await JsonSerializer.DeserializeAsync<T>(responseStream, options);
                return Tdata;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
    }
}
