using PL.ProductsWindows;
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
using System.Windows.Shapes;
using BO;
using PL.Utils;

namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        private BlApi.IBl bl = BlApi.Factory.Get();
        private PageStatus pageStatus;

        public Order MyOrder
        {
            get { return (Order)GetValue(MyOrderProperty); }
            set { SetValue(MyOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyOrderProperty =
            DependencyProperty.Register("MyOrder", typeof(Order), typeof(OrderWindow), new PropertyMetadata(null));



        public OrderWindow(int id, PageStatus _pageStatus = PageStatus.DISPLAY)
        {
            InitializeComponent();
            pageStatus = _pageStatus;
            var temp = bl.Order.GetOrder(id);
            MyOrder = temp == null ? new() : temp;

            if (pageStatus == PageStatus.DISPLAY)
                btnChangeStatus.Visibility = Visibility.Hidden;

            switch (MyOrder.Status)
            {
                case Enums.OrderStatus.approved:
                    if (pageStatus == PageStatus.EDIT) btnChangeStatus.Content = "Update Order Ship Date";
                    break;
                case Enums.OrderStatus.sent:
                    txtShipDate.Visibility = Visibility.Visible;
                    lblShipDate.Visibility = Visibility.Visible;
                    if(pageStatus==PageStatus.EDIT) btnChangeStatus.Content = "Update Order Delivery Date";
                    break;
                case Enums.OrderStatus.delivered:
                    btnChangeStatus.Visibility = Visibility.Hidden;
                    txtShipDate.Visibility = Visibility.Visible;
                    lblShipDate.Visibility = Visibility.Visible;
                    txtDeliveryDate.Visibility = Visibility.Visible;
                    lblDeliveryDate.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            PageStatus status = pageStatus == PageStatus.EDIT && MyOrder.Status == Enums.OrderStatus.approved ? PageStatus.EDIT : PageStatus.DISPLAY;
            new OrderItemsWindow(MyOrder.ID,status).Show();
        }
    }
}
