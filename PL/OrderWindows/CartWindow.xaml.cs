using BlApi;
using BO;
using System.Windows;
using System.Windows.Controls;

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

    private void Remove_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int id = int.Parse(((Button)sender).Tag.ToString()!);
            MyCart = bl.Cart.RemoveItem(MyCart, id);
        }
        catch (NotFoundException) { MessageBox.Show("Cannot find this product on your cart."); }
        catch (InvalidInputException) { MessageBox.Show("Sorry, your input was wrong. please try again."); }
        catch (DalException) { MessageBox.Show("sorry, something went wrong. please try again."); }
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int id = int.Parse(((Button)sender).Tag.ToString()!);
            MyCart = bl.Cart.AddItem(MyCart, id);
        }
        catch (NotFoundException) { MessageBox.Show("Cannot find this product on your cart."); }
        catch (InvalidInputException) { MessageBox.Show("Sorry, something went wrong. please try again."); }
        catch (SoldOutException) { MessageBox.Show("Sorry, this product was sold out"); }
        catch (DalException) { MessageBox.Show("sorry, something went wrong. please try again."); }
    }
}
