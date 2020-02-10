using Hao.Launcher.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Hao.Launcher
{
	public class Url2ImageConverter : IValueConverter
	{
		public Url2ImageConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object obj;
			List<ImagesItem> imagesItems = value as List<ImagesItem>;
			if (imagesItems != null)
			{
				List<Image> images = new List<Image>();
				Func<string, BitmapImage> func = (string imagePath) =>
				{
					BitmapImage bitmapImage;
					if (imagePath.StartsWith("pack://application"))
					{
						bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Absolute))
						{
							DecodePixelWidth = 740,
							DecodePixelHeight = 440
						};
					}
					else if (File.Exists(imagePath))
					{
						BitmapImage bitmapImage1 = new BitmapImage();
						bitmapImage1.BeginInit();
						bitmapImage1.CacheOption = BitmapCacheOption.OnLoad;
						using (Stream memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
						{
							bitmapImage1.StreamSource = memoryStream;
							bitmapImage1.EndInit();
							bitmapImage1.Freeze();
						}
						bitmapImage = bitmapImage1;
					}
					else
					{
						bitmapImage = null;
					}
					return bitmapImage;
				};
				foreach (ImagesItem imagesItem in imagesItems)
				{
					images.Add(new Image()
					{
						Width = 740,
						Height = 440,
						Source = func(imagesItem.BigImage)
					});
				}
				obj = images;
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}