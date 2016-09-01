using ForecastGrabber;
using ForecastGrabber.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using MarkdownLog;

namespace ForecastGrabberGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RetrieveButton_Click(object sender, RoutedEventArgs e)
        {
            GisMeteoForecastGrabber forecastGrabber = new GisMeteoForecastGrabber();
            ForecastRecord[] records = await forecastGrabber.RetrieveForecastAsync("Dnipro");
            var report = records.Select(x => new
            {
                City = x.City,
                Date = x.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Day = x.DayTemperature.ToString("+#;-#;0"),
                Night = x.NightTemperature.ToString("+#;-#;0")
            });
            string text = report.ToMarkdownTable().ToString();
            LogTextBox.Text = text;
        }
    }
}
