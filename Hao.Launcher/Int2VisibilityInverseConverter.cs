using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Hao.Launcher
{
	public class Int2VisibilityInverseConverter : IValueConverter
	{
		public Int2VisibilityInverseConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object obj;
			if (value is int)
			{
				if (((int?)(value as int?)).GetValueOrDefault() <= 0)
				{
					obj = Visibility.Visible;
					return obj;
				}
			}
			obj = Visibility.Collapsed;
			return obj;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}