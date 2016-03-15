using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpSim.NET.WPF
{
    public class DecimalToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ByteUtilities.ConvertByteToHexString((byte)value, 2);
            }
            catch
            {
                return ByteUtilities.ConvertByteToHexString(0x00, 2);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ByteUtilities.ConvertHexStringToByte(value.ToString());
            }
            catch
            {
                return 0x00;
            }
        }
    }
}
