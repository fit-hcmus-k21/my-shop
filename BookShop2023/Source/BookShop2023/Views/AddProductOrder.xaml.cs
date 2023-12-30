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
        public OrderDetail OrderDetail { get; set; }

        private Boolean didSelectProduct = false;
        private int productStock = 0;


        public AddProductOrder()
        {
            InitializeComponent();
            OrderDetail = new OrderDetail();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            #region validate and check input here

            if (! didSelectProduct)
            {
                MessageBox.Show("Hãy chọn sản phẩm cho đơn hàng!", "Chưa chọn sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            OrderDetail.Quantity = int.Parse(QuantityTextBox.Text);
            OrderDetail.Total = OrderDetail.Price * OrderDetail.Quantity;

            #endregion


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
            _categories.Add(new Category
            {
                ID = _categories[^1].ID + 1,
                Name = "Tất cả"
            });

            categoryCombobox.ItemsSource = _categories;

            categoryCombobox.SelectedIndex = _categories.Count - 1;

            _selectedProducts = _ProductBus.loadAllProductsWithQuantity();
            ProductListView.ItemsSource = _selectedProducts;


            DataContext = OrderDetail;
        }

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category selected = categoryCombobox.SelectedItem as Category;
            
            if (selected != null && selected.ID >= 0 && ! selected.Name.Equals("Tất cả" ))
            {
                _selectedProducts = _ProductBus.getProductsAccordingToSpecificCategory(_categories[categoryCombobox.SelectedIndex].ID);
                ProductListView.ItemsSource = _selectedProducts;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            var QuantityTextBox = e.OriginalSource as TextBox;

            if (QuantityTextBox != null && ! QuantityTextBox.Text.Equals(""))
            {
                int quantity = int.Parse(QuantityTextBox.Text);
              if (quantity == 0 || quantity > productStock )
                {
                    MessageBox.Show("Số lượng sản phẩm vượt quá số lượng tồn kho !", "Số lượng sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
                    QuantityTextBox.Text = "1";
                }
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
                OrderDetail.ProductID = _selectedProducts[(int)i].ID;
                OrderDetail.Quantity = 1;   // default 
                OrderDetail.Price = _selectedProducts[(int)i].PurchasePrice;
                productStock = _selectedProducts[(int)i].Quantity;

                didSelectProduct = true;


                ProductTextBox.Text = _selectedProducts[(int)i].Name;
                QuantityTextBox.Text = "1";
            }
        }
    }
}
