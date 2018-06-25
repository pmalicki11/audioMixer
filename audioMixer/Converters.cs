using System;
using System.Windows.Data;

namespace audioMixer
{
    public class MilisecondsToTimeFormat : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int miliseconds = int.Parse((string)value);
            int seconds = miliseconds / 1000;
            int minutes = seconds / 60;
            seconds -= minutes * 60;

            string min = minutes.ToString();
            string sec = seconds.ToString();
            if (minutes < 10)
            {
                min = "0" + min;
            }
            if (seconds < 10)
            {
                sec = "0" + sec;
            }
            
            return min + ":" + sec;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToTimeFormat : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int miliseconds = (int)value;
            int seconds = miliseconds / 1000;
            int minutes = seconds / 60;
            seconds -= minutes * 60;

            string min = minutes.ToString();
            string sec = seconds.ToString();
            if (minutes < 10)
            {
                min = "0" + min;
            }
            if (seconds < 10)
            {
                sec = "0" + sec;
            }

            return min + ":" + sec;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
