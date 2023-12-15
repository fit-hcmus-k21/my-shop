using Aspose.Cells;
using Microsoft.Graph;
using Microsoft.Win32;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using ProjectMyShop.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageProduct.xaml
    /// </summary>
    public partial class ManageProduct : Page
    {
        public ManageProduct()
        {
            InitializeComponent();
        }


        private ProductBUS _ProductBus = new ProductBUS();
        ProductViewModel _vm = new ProductViewModel();
        List<Category>? _categories = null;

        int _totalItems = 0;
        int _currentPage = 1;
        int _totalPages = 0;
        int _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));
        int i = 0;

        #region Features: search product with criterias: 
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search_text = searchTextBox.Text;
            if(search_text.Length > 0)
            {
                _currentPage = 1;
                previousButton.IsEnabled = false;

                _vm.SelectedProducts.Clear();
                BindingList<Product> Products = new BindingList<Product>();
                foreach (Product Product in _vm.Products)
                {
                    if(Product.Name.ToLower().Contains(search_text.ToLower()))
                    {
                        Products.Add(Product);
                    }
                }

                _vm.SelectedProducts = Products
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

                if(_vm.SelectedProducts.Count > 0)
                {
                    _currentPage = 1;
                    _totalItems = Products.Count;
                    _totalPages = Products.Count / _rowsPerPage +
                    (Products.Count % _rowsPerPage == 0 ? 0 : 1);
                    ProductsListView.ItemsSource = _vm.SelectedProducts;
                    currentPagingTextBlock.Text = $"{_currentPage}/{_totalPages}";
                }
                if(_totalPages <= 1)
                {
                    nextButton.IsEnabled = false;
                }
            }
            else
            {
                loadProducts();
            }
        }
        #endregion


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            previousButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            _currentPage = 0;
            _totalPages = 0;
            var catBUS = new CategoryBUS();
            var ProductBUS = new ProductBUS();
            _categories = catBUS.getCategoryList();
            categoriesListView.ItemsSource = _categories;
            
            if(_categories.Count > 0)
            {
                loadProducts();
            }

            AppConfig.SetValue(AppConfig.LastWindow, "ManageProduct");
        }

        private void filterRangeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        void loadProducts()
        {
            i = categoriesListView.SelectedIndex;
            if(i < 0)
            {
                i = 0;
            }


            if(_categories == null)
            {
                return;
            }
            _currentPage = 1;
            previousButton.IsEnabled = false;

            _vm.SelectedProducts = _vm.Products
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

            _totalItems = _vm.Products.Count;
            _totalPages = _vm.Products.Count / _rowsPerPage +
                (_vm.Products.Count % _rowsPerPage == 0 ? 0 : 1);
            _currentPage = _totalPages > 0 ? 1 : 0;
            currentPagingTextBlock.Text = $"{_currentPage}/{_totalPages}";

            if(_totalPages > 1)
            {
                nextButton.IsEnabled = true;
            }
            else
            {
                nextButton.IsEnabled=false;
            }

            ProductsListView.ItemsSource = _vm.SelectedProducts;
            
        }

        private void categoriesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            previousButton.IsEnabled = false;
            loadProducts();
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Product)ProductsListView.SelectedItem;
            var screen = new EditProductScreen(p);
            var result = screen.ShowDialog();
            if (result == true)
            {
                var info = screen.EditedProduct;
                p.Name = info.Name;
                p.SellingPrice = info.SellingPrice;
                p.PurchasePrice = info.PurchasePrice;
                p.Description = info.Description;
                p.ImagePath = info.ImagePath;
                p.Quantity = info.Quantity;
                try
                {
                    _ProductBus.updateProduct(p.ID, p);
                    searchTextBox_TextChanged(sender, null);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception here");
                    MessageBox.Show(screen, ex.Message);
                }

                //_vm.Products = _categories[i].Products;
                //_vm.SelectedProducts = _vm.Products
                //    .Skip((_currentPage - 1) * _rowsPerPage)
                //    .Take(_rowsPerPage).ToList();

                //ProductsListView.ItemsSource = _vm.SelectedProducts;
            }
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Product)ProductsListView.SelectedItem;
            var result = MessageBox.Show($"Bạn thật sự muốn xóa sản phẩm {p.Name} ?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                //_Products.Remove(p);
                _vm.Products.Remove(p);
                _ProductBus.removeProduct(p);
                searchTextBox_TextChanged(sender, null);
                //_vm.SelectedProducts.Remove(p);

                //_vm.SelectedProducts = _vm.Products
                //    .Skip((_currentPage - 1) * _rowsPerPage)
                //    .Take(_rowsPerPage).ToList();




                //// Tính toán lại thông số phân trang
                //_totalItems = _vm.Products.Count;
                //_totalPages = _vm.Products.Count / _rowsPerPage +
                //    (_vm.Products.Count % _rowsPerPage == 0 ? 0 : 1);

                //currentPagingTextBlock.Text = $"{_currentPage}/{_totalPages}";
                //if (_currentPage + 1 > _totalPages)
                //{
                //    nextButton.IsEnabled = false;
                //}

                //ProductsListView.ItemsSource = _vm.SelectedProducts;
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            nextButton.IsEnabled = true;
            _currentPage--;
            _vm.SelectedProducts = _vm.Products
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage)
                .ToList();

            // ép cập nhật giao diện
            ProductsListView.ItemsSource = _vm.SelectedProducts;
            currentPagingTextBlock.Text = $"{_currentPage}/{_totalPages}";
            if (_currentPage - 1 < 1)
            {
                previousButton.IsEnabled = false;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            previousButton.IsEnabled = true;
            _currentPage++;
            _vm.SelectedProducts = _vm.Products
                    .Skip((_currentPage - 1) * _rowsPerPage)
                    .Take(_rowsPerPage)
                    .ToList();

            // ép cập nhật giao diện
            ProductsListView.ItemsSource = _vm.SelectedProducts;
            currentPagingTextBlock.Text = $"{_currentPage}/{_totalPages}";

            if (_currentPage + 1 > _totalPages)
            {
                nextButton.IsEnabled = false;
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            _categories = new List<Category>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var screen = new OpenFileDialog();

            if (screen.ShowDialog() == true)
            {
                string filename = screen.FileName;

                var workbook = new Aspose.Cells.Workbook(filename);
                var _ProductBUS = new ProductBUS();
                var _cateBUS = new CategoryBUS();

                var tabs = workbook.Worksheets;

                #region Đọc dữ liệu Categories
                var categoriesTab = tabs[0];

                // bắt đầu từ ô B2
                var col = 'B';
                var row = 2;
                var cell = categoriesTab.Cells[$"{col}{row}"];

                while (cell.Value != null)
                {
                    Category cat = new Category()
                    {
                        Name = cell.StringValue,
                    };
                    _cateBUS.AddCategory(cat);
                    row++;
                    cell = categoriesTab.Cells[$"{col}{row}"];
                }

                _categories = _cateBUS.getCategoryList();
                Debug.WriteLine(_categories.Count);

                categoriesListView.ItemsSource = _categories;

                #endregion

                #region Tạo ra từ điển tra ngược từ tên Loại sản phẩm ra ID
                var dictionary = new Dictionary<string, int>();
                foreach (var category in _categories)
                {
                    dictionary.Add(category.Name, category.ID);
                }
                #endregion

                #region Đọc dữ liệu Products
                // Thông tin các cột: Order - Category - Name - Author - PublishedYear - PurchasePrice - SellingPrice - Quantity - ImageName - Description
                var productsTab = tabs[1];

                // bắt đầu từ ô B2
                col = 'B';
                row = 2;

                cell = productsTab.Cells[$"{col}{row}"];

                while (cell.Value != null)
                {
                    string category = cell.StringValue;
                    string name = productsTab.Cells[$"C{row}"].StringValue;
                    string author = productsTab.Cells[$"D{row}"].StringValue;
                    int publisedYear = productsTab.Cells[$"E{row}"].IntValue;
                    int purchasePrice = productsTab.Cells[$"F{row}"].IntValue;
                    int sellingPrice = productsTab.Cells[$"G{row}"].IntValue;
                    int quantity = productsTab.Cells[$"H{row}"].IntValue;
                    string imageName = productsTab.Cells[$"I{row}"].StringValue;
                    string description = productsTab.Cells[$"J{row}"].StringValue;
                    DateTime uploaddate = DateTime.Now;

                    var product = new Product()
                    {
                        CatID = dictionary.GetValueOrDefault(category),
                        Name = name,
                        Author = author,
                        PublishedYear = publisedYear,
                        PurchasePrice= purchasePrice,
                        SellingPrice = sellingPrice,
                        Quantity = quantity,
                        ImagePath = imageName,
                        Description = description,
                    };
                    _ProductBUS.addProduct(product);
                    row++;
                    cell = productsTab.Cells[$"{col}{row}"];
                }

                #endregion


                loadProducts();
            }
        }
        
        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddProductScreen(_categories!);
            var result = screen.ShowDialog();
            if (result == true)
            {
                var newProduct = screen.newProduct;
                Debug.WriteLine(newProduct.Name);
                var catIndex = screen.catIndex;
                if(catIndex >= 0)
                {
                    try
                    {
                    _ProductBus.addProduct(newProduct);
                    loadProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(screen, ex.Message);
                    }
                }
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            float fromPrice = float.Parse(fromTextbox.Text);
            float toPrice = float.Parse(toTextbox.Text);
            if (fromPrice >= 0 && toPrice > 0 && fromPrice < toPrice)
            {
                _currentPage = 1;
                previousButton.IsEnabled = false;

                _vm.SelectedProducts.Clear();
                BindingList<Product> Products = new BindingList<Product>();
                foreach (Product Product in _vm.Products)
                {
                    if (Product.SellingPrice >= fromPrice && Product.SellingPrice <= toPrice)
                    {
                        Products.Add(Product);
                    }
                }

                if(Products.Count <= 0)
                {
                    MessageBox.Show("Product not found!");
                    return;
                }

                _vm.SelectedProducts = Products
                .Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

                if (_vm.SelectedProducts.Count > 0)
                {
                    _currentPage = 1;
                    _totalItems = Products.Count;
                    _totalPages = Products.Count / _rowsPerPage +
                    (Products.Count % _rowsPerPage == 0 ? 0 : 1);
                    ProductsListView.ItemsSource = _vm.SelectedProducts;
                    currentPagingTextBlock.Text = $"{_currentPage}/{_totalPages}";
                }
                if (_totalPages <= 1)
                {
                    nextButton.IsEnabled = false;
                }
            }
            else
            {
               
                MessageBox.Show("Product not found!");
            }
        }
    }
}
