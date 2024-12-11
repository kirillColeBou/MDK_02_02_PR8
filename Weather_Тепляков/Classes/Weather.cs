using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Weather_Тепляков.Classes
{
    public class Weather
    {
        private const string ApiKey = "84d08dfffe2e4b631d17c38cfc1e730e";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        private readonly HttpClient _httpClient;

        public Weather()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric&lang=ru";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Не удалось получить данные о погоде");
            }

            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
            return weatherData;
        }
    }

    public class WeatherData
    {
        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("dt")]
        public long DateTimeUnix { get; set; }

        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; }

        public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(DateTimeUnix).DateTime;
        public string WeatherDescription => Weather?.Length > 0 ? Weather[0].Description : "Неизвестно";
    }

    public class WeatherDescription
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }


    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }
}
