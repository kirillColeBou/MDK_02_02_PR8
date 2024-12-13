using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Weather_Тепляков.Classes;

namespace Weather_Тепляков.Elements
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private ForecastData _forecastData;
        private readonly Weather _weatherService;
        public int _requestCount = 0;
        public int MaxRequestsPerDay = 50;

        public WeatherViewModel() => _weatherService = new Weather();

        public ForecastData ForecastData
        {
            get => _forecastData;
            set
            {
                _forecastData = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadWeatherAsync(string city)
        {
            using (var dbContext = new Context())
            {
                var existingData = dbContext.DataWeather.FirstOrDefault(w => w.City == city && w.RequestDate.Date == DateTime.Today);
                if (existingData != null)
                {
                    ForecastData = JsonConvert.DeserializeObject<ForecastData>(existingData.JsonData);
                }
                else
                {
                    if (_requestCount >= MaxRequestsPerDay)
                    {
                        throw new Exception("Достигнут лимит запросов на сегодня.");
                    }
                    ForecastData = await _weatherService.GetWeatherForecastAsync(city);
                    _requestCount++;
                    var weatherData = new DataWeather
                    {
                        City = city,
                        JsonData = JsonConvert.SerializeObject(ForecastData),
                        RequestDate = DateTime.Now
                    };
                    dbContext.DataWeather.Add(weatherData);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}