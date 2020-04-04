using System;
using System.Globalization;
using System.Windows.Data;

namespace FFmpeg.Converters
{
    class FieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int) return ((int) value == 0) ? null : value.ToString();
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String stringValue = (String)value;
            
            if (stringValue == null || stringValue == "") return null;
            try
            {
                return int.Parse(stringValue);
            }
            catch (FormatException)
            { 
                return value;
            }
        }
    }
}
