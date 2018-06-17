using System;
using System.Globalization;
using System.Windows.Data;
using AutorunMaster.Model.Enums;

namespace AutorunMaster.Converters
{
    public class AutorunTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((AutorunTypes)value)
            {
                case AutorunTypes.Registry:
                    return AutorunTypes.Registry.ToString();
                case AutorunTypes.StartMenu:
                    return "Start Menu";
                case AutorunTypes.Scheduler:
                    return "Task Scheduler";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
