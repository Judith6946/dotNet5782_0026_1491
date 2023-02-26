using BO;
using PL.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ProductsWindows;

/// <summary>
/// Interaction logic for ProductsWindow.xaml
/// </summary>
public partial class ProductsWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();

    public ObservableCollection<ProductForList?> MyProducts
    {
        get { return (ObservableCollection<ProductForList?>)GetValue(MyProductsProperty); }
        set { SetValue(MyProductsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyProductsProperty =
        DependencyProperty.Register("MyProducts", typeof(ObservableCollection<ProductForList>), typeof(ProductsWindow), new PropertyMetadata(null));


    public Array MyCategories
    {
        get
        {
            return Enum.GetValues(typeof(BO.Enums.Category));
        }
        
    }



    /// <summary>
    /// Initializes the entire form
    /// </summary>
    public ProductsWindow()
    {
        InitializeComponent();
        updateProducts();
    }

    /// <summary>
    /// Update MyProducts property.
    /// </summary>
    private void updateProducts()
    {
        try
        {
            //Requests a request from the logical layer to fetch all the products and displays them
            var temp = bl.Product.GetProducts();
            MyProducts = temp == null ? new() : new(temp);
        }
        catch (DalException) { MessageBox.Show("could not load products information, please try again later."); }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
    }


    /// <summary>
    /// Displays products filtered by selected category
    /// </summary>
    /// <param name="sender">AttributeSelector</param>
    /// <param name="e">more information about AttributeSelector</param>
    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //If there is a selected category, it will display products according to it
        if (AttributeSelector.SelectedIndex != -1)
        {
            //the selected category
            BO.Enums.Category selectedCategory = (BO.Enums.Category)((ComboBox)sender).SelectedItem;
            try
            {
                //Request all products by category from the logical layer
                var temp = bl.Product.GetProductsByFunc(x => x.Category == selectedCategory);
                MyProducts = temp == null ? new() : new(temp);
            }
            catch (DalException) { MessageBox.Show("Could not load products info."); }
            catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
        }
    }

    /// <summary>
    /// Resets the selection and displays all products without filtering
    /// </summary>
    /// <param name="sender">btnClearAll</param>
    /// <param name="e">more information about btnClearAll</param>
    private void btnClearAll_Click(object sender, RoutedEventArgs e)
    {
        updateProducts();
        AttributeSelector.SelectedIndex = -1;
    }

    /// <summary>
    /// When clicked, an add form will be displayed
    /// </summary>
    /// <param name="sender">btnAddProduct</param>
    /// <param name="e">more information btnAddProduct</param>
    private void btnAddProduct_Click(object sender, RoutedEventArgs e)
    {
        var window = new ProductWindow();
        window.Show();
        window.Closing += Window_Closing;
    }

    /// <summary>
    /// Updating the list
    /// </summary>
    /// <param name="sender"> Window_Closing</param>
    /// <param name="e">more information about  Window_Closing</param>
    private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        updateProducts();
    }

    /// <summary>
    /// When you double click on a product, a product update form will be displayed
    /// </summary>
    /// <param name="sender">productsListView</param>
    /// <param name="e">more information about productsListView</param>
    private void productsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (((ProductForList)((ListView)sender).SelectedItem) != null)
        {
            var window = new ProductWindow(((ProductForList)((ListView)sender).SelectedItem).ID, Utils.PageStatus.EDIT);
            window.Show();
            window.Closing += Window_Closing;
        }
    }

}
