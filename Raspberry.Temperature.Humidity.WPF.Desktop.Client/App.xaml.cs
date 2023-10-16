using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Services;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ConfigurationStore _configurationStore;
        public App()
        {
            _configurationStore = new ConfigurationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DialogService.RegisterDialog<ConfigurationNotificationView, ConfigurationNotificationViewModel>(new object[] { _configurationStore } );
            MainWindow window = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_configurationStore)
            };

            window.Show();
            base.OnStartup(e);
        }
    }
}
