using BookShop2023.BUS;
using BookShop2023.DTO;
using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
using ProjectMyShop.ViewModels;
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

        public BindingList<OrderDetail> orderDetailList { get; set; }

        public Customer customer { get; set; }


        public ManageOrderDetail(Order order, Customer customer)
        {
            InitializeComponent();

            this.order = order;
            this.customer = customer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            DataContext = order;

            if (orderDetailList == null)
            {
                orderDetailList = new BindingList<OrderDetail>();
            }
            ProductDataGrid.ItemsSource = orderDetailList;


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

            string name = CustomerNameText.Text;
            string address = CustomerAddressText.Text;
            string phoneNum = CustomerPhoneNumberText.Text;
            customer.Name = name;
            customer.Address = address;
            customer.PhoneNumber = phoneNum;

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
                //if (screen.ShowDialog() == true)
                //{
                //    if (orderDetailList == null)
                //        orderDetailList = new List<OrderDetail>();
                //    if (orderDetailList[i].Product.ID == screen.OrderDetail.Product.ID || !isInProductList(screen.OrderDetail.Product))
                //    {
                //        _orderBUS.UpdateOrderDetail(orderDetailList[i].Product.ID, screen.OrderDetail);
                //        orderDetailList[i] = (OrderDetail)screen.OrderDetail.Clone();
                //    }
                //    else
                //    {
                //        MessageBox.Show($"{screen.OrderDetail.Product.Name}'s already exists in detail order.\nPlease choose another Product", "Duplicate Product", MessageBoxButton.OK, MessageBoxImage.Error);
                //    }
                //    Reload();
                //}
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

    }
}
