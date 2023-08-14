using System.Collections.ObjectModel;

namespace TemperatureHumidityClient;

public partial class InitialPage : ContentPage
{
    private ObservableCollection<string> roomNames = new();

    public InitialPage()
	{
		InitializeComponent();
        GetRoomNamesAsync();
        MyListView.ItemsSource = roomNames;
    }

    private async void GetRoomNamesAsync()
    {
        var names = await ApiRepository.GetRoomNames();
        roomNames = new ObservableCollection<string>(names);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string s = (sender as Button).Text;
        await Navigation.PushAsync(new MainPage());
    }
}