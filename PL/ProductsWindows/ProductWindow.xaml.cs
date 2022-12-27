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
    private int productId;

    /// <summary>
    /// Initialize values for adding a product
    /// </summary>
    public ProductWindow()
    {
        InitializeComponent();
        _pageStatus = Utils.PageStatus.ADD;
        cmbProductCategory.ItemsSource= Enum.GetValues(typeof(BO.Enums.Category));
    }

    /// <summary>
    /// Initialize values for product update
    /// </summary>
    /// <param name="_productId">Product ID to update</param>
    public ProductWindow(int _productId)
    {
        InitializeComponent();
        _pageStatus = Utils.PageStatus.UPDATE;
        cmbProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        productId = _productId;
        insertValues();
    }

    /// <summary>
    /// Initializes the product fields according to the received ID
    /// </summary>
    private void insertValues()
    {
        //Product request from the logical layer by ID
        BO.Product product=new BO.Product();
        try { product = bl.Product.GetProduct(productId);}

        //If the request is not successful, it will be thrown and the user will be shown an appropriate message
        catch (BO.InvalidInputException){ MessageBox.Show("id cant be negative number"); }
        catch (BO.DalException) { MessageBox.Show("Sorry, we were unable to load the product for you!");this.Close(); }

        //The initialization of the values according to the variables
        txtProductId.Text = product.ID.ToString();
        txtProductName.Text = product.Name;
        txtProductPrice.Text=product.Price.ToString();
        txtInStock.Text = product.InStock.ToString();
        cmbProductCategory.SelectedItem=product.Category;
        txtProductId.IsEnabled = false;
    }

    private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
    {
        int id, inStock;
        double price;

        //If the user does not fill in all the fields, he will be shown an appropriate message
        if (txtProductId.Text == "" || txtProductName.Text == "" || txtProductPrice.Text == "" || txtInStock.Text == "" || cmbProductCategory.SelectedIndex == -1)
        {
            MessageBox.Show("Please fill in all fields!");
            return;
        }

        //If one of the inputs is invalid, an appropriate message will be displayed to the user
        if (!int.TryParse(txtProductId.Text, out id)) { MessageBox.Show("Invalid ID"); return; };
        if (!double.TryParse(txtProductPrice.Text, out price)) { MessageBox.Show("Invalid price"); return; };
        if (!int.TryParse(txtInStock.Text, out inStock)) { MessageBox.Show("Invalid stock quantity"); return; };

        //Creating a logical layer product based on the data
        BO.Product product = new BO.Product()
        {
            ID = id,
            Name=txtProductName.Text,
            Price=price,
            InStock= inStock,
            Category= (BO.Enums.Category?)cmbProductCategory.SelectedItem
        };

        //Request the logical layer update/add If there is a problem, the user will be shown an appropriate message
        try
        {
            if (_pageStatus == Utils.PageStatus.ADD)
                bl.Product.AddProduct(product);
            else
                bl.Product.UpdateProduct(product);
            this.Close();
        }
        catch(BO.InvalidInputException ) {
            MessageBox.Show("one of the fields isn't valid"); }
        catch(BO.DalException ) {
            if (_pageStatus == Utils.PageStatus.ADD)
                MessageBox.Show("the product cant added");
            else
                MessageBox.Show("the product cant updated");
        }   
    }
}
