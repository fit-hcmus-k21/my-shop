using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddProductOrder.xaml
    /// </summary>
    public partial class AddProductOrder : Window
    {
        ProductBUS _ProductBus;
        CategoryBUS _categoryBus;
        List<Category> _categories;
        List<Product> _selectedProducts;
        public OrderDetail OrderDetail;


        public AddProductOrder(OrderDetail OrderDetail)
        {
            InitializeComponent();
            this.OrderDetail = (OrderDetail)OrderDetail.Clone();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int i = ProductListView.SelectedIndex;
            if (i != -1)
            {
                //OrderDetail.Product = _selectedProducts[i];
            }
            OrderDetail.Quantity = int.Parse(QuantityTextBox.Text);
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ProductBus = new ProductBUS();
            _categoryBus = new CategoryBUS();

            _categories = _categoryBus.getCategoryList();

            categoryCombobox.ItemsSource = _categories;

            categoryCombobox.SelectedIndex = 0;

            if (categoryCombobox.SelectedIndex >= 0)
            {
                _selectedProducts = _ProductBus.getProductsAccordingToSpecificCategory(_categories[categoryCombobox.SelectedIndex].ID);
                ProductListView.ItemsSource = _selectedProducts;
            }

            DataContext = OrderDetail;
        }

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryCombobox.SelectedIndex >= 0)
            {
                _selectedProducts = _ProductBus.getProductsAccordingToSpecificCategory(_categories[categoryCombobox.SelectedIndex].ID);
                ProductListView.ItemsSource = _selectedProducts;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            var QuantityTextBox = e.OriginalSource as TextBox;

            if (QuantityTextBox != null)
            {
                if (QuantityTextBox.Text == "")
                {
                    QuantityTextBox.Text = "0";
                }
                //else if ((int.Parse(QuantityTextBox.Text)
                //    > OrderDetail.Product.Quantity))
                //{
                //    QuantityTextBox.Text = QuantityTextBox.Text.Remove(QuantityTextBox.Text.Length - 1);

                //    if (int.Parse(QuantityTextBox.Text)
                //        > OrderDetail.Product.Quantity)
                //        QuantityTextBox.Text = OrderDetail.Product.Quantity.ToString();
                //}
            }

        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex _regex = new Regex("[^0-9]+$"); //regex that matches disallowed text
            return _regex.IsMatch(text);
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = ProductListView.SelectedIndex;
            if (i != -1)
            {
                //OrderDetail.ProductID = _selectedProducts[(int)i];
                OrderDetail.Quantity = 0;

                //ProductTextBox.Text = OrderDetail.ProductName;
                QuantityTextBox.Text = OrderDetail.Quantity.ToString();
            }
        }
    }
}
