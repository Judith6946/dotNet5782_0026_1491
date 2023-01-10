using BlApi;
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
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();

    public Cart MyCart
    {
        get { return (Cart)GetValue(MyCartProperty); }
        set { SetValue(MyCartProperty, value); }
    }


    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyCartProperty =
        DependencyProperty.Register("MyCart", typeof(Cart), typeof(CartWindow), new PropertyMetadata(null));


    public CartWindow(Cart cart)
    {
        InitializeComponent();
        MyCart = cart;
    }

    private void btnFinishOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Cart.MakeOrder(MyCart);
            MessageBox.Show("Your order was made successfuly!");
            DialogResult = true;
            this.Close();
        }
        catch (SoldOutException ex) { MessageBox.Show("Sold out! \n " + ex.Message); }
        catch(InvalidInputException ex) { MessageBox.Show("Invalid input! \n " + ex.Message); }
    }
}
