using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using SportGate.App.Helpers;

namespace SportGate.App.Converters
{
    public class QrCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var text = value.ToString();
            return QrCodeHelper.GenerateQr(text);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
