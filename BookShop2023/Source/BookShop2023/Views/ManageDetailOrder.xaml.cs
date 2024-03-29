﻿using BookShop2023.BUS;
using BookShop2023.DTO;
using BookShop2023.Models;
using Microsoft.Graph;
using ProjectMyShop.BUS;
using ProjectMyShop.Converter;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ManageOrderDetail.xaml
    /// </summary>
    public partial class ManageOrderDetail : Window
    {
        public Order order { get; set; }


        public List<OrderDetail> orderDetailList { get; set; }
        public List<Voucher> listVouchers = new List<Voucher>();

        public bool HasVoucher { get; set; }

        public Customer customer { get; set; }
        public Boolean IsNewCustomer { get; set; }
        public BindingList<ProductDataGridItemSource> ListBinding { get; set; }

        private OrderDetailBUS _orderDetailBus;

        public List<OrderStatusEnum> Statuses = OrderStatusEnumBUS.Instance();

        public string type;


        public ManageOrderDetail(Order order, Customer customer, string type)
        {
            InitializeComponent();

            this.order = order;
            this.customer = customer;

            listVouchers = new VoucherBUS().GetAllVouchersExist(DateOnly.Parse(CreatedAtPicker.SelectedDate.Value.Date.ToShortDateString()));


            _orderDetailBus = new OrderDetailBUS();
            ListBinding = new BindingList<ProductDataGridItemSource>();

            ProductDataGrid.ItemsSource = ListBinding;
            StatusComboBox.ItemsSource = Statuses;

            // type: view, update, add
            // nếu là type view thì hide btn save, btn add, remove, disable edit text
            this.type = type;


            if (order != null)
            {
                foreach (var item in Statuses)
                {
                    if (item.Value == order.Status)
                    {
                        StatusComboBox.SelectedItem = item;
                        break;
                    }
                }
            } else
            {
                StatusComboBox.SelectedItem = null;
            }
        }

        private void DoReadOnly()
        {
            SaveButton.Visibility = Visibility.Hidden;
            ActionBar.Visibility = Visibility.Collapsed;

            // Tìm cột DeleteProductButton
            DataGridColumn deleteColumn = ProductDataGrid.Columns[3];

            // Ẩn cột
            deleteColumn.Visibility = Visibility.Collapsed;

            CustomerNameText.IsReadOnly = true;
            CustomerAddressText.IsReadOnly = true;
            CustomerPhoneNumberText.IsReadOnly = true;
            CreatedAtPicker.IsEnabled = false;

            VoucherComboBox.IsReadOnly = true;
            customerTypeComboBox.Visibility = Visibility.Collapsed;
            searchOldCustomerBox.Visibility = Visibility.Collapsed;


            StatusComboBox.IsReadOnly = true;
        }

        private void BindingData()
        {
            // customer info
            //MessageBox.Show("Custumer: " + customer.Name + " " + customer.Address);
            CustomerNameText.Text = customer.Name;
            CustomerAddressText.Text = customer.Address;
            CustomerPhoneNumberText.Text = customer.PhoneNumber;

            CreatedAtPicker.SelectedDate = order.CreatedAt.ToDateTime(new TimeOnly());
            if (order.VoucherID != null)
            {
                //MessageBox.Show("Voucher != null");
                foreach (var v in listVouchers)
                {
                    if (v.ID == order.VoucherID)
                    {
                        BillFinalTotalWithVoucher.Visibility = Visibility.Collapsed;
                        VoucherComboBox.SelectedItem = v;
                        //MessageBox.Show("Voucher: " + v.ID);
                        
                        break;
                    }
                }
            }

            // data list product
            if (order != null)
            {
                orderDetailList = _orderDetailBus.GetListByOrderID(order.ID);
            }
            Reload();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customerTypeComboBox.SelectedIndex = 1;

            DataContext = order;

            if (!type.Equals("add"))
            {
                listVouchers = new VoucherBUS().GetAllVouchers();


            }
            if (type.Equals("view"))
            {
                BindingData();

                DoReadOnly();

            }

            if (type.Equals("update"))
            {
                BindingData();
                customerTypeComboBox.Visibility = Visibility.Collapsed;
                searchOldCustomerBox.Visibility = Visibility.Collapsed;
            }

            if (orderDetailList == null)
            {
                orderDetailList = new List<OrderDetail>();
            }

            VoucherComboBox.ItemsSource = listVouchers;

  


            Reload();

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // date only cannot bound so have to do this 
            if (CreatedAtPicker.SelectedDate != null)
                order.CreatedAt = DateOnly.Parse(CreatedAtPicker.SelectedDate.Value.Date.ToShortDateString());

            if (IsNewCustomer)
            {
                string name = CustomerNameText.Text;
                string address = CustomerAddressText.Text;
                string phoneNum = CustomerPhoneNumberText.Text;
                customer.Name = name;
                customer.Address = address;
                customer.PhoneNumber = phoneNum;
            }
        

            var status = StatusComboBox.SelectedItem as OrderStatusEnum;
            if (status != null)
            {
                order.Status = status.Value;
            }

            if (HasVoucher)
            {
                Voucher voucher = VoucherComboBox.SelectedItem as Voucher;
                order.VoucherID = voucher.ID;
                int totalBill = order.FinalTotal - voucher.PercentOff * order.FinalTotal / 100;
                order.FinalTotal = totalBill;
            }

            DialogResult = true;
        }

        private DateOnly ConvertDateTimeToDateOnly(DateTime dateTime)
        {
            return DateOnly.Parse(dateTime.Date.ToShortDateString());
        }

        bool isInProductList(int ProductID)
        {
            bool result = false;
            if (orderDetailList != null)
            {
                foreach (OrderDetail detail in orderDetailList)
                {
                    if (detail.ProductID == ProductID)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        // do nothing
                    }
                }

            }

            return result;
        }

        private void ChooseProductButton_Click(object sender, RoutedEventArgs e)
        {
            
            var screen = new AddProductOrder();
            if (screen.ShowDialog() == true)
            {
              
                if (!isInProductList(screen.OrderDetail.ProductID))
                {
                    screen.OrderDetail.OrderID = order.ID;
                    orderDetailList.Add(screen.OrderDetail);
                    order.FinalTotal += screen.OrderDetail.Total;
                    
                    calcBill();
                }
                else
                {
                    MessageBox.Show($"Sản phẩm đã tồn tại trong đơn hàng.", "Duplicate Product", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Reload();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int i = ProductDataGrid.SelectedIndex;

            if (i != -1)
            {
                var screen = new AddProductOrder();
                screen.OrderDetail = (OrderDetail) orderDetailList[i];
                if (screen.ShowDialog() == true)
                {
                    if (orderDetailList == null)
                        orderDetailList = new List<OrderDetail>();
                    if (orderDetailList[i].ProductID == screen.OrderDetail.ProductID || !isInProductList(screen.OrderDetail.ProductID))
                    {
                        _orderDetailBus.UpdateOrderDetail(orderDetailList[i].ProductID, screen.OrderDetail);
                        order.FinalTotal -= orderDetailList[i].Total;
                        order.FinalTotal += screen.OrderDetail.Total;
                        orderDetailList[i] = (OrderDetail)screen.OrderDetail.Clone();
                        
                        calcBill();
                    }
                    else
                    {
                        MessageBox.Show($"Sản phẩm đã tồn tại trong danh sách.\nHãy chọn sản phẩm khác.", "Duplicate Product", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    Reload();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int i = ProductDataGrid.SelectedIndex;

            if (i != -1)
            {
                var res = MessageBox.Show($"Bạn có chắc chắn loại bỏ sản phẩm này ?", "Xóa sản phẩm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    orderDetailList.RemoveAt(i);
                    order.FinalTotal -= orderDetailList[i].Total;
                    
                    calcBill();


                    Reload();
                }
            }
            else
            {
                // do nothing
            }
        }

        void Reload()
        {
           Dictionary<int, Product> map = new Dictionary<int, Product>();
            map = new ProductBUS().ProductDictionary();

            ListBinding.Clear();
            foreach (var orderDetail in orderDetailList)
            {
                ListBinding.Add(new ProductDataGridItemSource
                {
                    ProductName = map[orderDetail.ProductID].Name,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                });
            }
           
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            int i = ProductDataGrid.SelectedIndex;
            if (i != -1)
            {
                var QuantityTextBox = e.OriginalSource as TextBox;

                if (QuantityTextBox != null)
                {
                    if (QuantityTextBox.Text == "")
                    {
                        QuantityTextBox.Text = "0";
                    }
                    else if ((orderDetailList != null && (int.Parse(QuantityTextBox.Text)
                        > orderDetailList[i].Quantity)))
                    {
                        QuantityTextBox.Text = QuantityTextBox.Text.Remove(QuantityTextBox.Text.Length - 1);

                        if ((orderDetailList != null && (int.Parse(QuantityTextBox.Text)
                            > orderDetailList[i].Quantity)))
                            QuantityTextBox.Text = orderDetailList[i].Quantity.ToString();
                    }
                }

            }
        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex _regex = new Regex("[^0-9]+$"); //regex that matches disallowed text
            return _regex.IsMatch(text);
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Xử lý sự kiện khi người dùng nhập liệu vào ô tìm kiếm
            string searchText = searchTextBox.Text.ToLower();
            List<Customer> searchResults = new CustomerBUS().GetCustomersByKeyWord(searchText);

            // Hiển thị kết quả tìm kiếm trong ListBox
            customerListBox.ItemsSource = searchResults;

            if (searchResults.Count > 0)
            {
                customerListBox.Visibility = Visibility.Visible;
            }
            else
            {
                customerListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void resetTextBox()
        {
            CustomerNameText.Text = "";
            CustomerAddressText.Text = "";
            CustomerPhoneNumberText.Text = "";
        }

  

        private void CustomerTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (customerTypeComboBox.SelectedIndex == 0)
            {
                searchOldCustomerBox.Visibility = Visibility.Collapsed;


                CustomerNameText.IsReadOnly = false;
                CustomerAddressText.IsReadOnly = false;
                CustomerPhoneNumberText.IsReadOnly = false;


                resetTextBox();
                IsNewCustomer = true;
            }
            else
            {
                searchOldCustomerBox.Visibility = Visibility.Visible;

                CustomerNameText.IsReadOnly = true;
                CustomerAddressText.IsReadOnly = true;
                CustomerPhoneNumberText.IsReadOnly = true;

                resetTextBox() ;
                IsNewCustomer = false;
            }
        }

        private void CustomerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer cust = customerListBox.SelectedItem as Customer;
            if (cust != null)
            {
                customer.ID = cust.ID;
                customer.Address = cust.Address;
                customer.PhoneNumber = cust.PhoneNumber;
                customer.Name = cust.Name;

                CustomerNameText.Text = cust.Name;
                CustomerAddressText.Text = cust.Address;
                CustomerPhoneNumberText.Text = cust.PhoneNumber;

                searchTextBox.Text = "";
                searchOldCustomerBox.Visibility = Visibility.Collapsed;
                customerListBox.ItemsSource = new List<Customer>();
            }
        }

        private void DatePicker_SelectedChange(object sender, SelectionChangedEventArgs e)
        {
            listVouchers.Clear();
            listVouchers = new VoucherBUS().GetAllVouchersExist(DateOnly.Parse(CreatedAtPicker.SelectedDate.Value.Date.ToShortDateString()));

            if (VoucherComboBox != null)
            {
                VoucherComboBox.ItemsSource = listVouchers;
            }
        }

        private void calcBill()
        {
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            var result = String.Format(info, "{0:c}", order.FinalTotal);
            BillFinalTotal.Text = result;

            var voucher = VoucherComboBox.SelectedItem as Voucher;
            if (voucher != null)
            {
                if (voucher.MinCost <= order.FinalTotal)
                {
                    int totalBill = order.FinalTotal - voucher.PercentOff * order.FinalTotal / 100;
                    info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                    result = String.Format(info, "{0:c}", totalBill);
                    BillFinalTotalWithVoucher.Text = result;
                    if (type.Equals("view") || type.Equals("update"))
                    {
                        BillFinalTotal.TextDecorations = null;

                    }else
                    {
                        BillFinalTotal.TextDecorations = TextDecorations.Strikethrough;

                    }

                    HasVoucher = true;
                } else
                {
                    BillFinalTotal.TextDecorations = null;

                    BillFinalTotalWithVoucher.Text = "Đơn hàng chưa đủ điều kiện áp dụng mã.";
                    HasVoucher = false;
                }

            }
        }

        private void VoucherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calcBill();
        }
    }
}
