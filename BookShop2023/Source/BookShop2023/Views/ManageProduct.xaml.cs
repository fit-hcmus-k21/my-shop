using Aspose.Cells;
using Aspose.Cells.Drawing;
using BookShop2023.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using ProjectMyShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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

        public ObservableCollection<ComboBoxItem> Items { get; set; }



        public ManageProduct()
        {
            InitializeComponent();

            #region Initialize and populate the Items collection
            Items = new ObservableCollection<ComboBoxItem>
            {
            new ComboBoxItem { Content = "Mặc định (ID)", Tag = "ID" },
            new ComboBoxItem { Content = "Theo thứ tự Alphabetical", Tag = "Name" },
            new ComboBoxItem { Content = "Theo giá tăng dần", Tag = "PriceAsc" },
            new ComboBoxItem { Content = "Theo giá giảm dần", Tag = "PriceDesc" },
            new ComboBoxItem { Content = "Mới nhất trước tiên", Tag = "NewestFirst" },
            new ComboBoxItem { Content = "Cũ nhất trước tiên", Tag = "OldestFirst" },
            };

            // Set the DataContext to this instance (or to a ViewModel)
            sortingComboBox.ItemsSource = Items;
            sortingComboBox.SelectedIndex = 0;
            #endregion
        }

        #region Khai báo biến...


        private ProductBUS _ProductBus = new ProductBUS();
        List<Category>? _categories = null;
        BindingList<Product> _products = new BindingList<Product>();

        int _totalItems = 0;
        int _currentPage = 1;
        int _totalPages = 0;
        int _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));


        #endregion

        #region Features: search product with criterias: 
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search_text = searchTextBox.Text;
            _ProductBus.setSearchKeyword(search_text);
            loadProducts();
        }
        #endregion


        #region page loaded
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Page Loaded - Manage Product");
            previousButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            _currentPage = 0;
            _totalPages = 0;
            var catBUS = new CategoryBUS();
            var ProductBUS = new ProductBUS();
            _categories = catBUS.getCategoryList();

            // add all option for filter and display if has data
            if (_categories.Count > 0)
            {
                _categories.Add(new Category()
                {
                    ID = _categories[^1].ID + 1,
                    Name = "Tất cả"
                });
            }

            categoriesComboBox.ItemsSource = _categories;
            categoriesComboBox.SelectedIndex = _categories.Count - 1;
            
            if(_categories.Count > 0)
            {

                loadProducts();
            } 

            AppConfig.SetValue(AppConfig.LastWindow, "ManageProduct");

            sortingComboBox.SelectedIndex = 0;

        }
        #endregion


        #region load products

        void loadProducts()
        {

            List<Product> listProducts = _ProductBus.loadAllProducts()
                                                      .Skip((_currentPage - 1) * _rowsPerPage)
                                                      .Take(_rowsPerPage).ToList();

            BindingList<Product> bindingList = new BindingList<Product>(listProducts);

            _products = bindingList;

            ProductsListView.ItemsSource = _products;

            devidePaging();



        }
        #endregion

        #region Tính toán phân trang
        public void devidePaging()
        {
            int count = _ProductBus.loadAllProducts().Count;

            if (count != _totalItems)
            {
                _totalItems = count;
                _totalPages = (_totalItems / _rowsPerPage) + (((_totalItems % _rowsPerPage) == 0) ? 0 : 1);

                // tạo thông tin phân trang
                var pageInfos = new List<object>();

                for (int i = 1; i <= _totalPages; i++)
                {
                    pageInfos.Add(new
                    {
                        Page = i,
                        Total = _totalPages
                    });
                };

                pagingComboBox.ItemsSource = pageInfos;
                pagingComboBox.SelectedIndex = 0;

            }

            if (_totalPages > 1)
            {
                nextButton.IsEnabled = true;
            }
            else
            {
                nextButton.IsEnabled = false;
            }
        }
        #endregion

        #region Xử lý sự kiện click menu item của sản phẩm trong listView | xem chi tiết, chỉnh sửa, xóa sản phẩm
        private void viewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Product)ProductsListView.SelectedItem;
            var catName = "";
            foreach (var cat in _categories)
            {
                if (cat.ID == p.CatID)
                {
                    catName = cat.Name;
                    break;
                }
            }

            var screen = new ViewDetailProduct(p, catName);
            screen.ShowDialog();
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Product)ProductsListView.SelectedItem;
            var screen = new EditProductScreen(p, _categories);
            var result = screen.ShowDialog();
            if (result == true)
            {
                var info = screen.EditedProduct;
                p.Name = info.Name;
                p.CatID = info.CatID;
                p.Author = info.Author;
                p.PublishedYear = info.PublishedYear;
                p.SellingPrice = info.SellingPrice;
                p.PurchasePrice = info.PurchasePrice;
                p.Description = info.Description;
                p.ImagePath = info.ImagePath;
                p.Quantity = info.Quantity;
                try
                {
                    _ProductBus.updateProduct(p.ID, p);
                    loadProducts();
                    MessageBox.Show($"Đã cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception here");
                    MessageBox.Show(screen, ex.Message);
                }

            }
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Product)ProductsListView.SelectedItem;
            var result = MessageBox.Show($"Bạn thật sự muốn xóa sản phẩm {p.Name} ?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {
                
                _ProductBus.removeProduct(p);
                loadProducts();
                
                MessageBox.Show($"Đã xóa sản phẩm {p.Name} thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Chuyển trang trước, trang sau khi click button
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex > 0)
            {
                pagingComboBox.SelectedIndex--;
            }

           
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex < _totalPages - 1)
            {
                pagingComboBox.SelectedIndex++;
            }

        }
        #endregion

        #region Import data
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
                var _productsTemp = new List<Product>();

                var tabs = workbook.Worksheets;

                #region Đọc dữ liệu Categories
                var categoriesTab = tabs[0];
                Debug.WriteLine("Sheet name: " + categoriesTab.Name);


                // bắt đầu từ ô B2
                var col = 'B';
                int row = 2;
                var cell = categoriesTab.Cells[$"{col}{row}"];
                Debug.WriteLine("Cell value: " + cell.Value);


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
                // add all option for filter and display if has data
                if (_categories.Count > 0)
                {
                    _categories.Add(new Category()
                    {
                        ID = _categories[^1].ID + 1,
                        Name = "Tất cả"
                    });
                }

                Debug.WriteLine(_categories.Count);

                categoriesComboBox.ItemsSource = _categories;

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
                Debug.WriteLine("Sheet name: " + productsTab.Name);


                // bắt đầu từ ô B2
                col = 'B';
                row = 2;

                cell = productsTab.Cells[$"{col}{row}"];
                Debug.WriteLine("Cell value: " + cell.Value);

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
                    _productsTemp.Add(product);
                    row++;
                    cell = productsTab.Cells[$"{col}{row}"];
                }

                #endregion

                //MessageBox.Show("Count Category: " + _categories.Count + " | Product count: " + _ProductBUS.GetTotalProduct());

                #region Copy ảnh vào thư mục
                // xác định thư mục chứa file excel
                var excelBaseFolder = new FileInfo(filename);

                // lấy thư mục chứa ảnh cùng với file excel
                var productImagesFolder = excelBaseFolder.Directory + @"\Images";

                // Nếu có thì import ảnh
                if (System.IO.Directory.Exists(productImagesFolder))
                {
                    // Lấy thư mục ảnh hiện hành
                    var exeFolder = AppDomain.CurrentDomain.BaseDirectory;
                    var imgSubFolder = exeFolder + "Images";

                    // tạo thư mục để chứa ảnh
                    if (! System.IO.Directory.Exists(imgSubFolder))
                    {
                        System.IO.Directory.CreateDirectory(imgSubFolder);
                    }

                    foreach (var p in _productsTemp) 
                    {
                        var imagePath = p.ImagePath;
                        if (! imagePath.IsNullOrEmpty())
                        {
                            var sourceImg = productImagesFolder + @"\" + imagePath;
                            var sourceInfo = new FileInfo(sourceImg);
                            var extension = sourceInfo.Extension;

                            // phát sinh id duy nhất toàn hệ thống
                            var newName = Guid.NewGuid() + extension;
                            var destination = $"{imgSubFolder}\\{newName}";

                            if (System.IO.File.Exists(sourceImg))
                            {
                                // Thực hiện sao chép và cập nhật
                                System.IO.File.Copy(sourceImg, destination);

                            }
                            else
                            {
                                // Xử lý trường hợp tệp không tồn tại
                                Debug.WriteLine($"Source image does not exist: {sourceImg}");
                            }


                            // cập nhật tên mới để lưu vào db, (Images/...jpg..)
                            var relativePath = "Images" + @"\" + newName;

                            p.ImagePath = relativePath;

                            _ProductBUS.updateProduct(p);

                        }
                    }
                }

                #endregion

                loadProducts();

                //  select combobox all
                if (_categories.Count > 0) 
                {
                    categoriesComboBox.ItemsSource = _categories;
                    categoriesComboBox.SelectedIndex = _categories.Count - 1;
                }

            }
        }
        #endregion

        #region Thêm sản phẩm mới
        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddProductScreen(_categories!);
            var result = screen.ShowDialog();
            if (result == true)
            {
                var newProduct = screen.newProduct;
                Debug.WriteLine(newProduct.Name);
                
                    try
                    {
                    _ProductBus.addProduct(newProduct);
                    loadProducts();
                    MessageBox.Show($"Thêm sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show(screen, ex.Message);
                    }
                
            }
        }
        #endregion

        #region Lọc giá sản phẩm
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // validate input
            //...

            string min = minPriceTextbox.Text;
            string max = maxPriceTextbox.Text;
            if (min.IsNullOrEmpty() && max.IsNullOrEmpty())
            {
                _ProductBus.removeFilterPrice();
                loadProducts();
                return;
            }


            try
            {
                int minPrice = int.Parse(minPriceTextbox.Text);
                int maxPrice = int.Parse(maxPriceTextbox.Text);
                if (minPrice >= 0 && maxPrice > 0 && minPrice < maxPrice)
                {
                    _ProductBus.setFilterPrice(minPrice, maxPrice);
                    loadProducts();

                }
                else
                {
                    MessageBox.Show("Invalid price input for filter !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid price input for filter !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Xử lý lọc theo thể loại sản phẩm
        private void categoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = categoriesComboBox.SelectedIndex;
            // MessageBox.Show("Category choose: " + _categories[id].Name );
            if (!_categories[id].Name.Equals("Tất cả"))
            {
                int catID = _categories[id].ID;
                _ProductBus.setFilterCat(catID);
            }
            else
            {
                _ProductBus.setFilterCat(-1);
            }
            loadProducts();
        }
        #endregion

        #region Thay đổi trang xem : click pagingComboBox
        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic info = pagingComboBox.SelectedItem;
            if (info != null)
            {
                if (info?.Page != _currentPage)
                {
                    _currentPage = info?.Page;
                    loadProducts();
                }
            }

            if (_currentPage == _totalPages)
            {
                nextButton.IsEnabled = false;
            } else
            {
                nextButton.IsEnabled = true;
            }
            if (_currentPage == 1 )
            {
                previousButton.IsEnabled = false;
            } else
            {
                previousButton.IsEnabled = true;
            }
        }
        #endregion

        #region Xử lý giao diện khi người dùng tương tác
        private void pagingComboBox_DropDownOpened(object sender, EventArgs e)
        {
            // Thay đổi màu nền khi dropdown mở
            pagingComboBox.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void pagingComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Thay đổi màu nền khi dropdown đóng
            pagingComboBox.Background = new SolidColorBrush(Colors.White);
        }



        private void categoriesComboBox_DropDownOpened(object sender, EventArgs e)
        {
            // Thay đổi màu nền khi dropdown mở
            pagingComboBox.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void categoriesComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Thay đổi màu nền khi dropdown đóng
            pagingComboBox.Background = new SolidColorBrush(Colors.White);
        }

        private void pagingComboBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                // Toggle the IsDropDownOpen state
                pagingComboBox.IsDropDownOpen = !pagingComboBox.IsDropDownOpen;
            }
        }

        private void categoriesComboBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                // Toggle the IsDropDownOpen state
                categoriesComboBox.IsDropDownOpen = !categoriesComboBox.IsDropDownOpen;
            }
        }
        #endregion


        #region Xử lý các phương thức sắp xếp
        private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected item from the ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)sortingComboBox.SelectedItem;

            // Get the Tag value, which contains the sorting criteria
            string sortingCriteria = selectedItem?.Tag?.ToString();

            // Call a method to apply sorting based on the selected criteria
            ApplySorting(sortingCriteria);
            _ProductBus.setSortingCriteriaQuery(_sortingCriteriaQuery);

            loadProducts();
        }

        string _sortingCriteriaQuery = " ORDER BY ID ";
        public enum SortingCriteria
        {
            Default,         // Default sorting (by ID)
            Alphabetical,    // Sort by name alphabetically
            PriceAscending,  // Sort by price ascending
            PriceDescending, // Sort by price descending
            NewestFirst,     // Sort by newest first
            OldestFirst      // Sort by oldest first
        }


        private void ApplySorting(string sortingCriteria)
        {
            switch (sortingCriteria)
            {
                case "Name":
                    // Sort by name alphabetically
                    _sortingCriteriaQuery = " ORDER BY Name ASC ";

                    break;

                case "PriceAsc":
                    // Sort by price ascending
                    _sortingCriteriaQuery = " ORDER BY SellingPrice ASC ";


                    break;

                case "PriceDesc":
                    // Sort by price descending
                    _sortingCriteriaQuery = " ORDER BY SellingPrice DESC ";


                    break;

                case "NewestFirst":
                    // Sort by newest first
                    _sortingCriteriaQuery = " ORDER BY PublishedYear DESC ";


                    break;

                case "OldestFirst":
                    // Sort by oldest first
                    _sortingCriteriaQuery = " ORDER BY PublishedYear ASC ";


                    break;

                default:
                    // Default sorting by ID
                    _sortingCriteriaQuery = " ORDER BY ID ";


                    break;
            }

          
        }

        private void sortingComboBox_DropDownOpened(object sender, EventArgs e)
        {
            // Thay đổi màu nền khi dropdown mở
            pagingComboBox.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void sortingComboBox_DropDownClosed(object sender, EventArgs e)
        {
            // Thay đổi màu nền khi dropdown đóng
            pagingComboBox.Background = new SolidColorBrush(Colors.White);
        }

        private void sortingComboBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                // Toggle the IsDropDownOpen state
                sortingComboBox.IsDropDownOpen = !sortingComboBox.IsDropDownOpen;
            }
        }
        #endregion
    }
}
