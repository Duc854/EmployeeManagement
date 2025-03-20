using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Presentation.Helper
{
    public class UserIdToVisibilityConverter : IMultiValueConverter
    {
        public UserIdToVisibilityConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                if (values[0] is int rowUserId && values[1] is int appUserId)
                {
                    return rowUserId != appUserId ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null; // We don't need to convert back
        }
    }
}
