using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpSim.NET.Presentation
{
    public class InstructionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            Instruction instruction = (Instruction)value;

            return instruction.Byte1.ToHexString(2) + instruction.Byte2.ToHexString(2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
