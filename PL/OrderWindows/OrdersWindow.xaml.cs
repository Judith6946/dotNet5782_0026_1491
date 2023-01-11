using BO;
using PL.ProductsWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PL.OrderWindows;

/// <summary>
/// Interaction logic for OrdersWindow.xaml
/// </summary>
public partial class OrdersWindow : Window
{

    private BlApi.IBl bl = BlApi.Factory.Get();

    public ObservableCollection<OrderForList?> MyOrders
    {
        get { return (ObservableCollection<OrderForList?>)GetValue(MyOrdersProperty); }
        set { SetValue(MyOrdersProperty, value); }
    }

   

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyOrdersProperty =
        DependencyProperty.Register("MyOrders", typeof(ObservableCollection<OrderForList>), typeof(OrdersWindow), new PropertyMetadata(null));


    public OrdersWindow()
    {
        InitializeComponent();

        //Requests a request from the logical layer to fetch all the products and displays them
        var temp = bl.Order.GetOrders();
        MyOrders = temp == null ? new() : new(temp);

    }

    private void ordersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var window = new OrderWindow(((OrderForList)((ListView)sender).SelectedItem).ID,Utils.PageStatus.EDIT);
        window.Show();
        window.Closing += Window_Closing;
    }

    private void Window_Closing(object? sender, CancelEventArgs e)
    {
        //Requests a request from the logical layer to fetch all the products and displays them
        var temp = bl.Order.GetOrders();
        MyOrders = temp == null ? new() : new(temp);
    }
}
