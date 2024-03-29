﻿using BO;
using System;
using System.Windows;
namespace PL.ProductsWindows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private Utils.PageStatus _pageStatus;
    private BlApi.IBl bl = BlApi.Factory.Get();

    #region Properties

    public Utils.PageStatus MyPageStatus { get { return _pageStatus; } }

    public Product MyProduct
    {
        get { return (Product)GetValue(MyProductProperty); }
        set { SetValue(MyProductProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyProductProperty =
        DependencyProperty.Register("MyProduct", typeof(Product), typeof(ProductWindow), new PropertyMetadata(null));

    public Array MyCategories { get {return Enum.GetValues(typeof(BO.Enums.Category)); } }

    #endregion

    #region Ctors

    /// <summary>
    /// Initialize values for adding a product
    /// </summary>
    public ProductWindow()
    {
        _pageStatus = Utils.PageStatus.ADD;
        InitializeComponent();
        MyProduct = new Product();
    }

    /// <summary>
    /// Initialize values for product update/ view
    /// </summary>
    /// <param name="_productId">Product ID</param>
    public ProductWindow(int _productId,Utils.PageStatus status= Utils.PageStatus.DISPLAY)
    {
        _pageStatus = status;
        InitializeComponent();
       

        //Product request from the logical layer by ID
        try { MyProduct = bl.Product.GetProduct(_productId); }

        //If the request is not successful, it will be thrown and the user will be shown an appropriate message
        catch (BO.InvalidInputException) { MessageBox.Show("id cant be negative number"); }
        catch (BO.DalException) { MessageBox.Show("Sorry, we were unable to load the product for you!"); this.Close(); }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
    }

    #endregion


    /// <summary>
    /// Click event of save button
    /// </summary>
    private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
    {
        

        //If the user does not fill in all the fields, he will be shown an appropriate message
        if (MyProduct.Name == "" || MyProduct.ID <= 0 || MyProduct.Price <= 0 || MyProduct.Price < 0 || MyProduct.Category == null)
        {
            MessageBox.Show("Please fill in all fields correctly!");
            return;
        }
  

        //Request the logical layer update/add If there is a problem, the user will be shown an appropriate message
        try
        {
            if (_pageStatus == Utils.PageStatus.ADD)
                bl.Product.AddProduct(MyProduct);
            else
                bl.Product.UpdateProduct(MyProduct);
            this.Close();
        }
        catch(BO.InvalidInputException ) {MessageBox.Show("one of the fields isn't valid"); }
        catch(BO.DalException ) {
            if (_pageStatus == Utils.PageStatus.ADD)
                MessageBox.Show("the product cant added");
            else
                MessageBox.Show("the product cant updated");
        }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }

    }

    /// <summary>
    /// Delete the product
    /// </summary>
    private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Product.DeleteProduct(MyProduct.ID);
            MessageBox.Show("Product was deleted!");
            Close();
        }
        catch (DalException) { MessageBox.Show("Could not delete this product."); }
        catch (BO.InvalidInputException) { MessageBox.Show("Input was invalid. please try again."); }
        catch (BO.ImpossibleException) { MessageBox.Show("This product was already ordered, it cannot be deleted."); }
        catch (Exception) { MessageBox.Show("Sorry, something went wrong. please try again"); }
    }


}
