using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace Hao.Launcher.Model
{
	/// <summary>
	/// 当前的图片泪飙
	/// </summary>
	public class ImagesItem : ViewModelBase
	{
		private bool _isNew;

		/// <summary>
		/// 当前的图片路径
		/// </summary>
		public string BigImage
		{
			get;
			set;
		}



		/// <summary>
		/// 是否是最新
		/// </summary>
		public bool IsNew
		{
			get
			{
				return this._isNew;
			}
			set
			{
				base.Set<bool>(ref this._isNew, value, false, "IsNew");
			}
		}

		/// <summary>
		/// 当前是否是
		/// </summary>
		public string Link
		{
			get;
			set;
		}

		public bool OpenLink { get; set; } = true;

		public string SmallImage
		{
			get;
			set;
		}

		public ImagesItem()
		{
			base.PropertyChanged += new PropertyChangedEventHandler(this.ImagesItem_PropertyChanged);
		}

		private void ImagesItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsNew")
			{
				base.MessengerInstance.Send<ImagesItem>(this, "IsNew");
			}
		}
	}
}