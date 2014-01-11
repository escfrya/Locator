using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Motivator.Mobile.WP.Converters
{
    public class BoolToVisibileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = false;
            if (value is bool) boolean = (bool)value;
            if ((string)parameter == "invert") boolean = !boolean;
            return boolean ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
