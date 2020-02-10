using Hao.Launcher.Model;
using System.Collections.Generic;

namespace Hao.Launcher.Data
{
	/// <summary>
	/// 当前的默认数据信息
	/// </summary>
	public class DefaultData
	{
		public static ImageLinkData DefaultImageLinkData
		{
			get
			{
				return new ImageLinkData()
				{
					Version = 100,
					Navbars = new List<NavbarsItem>()
					{
						new NavbarsItem()
						{
							NavbarName = "主页",
							Images = new List<ImagesItem>()
							{
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/main1.png",
									Link = "http://www.wengwengkeji.com",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								},
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/main2.png",
									Link = "http://www.wengwengkeji.com",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								}
							}
						},
						new NavbarsItem()
						{
							NavbarName = "招聘",
							Images = new List<ImagesItem>()
							{
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/joinus.png",
									Link = "http://www.wengwengkeji.cn:8068/forum.php?mod=forumdisplay&fid=63",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								}
							}
						},
						new NavbarsItem()
						{
							NavbarName = "学习",
							Images = new List<ImagesItem>()
							{
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/teach.png",
									Link = "http://www.wengwengkeji.cn:8068/forum.php?mod=forumdisplay&fid=62",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								},
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/teach1.png",
									Link = "http://www.wengwengkeji.cn:8068/forum.php?mod=forumdisplay&fid=61",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								}
							}
						},
						new NavbarsItem()
						{
							NavbarName = "培训",
							Images = new List<ImagesItem>()
							{
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/train.png",
									Link = "http://www.wengwengkeji.cn:8068/forum.php?mod=viewthread&tid=46&page=1&extra=#pid65",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								}
							}
						},
						new NavbarsItem()
						{
							NavbarName = "新闻",
							Images = new List<ImagesItem>()
							{
								new ImagesItem()
								{
									BigImage = "pack://application:,,,/Resource/Image/news.png",
									Link = "http://www.wengwengkeji.cn:8068/forum.php?mod=viewthread&tid=57&extra=",
									SmallImage = "pack://application:,,,/Resource/Image/right.png"
								}
							}
						}
					}
				};
			}
		}

		public DefaultData()
		{
		}
	}
}