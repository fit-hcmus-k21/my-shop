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
        public Order order;
        private OrderBUS _orderBUS;

        OrderDetailViewModel _vm;

        OrderDetail OrderDetail;

        public ManageOrderDetail(Order order)
        {
            InitializeComponent();

            this.order = order;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (order.CreatedAt.Equals(DateOnly.Parse(DateTime.MinValue.Date.ToShortDateString()))) 
                order.CreatedAt = DateOnly.Parse(DateTime.Now.Date.ToShortDateString());
            CreatedAtPicker.SelectedDate = DateTime.Parse(order.CreatedAt.ToString());
            //StatusComboBox.ItemsSource = Order.GetAllStatusValues();
            DataContext = order;

            if (order.ID == 0)
            {
                ChooseProductButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }

            _vm = new OrderDetailViewModel();
            //if (order.OrderDetailList != null)
            //{
            //    _vm.OrderDetails = new BindingList<OrderDetail>(order.OrderDetailList);
            //    ProductDataGrid.ItemsSource = order.OrderDetailList;
            //}

            _orderBUS = new OrderBUS();
            OrderDetail = new OrderDetail();
            OrderDetail.OrderID = order.ID;
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
            DialogResult = true;
        }

        private DateOnly ConvertDateTimeToDateOnly(DateTime dateTime)
        {
            return DateOnly.Parse(dateTime.Date.ToShortDateString());
        }

        bool isInProductList(Product Product)
        {
            bool result = false;
            //if (order.OrderDetailList != null)
            //{
            //    foreach (OrderDetail detail in order.OrderDetailList) {
            //        if (detail.Product.ID == Product.ID)
            //        {
            //            result = true;
            //            break;
            //        }
            //        else
            //        {
            //            // do nothing
            //        }
            //    }

            //}

            return result;
        }

        private void ChooseProductButton_Click(object sender, RoutedEventArgs e)
        {
            //OrderDetail.Product = new Product();
            //OrderDetail.Product.Name = "Choose a Product";
            //OrderDetail.Quantity = 0;
            //var screen = new AddProductOrder(OrderDetail);
            //if (screen.ShowDialog() == true)
            //{
            //    if (order.OrderDetailList == null)
            //        order.OrderDetailList = new List<OrderDetail>();
            //    if (!isInProductList(screen.OrderDetail.Product))
            //    {
            //        _orderBUS.AddOrderDetail(screen.OrderDetail);
            //        order.OrderDetailList.Add(screen.OrderDetail);
            //    }
            //    else
            //    {
            //        MessageBox.Show($"{screen.OrderDetail.Product.Name}'s already exists in detail order.\nChoose 'Update' instead", "Duplicate Product", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    Reload();
            //}
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int i = ProductDataGrid.SelectedIndex;

            if (i != -1)
            {

                //OrderDetail.Product = new Product();
                //OrderDetail.Product = (Product)order.OrderDetailList[i].Product.Clone();
                //OrderDetail.Quantity = order.OrderDetailList[i].Quantity;
                //var screen = new AddProductOrder(OrderDetail);
                //if (screen.ShowDialog() == true)
                //{
                //    if (order.OrderDetailList == null)
                //        order.OrderDetailList = new List<OrderDetail>();
                //    if (order.OrderDetailList[i].Product.ID == screen.OrderDetail.Product.ID || !isInProductList(screen.OrderDetail.Product))
                //    {
                //        _orderBUS.UpdateOrderDetail(order.OrderDetailList[i].Product.ID, screen.OrderDetail);
                //        order.OrderDetailList[i] = (OrderDetail)screen.OrderDetail.Clone();
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
                //var res = MessageBox.Show($"Are you sure to discard this Product: {order.OrderDetailList[i].Product.Name}?", "Delete Product from order", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //if (res == MessageBoxResult.Yes)
                //{
                //    _orderBUS.DeleteOrderDetail(order.OrderDetailList[i]);
                //    order.OrderDetailList.RemoveAt(i);
                //    Reload();
                //}
            }
            else
            {
                // do nothing
            }
        }

        void Reload()
        {

            //if (order.OrderDetailList != null)
            //{
            //    _vm.OrderDetails = new BindingList<OrderDetail>(order.OrderDetailList);
            //    ProductDataGrid.ItemsSource = _vm.OrderDetails;
            //}
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
                    //else if ((order.OrderDetailList !=  null && (int.Parse(QuantityTextBox.Text)
                    //    > order.OrderDetailList[i].Product.Quantity)))
                    //{
                    //    QuantityTextBox.Text = QuantityTextBox.Text.Remove(QuantityTextBox.Text.Length - 1);

                    //    if ((order.OrderDetailList != null && (int.Parse(QuantityTextBox.Text)
                    //        > order.OrderDetailList[i].Product.Quantity)))
                    //        QuantityTextBox.Text = order.OrderDetailList[i].Product.Quantity.ToString();
                    //}
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
