using Hao.Launcher.Data;
using Hao.Launcher.Helper;
using Hao.Launcher.Model;
using Hao.Launcher.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hao.Launcher.ViewModel
{
	public class BeePCManageViewModel : ViewModelBase
	{
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private ObservableCollection<BeePCProduct> _beePCProducts = new ObservableCollection<BeePCProduct>();

		private IDataService _dataService;

		public ObservableCollection<BeePCProduct> BeePCProducts
		{
			get
			{
				return this._beePCProducts;
			}
			set
			{
				base.Set<ObservableCollection<BeePCProduct>>("BeePCProducts", ref this._beePCProducts, value, true);
			}
		}

		public RelayCommand<BeePCProduct> DelBeePCCmd
		{
			get
			{
				return (new Lazy<RelayCommand<BeePCProduct>>(() => new RelayCommand<BeePCProduct>((BeePCProduct beepc) => base.MessengerInstance.Send<string>(beepc.InstallPath, MessageToken.ToDelBeePC), false))).Value;
			}
		}

		/// <summary>
		/// 初始化当前的界面显示
		/// </summary>
		/// <param name="dataService"></param>
		public BeePCManageViewModel(IDataService dataService)
		{

			this._dataService = dataService;
			this.BeePCProducts.Clear();

			//获取当前所有产品信息
			foreach (BeePCProduct beePCProduct1 in this._dataService.GetBeePCProducts())
			{
				this.BeePCProducts.Add(beePCProduct1);
			}
			base.MessengerInstance.Register<string>(this, MessageToken.ToAddBeePC, (string path) => path.InitBeePCProduct((BeePCProduct beepc, Exception ex) =>
			{
				if (ex != null)
				{
					this._logger.Error<Exception>(ex);
				}
				else if (this.BeePCProducts.FirstOrDefault<BeePCProduct>((BeePCProduct item) => item.InstallPath.Equals(beepc.InstallPath)) == null)
				{
					this.BeePCProducts.Add(beepc);
				}
			}), false);
			base.MessengerInstance.Register<string>(this, MessageToken.ToDelBeePC, (string path) =>
			{
				BeePCProduct beePCProduct = this.BeePCProducts.FirstOrDefault<BeePCProduct>((BeePCProduct item) => item.InstallPath.Equals(path));
				if (beePCProduct != null)
				{
					this.BeePCProducts.Remove(beePCProduct);
				}
			}, false);
		}
	}
}