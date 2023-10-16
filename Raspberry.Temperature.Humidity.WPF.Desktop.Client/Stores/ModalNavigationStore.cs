using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores
{
    public class ModalNavigationStore
    {
		private ViewModelBase _currentViewModel;
		public ViewModelBase CurrentViewModel
		{
			get
			{
				return _currentViewModel;
			}
			set
			{
                _currentViewModel = value;
				CurrentViewModelChanged?.Invoke();

            }
		}

		public bool IsOpen => _currentViewModel.IsOpen;

        public event Action CurrentViewModelChanged;	
	}
}
