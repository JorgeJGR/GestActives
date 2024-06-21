using System;
using System.Globalization;
using System.Windows.Data;

namespace GestActives
{
    public class FieldsEmptyToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                foreach (var val in values)
                {
                    if (val is string str && !string.IsNullOrWhiteSpace(str))
                    {
                        return false;
                    }
                    if (val is bool boolVal && boolVal)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
