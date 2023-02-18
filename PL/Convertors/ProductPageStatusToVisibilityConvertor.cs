using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Convertors;

internal class ProductPageStatusToVisibilityConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Utils.PageStatus status = (Utils.PageStatus)value;
        string? target = parameter.ToString();

        switch (status)
        {
            case Utils.PageStatus.EDIT: return Visibility.Visible;
            case Utils.PageStatus.DISPLAY: return Visibility.Hidden;
            case Utils.PageStatus.ADD: return target == "btnDeleteProduct" ? Visibility.Hidden : Visibility.Visible;
        }

        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
