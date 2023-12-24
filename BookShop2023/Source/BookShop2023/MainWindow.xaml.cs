using Microsoft.IdentityModel.Tokens;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.Views;
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

namespace ProjectMyShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Khai báo các user controls, buttons..
        Dashboard dashboard;
        ManageProduct _manageProductPage;
        ManageOrder _manageOrderPage;
        Statistics _statisticsPage;
        ManageCategory _manageCategory;
        Configuration _configPage;
        Login login;
        Button[] buttons;
        #endregion

        #region Kiểm tra user đăng nhập mode staff hay admin để chuyển giao diện dashboard
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // check whether user logged in mode staff or admin
            string? loginMode = AppConfig.GetValue(AppConfig.LoginMode);
            if (loginMode.IsNullOrEmpty())
            {
                loginMode = AccountBUS.Instance().GetRole();
            } else
            {
                if (int.Parse(loginMode) == 1)
                {
                    loginMode = "admin";
                } else
                {
                    loginMode = "staff";
                }
            }
            //MessageBox.Show("Role: " + loginMode);

            if (loginMode != null && loginMode.Equals("admin"))
            {
                // role admin
                openDashBoardAdmin();

            } else
            {
                // role staff
                openDashBoardStaff();

            }              
          
        }
        #endregion

        #region Mở dashboard cho admin: có cả thống kê chi tiêu, quản lý ...
        private void openDashBoardAdmin()
        {
            dashboard = new Dashboard();
            _manageOrderPage = new ManageOrder();
            _manageCategory = new ManageCategory();

            Button[] buttons1 = new Button[] { dashboardButton, categoriesButton, productButton, orderButton, statButton, configButton };
            buttons = buttons1;
            if (AppConfig.GetValue(AppConfig.LastWindow) == "0")
            {
                changeButtonColor(dashboardButton);
                pageNavigation.NavigationService.Navigate(dashboard);
            }
            else
            {
                if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageCategory")
                {
                    changeButtonColor(categoriesButton);
                    _manageCategory = new ManageCategory();
                    pageNavigation.NavigationService.Navigate(_manageCategory);
                }
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageOrder")
                {
                    changeButtonColor(orderButton);
                    _manageOrderPage = new ManageOrder();   
                    pageNavigation.NavigationService.Navigate(_manageOrderPage);
                }
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "Statistics")
                {
                    changeButtonColor(statButton);
                    _statisticsPage = new Statistics();
                    pageNavigation.NavigationService.Navigate(_statisticsPage);
                }

                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageProduct")
                {
                    changeButtonColor(productButton);
                    _manageProductPage = new ManageProduct();
                    pageNavigation.NavigationService.Navigate(_manageProductPage);
                }

            }

        }
        #endregion

        #region Mở dashboard cho staff: không có các chức năng riêng của admin: thống kê..
        private void openDashBoardStaff()
        {
           
            dashboard = new Dashboard();
            _manageOrderPage = new ManageOrder();
            _manageCategory = new ManageCategory();

            // hide statistics
            statButton.Visibility = Visibility.Collapsed;

            Button[] buttons1 = new Button[] { dashboardButton, categoriesButton, productButton, orderButton, configButton };
            buttons = buttons1;
            if (AppConfig.GetValue(AppConfig.LastWindow) == "0")
            {
                changeButtonColor(dashboardButton);
                pageNavigation.NavigationService.Navigate(dashboard);
            }
            else
            {
                if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageCategory")
                {
                    changeButtonColor(categoriesButton);
                    _manageCategory = new ManageCategory();
                    pageNavigation.NavigationService.Navigate(_manageCategory);
                }
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageOrder")
                {

                    changeButtonColor(orderButton);
                    _manageOrderPage = new ManageOrder();
                    pageNavigation.NavigationService.Navigate(_manageOrderPage);
                }

                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageProduct")
                {
                    changeButtonColor(productButton);
                    _manageProductPage = new ManageProduct();
                    pageNavigation.NavigationService.Navigate(_manageProductPage);
                }

            }

        }
        #endregion

        #region Quản lý sự kiện click buttons
        private void changeButtonColor(Button b)
        {
            foreach (var button in buttons)
            {
                button.Background = (Brush)Application.Current.Resources["MyPinkGradient"];
            }
            b.Background = (Brush)Application.Current.Resources["MyRedGradient"];
        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(dashboardButton);
            dashboard = new Dashboard();
            pageNavigation.NavigationService.Navigate(dashboard);
        }

        private void productButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(productButton);
            _manageProductPage = new ManageProduct();
            pageNavigation.NavigationService.Navigate(_manageProductPage);
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(orderButton);
            pageNavigation.NavigationService.Navigate(_manageOrderPage);
        }

        private void statButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(statButton);
            _statisticsPage = new Statistics();
            pageNavigation.NavigationService.Navigate(_statisticsPage);
        }

        private void configButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(configButton);
            _configPage = new Configuration();
            pageNavigation.NavigationService.Navigate(_configPage);
        }

        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(categoriesButton);
            pageNavigation.NavigationService.Navigate(_manageCategory);
        }
        #endregion
    }
}
