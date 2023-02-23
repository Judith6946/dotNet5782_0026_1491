using BO;
using PL.ProductsWindows;
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

    public Array MyCategories
    {
        get
        {
            return Enum.GetValues(typeof(BO.Enums.Category));
        }

    }

    public CatalogWindow()
    {
        InitializeComponent();

        myCart = new() { TotalPrice = 0, ItemsList = new() };
        try
        {
            //Requests a request from the logical layer to fetch all the products and displays them
            var temp = bl.Product.GetProductItems(myCart);
            MyProductItems = temp == null ? new() : new(temp);
        }
        catch (DalException) { MessageBox.Show("Sorry, something went wrong, please try again."); }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }

    }

    /// <summary>
    /// Click event of remove buttons
    /// </summary>
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
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
    }

    /// <summary>
    /// Click event of add button.
    /// </summary>
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
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
    }

    /// <summary>
    /// Click event of 'show cart' button.
    /// </summary>
    private void btnShowCart_Click(object sender, RoutedEventArgs e)
    {
        var cartWindow = new CartWindow(myCart);
        if (cartWindow.ShowDialog() == true)
            this.Close();
        else
        {
            myCart = cartWindow.MyCart;
            var temp = bl.Product.GetProductItems(myCart);
            MyProductItems = temp == null ? new() : new(temp);
        }
    }

    /// <summary>
    /// Displays products filtered by selected category
    /// </summary>
    /// <param name="sender">AttributeSelector</param>
    /// <param name="e">more information about AttributeSelector</param>
    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            //If there is a selected category, it will display products according to it
            if (AttributeSelector.SelectedIndex != -1)
            {
                //the selected category
                BO.Enums.Category selectedCategory = (BO.Enums.Category)((ComboBox)sender).SelectedItem;

                //Request all products by category from the logical layer
                MyProductItems = new(bl.Product.GetProductItemsByFunc(myCart, x => x.Category == selectedCategory));
            }
        }
        catch (DalException) { MessageBox.Show("Something went wrong. please try again."); }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }

    }

    /// <summary>
    /// Resets the selection and displays all products without filtering
    /// </summary>
    /// <param name="sender">btnClearAll</param>
    /// <param name="e">more information about btnClearAll</param>
    private void btnClearAll_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //Requests a request from the logical layer to fetch all the products and displays them
            var temp = bl.Product.GetProductItems(myCart);
            MyProductItems = temp == null ? new() : new(temp);
            AttributeSelector.SelectedIndex = -1;
        }
        catch (DalException) { MessageBox.Show("Sorry, something went wrong, please try again."); }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }

    }

    /// <summary>
    /// Show product details
    /// </summary>
    private void productsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (((ProductItem)((ListView)sender).SelectedItem) != null)
        {
            int id = ((ProductItem)((ListView)sender).SelectedItem).ID;
            new ProductWindow(id, Utils.PageStatus.DISPLAY).ShowDialog();
        }
       
    }
}
