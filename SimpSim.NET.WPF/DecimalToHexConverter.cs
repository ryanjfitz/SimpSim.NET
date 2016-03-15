using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpSim.NET.WPF
{
    public class DecimalToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ByteUtilities.ConvertByteToHexString((byte)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ByteUtilities.ConvertHexStringToByte(value.ToString());
        }
    }
}
