using BookShop2023;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        AccountBUS _accountBUS = AccountBUS.Instance();
        private string _username;
        private string _password;

        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Connect to database
            DB.Instance.Connect();
            #endregion


            #region check whether user logged in or not
            if (int.Parse(AppConfig.GetValue(AppConfig.LoginStatus)).Equals((int) AppConfig.LoginStatusEnum.LoggedOut))
            {
                // not logged in
                // ...
            } else
            {
                // user logged in, open main window
                MainWindow window = new MainWindow();
                this.Close();
                window.Show();
            }
            #endregion

        }

        #region Xử lý sự kiện click login btn
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            _username = txtBoxUserName.Text;
            if (txtBoxPassWord.Visibility== Visibility.Visible)
            {
                _password = txtBoxPassWord.Text;
            } else
            {
                _password = passwordBoxLogin.Password.ToString();
            }


            if (_username != null && _password != null)
            {
                
                // validate username and password
                // ...

                // Authenticate
                if (_accountBUS.Authenticate(_username, _password))
                {
                    #region Ghi nhớ đăng nhập nếu user check box
                    if (rememberMe.IsChecked == true)
                    {
                        AppConfig.SetValue(AppConfig.LoginStatus, ((int)AppConfig.LoginStatusEnum.LoggedIn).ToString());
                        AppConfig.SetValue(AppConfig.LoginMode, AccountBUS.Instance().GetRole());
                       
                    }

                    #endregion


                    // open main window
                    MainWindow window = new MainWindow();
                    this.Close();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // gain focus 
            }

        }
        #endregion

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #region Mở cửa sổ Settings

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsScreen = new Settings();

            // Tạo một đối tượng Settings nếu chưa có

            settingsScreen = new Settings();
            settingsScreen.Closed += SettingsScreen_Closed; // Đăng ký sự kiện khi cửa sổ được đóng

            // Ẩn cửa sổ đăng nhập
            this.Hide();

            // Hiển thị cửa sổ cài đặt
            settingsScreen.Show();
        }

        private void SettingsScreen_Closed(object sender, EventArgs e)
        {
            // Hiển thị lại cửa sổ đăng nhập khi cửa sổ cài đặt được đóng
            this.Show();
        }
        #endregion

        private void ShowHidePassword_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxShowPass.IsChecked == true)
            {
                // is check box
                string pass = passwordBoxLogin.Password.ToString();
                TextBox txtBoxPassword = (txtBoxPassWord as TextBox);
                txtBoxPassword.Text = pass;
                txtBoxPassword.Visibility = Visibility.Visible;
                passwordBoxLogin.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordBox passwordBox = (passwordBoxLogin as PasswordBox);
                passwordBox.Password = txtBoxPassWord.Text;
                passwordBox.Visibility = Visibility.Visible;
                txtBoxPassWord.Visibility = Visibility.Collapsed;
                
            }
        }
    }
}
