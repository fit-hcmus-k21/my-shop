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


        public AddVoucherScreen()
             {
                InitializeComponent();

                voucher = new Voucher();
            _voucherBus = new VoucherBUS();
                this.DataContext = voucher;
            }




            private void cancelButton_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = false;
            }

            private void addButton_Click(object sender, RoutedEventArgs e)
            {
            if (StartDatePicker.SelectedDate != null)
                voucher.StartDate = DateOnly.Parse(StartDatePicker.SelectedDate.Value.Date.ToShortDateString());

            if (EndDatePicker.SelectedDate != null)
                voucher.EndDate = DateOnly.Parse(EndDatePicker.SelectedDate.Value.Date.ToShortDateString());

            DialogResult = true;
            }
        


    }


}
