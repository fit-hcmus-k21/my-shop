using ProjectMyShop.DTO;
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

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for AddQuantityScreen.xaml
    /// </summary>
    public partial class AddQuantityScreen : Window
    {
        public Product newProduct { get; set; }
        public AddQuantityScreen(Product p)
        {
            InitializeComponent();
            newProduct = (Product)p.Clone();
            Debug.WriteLine(newProduct.Description);
            this.DataContext = newProduct;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
