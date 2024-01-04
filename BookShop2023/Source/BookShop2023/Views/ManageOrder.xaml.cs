using BookShop2023.BUS;
using BookShop2023.DTO;
using BookShop2023.Models;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
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
using System.Windows.Shapes;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageOrder.xaml
    /// </summary>
    public partial class ManageOrder : Page
    {
        private OrderBUS _orderBUS;
        private CustomerBUS _customerBUS;
        private OrderDetailBUS _orderDetailBUS;

        BindingList<Order> _orders = new BindingList<Order>();
        BindingList<OrderDataGridItemSource> DataGridBinding = new BindingList<OrderDataGridItemSource>();


        DateTime FromDate;
        DateTime ToDate;

        int _totalItems = 0;
        int _currentPage = 0;
        int _totalPages = 0;
        int _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));

        public ManageOrder()
        {
            InitializeComponent();
            this._customerBUS = new CustomerBUS();
            this._orderBUS = new OrderBUS();
            this._orderDetailBUS = new OrderDetailBUS();
        }

        #region Load page 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _currentPage = 0;
            _totalPages = 0;
            _totalItems = 0;
            _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));

            _orderBUS = new OrderBUS();

            FromDate = DateTime.Parse("1/1/1970");
            ToDate = DateTime.MaxValue;
            
            OrderDataGrid.ItemsSource = DataGridBinding;

            AppConfig.SetValue(AppConfig.LastWindow, "ManageOrder");
            Reload();


        }
        #endregion


        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        void Reload()
        {
            List<Order> listOrders = _orderBUS.GetAllOrdersByDate(FromDate, ToDate)
                                                      .Skip((_currentPage - 1) * _rowsPerPage)
                                                      .Take(_rowsPerPage).ToList();

            BindingList<Order> bindingList = new BindingList<Order>(listOrders);

            _orders = bindingList;


            if (_orders.Count > 0)
            {
                DataGridBinding.Clear();
                Dictionary<int, Customer> CustomerDictionary = _customerBUS.CustomerDictionary();
                Dictionary<int, string> StatusDitionary = new OrderStatusEnumBUS().StatusDictionary();
                foreach (var order in _orders) 
                {
                    DataGridBinding.Add(new OrderDataGridItemSource
                    {
                        ID = order.ID,
                        CustomerName = CustomerDictionary[order.CustomerID].Name,
                        CustomerAddress = CustomerDictionary[order.CustomerID].Address,
                        CreatedAt = order.CreatedAt,
                        Status = StatusDitionary[order.Status],
                        FinalTotal = (int) order.FinalTotal

                    });

                }
            }

            dividePaging();

            
        }

        #region Tính toán phân trang
        public void dividePaging()
        {
            int count = _orderBUS.GetAllOrdersByDate(FromDate, ToDate).Count();

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
            int index = OrderDataGrid.SelectedIndex;
            if (index != -1)
            {
                Order order = _orders[index];
                Customer customer = _customerBUS.GetCustomerByID(order.CustomerID);
                var screen = new ManageOrderDetail(order, customer, "update");
                screen.Owner = this.Parent as Window;
                var result = screen.ShowDialog();

                if (result == true)
                {
                    _orderBUS.UpdateOrder(_orders[index].ID, order);

                    // delete old order detail list
                    _orderDetailBUS.DeleteOrderDetailList(order.ID);

                    // insert new order detail list
                    List<OrderDetail> listOrderDetail = new List<OrderDetail>(screen.orderDetailList);
                    _orderDetailBUS.InsertList(listOrderDetail);

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

        #region Thêm đơn hàng mới
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order { FinalTotal = 0, Status = OrderStatusEnumBUS.GetValueKeyNew()};
            order.ID = _orderBUS.GetLatestInsertID() + 1;
            Customer customer = new Customer();
            var screen = new ManageOrderDetail(order, customer, "add");
            if (screen.ShowDialog() == true)
            {
                List<OrderDetail> listOrderDetail = new List<OrderDetail>(screen.orderDetailList);

                #region insert thông tin khách hàng, insert Order, insert list OrderDetail

                // insert customer info
                //MessageBox.Show("Name: " + customer.Name + ", Address: " + customer.Address, "Thông tin khách hàng", MessageBoxButton.OK);

                _customerBUS.Insert(customer);
                int customerId = _customerBUS.GetLatestInsertID();

                // insert order info               
                order.CustomerID = customerId;
                _orderBUS.InsertOrder(order);


                // insert order detail
                _orderDetailBUS.InsertList(listOrderDetail);

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
            int i = OrderDataGrid.SelectedIndex;

            if (i != - 1)
            {
                Order order = _orders[i];
                var res = MessageBox.Show($"Bạn có chắc chắn muốn xóa đơn hàng này không ?", "Delete order", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    _orderDetailBUS.DeleteOrderDetailList(order.ID);
                    _orderBUS.DeleteOrder(order.ID);
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

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            int index = OrderDataGrid.Items.IndexOf(row.Item);
            if (index != -1)
            {
                Order order = _orders[index];
                Customer customer = _customerBUS.GetCustomerByID(order.CustomerID);
                var screen = new ManageOrderDetail(order, customer, "view");
                screen.Owner = this.Parent as Window;
                var result = screen.ShowDialog();


                if (result == true)
                {
                    _orderBUS.UpdateOrder(_orders[index].ID, order);
                    Reload();
                }
                else
                {
                }
            }
            else
            {
                // Do nothing
            }
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


        #region Filter orders từ ngày đến ngày
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (FromDatePicker.SelectedDate != null)
            {
                FromDate = (DateTime)FromDatePicker.SelectedDate;
            }
            else
            {
                FromDate = DateTime.Parse("1/1/1970");
            }

            if (ToDatePicker.SelectedDate != null)
            {
                ToDate = (DateTime)ToDatePicker.SelectedDate;
            }
            else
            {
                ToDate= DateTime.MaxValue;
            }

            if (FromDate <= ToDate)
            {
                Reload();
            }
            else
            {
                MessageBox.Show("Start Date cannot after End Date", "Date Filter", MessageBoxButton.OK, MessageBoxImage.Warning);
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
