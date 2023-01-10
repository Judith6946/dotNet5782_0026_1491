using PL.OrderWindows;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       private BlApi.IBl bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            new CatalogWindow().Show();
        }

        private void btnFollowOrder_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if(!int.TryParse(txtOrderId.Text,out id))
            {
                MessageBox.Show("Invalid ID");
                return;
            }
            new OrderTrackingWindow(id).Show();
        }
    }
}
