using LiveCharts;
using LiveCharts.Defaults;
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
        private ObservableCollection<RoomStats> _roomStats;
        private SeriesCollection _series;
        private List<DateTime> customXAxisLabels;
        private double _yaxisMax;

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
        }


        private void _configurationStore_OnRoomStatsUpdated(object? sender, RoomStats[] e)
        {
            var samples = e.OrderByDescending(d => d.CreatedAt).Take(1000).OrderBy(d => d.CreatedAt).ToList();
            AdjustYAxisMaxValue(samples);

            Series = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Temperature [C]",
                    Values = new ChartValues<double>(samples.Select(roomStat => (double)roomStat.Temperature)),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10, // Set the size of the points
                    ScalesXAt = 0// This specifies the X-axis to use, 0 means the first X-axis
                }
            };

            CustomXAxisLabels = samples.Select(s => s.CreatedAt).ToList();


        }

        private void AdjustYAxisMaxValue(List<RoomStats> samples)
        {
            var maxTemperatureSample = (int)samples.Max(s => s.Temperature);
            YAxisMax = maxTemperatureSample < 50 ? 50 : 100;
        }
    }
}
