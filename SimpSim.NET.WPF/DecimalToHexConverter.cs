using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SimpSim.NET.WPF
{
    public class DecimalToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToByte(value).ToHexString(2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return System.Convert.ToString(value).ToByteFromHex();
            }
            catch
            {
                return new ValidationResult(false, "Input must be a hexadecimal number between 00 and FF.");
            }
        }
    }
}
