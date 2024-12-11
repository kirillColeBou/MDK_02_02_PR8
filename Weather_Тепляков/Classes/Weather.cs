using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather_Тепляков.Classes
{
    public class Weather
    {
        private const string ApiKey = "84d08dfffe2e4b631d17c38cfc1e730e";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        private readonly HttpClient _httpClient;

        public Weather()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ForecastData> GetWeatherForecastAsync(string city)
        {
            var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric&lang=ru";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Не удалось получить данные о погоде");
            }

            var json = await response.Content.ReadAsStringAsync();
            var forecastData = JsonConvert.DeserializeObject<ForecastData>(json);
            return forecastData;
        }
    }

    public class ForecastData
    {
        [JsonProperty("list")]
        public ListItem[] List { get; set; }
    }

    public class ListItem
    {
        [JsonProperty("dt")]
        public long DateTimeUnix { get; set; }

        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; }

        public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(DateTimeUnix).DateTime;
        public string WeatherDescription => Weather?.Length > 0 ? Weather[0].Description : "Неизвестно";
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        public int TemperatureRounded => (int)Math.Round(Temperature);

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }

    public class WeatherDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}