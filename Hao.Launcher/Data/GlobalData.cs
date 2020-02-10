using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Hao.Launcher.Data
{
	/// <summary>
	/// 当前的全局数据信息
	/// </summary>
	public class GlobalData
	{
		/// <summary>
		/// 当前的目录名称
		/// </summary>
		public readonly static string FolderName;

		/// <summary>
		/// 当前的全局目录
		/// </summary>
		public readonly static string FullFolder;

		/// <summary>
		/// 当前的保存配置路径
		/// </summary>
		public readonly static string SaveConfigPath;

		/// <summary>
		/// 当前的基础服务路径
		/// </summary>
		public static string BaseServerUrl;

		/// <summary>
		/// 当前的全局配置信息
		/// </summary>
		public static AppConfig Config
		{
			get;
			set;
		}

		/// <summary>
		/// 当前的静态构造函数
		/// </summary>
		static GlobalData()
		{
			GlobalData.FolderName = "BeePC";
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string str = Path.DirectorySeparatorChar.ToString();
			string folderName = GlobalData.FolderName;
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			GlobalData.FullFolder = string.Concat(folderPath, str, folderName, directorySeparatorChar.ToString());
			GlobalData.SaveConfigPath = string.Concat(GlobalData.FullFolder, "GlobalData.dat");
		}

		/// <summary>
		/// 获取当前的全局数据
		/// </summary>
		public GlobalData()
		{
		}

		/// <summary>
		/// 获取当前应用程序的配置
		/// </summary>
		/// <param name="strKey"></param>
		/// <returns></returns>
		private static string GetAppConfig(string strKey)
		{
			string item;
			foreach (string appSetting in ConfigurationManager.AppSettings)
			{
				if (appSetting == strKey)
				{
					item = ConfigurationManager.AppSettings[strKey];
					return item;
				}
			}
			item = null;
			return item;
		}

		/// <summary>
		/// 初始化当前的全部配置信息
		/// </summary>
		public static void Init()
		{
			//判断当前是否存在指定目录
			if (!Directory.Exists(GlobalData.FullFolder))
			{
				Directory.CreateDirectory(GlobalData.FullFolder);
			}
			//获取服务地址
			GlobalData.BaseServerUrl = GlobalData.GetAppConfig((Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).Any<string>((string item) => item.Contains("测试环境")) ? "DebugServerUrl" : "BaseServerUrl"));
			
			//初始化配置信息
			if (!File.Exists(GlobalData.SaveConfigPath))
			{
				GlobalData.Config = new AppConfig();
			}
			else
			{
				try
				{
					//读取配置信息
					string str = File.ReadAllText(GlobalData.SaveConfigPath);
					GlobalData.Config = JsonConvert.DeserializeObject<AppConfig>(str);
				}
				catch
				{
					GlobalData.Config = new AppConfig();
				}
			}
		}

		/// <summary>
		/// 保存当前的配置信息
		/// </summary>
		public static void Save()
		{
			string str = JsonConvert.SerializeObject(GlobalData.Config);
			File.WriteAllText(GlobalData.SaveConfigPath, str);
		}
	}
}