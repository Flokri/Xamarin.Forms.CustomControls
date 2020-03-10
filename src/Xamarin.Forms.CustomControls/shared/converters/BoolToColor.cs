using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms.CustomControls.Menu;

namespace Xamarin.Forms.CustomControls.Converters
{
    public class BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((bool)value)
                    return ((FloatingButtonMenu)parameter).SelectedColor;

                return Color.Transparent;
            }
            catch
            {
                return Color.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
