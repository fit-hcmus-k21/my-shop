using BookShop2023.BUS;
using BookShop2023.DTO;
using BookShop2023.Models;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using ProjectMyShop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookShop2023.Views
{


        public partial class ManageCustomer : Page
        {
            private CustomerBUS _customerBUS;

            BindingList<CustomerDataGrid> DataGridBinding = new BindingList<CustomerDataGrid>();



            int _totalItems = 0;
            int _currentPage = 0;
            int _totalPages = 0;
            int _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));

            public ManageCustomer()
            {
                InitializeComponent();
                this._customerBUS = new CustomerBUS();
            }

            #region Load page 
            private void Page_Loaded(object sender, RoutedEventArgs e)
            {
                _currentPage = 0;
                _totalPages = 0;
                _totalItems = 0;
                _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));


      


                AppConfig.SetValue(AppConfig.LastWindow, "ManageCustomer");
                Reload();



        }
        #endregion


        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }


            void Reload()
            {
                List<CustomerDataGrid> listCustomers = _customerBUS.loadAllCustomerDataGrid()
                                                          .Skip((_currentPage - 1) * _rowsPerPage)
                                                          .Take(_rowsPerPage).ToList();

                DataGridBinding = new BindingList<CustomerDataGrid>(listCustomers);

            CustomerDataGrid.ItemsSource = DataGridBinding;




            dividePaging();


            }

            #region Tính toán phân trang
            public void dividePaging()
            {
            int count = _customerBUS.loadAll().Count;

                if (count != _totalItems)
                {
                    _totalItems = count;
                    _totalPages = (_totalItems / _rowsPerPage) + (((_totalItems % _rowsPerPage) == 0) ? 0 : 1);

                    // tạo thông tin phân trang
                    var pageInfos = new List<object>();

                    for (int i = 1; i <= _totalPages; i++)
                    {
                        pageInfos.Add(new
                        {
                            Page = i,
                            Total = _totalPages
                        });
                    };

                    pagingComboBox.ItemsSource = pageInfos;
                    pagingComboBox.SelectedIndex = 0;

                }

                if (_totalPages > 1)
                {
                    nextButton.IsEnabled = true;
                }
                else
                {
                    nextButton.IsEnabled = false;
                }
            }
            #endregion

            private void UpdateButton_Click(object sender, RoutedEventArgs e)
            {
            int index = CustomerDataGrid.SelectedIndex;
            if (index != -1)
            {
                Customer cust = CustomerDataGrid.SelectedItem as Customer;
                var screen = new AddCustomer( cust, "Update");
                var result = screen.ShowDialog();

                if (result == true)
                {
                    _customerBUS.Update(cust);
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công !");


                    Reload();
                }
                else
                {
                    // Do nothing
                }
            }
            else
            {
                // Do nothing
            }
        }

            #region Thêm khách hàng mới
            private void AddButton_Click(object sender, RoutedEventArgs e)
            {
       
            Customer customer = new Customer();
            var screen = new AddCustomer( customer, "Add");
            if (screen.ShowDialog() == true)
            {

                #region insert thông tin khách hàng, 

                // insert customer info
                //MessageBox.Show("Name: " + customer.Name + ", Address: " + customer.Address, "Thông tin khách hàng", MessageBoxButton.OK);

                _customerBUS.Insert(customer);

                MessageBox.Show("Thêm khách hàng mới thành công !");
               

                #endregion

                Reload();
            }
            else
            {
                // do nothing
            }


        }
            #endregion


            private void DeleteButton_Click(object sender, RoutedEventArgs e)
            {
            int i = CustomerDataGrid.SelectedIndex;

            if (i != -1)
            {
                Customer customer = CustomerDataGrid.SelectedItem as Customer;
                var res = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng này không ?", "Xóa khách hàng", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    _customerBUS.Remove(customer);
                    Reload();

                }
            }
            else
            {
                // do nothing
            }
        }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {

            }

            private TargetType GetParent<TargetType>(DependencyObject o) where TargetType : DependencyObject
            {
                if (o == null || o is TargetType) return (TargetType)o;
                return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
            }

           

            #region Chuyển trang trước, trang sau khi click button
            private void previousButton_Click(object sender, RoutedEventArgs e)
            {
                if (pagingComboBox.SelectedIndex > 0)
                {
                    pagingComboBox.SelectedIndex--;
                }


            }

            private void nextButton_Click(object sender, RoutedEventArgs e)
            {
                if (pagingComboBox.SelectedIndex < _totalPages - 1)
                {
                    pagingComboBox.SelectedIndex++;
                }

            }
            #endregion





            #region Thay đổi trang xem : click pagingComboBox
            private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                dynamic info = pagingComboBox.SelectedItem;
                if (info != null)
                {
                    if (info?.Page != _currentPage)
                    {
                        _currentPage = info?.Page;
                        Reload();
                    }
                }

                if (_currentPage == _totalPages)
                {
                    nextButton.IsEnabled = false;
                }
                else
                {
                    nextButton.IsEnabled = true;
                }
                if (_currentPage == 1)
                {
                    previousButton.IsEnabled = false;
                }
                else
                {
                    previousButton.IsEnabled = true;
                }
            }

            private void pagingComboBox_DropDownOpened(object sender, EventArgs e)
            {
                // Thay đổi màu nền khi dropdown mở
                pagingComboBox.Background = new SolidColorBrush(Colors.LightBlue);
            }

            private void pagingComboBox_DropDownClosed(object sender, EventArgs e)
            {
                // Thay đổi màu nền khi dropdown đóng
                pagingComboBox.Background = new SolidColorBrush(Colors.White);
            }

            private void pagingComboBox_Click(object sender, RoutedEventArgs e)
            {
                if (sender is System.Windows.Controls.Button button)
                {
                    // Toggle the IsDropDownOpen state
                    pagingComboBox.IsDropDownOpen = !pagingComboBox.IsDropDownOpen;
                }
            }
            #endregion

        }
    


}
