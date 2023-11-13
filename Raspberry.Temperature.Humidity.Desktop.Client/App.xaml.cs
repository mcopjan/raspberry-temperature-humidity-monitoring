using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.Windows;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ConfigurationStore _configurationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        public App()
        {
            _configurationStore = new ConfigurationStore();
            _modalNavigationStore = new ModalNavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_configurationStore, _modalNavigationStore)
            };

            window.Show();
            base.OnStartup(e);
        }
    }
}
