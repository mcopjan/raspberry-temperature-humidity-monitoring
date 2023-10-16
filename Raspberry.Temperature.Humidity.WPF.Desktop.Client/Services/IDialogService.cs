using System;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Services
{
    public interface IDialogService
    {
        void ShowDialog(string name, Action<string> callback);
        void ShowDialog<ViewModel>(Action<string> callback);
    }
}