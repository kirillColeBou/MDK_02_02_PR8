using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Weather_Тепляков.Classes;

namespace Weather_Тепляков.Elements
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private ForecastData _forecastData;
        private readonly Weather _weatherService;

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

        public async Task LoadWeatherAsync(string city) => ForecastData = await _weatherService.GetWeatherForecastAsync(city);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}