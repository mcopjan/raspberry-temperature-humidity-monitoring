using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Services
{
    public class DialogService : IDialogService
    {
        static Dictionary<Type,Type> _mappinngs = new Dictionary<Type,Type>();
        static Dictionary<Type, object[]> _mappinngsParams = new Dictionary<Type, object[]>();

        public static void RegisterDialog<TView, TViewModel>()
        { 
            _mappinngs.Add(typeof(TViewModel), typeof(TView));
        
        }

        public static void RegisterDialog<TView, TViewModel>(params object[] ctorParams)
        {
            _mappinngs.Add(typeof(TViewModel), typeof(TView));
            _mappinngsParams.Add(typeof(TViewModel), ctorParams);
        }


        public void ShowDialog(string name, Action<string> callback)
        {
            var type = Type.GetType($"Raspberry.Temperature.Humidity.WPF.Desktop.Client.Views.{name}");
            ShowDialogInternal(type, null, callback) ;
        }

        public void ShowDialog<TViewModel>(Action<string> callback)
        {
            var type = _mappinngs[typeof(TViewModel)];
            ShowDialogInternal(type, typeof(TViewModel), callback);
        }

        public void ShowDialog<TViewModel>(Action<string> callback, params object[] ctorParameters)
        {
            var type = _mappinngs[typeof(TViewModel)];
            ShowDialogInternal(type, typeof(TViewModel), callback);
        }

        private static void ShowDialogInternal(Type viewType, Type viewModelType, Action<string> callback)
        {
            var dialogWindow = new DialogWindow();

            EventHandler closeEventHandler = null;
            closeEventHandler = (s, e) =>
            {
                callback(dialogWindow.DialogResult.ToString());
                dialogWindow.Closed -= closeEventHandler;
            };
            dialogWindow.Closed += closeEventHandler;

            var content = Activator.CreateInstance(viewType);
            if (viewModelType != null)
            {
                if (_mappinngsParams.ContainsKey(viewModelType))
                {
                    var ctorParams = _mappinngsParams[viewModelType];
                    var vmContent = Activator.CreateInstance(viewModelType, ctorParams);
                    //this is dirty, I should perhaps check for metadata of a class and subscribe based on that, 
                    //maybe have ICloseble and check if for that and then subscribe
                    (vmContent as ConfigurationNotificationViewModel).OnRequestClose += (s, e) => 
                    {
                        dialogWindow.Close();
                        dialogWindow.DataContext = null;
                        };
                    (content as FrameworkElement).DataContext = vmContent;
                }
                else
                {
                    var vmContent = Activator.CreateInstance(viewModelType);
                    (content as FrameworkElement).DataContext = vmContent;
                }

            }
            
            dialogWindow.Content = content;
            dialogWindow.ShowDialog();
        }

        
    }
}
