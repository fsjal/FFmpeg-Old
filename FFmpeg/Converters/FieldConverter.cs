using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FFmpeg.Converters
{
    class FieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int) return ((int) value == 0) ? "" : value.ToString();
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string)
            {
                try
                {
                    return int.Parse(value.ToString());
                }
                catch (FormatException)
                {
                    return value;
                }
            }
            return 0;
        }
    }
}
