using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Weather_Тепляков.Classes;
using Weather_Тепляков.Elements;

namespace Weather_Тепляков
{
    public partial class MainWindow : Window
    {
        private const string PlaceholderText = "Здесь можно указать ваш город";
        private readonly WeatherViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new WeatherViewModel();
            DataContext = _viewModel;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(city.Text))
            {
                city.Text = PlaceholderText;
                city.Foreground = Brushes.Gray;
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (city.Text == PlaceholderText)
            {
                city.Text = string.Empty;
                city.Foreground = Brushes.Black;
            }
        }

        private async void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var cityName = city.Text;
                if (string.IsNullOrWhiteSpace(cityName))
                {
                    MessageBox.Show("Введите название города");
                    return;
                }
                try
                {
                    await _viewModel.LoadWeatherAsync(cityName);
                    parent.Children.Clear();
                    if (_viewModel.ForecastData?.List != null)
                    {
                        foreach (var forecastItem in _viewModel.ForecastData.List)
                        {
                            var element = new Elements.Elements
                            {
                                DataContext = forecastItem
                            };
                            parent.Children.Add(element);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}