using LiveCharts;
using LiveCharts.Wpf;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        private ConfigurationStore _configurationStore;
        private SeriesCollection _series;
        private string _selectedDateRange;
        private List<DateTime> customXAxisLabels;
        private double _yaxisMax;
        private List<RoomStats> _roomStats;

        public ObservableCollection<string> DateRangeItems { get; }

        public string SelectedDateRange
        {
            get
            {
                return _selectedDateRange;
            }
            set
            {
                _selectedDateRange = value;
                OnPropertyChanged(nameof(SelectedDateRange));
                if (_roomStats != null) { RenderChart(); }
            }
        }


        public SeriesCollection Series
        {
            get
            {
                return _series;
            }
            set
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        

        public List<DateTime> CustomXAxisLabels
        {
            get { return customXAxisLabels; }
            set
            {
                customXAxisLabels = value;
                OnPropertyChanged(nameof(CustomXAxisLabels));
            }
        }


        public double YAxisMax
        {
            get { return _yaxisMax; }
            set
            {
                _yaxisMax = value;
                OnPropertyChanged("YAxisMax");
            }
        }


        public ChartsViewModel(ConfigurationStore store)
        {
            YAxisMax = 100;
            _configurationStore = store;
            _configurationStore.OnRoomStatsUpdated += _configurationStore_OnRoomStatsUpdated;
            DateRangeItems = new ObservableCollection<string>() { "Last Hour","Today", "Last 7 Days", "Last Month", "This Year" };
            SelectedDateRange = DateRangeItems.First();
        }


        private void _configurationStore_OnRoomStatsUpdated(object? sender, RoomStats[] e)
        {
            _roomStats = e.ToList();
            SelectedDateRange = DateRangeItems.First();
            RenderChart();
        }

        private void RenderChart()
        {
            List<RoomStats> filteredStats = new List<RoomStats>();
            bool displayWithAnimation = false;
            if (SelectedDateRange.Equals("Last Hour")) //change into enum
            {
                displayWithAnimation = true;
                filteredStats.AddRange(_roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromHours(1)).OrderBy(d => d.CreatedAt).ToList());
            }
            else if (SelectedDateRange.Equals("Today"))
            {
                //there can be max 1440 samples a day, still ok to render 
                filteredStats.AddRange(_roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(1)).OrderBy(d => d.CreatedAt).ToList());
            }
            else if (SelectedDateRange.Equals("Last 7 Days"))
            {
                // LiveChart does not perform very well with a lot of samples, if we collected 1 minutes, we would have to render around 10000 points. We will average values every 15 mins 
                var numberOfSamplesIn7days = _roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(7)).ToList();

                var averagedStats = _roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(7))
                    .GroupBy(stats => new DateTime(stats.CreatedAt.Year, stats.CreatedAt.Month, stats.CreatedAt.Day, stats.CreatedAt.Hour, stats.CreatedAt.Minute / 15 * 15, 0))
                    .Select(group => new RoomStats
                    {
                        CreatedAt = group.Key,
                        Temperature = (long)group.Average(stats => stats.Temperature),
                        Humidity = (long)group.Average(stats => stats.Humidity),
                        TemperatureUnit = group.First().TemperatureUnit,
                        RoomName = group.First().RoomName,
                    })
                    .OrderBy(d => d.CreatedAt)
                    .ToList();
                filteredStats.AddRange(averagedStats);
            }
            else if (SelectedDateRange.Equals("Last Month"))
            {

                var samplesCount = _roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(30)).ToList();

                //30mins averages
                var averagedStats = _roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(30))
                    .GroupBy(stats => new DateTime(stats.CreatedAt.Year, stats.CreatedAt.Month, stats.CreatedAt.Day, stats.CreatedAt.Hour, stats.CreatedAt.Minute / 30 * 30, 0))
                    .Select(group => new RoomStats
                    {
                        CreatedAt = group.Key,
                        Temperature = (long)group.Average(stats => stats.Temperature),
                        Humidity = (long)group.Average(stats => stats.Humidity),
                        TemperatureUnit = group.First().TemperatureUnit,
                        RoomName = group.First().RoomName,
                    })
                    .OrderBy(d => d.CreatedAt)
                    .ToList();
                filteredStats.AddRange(averagedStats);
            }
            else if (SelectedDateRange.Equals("This Year"))
            {
                var samplesCount = _roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(365)).ToList();

                //60mins averages
                var averagedStats = _roomStats.Where(s => s.CreatedAt > DateTime.Now - TimeSpan.FromDays(365))
                    .GroupBy(stats => new DateTime(stats.CreatedAt.Year, stats.CreatedAt.Month, stats.CreatedAt.Day, stats.CreatedAt.Hour, stats.CreatedAt.Minute / 60 * 60, 0))
                    .Select(group => new RoomStats
                    {
                        CreatedAt = group.Key,
                        Temperature = (long)group.Average(stats => stats.Temperature),
                        Humidity = (long)group.Average(stats => stats.Humidity),
                        TemperatureUnit = group.First().TemperatureUnit,
                        RoomName = group.First().RoomName,
                    })
                    .OrderBy(d => d.CreatedAt)
                    .ToList();
                filteredStats.AddRange(averagedStats);
            }

            Series = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Temperature [C]",
                    Values = new ChartValues<float>(filteredStats.Select(roomStat => (float)roomStat.Temperature)),
                    PointGeometry = displayWithAnimation ? DefaultGeometries.Circle : null,
                    LabelPoint = point => $"{point.Y:0.00}",
                    PointGeometrySize = 10, // Set the size of the points
                    ScalesXAt = 0// This specifies the X-axis to use, 0 means the first X-axis
                },
                new LineSeries
                {
                    Title = "Humidity [%]",
                    Values = new ChartValues<float>(filteredStats.Select(roomStat => (float)roomStat.Humidity)),
                    PointGeometry = displayWithAnimation ? DefaultGeometries.Circle : null,
                    LabelPoint = point => $"{point.Y:0.00}",
                    PointGeometrySize = 10, // Set the size of the points
                    ScalesXAt = 0// This specifies the X-axis to use, 0 means the first X-axis
                }
            };

            CustomXAxisLabels = filteredStats.Select(s => s.CreatedAt).ToList();
        }

        private void AdjustYAxisMaxValue(List<RoomStats> samples)
        {
            var maxTemperatureSample = (int)samples.Max(s => s.Temperature);
            YAxisMax = maxTemperatureSample < 50 ? 50 : 100;
        }
    }
}
