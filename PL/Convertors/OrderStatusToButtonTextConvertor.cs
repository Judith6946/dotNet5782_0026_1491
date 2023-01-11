using BO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Convertors
{
    internal class OrderStatusToButtonTextConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enums.OrderStatus status = (Enums.OrderStatus)value;
            switch (status)
            {
                case Enums.OrderStatus.approved: return "Update ship date";
                case Enums.OrderStatus.sent: return "Update delivery date";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
