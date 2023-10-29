using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Media;
using System;
using System.Linq;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        private ConfigurationStore _configurationStore;
        private ObservableCollection<RoomStats> _roomStats;
        private SeriesCollection _series;
        private double _axisMax;
        private double _axisMin;
        public Func<double, string> DateTimeFormatter { get; set; }
        //public SeriesCollection Series { get; set; }
        public double AxisStep { get; set; }

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

        public ObservableCollection<RoomStats> RoomsData
        {
            get
            {
                return _roomStats;
            }
            set
            {
                _roomStats = value;
                OnPropertyChanged(nameof(RoomsData));
            }
        }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }


        public ChartsViewModel(ConfigurationStore store)
        {
            _configurationStore = store;
            _configurationStore.OnRoomStatsUpdated += _configurationStore_OnRoomStatsUpdated;
            DateTimeFormatter = value => new DateTime((long)(value)).ToString("dd/MM mm:ss");
            AxisStep = TimeSpan.FromDays(1).Ticks;
            SetAxisLimits(DateTime.Now);
            //AxisStep = TimeSpan.FromSeconds(1).Ticks;
            //Formatter = value => new System.DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("t");
            //Formatter = value => new System.DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("t");


        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks; // lets force the axis to be 100ms ahead
            //AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds

            AxisMin = now.Ticks - TimeSpan.FromHours(1).Ticks;  // changed to 30 seconds
        }

        private void _configurationStore_OnRoomStatsUpdated(object? sender, RoomStats[] e)
        {

            var dayConfig = Mappers.Xy<RoomStats>()
                .X(dayModel => (double)dayModel.CreatedAt.Ticks / TimeSpan.FromDays(1).Ticks)
                .Y(dayModel => dayModel.Temperature);

            //var temp new ChartValues<RoomStats>(e)
            RoomsData = new ObservableCollection<RoomStats>(e);
            var samples = e.OrderByDescending(d=>d.CreatedAt).Take(1000).ToList();
            Series = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<RoomStats>(samples),

                    Fill = Brushes.Transparent
                }
            };
           
        }
    }
}
