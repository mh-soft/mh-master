using Hao.Launcher.Model;
using System.Collections.Generic;

namespace Hao.Launcher.Data
{
	public class AppConfig
	{
		public List<string> BeePCProductList { get; set; } = new List<string>();

		public bool IsFestivalImgReplay { get; set; } = false;

		public ImageLinkData LocalImageLinkData
		{
			get;
			set;
		}

		public bool NotifyIconIsShow { get; set; } = false;

		/// <summary>
		/// 当前的QQ的路径
		/// </summary>
		public string QQUrl
		{
			get
			{
				return "tencent://groupwpa/?subcmd=all&param=7B2267726F757055696E223A3631343033313033342C2274696D655374616D70223A313536303333313634377D0A";
			}
		}

		/// <summary>
		/// 当前的PC的选择
		/// </summary>
		public string SelectBeePC
		{
			get;
			set;
		}

		/// <summary>
		/// 当前的Revit版本的选择
		/// </summary>
		public string SelectRevit
		{
			get;
			set;
		}

		/// <summary>
		/// 获取天气的路径
		/// </summary>
		public string WeatherUrl
		{
			get
			{
				return "https://tianqiapi.com/api.php?style=tv&skin=banana&align=center&paddingtop=25&fontsize=16&color=f8f8f8";
			}
		}

		/// <summary>
		/// 配置文件的构造函数
		/// </summary>
		public AppConfig()
		{
		}
	}
}