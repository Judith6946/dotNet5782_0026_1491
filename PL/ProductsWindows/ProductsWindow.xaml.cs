using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
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

namespace PL.ProductsWindows
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {

        private IBl bl = new BL();

        public ProductsWindow()
        {
            InitializeComponent();
            productsListView.ItemsSource = bl.Product.GetProducts();
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category selectedCategory = (BO.Enums.Category)((ComboBox)sender).SelectedItem;
            productsListView.ItemsSource = bl.Product.GetProductsByFunc(x => x.Category == selectedCategory);
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            productsListView.ItemsSource = bl.Product.GetProducts();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductWindow();
            window.Show();
            window.Closing += Window_Closing;
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            productsListView.ItemsSource = bl.Product.GetProducts();
        }

        
        private void productsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var window = new ProductWindow(((ProductForList)((ListView)sender).SelectedItem).ID);
            window.Show();
            window.Closing += Window_Closing;
        }
    }
}
