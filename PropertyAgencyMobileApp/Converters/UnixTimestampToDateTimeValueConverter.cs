using System;
using System.Globalization;
using Xamarin.Forms;

namespace PropertyAgencyMobileApp.Converters
{
    public class UnixTimestampToDateTimeValueConverter : IValueConverter
    {
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              CultureInfo culture)
        {
            long unixTimestamp = (long)value;
            return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
