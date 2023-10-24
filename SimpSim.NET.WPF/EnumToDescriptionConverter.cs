using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace SimpSim.NET.WPF;

public class EnumToDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || targetType != typeof(string))
            return string.Empty;

        var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0 && !string.IsNullOrWhiteSpace(attributes[0].Description))
            return attributes[0].Description;
        else
            return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}