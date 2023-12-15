﻿using ProjectMyShop.BUS;
using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public int totalProduct { get; set; } = 0;
        public int weekOrder { get; set; } = 0;
        public int monthOrder { get; set; } = 0;

        List<Product>? _Products = null;
        ProductBUS _ProductBUS = new ProductBUS();
        OrderBUS _orderBUS = new OrderBUS();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            totalProduct = _ProductBUS.GetTotalProduct();
            weekOrder = _orderBUS.CountOrderByWeek();
            monthOrder = _orderBUS.CountOrderByMonth();
            _Products = _ProductBUS.Top5OutQuantity();

            ProductDataGrid.ItemsSource = _Products;
            DataContext = this;
            AppConfig.SetValue(AppConfig.LastWindow, "Dashboard");
        }

        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            int index = ProductDataGrid.Items.IndexOf(row.Item);
            if (index != -1)
            {
                Product p = _Products![index];
                var screen = new AddQuantityScreen(p);
                var result = screen.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        var newProduct = screen.newProduct;
                        _ProductBUS.updateProduct(p.ID, newProduct);

                        // reload page
                        _Products = _ProductBUS.Top5OutQuantity();
                        ProductDataGrid.ItemsSource = _Products;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private TargetType GetParent<TargetType>(DependencyObject o) where TargetType : DependencyObject
        {
            if (o == null || o is TargetType) return (TargetType)o;
            return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
        }
    }
}
