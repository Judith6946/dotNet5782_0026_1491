using BO;
using PL.OtherWindows;
using PL.Utils;
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
/// Interaction logic for OrderItemsWindow.xaml
/// </summary>
public partial class OrderItemsWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    private PageStatus pageStatus = PageStatus.EDIT;
    private int orderId;

    public ObservableCollection<OrderItem?> MyOrderItems
    {
        get { return (ObservableCollection<OrderItem?>)GetValue(MyOrderItemsProperty); }
        set { SetValue(MyOrderItemsProperty, value); }
    }



    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyOrderItemsProperty =
        DependencyProperty.Register("MyOrderItems", typeof(ObservableCollection<OrderItem>), typeof(OrderItemsWindow), new PropertyMetadata(null));



    public OrderItemsWindow(int id, PageStatus _pageStatus = PageStatus.DISPLAY)
    {
        InitializeComponent();
        orderId = id;

        try
        {
            //Requests a request from the logical layer to fetch all the products and displays them
            var order = bl.Order.GetOrder(id);
            var temp = order.ItemsList;
            MyOrderItems = temp == null ? new() : new(temp);
        }
        catch (InvalidInputException) { MessageBox.Show("Your order was not found, please try again."); }
        catch (DalException) { MessageBox.Show("Sorry, something went wrong, please try again"); }

        pageStatus = _pageStatus;
        if (pageStatus != PageStatus.EDIT)
        {
            lblInstructions.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Click event of order items list
    /// </summary>
    private void orderItemsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        int amount;

        if (pageStatus != PageStatus.DISPLAY)
        {
            var dialogWindow = new InputDialogWindow("Please enter new amount", ((OrderItem)((ListView)sender).SelectedItem).Amount.ToString());
            if (dialogWindow.ShowDialog() == true)
            {
                if (!int.TryParse(dialogWindow.Answer, out amount))
                    MessageBox.Show("Invalid amount");
                else
                {

                    Order? order = UpdateProductAmount(((OrderItem)((ListView)sender).SelectedItem).ProductId, amount);
                    //Requests a request from the logical layer to fetch all the products and displays them
                    var temp = order != null ? order?.ItemsList : null;
                    MyOrderItems = temp == null ? new() : new(temp);


                }

            }
        }
    }

    /// <summary>
    /// Update a product amount in order.
    /// </summary>
    /// <param name="productId">Product to be updated.</param>
    /// <param name="newAmount">New amount of product.</param>
    /// <returns>Updated order.</returns>
    private Order? UpdateProductAmount(int productId, int newAmount)
    {
        try
        {
            return bl.Order.UpdateOrder(orderId, productId, newAmount);
        }
        catch (InvalidInputException) { MessageBox.Show("Your input was invalid"); }
        catch (ImpossibleException) { MessageBox.Show("Order was already shipped"); }
        catch (NotFoundException) { MessageBox.Show("Cannot find this product on your order"); }
        catch (DalException) { MessageBox.Show("Sorry, something went wrong. please try again"); }

        return null;
    }

}
