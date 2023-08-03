using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

namespace TemperatureHumidityClient
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }


        async void OnButtonClicked(object sender, EventArgs args)
        {
            var data = await ApiRepository.GetRoomStats();

        }

    }
}