using BookShop2023.BUS;
using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
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

namespace BookShop2023.Views
{
    /// <summary>
    /// Interaction logic for AddVoucherScreen.xaml
    /// </summary>
    public partial class AddVoucherScreen : Window
    {
        public Voucher voucher { get; set; }
        VoucherBUS _voucherBus { get; set; }
        string type; 

        public AddVoucherScreen(Voucher voucher, string type)
             {
                InitializeComponent();

            this.voucher = voucher;
            _voucherBus = new VoucherBUS();
                this.DataContext = voucher;
            this.type = type;

            if (type.Equals("View"))
            {
                this.Title = "Thông tin mã giảm";
                cancelButton.Visibility = Visibility.Hidden;
                addButton.Content = "Trở về";
            }
            }




            private void cancelButton_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = false;
            }

            private void addButton_Click(object sender, RoutedEventArgs e)
            {
            if (type.Equals("View") )
            {
                DialogResult = false;
                return;
            }
            if (StartDatePicker.SelectedDate != null)
                voucher.StartDate = DateOnly.Parse(StartDatePicker.SelectedDate.Value.Date.ToShortDateString());

            if (EndDatePicker.SelectedDate != null)
                voucher.EndDate = DateOnly.Parse(EndDatePicker.SelectedDate.Value.Date.ToShortDateString());

                DialogResult = true;
            }
        


    }


}
