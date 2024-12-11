using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Weather_Тепляков.Classes;

namespace Weather_Тепляков.Elements
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData _weatherData;
        private readonly Weather _weatherService;

        public WeatherViewModel() => _weatherService = new Weather();

        public WeatherData WeatherData
        {
            get => _weatherData;
            set
            {
                _weatherData = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadWeatherAsync(string city) => WeatherData = await _weatherService.GetWeatherAsync(city);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}