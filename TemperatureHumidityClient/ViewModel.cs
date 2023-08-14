using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace TemperatureHumidityClient;

[ObservableObject]
public partial class ViewModel
{
    private ObservableCollection<DateTimePoint> temperatureData = new();
    private ObservableCollection<DateTimePoint> humidityData = new();
    public static string RoomName;
    public ViewModel()
    {
        Series.Add(new ColumnSeries<DateTimePoint>
        {
            TooltipLabelFormatter = (chartPoint) =>$"{new DateTime((long) chartPoint.SecondaryValue):MMMM dd tt mm}: {chartPoint.PrimaryValue}",
            Values = temperatureData,
        });

        /*
        Series.Add(new ColumnSeries<DateTimePoint>
        {
            TooltipLabelFormatter = (chartPoint) => $"{new DateTime((long)chartPoint.SecondaryValue):MMMM dd}: {chartPoint.PrimaryValue}",
            Values = humidityData,
        });*/
        FetchDataAsync();
    }


    public object Sync { get; } = new();
    public List<ISeries> Series { get; set; } = new();

    async void FetchDataAsync()
    {
        var data = await ApiRepository.GetRoomStats(RoomName);
        lock (Sync)
        {
            foreach (var d in data)
            {
                temperatureData.Add(new DateTimePoint(d.CreatedAt,d.Temperature));
                humidityData.Add(new DateTimePoint(d.CreatedAt, d.Humidity));
            }
        }
    }

    public Axis[] XAxes { get; set; } =
    {
        new Axis
        {
            Labeler = value => new DateTime((long) value).ToString("MMMM dd"),
            LabelsRotation = 15,

            // when using a date time type, let the library know your unit 
            UnitWidth = TimeSpan.FromMinutes(1).Ticks, 

            // if the difference between our points is in hours then we would:
            // UnitWidth = TimeSpan.FromHours(1).Ticks,

            // since all the months and years have a different number of days
            // we can use the average, it would not cause any visible error in the user interface
            // Months: TimeSpan.FromDays(30.4375).Ticks
            // Years: TimeSpan.FromDays(365.25).Ticks

            // The MinStep property forces the separator to be greater than 1 day.
            MinStep = TimeSpan.FromMinutes(1).Ticks
        }
    };
}