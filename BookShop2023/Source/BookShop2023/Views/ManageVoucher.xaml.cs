using BookShop2023.BUS;
using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using ProjectMyShop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for ManageVoucher.xaml
    /// </summary>
    public partial class ManageVoucher : Page
    {
        public ManageVoucher()
        {
            InitializeComponent();
        }


        #region Khai báo biến...



        int _totalItems = 0;
        int _currentPage = 1;
        int _totalPages = 0;
        int _rowsPerPage = int.Parse(AppConfig.GetValue(AppConfig.NumberProductPerPage));

        VoucherBUS _VoucherBus;

        #endregion

        #region page loaded
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            previousButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            _currentPage = 0;
            _totalPages = 0;
            _VoucherBus = new VoucherBUS();

            AppConfig.SetValue(AppConfig.LastWindow, "ManageVoucher");



            loadVouchers();


        }
        #endregion

        #region load vouchers
        void loadVouchers()
        {

            List<Voucher> listVouchers = _VoucherBus.GetAllVouchers()
                                                      .Skip((_currentPage - 1) * _rowsPerPage)
                                                      .Take(_rowsPerPage).ToList();

            BindingList<Voucher> bindingList = new BindingList<Voucher>(listVouchers);


            VouchersListView.ItemsSource = bindingList;

            dividePaging();



        }
        #endregion


        #region Xử lý sự kiện click menu item của voucher trong listView | xem chi tiết, chỉnh sửa, xóa voucher
        private void viewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Voucher)VouchersListView.SelectedItem;
            

            //var screen = new ViewDetailVoucher(p, catName);
            //screen.ShowDialog();
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var p = (Voucher)VouchersListView.SelectedItem;
            //var screen = new EditVoucherScreen(p, _categories);
            //var result = screen.ShowDialog();
            //if (result == true)
            //{
            //    var info = screen.EditedVoucher;
            //    p.Name = info.Name;
            //    p.CatID = info.CatID;
            //    p.Author = info.Author;
            //    p.PublishedYear = info.PublishedYear;
            //    p.SellingPrice = info.SellingPrice;
            //    p.PurchasePrice = info.PurchasePrice;
            //    p.Description = info.Description;
            //    p.ImagePath = info.ImagePath;
            //    p.Quantity = info.Quantity;
            //    try
            //    {
            //        _VoucherBus.updateVoucher(p.ID, p);
            //        loadVouchers();
            //        MessageBox.Show($"Đã cập nhật voucher thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine("Exception here");
            //        MessageBox.Show(screen, ex.Message);
            //    }

            //}
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var p = (Voucher)VouchersListView.SelectedItem;
            var result = MessageBox.Show($"Bạn thật sự muốn xóa voucher {p.DisplayText} ?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {

                _VoucherBus.removeVoucher(p);
                loadVouchers();

                MessageBox.Show($"Đã xóa voucher {p.DisplayText} thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Tính toán phân trang
        public void dividePaging()
        {
            int count = _VoucherBus.GetAllVouchers().Count;

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

        #region Thay đổi trang xem : click pagingComboBox
        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic info = pagingComboBox.SelectedItem;
            if (info != null)
            {
                if (info?.Page != _currentPage)
                {
                    _currentPage = info?.Page;
                    loadVouchers();
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
        #endregion

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

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddVoucherScreen();
            if (screen.ShowDialog() == true)
            {
                //MessageBox.Show(screen.voucher.DisplayText);
                _VoucherBus.AddVoucher(screen.voucher);
                MessageBox.Show($"Đã thêm mã giảm thành công.", "Thêm mã giảm", MessageBoxButton.OK, MessageBoxImage.Information);

                loadVouchers();
            }
        }
    }
}
