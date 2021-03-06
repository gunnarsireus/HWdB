﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HWdB.Utils
{
    public class BooleanToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Always test MultiValueConverter inputs for non-null
            // (to avoid crash bugs for views in the designer)
            if (!(value is bool)) return Brushes.Transparent;
            if ((bool)value) return Brushes.DeepSkyBlue;

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

