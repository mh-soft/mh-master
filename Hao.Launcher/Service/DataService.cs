using Autodesk.RevitAddIns;
using Hao.Launcher.Data;
using Hao.Launcher.Helper;
using Hao.Launcher.Model;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hao.Launcher.Service
{
	public class DataService : IDataService
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// 当前的图片路径
		/// </summary>
		private const string ImageDataUrl = "/api/BeePCLauncher/GetBeePCLauncherInfo";

		/// <summary>
		/// 当前连接路径
		/// </summary>
		private const string LikeNumberUrl = "/api/BeePCLauncher/GetLikeNumber";

		/// <summary>
		/// 当前的
		/// </summary>
		private const string LikeBeePCUrl = "/api/BeePCLauncher/ClickBeePCLauncher";

		/// <summary>
		/// 当前的构造函数
		/// </summary>
		public DataService()
		{
		}

		/// <summary>
		/// 获取当前的配置信息
		/// </summary>
		/// <returns></returns>
		public AppConfig GetAppConfig()
		{
			return GlobalData.Config;
		}


		/// <summary>
		/// 获取当前的BEEPC的产品信息
		/// </summary>
		/// <returns></returns>
		public IEnumerable<BeePCProduct> GetBeePCProducts()
		{
			//获取当前的全部路径
			string fullName = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;

			//当前配置的产品列表
			if ((GlobalData.Config.BeePCProductList == null ? true : !GlobalData.Config.BeePCProductList.Any<string>()))
			{
				if (GlobalData.Config.BeePCProductList == null)
				{
					GlobalData.Config.BeePCProductList = new List<string>();
				}
				fullName.InitBeePCProduct((BeePCProduct beepc, Exception ex) =>
				{
					if (ex != null)
					{
						this._logger.Error<Exception>(ex);
					}
					else
					{
						GlobalData.Config.BeePCProductList.Add(fullName);
					}
				});
			}
			//移除无关的产品
			List<BeePCProduct> beePCProducts = new List<BeePCProduct>();
			try
			{
				foreach (string beePCProductList in GlobalData.Config.BeePCProductList)
				{
					beePCProductList.InitBeePCProduct((BeePCProduct beepc, Exception ex) =>
					{
						if (ex != null)
						{
							this._logger.Error<Exception>(ex);
						}
						else
						{
							if (GlobalData.Config.BeePCProductList.IndexOf(beePCProductList) == 0)
							{
								beepc.CanRemove = false;
							}
							beePCProducts.Add(beepc);
						}
					});
				}
			}
			catch (Exception exception)
			{
				this._logger.Error<Exception>(exception);
			}

			//当前的默认产品信息
			if (beePCProducts.FirstOrDefault<BeePCProduct>((BeePCProduct item) => item.InstallPath == fullName) == null)
			{
				fullName.InitBeePCProduct((BeePCProduct beepc, Exception ex) =>
				{
					if (ex != null)
					{
						this._logger.Error<Exception>(ex);
					}
					else
					{
						beePCProducts.Add(beepc);
					}
				});
			}
			return beePCProducts;
		}

		/// <summary>
		/// 获取历史文件
		/// </summary>
		/// <param name="revitVersion"></param>
		/// <returns></returns>
		public IEnumerable<HistoryFile> GetHistoryFiles(string revitVersion)
		{
			return new List<HistoryFile>();
		}

		/// <summary>
		/// 获取指定的图片连接
		/// </summary>
		/// <param name="callback"></param>
		public async void GetImageLinkData(Action<ImageLinkData, Exception> callback)
		{
			await Task.Run(() =>
			{
				try
				{
					ServerResult serverResult = JsonConvert.DeserializeObject<ServerResult>(HttpRequestHelper.HttpGet(string.Concat(GlobalData.BaseServerUrl, "/api/BeePCLauncher/GetBeePCLauncherInfo"), string.Empty));
					if (serverResult.StatusCode != 200)
					{
						this._logger.Error(serverResult.Data);
					}
					else
					{
						ImageLinkData imageLinkDatum = JsonConvert.DeserializeObject<ImageLinkData>(serverResult.Data);
						callback(imageLinkDatum, null);
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					this._logger.Error<Exception>(exception);
					callback(null, exception);
				}
			});
		}

		/// <summary>
		/// 获取当前的连接数量
		/// </summary>
		/// <param name="callback"></param>
		public async void GetLikeNumber(Action<string, Exception> callback)
		{
			await Task.Run(() =>
			{
				try
				{
					ServerResult serverResult = JsonConvert.DeserializeObject<ServerResult>(HttpRequestHelper.HttpGet(string.Concat(GlobalData.BaseServerUrl, "/api/BeePCLauncher/GetLikeNumber"), string.Empty));
					if (serverResult.StatusCode != 200)
					{
						this._logger.Error(serverResult.Data);
					}
					else
					{
						string data = serverResult.Data;
						int num = 1306;
						if (!int.TryParse(data, out num))
						{
							callback("暂无数据", null);
						}
						else
						{
							callback(num.ToString(), null);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					this._logger.Error<Exception>(exception);
					callback("暂无数据", exception);
				}
			});
		}

		public ImageLinkData GetLocalImageLinkData()
		{
			return GlobalData.Config.LocalImageLinkData ?? DefaultData.DefaultImageLinkData;
		}

		/// <summary>
		/// 获取当前所有的Revit产品信息
		/// </summary>
		/// <returns></returns>
		public IEnumerable<RevitProduct> GetRevitProducts()
		{
			IEnumerable<RevitProduct> list = (
				from item in RevitProductUtility.GetAllInstalledRevitProducts()
				orderby item.Name
				select item).ToList<RevitProduct>();
			return list;
		}

		/// <summary>
		/// 获取当前的天气信息
		/// </summary>
		/// <param name="weatherUrl"></param>
		/// <param name="callback"></param>
		public void GetWeather(string weatherUrl, Action<WeatherInfo, Exception> callback)
		{
			try
			{
				callback(this.GetWeather(weatherUrl), null);
			}
			catch (Exception exception)
			{
				callback(null, exception);
			}
		}
		/// <summary>
		/// 获取当前的天气信息
		/// </summary>
		/// <param name="weatherUrl"></param>
		/// <returns></returns>
		private WeatherInfo GetWeather(string weatherUrl)
		{
			WeatherInfo weatherInfo;
			Dictionary<string, string> strs = new Dictionary<string, string>();
			using (HttpClient httpClient = new HttpClient())
			{
				string result = httpClient.GetStringAsync(weatherUrl).Result;
				string str = this.SubstringSingle(result, "<img src=\"", "\" alt");
				string str1 = this.SubstringSingle(result, "<em>", "</em>");
				string str2 = this.SubstringSingle(result, "<em class=\"wTemp\">", "</em>");
				weatherInfo = new WeatherInfo()
				{
					City = str1,
					ImageUrl = str,
					Temperature = str2
				};
			}
			return weatherInfo;
		}

		/// <summary>
		/// 获取当前的连接地址
		/// </summary>
		/// <param name="callback"></param>
		public async void LikeBeePC(Action<string, Exception> callback)
		{
			await Task.Run(() =>
			{
				try
				{
					ServerResult serverResult = JsonConvert.DeserializeObject<ServerResult>(HttpRequestHelper.GetResponseString(HttpRequestHelper.CreatePostHttpResponse(string.Concat(GlobalData.BaseServerUrl, "/api/BeePCLauncher/ClickBeePCLauncher"), new Dictionary<string, string>()
					{
						{ "IntoType", "1" }
					})));
					if (serverResult.StatusCode != 200)
					{
						this._logger.Error(serverResult.Data);
					}
					else
					{
						string data = serverResult.Data;
						int num = 1306;
						if (!int.TryParse(data, out num))
						{
							callback("暂无数据", null);
						}
						else
						{
							callback(num.ToString(), null);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					this._logger.Error<Exception>(exception);
					callback("暂无数据", exception);
				}
			});
		}

		/// <summary>
		/// 提交当前的参数信息
		/// </summary>
		/// <param name="source"></param>
		/// <param name="startStr"></param>
		/// <param name="endStr"></param>
		/// <returns></returns>
		public List<string> SubstringMultiple(string source, string startStr, string endStr)
		{
			Regex regex = new Regex(string.Concat(new string[] { "(?<=(", startStr, "))[.\\s\\S]*?(?=(", endStr, "))" }), RegexOptions.Multiline | RegexOptions.Singleline);
			MatchCollection matchCollections = regex.Matches(source);
			List<string> strs = new List<string>();
			foreach (Match match in matchCollections)
			{
				strs.Add(match.Value);
			}
			return strs;
		}

		/// <summary>
		/// 提交当前的参数信息
		/// </summary>
		/// <param name="source"></param>
		/// <param name="startStr"></param>
		/// <param name="endStr"></param>
		/// <returns></returns>
		public string SubstringSingle(string source, string startStr, string endStr)
		{
			Regex regex = new Regex(string.Concat(new string[] { "(?<=(", startStr, "))[.\\s\\S]*?(?=(", endStr, "))" }), RegexOptions.Multiline | RegexOptions.Singleline);
			return regex.Match(source).Value;
		}
	}
}