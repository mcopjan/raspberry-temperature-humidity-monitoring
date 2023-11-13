using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Raspberry.Temperature.Humidity.WPF.Desktop.Client.Converters
{
    public class DateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1 && values[0] is List<DateTime> dateTimes)
            {
                // Convert the list of DateTime objects to a list of formatted strings
                List<string> formattedDates = dateTimes.Select(dt => dt.ToString("dd/MM/yyyy HH:mm:ss")).ToList();
                return formattedDates;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
