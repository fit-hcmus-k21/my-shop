using BookShop2023.BUS;
using BookShop2023.DTO;
using BookShop2023.Models;
using ProjectMyShop.BUS;
using ProjectMyShop.DTO;
using ProjectMyShop.Views;
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
using System.Windows.Shapes;

namespace BookShop2023.Views
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {


        


            public Customer customer { get; set; }




            public AddCustomer( Customer customer, string type)
            {
                InitializeComponent();
                

                this.customer = customer;
                DataContext = customer;

                if (type.Equals("Add"))
            {
                customer.ID = new CustomerBUS().GetLatestInsertID() + 1;    // just for display #
                
            } else
            {
                this.Title = "Cập nhật thông tin khách hàng";
            }

                
              
            }

   


            private void Window_Loaded(object sender, RoutedEventArgs e)
            {

             


            }

            private void ReturnButton_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = false;
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
                

                DialogResult = true;
            }

     

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

         


        }
    

}
