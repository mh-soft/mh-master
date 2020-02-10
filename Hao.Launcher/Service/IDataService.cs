using Autodesk.RevitAddIns;
using Hao.Launcher.Data;
using Hao.Launcher.Model;
using System;
using System.Collections.Generic;

namespace Hao.Launcher.Service
{
	public interface IDataService
	{
		AppConfig GetAppConfig();

		IEnumerable<BeePCProduct> GetBeePCProducts();

		IEnumerable<HistoryFile> GetHistoryFiles(string revitVersion);

		void GetImageLinkData(Action<ImageLinkData, Exception> callback);

		void GetLikeNumber(Action<string, Exception> callback);

		ImageLinkData GetLocalImageLinkData();

		IEnumerable<RevitProduct> GetRevitProducts();

		void GetWeather(string weatherUrl, Action<WeatherInfo, Exception> callback);

		void LikeBeePC(Action<string, Exception> callback);
	}
}