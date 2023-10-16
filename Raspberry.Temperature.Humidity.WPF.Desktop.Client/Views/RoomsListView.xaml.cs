using Raspberry.Temperature.Humidity.WPF.Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Views
{
    /// <summary>
    /// Interaction logic for RoomsListView.xaml
    /// </summary>
    public partial class RoomsListView : UserControl
    {
        public RoomsListView()
        {
            //DataContext = new RoomsListModel();
            InitializeComponent();
        }
    }
}
