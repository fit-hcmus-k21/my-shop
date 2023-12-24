using ProjectMyShop.Config;
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
    }
}
