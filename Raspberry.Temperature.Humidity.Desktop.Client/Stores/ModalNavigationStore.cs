using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using System;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Stores
{
    public class ModalNavigationStore
    {
        public event Action CurrentViewModelChanged;
        private ViewModelBase _currentViewModel;
		
		
		public ViewModelBase CurrentViewModel
		{
			get => _currentViewModel;
            set
			{
                _currentViewModel = value;
				OnCurrentViewModelChanged();
            }
		}

        public bool IsOpen => _currentViewModel != null;

        private void OnCurrentViewModelChanged()
        {
			CurrentViewModelChanged?.Invoke();
        }

		public void Close()
		{
			CurrentViewModel = null;
		}
	}
}
