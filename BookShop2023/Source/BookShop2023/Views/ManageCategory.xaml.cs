using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Aspose.Cells;
using Microsoft.Win32;
using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
using ProjectMyShop.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectMyShop.Config;
using System.Configuration;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Page
    {

        List<Category>? _categories = null;        
        private CategoryBUS _categoryBUS = new CategoryBUS();
        public BindingList<CategoryUpdated> _listCat = new BindingList<CategoryUpdated> { };

        public class CategoryUpdated
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int TotalProducts { get; set; }

        }
        

        public ManageCategory()
        {


            InitializeComponent();
            CategoryBUS catBUS = new CategoryBUS();


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddCategoryScreen();
            var result = screen.ShowDialog();
            if (result == true)
            {
                var newCategory = screen.newCategory;
                Debug.WriteLine(newCategory.Name);
               

                try
                {
                    _categoryBUS.AddCategory(newCategory);           
                    loadCategory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(screen, ex.Message);
                }

            }
            
        }

        void loadCategory()
        {
            _categories = _categoryBUS.getCategoryList();
            _listCat.Clear();

            foreach (var category in _categories)
            {
                _listCat.Add(new CategoryUpdated
                {
                    ID = category.ID,
                    Name = category.Name,
                    TotalProducts = _categoryBUS.getTotalProductsOfCat(category.ID)

                });
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy nút được click
            Button button = sender as Button;

            // Lấy dòng (DataRow) chứa nút
            DataGridRow dataGridRow = FindAncestor<DataGridRow>(button);

            // Lấy đối tượng dữ liệu từ dòng hiện tại
            CategoryUpdated rowData = dataGridRow.DataContext as CategoryUpdated;

            // Sử dụng rowData 

            var cat = new Category
            {
                ID = rowData.ID,
                Name = rowData.Name,
            };
            var screen = new EditCategoryScreen(cat);
            var result = screen.ShowDialog();
            if (result == true)
            {
                var info = screen.EditedCategory;
                cat.Name = info.Name;
                try
                {
                    _categoryBUS.updateCategory(cat.ID, cat);
                    MessageBox.Show($"Cập nhật thể loại thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    loadCategory();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception here");
                    MessageBox.Show(screen, ex.Message);
                }
            }
        }

        // Hàm hỗ trợ tìm đối tượng chủ của kiểu T trong Visual Tree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T ancestor)
                {
                    return ancestor;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy nút được click
            Button button = sender as Button;

            // Lấy dòng (DataRow) chứa nút
            DataGridRow dataGridRow = FindAncestor<DataGridRow>(button);

            // Lấy đối tượng dữ liệu từ dòng hiện tại
            CategoryUpdated rowData = dataGridRow.DataContext as CategoryUpdated;

            // Sử dụng rowData 

            var cat = new Category
            {
                ID = rowData.ID,
                Name = rowData.Name,
            };

            var result = MessageBox.Show($"Bạn thật sự muốn xóa thể loại {cat.Name}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {

                _categoryBUS.removeCategory(cat);
                loadCategory();

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            AppConfig.SetValue(AppConfig.LastWindow, "ManageCategory");
            loadCategory();
            categoriesDataGrid.ItemsSource = _listCat;



        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
    }
}
