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

        }

        private void btnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            PageStatus status = pageStatus == PageStatus.EDIT && MyOrder.Status == Enums.OrderStatus.approved ? PageStatus.EDIT : PageStatus.DISPLAY;
            new OrderItemsWindow(MyOrder.ID, status).Show();
        }

        private void btnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (MyOrder.Status == Enums.OrderStatus.approved)
                MyOrder = bl.Order.UpdateOrderShipping(MyOrder.ID);
            else if (MyOrder.Status == Enums.OrderStatus.sent)
                MyOrder = bl.Order.UpdateOrderDelivery(MyOrder.ID);
            MessageBox.Show("Order was updated!");
        }
    }
}
