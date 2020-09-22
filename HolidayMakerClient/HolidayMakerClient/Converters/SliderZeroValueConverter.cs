using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HolidayMakerClient.Converters
{
    public class SliderZeroValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts the value 0 on a slider (The far left side) to say 
        /// "Ej valt" to indicate that this value won't be counted when filtering the homelist.
        /// Otherwise it returns the sliders value as a string with the kilometer suffix
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (((double)value) == 0d)
                return "Ej valt";
            else
                return value.ToString()+" km";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
