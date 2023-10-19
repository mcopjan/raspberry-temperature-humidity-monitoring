using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.ViewModels
{
    public class ChartsViewModel : ViewModelBase
    {
        private ConfigurationStore store;

		private List<ChartItem> _dummyData;
        

        public List<ChartItem> DummyData
		{
			get
			{
				return _dummyData;
			}
			set
			{
                _dummyData = value;
				OnPropertyChanged(nameof(DummyData));
			}
		}
		public ChartsViewModel(ConfigurationStore store)
        {
            this.store = store;
            DummyData = new List<ChartItem>() { new ChartItem() { Name = "item1" } };
        }

        public class ChartItem
        {
            public string Name { get; set; }
        }
    }
}
