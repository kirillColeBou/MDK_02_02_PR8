using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Weather_Тепляков
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PlaceholderText = "Здесь можно указать ваш город";

        public MainWindow()
        {
            InitializeComponent();
            parent.Children.Add(new Elements.Elements());
            parent.Children.Add(new Elements.Elements());
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

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {

            }
        }
    }
}
