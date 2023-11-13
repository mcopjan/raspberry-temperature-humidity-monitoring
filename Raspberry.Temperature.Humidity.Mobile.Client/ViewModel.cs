using System.Collections.ObjectModel;
using System.Drawing;
//using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace TemperatureHumidityClient;

[ObservableObject]
public partial class ViewModel
{
    private ObservableCollection<DateTimePoint> temperatureData = new();
    private ObservableCollection<DateTimePoint> humidityData = new();
    public static string RoomName;
    public ViewModel()
    {
        DrawMargin = new Margin(70, Margin.Auto, Margin.Auto, Margin.Auto);
        TempChartTitle = "Temperature [C]";
        HumChartTitle = "Humidity [%]";

        TempSeries.Add(new ColumnSeries<DateTimePoint>
        {
            TooltipLabelFormatter = (chartPoint) =>$"{new DateTime((long) chartPoint.SecondaryValue):MMMM dd tt mm}: {chartPoint.PrimaryValue}",
            Values = temperatureData,
            Fill = new SolidColorPaint(SKColors.IndianRed)

        });


        HumSeries.Add(new ColumnSeries<DateTimePoint>
        {
            TooltipLabelFormatter = (chartPoint) => $"{new DateTime((long)chartPoint.SecondaryValue):MMMM dd tt mm}: {chartPoint.PrimaryValue}",
            Values = humidityData,
            Fill = new SolidColorPaint(SKColors.DodgerBlue)
        });
        FetchDataAsync();
    }


    public object Sync { get; } = new();
    public List<ISeries> TempSeries { get; set; } = new();
    public List<ISeries> HumSeries { get; set; } = new();
    public Margin DrawMargin { get; set; }
    public string TempChartTitle { get; set; }
    public string HumChartTitle{ get; set; }

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