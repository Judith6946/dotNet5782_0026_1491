using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PL.ProductsWindows;


/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private Utils.PageStatus _pageStatus;
    private IBl bl = new BL();
    private int productId;

    public ProductWindow()
    {
        InitializeComponent();
        _pageStatus = Utils.PageStatus.ADD;
        cmbProductCategory.ItemsSource= Enum.GetValues(typeof(BO.Enums.Category));
    }

    public ProductWindow(int _productId)
    {
        InitializeComponent();
        _pageStatus = Utils.PageStatus.UPDATE;
        cmbProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        productId = _productId;
        insertValues();
    }

    private void insertValues()
    {
        BO.Product product=bl.Product.GetProduct(productId);
        txtProductId.Text = product.ID.ToString();
        txtProductName.Text = product.Name;
        txtProductPrice.Text=product.Price.ToString();
        txtInStock.Text = product.InStock.ToString();
        cmbProductCategory.SelectedItem=product.Category;
        txtProductId.IsEnabled = false;
    }

    private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
    {
        BO.Product product = new BO.Product()
        {
            ID=int.Parse(txtProductId.Text),
            Name=txtProductName.Text,
            Price=double.Parse(txtProductPrice.Text),
            InStock= int.Parse(txtInStock.Text),
            Category= (BO.Enums.Category?)cmbProductCategory.SelectedItem
        };
        try
        {
            if (_pageStatus == Utils.PageStatus.ADD)
                bl.Product.AddProduct(product);
            else
                bl.Product.UpdateProduct(product);
            this.Close();
        }
        catch(Exception ex)
        {

        }
        
    }
}
