using Microsoft.Win32;
using ProjectMyShop.DTO;
using ProjectMyShop.BUS;
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
using System.IO;
using Microsoft.IdentityModel.Tokens;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for AddProductScreen.xaml
    /// </summary>
    public partial class AddProductScreen : Window
    {
        public Product newProduct { get; set; }
        public List<Category> catListCopy;

        public AddProductScreen(List<Category> categories)
        {
            InitializeComponent();
            newProduct = new Product();
            this.DataContext = newProduct;
            newProduct.CatID = -1;

            // Tạo một bản sao của danh sách để thực hiện xóa
            catListCopy = new List<Category>(categories);

            // if list cat has All opt, then remove, just for display in manage product
            if (catListCopy[^1] != null && catListCopy[^1].Name.Equals("Tất cả"))
            {
                catListCopy.Remove(catListCopy[^1]);
            }

            // add option 'Chọn thể loại'
            catListCopy.Add(new Category
            {
                ID = catListCopy[^1].ID,
                Name = "----Chọn thể loại----"
            });

            categoryCombobox.ItemsSource = catListCopy;
            categoryCombobox.SelectedIndex = catListCopy.Count - 1;

            // display template image for product
            newProduct.ImagePath = "Assets/Images/template.jpg";
            
        }

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newProduct.CatID = ((Category)categoryCombobox.SelectedItem).ID;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Check validity:

            // kiểm tra đã chọn thể loại chưa
            if (newProduct.CatID == -1 || newProduct.CatID == catListCopy.Count - 1)
            {
                MessageBox.Show($"Vui lòng chọn thể loại sản phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }

            // kiểm tra đã chọn ảnh sản phẩm chưa
            if (newProduct.ImagePath.Equals("Assets/Images/template.jpg"))
            {
                MessageBox.Show($"Vui lòng chọn ảnh minh họa sản phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // kiểm tra đã thêm tên, tác giả, giá bán , năm xuất bản, số lượng, giá mua.. hay chưa
            if (newProduct.Name.IsNullOrEmpty() || newProduct.Quantity <= 0 || newProduct.SellingPrice <= 0 || newProduct.PurchasePrice <= 0 )  
            {
                MessageBox.Show($"Vui lòng cung cấp đầy đủ thông tin cần thiết cho sản phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (newProduct.Author.IsNullOrEmpty())
            {
                newProduct.Author = "Anonymous";
            }

            if (newProduct.PublishedYear <= 0 ) 
            {
                newProduct.PublishedYear = 2023;
            }

            DialogResult = true;


        }

        private void chooseImageButton_Click(object sender, RoutedEventArgs e)
        {

            var screen = new OpenFileDialog();
            screen.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
            if (screen.ShowDialog() == true)
            {
                var fileName = screen.FileName;

                var sourceInfo = new FileInfo(fileName);
                var extension = sourceInfo.Extension;

                // Lấy thư mục ảnh hiện hành
                var exeFolder = AppDomain.CurrentDomain.BaseDirectory;
                var imgSubFolder = exeFolder + "Images";

                // phát sinh id duy nhất toàn hệ thống
                var newName = Guid.NewGuid() + extension;
                var destination = $"{imgSubFolder}\\{newName}";

                System.IO.File.Copy(fileName, destination);

                // cập nhật tên mới để lưu vào db, (Images/...jpg..)
                string relativePath = "Images" + @"\" + newName;

                newProduct.ImagePath = relativePath;

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = System.IO.Path.Combine(baseDirectory, relativePath);


                // Tạo một đối tượng BitmapImage
                BitmapImage bitmapImage = new BitmapImage(new Uri(fullPath));

                // Gán BitmapImage vào thuộc tính Source của đối tượng Image
                imagePath.Source = bitmapImage;


                //MessageBox.Show("Path: " + relativePath);
            }
        }
    }
}
