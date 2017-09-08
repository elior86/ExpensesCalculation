using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MasterInstallerCopy.Converters
{
    public class ValuestoListConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<object> newValuesList = new List<object>();
            if (string.IsNullOrEmpty(System.Convert.ToString(values[0])) == false)
            {
                newValuesList.Add(values[0]);
            }
            if (string.IsNullOrEmpty(System.Convert.ToString(values[1])) == false)
            {
                newValuesList.Add(values[1]);
            }
            for (int i = 2; i < values.Length;)
            {
                if (values[i+1].ToString().Equals("True"))
                {
                    newValuesList.Add(values[i]);
                }
                i += 2;
            }

            return newValuesList;
            //return values.ToList();


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
