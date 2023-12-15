using ProjectMyShop.Config;
using ProjectMyShop.Helpers;
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

namespace BookShop2023
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            // TODO: test connection xem có kết nối được không
            string server = txtServerName.Text;
            string database = txtDatabase.Text;
            int authType = comboBoxAuth.SelectedIndex;  // 0: windows | 1: sql server , trùng với authTypeEnum
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            bool res = DB.TestConnection(server, database, authType, username, password );

            if (res)
            {
                MessageBox.Show("Connection successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Thực hiện các hành động sau khi kết nối thành công
            }
            else
            {
                MessageBox.Show("Connection failed. Please check your connection settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Thực hiện các hành động khi kết nối thất bại
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        #region Lưu cấu hình hiện tại
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO: lưu vào app config
            AppConfig.SetValue(AppConfig.Server, txtServerName.Text);
            AppConfig.SetValue(AppConfig.Database, txtDatabase.Text);

            // Kiểm tra xem kiểu auth là windows hay sql server
            ComboBoxItem selectedItem = (ComboBoxItem)comboBoxAuth.SelectedItem;

            if (selectedItem != null)
            {
                
                if (selectedItem.Content.ToString() == "SQL Server Authentication")
                {

                    AppConfig.SetValue(AppConfig.AuthType, ((int)AppConfig.AuthTypeEnum.SqlServerAuthentication).ToString());

                    // set username and password
                    AppConfig.SetValue(AppConfig.Username, txtUsername.Text);
                    AppConfig.SetPassword(txtPassword.Password.ToString());

                }
                else
                {
                    AppConfig.SetValue(AppConfig.AuthType, ((int)AppConfig.AuthTypeEnum.WindowsAuthentication).ToString());
                }
            }


            this.Close();
        }
        #endregion

        #region  Bắt sự kiện thay đổi kiểu authentication
        private void comboBoxAuth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra xem item nào được chọn
            ComboBoxItem selectedItem = (ComboBoxItem)comboBoxAuth.SelectedItem;

            if (selectedItem != null)
            {
                // Ẩn/hiển thị TextBox dựa trên lựa chọn trong ComboBox
                if (selectedItem.Content.ToString() == "SQL Server Authentication")
                {
                    showUsernamePasswordBox();

                }
                else
                {
                    hideUsernamePasswordBox();

                }
            }
        }
        #endregion


        #region Một số hàm bổ trợ
        private void showUsernamePasswordBox()
        {
            usernameBox.Opacity = 1;
            passwordBox.Opacity = 1;
            txtUsername.IsEnabled = true;
            txtPassword.IsEnabled = true;
        }

        private void hideUsernamePasswordBox()
        {
            clearTextBox();
            usernameBox.Opacity = 0.5;
            passwordBox.Opacity = 0.5;
            txtUsername.IsEnabled = false;
            txtPassword.IsEnabled = false;
        }

        private void clearTextBox()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Password = string.Empty;
        }
        #endregion

        #region Thực hiện 1 số việc khi mở cửa sổ này
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: kiểm tra app config và cài đặt hiển thị

            string server = AppConfig.GetValue(AppConfig.Server);
            if (server != null)
            {
                txtServerName.Text = server;
            }
            
            string database = AppConfig.GetValue(AppConfig.Database);
            if (database != null)
            {
                txtDatabase.Text = database;
            }

            string authType = AppConfig.GetValue(AppConfig.AuthType);
            if (authType != null)
            {
                
                if (int.Parse(authType) == (int) AppConfig.AuthTypeEnum.SqlServerAuthentication)
                {
                    showUsernamePasswordBox();
                    comboBoxAuth.SelectedIndex = 1;
                } else
                {
                    hideUsernamePasswordBox();
                    comboBoxAuth.SelectedIndex = 0;

                }
            }


        }
        #endregion
    }
}
