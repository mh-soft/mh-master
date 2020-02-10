using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Hao.Launcher.Model
{
	/// <summary>
	/// 当前的连接数据
	/// </summary>
	public class ImageLinkData : ViewModelBase
	{
		public Model.FestivalImg FestivalImg
		{
			get;
			set;
		}

		/// <summary>
		/// 当前的导航菜单
		/// </summary>
		public List<NavbarsItem> Navbars
		{
			get;
			set;
		}

		public int Version
		{
			get;
			set;
		}

		public ImageLinkData()
		{
		}
	}
}