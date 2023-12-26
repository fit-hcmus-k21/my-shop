using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProjectMyShop.Converter
{
    public class RelativeToAbsoluteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string relativePath)
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string fullPath = System.IO.Path.Combine(baseDirectory, relativePath);
                    Debug.WriteLine("Path: " + fullPath);
                    return fullPath;
                }
                else
                {
                    MessageBox.Show("Invalid input value. Expected string.");
                    return DependencyProperty.UnsetValue; // Return DependencyProperty.UnsetValue to indicate a failed conversion.
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi qua MessageBox
                MessageBox.Show($"Error in RelativeToAbsoluteConverter:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return DependencyProperty.UnsetValue; // Return DependencyProperty.UnsetValue to indicate a failed conversion.
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
