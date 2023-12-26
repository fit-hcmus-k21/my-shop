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
        public EditProductScreen(Product p, List<Category> _listCat)
        {
            InitializeComponent();
            EditedProduct = (Product)p.Clone();
            this.DataContext = EditedProduct;
            categoryCombobox.ItemsSource = _listCat;
            int catId = p.CatID;
            for (int i = 0; i < _listCat.Count; i++)
            {
                if (catId == _listCat[i].ID)
                {
                    categoryCombobox.SelectedIndex = i;
                    break;
                }
            }

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




        #region Cập nhật ảnh sản phẩm 
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

                EditedProduct.ImagePath = relativePath;

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = System.IO.Path.Combine(baseDirectory, relativePath);


                // Tạo một đối tượng BitmapImage
                BitmapImage bitmapImage = new BitmapImage(new Uri(fullPath));

                // Gán BitmapImage vào thuộc tính Source của đối tượng Image
                imagePath.Source = bitmapImage;


                //MessageBox.Show("Path: " + relativePath);
            }
        }
        #endregion

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = categoryCombobox.SelectedIndex;
            try
            {
                int catID = ((Category)categoryCombobox.SelectedItem).ID;

                EditedProduct.CatID = catID;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("Error: " + ex.Message.ToString());
            }

        }
    }
}
