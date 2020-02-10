using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Launcher.Model
{
	/// <summary>
	/// 当前的导航菜单
	/// </summary>
	public class NavbarsItem : ViewModelBase
	{
		public bool Carousel { get; set; } = true;

		/// <summary>
		/// 当前的图片列表
		/// </summary>
		public List<ImagesItem> Images
		{
			get;
			set;
		}
		/// <summary>
		/// 当前的导航名称
		/// </summary>
		public string NavbarName
		{
			get;
			set;
		}
		/// <summary>
		/// 当前的新数量
		/// </summary>
		public int NewCount
		{
			get
			{
				int num = this.Images.Count<ImagesItem>((ImagesItem item) => item.IsNew);
				return num;
			}
		}

		/// <summary>
		/// 当前的构造函数
		/// </summary>
		public NavbarsItem()
		{
			base.MessengerInstance.Register<ImagesItem>(this, "IsNew", (ImagesItem item) => this.RaisePropertyChanged("NewCount"), false);
		}
	}
}