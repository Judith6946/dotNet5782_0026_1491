using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Convertors;

public class OrderStatusToVisibilityConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Enums.OrderStatus status = (Enums.OrderStatus)value;
        string? target = parameter.ToString();
        switch (status)
        {
            case Enums.OrderStatus.approved:
                switch (target)
                {
                    case "btnChangeStatus": return Visibility.Visible;
                    case "txtShipDate": return Visibility.Hidden;
                    case "lblShipDate": return Visibility.Hidden;
                    case "txtDeliveryDate": return Visibility.Hidden;
                    case "lblDeliveryDate": return Visibility.Hidden;
                }
                break;
            case Enums.OrderStatus.sent:
                switch (target)
                {
                    case "btnChangeStatus": return Visibility.Visible;
                    case "txtShipDate": return Visibility.Visible;
                    case "lblShipDate": return Visibility.Visible;
                    case "txtDeliveryDate": return Visibility.Hidden;
                    case "lblDeliveryDate": return Visibility.Hidden;
                }
                break;
            case Enums.OrderStatus.delivered:
                switch (target)
                {
                    case "btnChangeStatus": return Visibility.Hidden;
                    case "txtShipDate": return Visibility.Visible;
                    case "lblShipDate": return Visibility.Visible;
                    case "txtDeliveryDate": return Visibility.Visible;
                    case "lblDeliveryDate": return Visibility.Visible;
                }
                break;
            default:
                return Visibility.Visible;
        }
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
