using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();

    public OrderTracking MyOrderTracking
    {
        get { return (OrderTracking)GetValue(MyOrderTrackingProperty); }
        set { SetValue(MyOrderTrackingProperty, value); }
    }



    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyOrderTrackingProperty =
        DependencyProperty.Register("MyOrderTracking", typeof(OrderTracking), typeof(OrderTrackingWindow), new PropertyMetadata(null));

  
    public OrderTrackingWindow(int id)
    {
        InitializeComponent();
        try
        {
            //Requests a request from the logical layer to fetch all the products and displays them
            var temp = bl.Order.FollowOrder(id);
            MyOrderTracking = temp == null ? new() : temp;
        }
        catch (DalException) { MessageBox.Show("Could not find your order information. please try again later"); }
        catch (InvalidInputException) { MessageBox.Show("ID was not valid"); }

       
    }

    /// <summary>
    /// Click event of show details button.
    /// </summary>
    private void btnShowDetails_Click(object sender, RoutedEventArgs e)
    {
        new OrderWindow(MyOrderTracking.OrderId).Show();
    }
}
