using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MasterInstallerCopy.Converters
{
    public class ValuesToButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() >= 6)
            {
                if (string.IsNullOrEmpty(System.Convert.ToString(values[0])) == true || string.IsNullOrEmpty(System.Convert.ToString(values[1])) == true)
                {
                    return false;
                }
                if (System.Convert.ToString(values[2]).Equals("True") || System.Convert.ToString(values[3]).Equals("True") || System.Convert.ToString(values[4]).Equals("True") || System.Convert.ToString(values[5]).Equals("True"))
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
