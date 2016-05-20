using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Vulcan.Wpf.Core;

namespace Vulcan.Wpf.Utils.Converters
{
    [ObjectExport(nameof(NullToVisibilityConverter))]
    public class NullToVisibilityConverter : IValueConverter, IExportedObject
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (null == value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
