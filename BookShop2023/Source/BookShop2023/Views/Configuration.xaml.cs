using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Page
    {

        private string nProduct = "10";

        public Configuration()
        {
            InitializeComponent();
        }



        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string content = rowsPerPage.Text.ToString();

            // validate content
            //...

            try
            {
                int num = int.Parse(content);
                // Nếu chuyển đổi thành công
                if (num <= 0)
                {
                    // Hiển thị hộp thoại thông báo khi giá trị nhỏ hơn 0
                    MessageBox.Show("Number of product per page must be greater than or equal to 0!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }

            catch (FormatException)
            {
                // Xử lý trường hợp chuỗi không thể chuyển đổi thành số nguyên
                MessageBox.Show("Invalid input. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (content != "")
                AppConfig.SetValue(AppConfig.NumberProductPerPage, content);

            if (lastWindowCheckBox.IsChecked == false)
                AppConfig.SetValue(AppConfig.OpenLastWindow, "0");
            else
                AppConfig.SetValue(AppConfig.OpenLastWindow, "1");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppConfig.GetValue(AppConfig.NumberProductPerPage) != null)
            {
                nProduct = AppConfig.GetValue(AppConfig.NumberProductPerPage);
            }

            rowsPerPage.Text = nProduct;


            if (AppConfig.GetValue(AppConfig.OpenLastWindow) == "0")
                lastWindowCheckBox.IsChecked = false;
            else
                lastWindowCheckBox.IsChecked = true;
        }

        #region Import data
        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            List<Category> categories = new List<Category>();
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

                categories = _cateBUS.getCategoryList();
                // add all option for filter and display if has data
                if (categories.Count > 0)
                {
                    categories.Add(new Category()
                    {
                        ID = categories[^1].ID + 1,
                        Name = "Tất cả"
                    });
                }

                Debug.WriteLine(categories.Count);


                #endregion

                #region Tạo ra từ điển tra ngược từ tên Loại sản phẩm ra ID
                var dictionary = new Dictionary<string, int>();
                foreach (var category in categories)
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
                        PurchasePrice = purchasePrice,
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

                //MessageBox.Show("Count Category: " + categories.Count + " | Product count: " + _ProductBUS.GetTotalProduct());

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
                    if (!System.IO.Directory.Exists(imgSubFolder))
                    {
                        System.IO.Directory.CreateDirectory(imgSubFolder);
                    }

                    foreach (var p in _productsTemp)
                    {
                        var imagePath = p.ImagePath;
                        if (!imagePath.IsNullOrEmpty())
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


                MessageBox.Show("Nhập dữ liệu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
        #endregion
    }
}
