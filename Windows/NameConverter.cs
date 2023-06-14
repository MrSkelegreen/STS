using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace STS.Windows
{
    public class NameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name;

            if (values[2] != string.Empty)
            {
                name = "Автор: " + values[0].ToString().Substring(0, 1) + "." + values[2].ToString().Substring(0, 1) + ". " + values[1];              
            }
            else
            {
                name = "Автор: " + values[0].ToString().Substring(0, 1) + ". " + values[1];
            }

            return name;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
}
