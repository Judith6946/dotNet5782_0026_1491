using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Convertors;

internal class ProductPageStatusToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string? target = parameter as string;
        Utils.PageStatus status = (Utils.PageStatus)value;
        switch(status)
        {
            case Utils.PageStatus.ADD:return true;
            case Utils.PageStatus.DISPLAY:return false;
            case Utils.PageStatus.EDIT:return target== "txtProductId"?false:true;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
