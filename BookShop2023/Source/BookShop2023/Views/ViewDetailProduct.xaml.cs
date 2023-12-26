using ProjectMyShop.DTO;
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

namespace BookShop2023.Views
{
    /// <summary>
    /// Interaction logic for ViewDetailProduct.xaml
    /// </summary>
    public partial class ViewDetailProduct : Window
    {
        public Product CurrentProduct { get; set; }

        public ViewDetailProduct(Product p, String CatName)
        {
            InitializeComponent();
            CurrentProduct = (Product)p.Clone();
            this.DataContext = CurrentProduct;
            catNameTextBox.Text = CatName;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
