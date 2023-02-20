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

            UpdateOrder(id);

            if (pageStatus == PageStatus.DISPLAY)
                btnChangeStatus.Visibility = Visibility.Hidden;

        }


        /// <summary>
        /// Update MyOrder
        /// </summary>
        /// <param name="id">Id of order</param>
        private void UpdateOrder(int id)
        {
            try
            {
                var temp = bl.Order.GetOrder(id);
                MyOrder = temp == null ? new() : new() { ID = temp.ID, CustomerAdress = temp.CustomerAdress, CustomerEmail = temp.CustomerEmail, CustomerName = temp.CustomerName, DeliveryDate = temp.DeliveryDate, OrderDate = temp.OrderDate, ShipDate = temp.ShipDate, Status = temp.Status, TotalPrice = temp.TotalPrice, ItemsList = new(temp.ItemsList!) };
            }
            catch (DalException) { MessageBox.Show("Could not load your order information, please check your input and try again."); }
            catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
        }

        /// <summary>
        /// Click event of show products button
        /// </summary>
        private void btnShowProducts_Click(object sender, RoutedEventArgs e)
        {
            PageStatus status = pageStatus == PageStatus.EDIT && MyOrder.Status == Enums.OrderStatus.approved ? PageStatus.EDIT : PageStatus.DISPLAY;
            var window=new OrderItemsWindow(MyOrder.ID, status);
            window.Show();
            window.Closed += Window_Closed;
        }

        /// <summary>
        /// On products-window closing.
        /// </summary>
        private void Window_Closed(object? sender, EventArgs e)
        {
            UpdateOrder(MyOrder.ID);
        }

        /// <summary>
        /// Click event of changeStatus button
        /// </summary>
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
