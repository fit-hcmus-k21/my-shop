using BookShop2023.Views;
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
        ManageCustomer _manageCustomer;
        Configuration _configPage;
        ManageVoucher _manageVoucher;
        Login login;
        Button[] buttons;

        string username = "";
        string greeting = "";
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
                greeting = "Admin | ";
                username = AccountBUS.Instance().GetName();

                openDashBoardAdmin();



            }
            else
            {
                // role staff
                greeting = "Hello, ";
                username = AccountBUS.Instance().GetName();

                openDashBoardStaff();


            }

        }
        #endregion

        #region Mở dashboard cho admin: có cả thống kê chi tiêu, quản lý ...
        private void openDashBoardAdmin()
        {


            dashboardText.Text = greeting + username;

            Button[] buttons1 = new Button[] { dashboardButton, categoriesButton, productButton, orderButton, statButton, configButton, custButton, voucherButton };
            buttons = buttons1;
            if (AppConfig.GetValue(AppConfig.LastWindow) == "0")
            {
                changeButtonColor(dashboardButton);
                dashboard = new Dashboard();
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
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageCustomer")
                {
                    changeButtonColor(custButton);
                    _manageCustomer = new ManageCustomer();
                    pageNavigation.NavigationService.Navigate(_manageCustomer);
                }
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageVoucher")
                {
                    changeButtonColor(voucherButton);
                    _manageVoucher = new ManageVoucher();
                    pageNavigation.NavigationService.Navigate(_manageVoucher);
                }

            }

        }
        #endregion

        #region Mở dashboard cho staff: không có các chức năng riêng của admin: thống kê..
        private void openDashBoardStaff()
        {
           


            dashboardText.Text = greeting + username;


            // hide statistics
            statButton.Visibility = Visibility.Collapsed;

            Button[] buttons1 = new Button[] { dashboardButton, categoriesButton, productButton, orderButton, configButton, custButton, voucherButton };
            buttons = buttons1;
            if (AppConfig.GetValue(AppConfig.LastWindow) == "0")
            {
                changeButtonColor(dashboardButton);
                dashboard = new Dashboard();
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
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageCustomer")
                {
                    changeButtonColor(custButton);
                    _manageCustomer = new ManageCustomer();
                    pageNavigation.NavigationService.Navigate(_manageCustomer);
                }
                else if (AppConfig.GetValue(AppConfig.LastWindow) == "ManageVoucher")
                {
                    changeButtonColor(voucherButton);
                    _manageVoucher = new ManageVoucher();
                    pageNavigation.NavigationService.Navigate(_manageVoucher);
                }

            }

        }
        #endregion

        #region Quản lý sự kiện click buttons
        private void changeButtonColor(Button b)
        {
            foreach (var button in buttons)
            {
                button.Background = Brushes.AliceBlue;
            }
            b.Background = Brushes.Bisque;
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
            _manageOrderPage = new ManageOrder();
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
            _manageCategory = new ManageCategory();
            pageNavigation.NavigationService.Navigate(_manageCategory);
        }
        #endregion

        private void pageNavigation_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void custButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(custButton);
            _manageCustomer = new ManageCustomer();
            pageNavigation.NavigationService.Navigate(_manageCustomer);
        }

        private void voucherButton_Click(object sender, RoutedEventArgs e)
        {
            changeButtonColor(voucherButton);
            _manageVoucher = new ManageVoucher();
            pageNavigation.NavigationService.Navigate(_manageVoucher);
            //...

        }

        private void loggoutButton_Click(object sender, RoutedEventArgs e)
        {

            AppConfig.SetValue(AppConfig.LoginStatus, ((int)AppConfig.LoginStatusEnum.LoggedOut).ToString());

            AppConfig.SetValue(AppConfig.LastWindow, "0");

            var loginScreen = new Login();
            loginScreen.Show();

            this.Close();

        }


    }
}
