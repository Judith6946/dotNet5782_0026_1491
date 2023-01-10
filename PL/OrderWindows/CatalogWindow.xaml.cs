using BO;
using PL.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
/// Interaction logic for CatalogWindow.xaml
/// </summary>
public partial class CatalogWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    private BO.Cart myCart;

    public ObservableCollection<ProductItem?> MyProductItems
    {
        get { return (ObservableCollection<ProductItem?>)GetValue(MyProductItemsProperty); }
        set { SetValue(MyProductItemsProperty, value); }
    }


    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyProductItemsProperty =
        DependencyProperty.Register("MyProductItems", typeof(ObservableCollection<ProductItem>), typeof(CatalogWindow), new PropertyMetadata(null));


    public CatalogWindow()
    {
        InitializeComponent();

        myCart = new() { TotalPrice = 0, ItemsList = new() };

        //Requests a request from the logical layer to fetch all the products and displays them
        var temp = bl.Product.GetProductItems(myCart);
        MyProductItems = temp == null ? new() : new(temp);
    }

    private void Remove_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int id = int.Parse(((Button)sender).Tag.ToString()!);
            myCart = bl.Cart.RemoveItem(myCart, id);
            var temp = bl.Product.GetProductItems(myCart);
            MyProductItems = temp == null ? new() : new(temp);
        }
        catch (NotFoundException) { MessageBox.Show("Cannot find this product on your cart."); }
        catch (InvalidInputException) { MessageBox.Show("Sorry, something went wrong. please try again."); }
    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int id = int.Parse(((Button)sender).Tag.ToString()!);
            myCart = bl.Cart.AddItem(myCart, id);
            var temp = bl.Product.GetProductItems(myCart);
            MyProductItems = temp == null ? new() : new(temp);
        }
        catch (NotFoundException) { MessageBox.Show("Cannot find this product on your cart."); }
        catch (InvalidInputException) { MessageBox.Show("Sorry, something went wrong. please try again."); }
        catch (SoldOutException) { MessageBox.Show("Sorry, this product was sold out"); }
    }

    private void btnShowCart_Click(object sender, RoutedEventArgs e)
    {
        var cartWindow = new CartWindow(myCart);
        if (cartWindow.ShowDialog() == true)
            this.Close();
    }
}
