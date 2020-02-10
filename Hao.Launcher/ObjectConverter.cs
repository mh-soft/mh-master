using Autodesk.RevitAddIns;
using Hao.Launcher.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Hao.Launcher
{
	public class ObjectConverter : IValueConverter
	{
		public ObjectConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object obj;
			IEnumerable<RevitProduct> revitProducts = value as IEnumerable<RevitProduct>;
			if ((revitProducts == null ? true : targetType != typeof(bool)))
			{
				Visibility visibility = Visibility.Visible;
				IEnumerable<BeePCProduct> beePCProducts = value as IEnumerable<BeePCProduct>;
				if (beePCProducts != null)
				{
					visibility = (beePCProducts.Count<BeePCProduct>() <= 1 ? Visibility.Collapsed : Visibility.Visible);
				}
				obj = ((parameter == null ? true : parameter.ToString().ToLower().Equals("textblock")) ? (visibility != Visibility.Collapsed ? Visibility.Collapsed : Visibility.Visible) : visibility);
			}
			else
			{
				obj = revitProducts.Any<RevitProduct>();
			}
			return obj;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object obj;
			string str = "otherValue";
			string[] strArrays = parameter.ToString().ToLower().Split(new char[] { ':' });
			if (value == null)
			{
				obj = str;
			}
			else if (value.ToString().ToLower() == strArrays[1])
			{
				obj = (strArrays[0].Contains("|") ? strArrays[0].Split(new char[] { '|' })[0] : strArrays[0]);
			}
			else
			{
				obj = str;
			}
			return obj;
		}
	}
}