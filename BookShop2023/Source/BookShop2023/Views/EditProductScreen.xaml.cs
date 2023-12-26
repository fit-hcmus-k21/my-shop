using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for EditProductScreen.xaml
    /// </summary>
    public partial class EditProductScreen : Window
    {
        public Product EditedProduct { get; set; }
        public EditProductScreen(Product p)
        {
            InitializeComponent();
            EditedProduct = (Product)p.Clone();
            this.DataContext = EditedProduct;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };




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
                var relativePath = "Images" + @"\" + newName;
                EditedProduct.ImagePath = relativePath;
                MessageBox.Show("Path: " + relativePath);
            }
        }

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
