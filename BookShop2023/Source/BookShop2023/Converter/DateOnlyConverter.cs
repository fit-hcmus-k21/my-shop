using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectMyShop.Converter
{


    public class DateOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly dateOnly)
            {
                return dateOnly.ToString("dd/MM/yyyy");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
