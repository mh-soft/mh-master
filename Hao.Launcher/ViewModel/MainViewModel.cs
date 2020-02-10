using Autodesk.RevitAddIns;
using Hao.Launcher.Data;
using Hao.Launcher.Helper;
using Hao.Launcher.Model;
using Hao.Launcher.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using HandyControl.Controls;
using HandyControl.Data;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Hao.Launcher.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IDataService _dataService;

        private ObservableCollection<BeePCProduct> _beePCProducts = new ObservableCollection<BeePCProduct>();

        private ObservableCollection<HistoryFile> _historyFiles = new ObservableCollection<HistoryFile>();

        private ObservableCollection<RevitProduct> _revitProducts = new ObservableCollection<RevitProduct>();

        private ImageLinkData _imageLinkData;

        private NavbarsItem _selectNavbarsItem;

        private string _smallImageUrl = "pack://application:,,,/Resource/Image/right.png";

        private RevitProduct _selectRevitProduct;

        private BeePCProduct _selectedBeePCProduct;

        private AppConfig _config;

        private WeatherInfo _locationWeatherInfo = null;

        private string _likeNumber = string.Empty;

        private bool _canLike = false;

        public ObservableCollection<BeePCProduct> BeePCProducts
        {
            get
            {
                return this._beePCProducts;
            }
            set
            {
                base.Set<ObservableCollection<BeePCProduct>>("BeePCProducts", ref this._beePCProducts, value, false);
            }
        }

        public bool CanLike
        {
            get
            {
                return this._canLike;
            }
            set
            {
                base.Set<bool>(ref this._canLike, value, false, "CanLike");
            }
        }

        public bool CanOpen
        {
            get
            {
                return (this.SelectedRevitProduct == null ? false : this.SelectedBeePCProduct != null);
            }
        }

        public AppConfig Config
        {
            get
            {
                return this._config;
            }
            set
            {
                base.Set<AppConfig>("Config", ref this._config, value, false);
            }
        }

        public ObservableCollection<HistoryFile> HistoryFiles
        {
            get
            {
                return this._historyFiles;
            }
            set
            {
                base.Set<ObservableCollection<HistoryFile>>("HistoryFiles", ref this._historyFiles, value, false);
            }
        }

        public ImageLinkData ImageLinkData
        {
            get
            {
                return this._imageLinkData;
            }
            set
            {
                base.Set<ImageLinkData>("ImageLinkData", ref this._imageLinkData, value, false);
            }
        }

        public RelayCommand LikeBeePCCmd
        {
            get
            {
                return (new Lazy<RelayCommand>(() => new RelayCommand(() => {
                    if (this.CanLike)
                    {
                        this._dataService.LikeBeePC((string number, Exception ex) => {
                            if (ex == null)
                            {
                                this.LikeNumber = number;
                            }
                        });
                    }
                }, false))).Value;
            }
        }

        public string LikeNumber
        {
            get
            {
                return this._likeNumber;
            }
            set
            {
                base.Set<string>(ref this._likeNumber, value, false, "LikeNumber");
            }
        }

        public WeatherInfo LocationWeatherInfo
        {
            get
            {
                return this._locationWeatherInfo;
            }
            set
            {
                base.Set<WeatherInfo>(ref this._locationWeatherInfo, value, false, "LocationWeatherInfo");
            }
        }

        public RelayCommand OnKeyDownICmd
        {
            get
            {
                return (new Lazy<RelayCommand>(() => new RelayCommand(() => {
                }, false))).Value;
            }
        }

        public RelayCommand<HistoryFile> OnOpenRevitCmd
        {
            get
            {
                return (new Lazy<RelayCommand<HistoryFile>>(() => new RelayCommand<HistoryFile>((HistoryFile historyFile) => {
                    if (historyFile != null)
                    {
                        Messenger.Default.Send<string>(historyFile.FilePath, MessageToken.ToOpenRevit);
                    }
                }, this.CanOpen))).Value;
            }
        }

        public RelayCommand<string> OpenUrlCmd
        {
            get
            {
                return (new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(new Action<string>(MainViewModel.OpenUrl), false))).Value;
            }
        }

        public ObservableCollection<RevitProduct> RevitProducts
        {
            get
            {
                return this._revitProducts;
            }
            set
            {
                base.Set<ObservableCollection<RevitProduct>>("RevitProducts", ref this._revitProducts, value, false);
            }
        }

        public BeePCProduct SelectedBeePCProduct
        {
            get
            {
                return this._selectedBeePCProduct;
            }
            set
            {
                base.Set<BeePCProduct>("SelectedBeePCProduct", ref this._selectedBeePCProduct, value, true);
            }
        }

        /// <summary>
        /// 当前的选择对象
        /// </summary>
        public NavbarsItem SelectedNavbar
        {
            get
            {
                return this._selectNavbarsItem;
            }
            set
            {
                base.Set<NavbarsItem>("SelectedNavbar", ref this._selectNavbarsItem, value, false);
            }
        }

        public RevitProduct SelectedRevitProduct
        {
            get
            {
                return this._selectRevitProduct;
            }
            set
            {
                base.Set<RevitProduct>("SelectedRevitProduct", ref this._selectRevitProduct, value, true);
            }
        }

        public string SmallImageUrl
        {
            get
            {
                return this._smallImageUrl;
            }
            set
            {
                base.Set<string>("SmallImageUrl", ref this._smallImageUrl, value, false);
            }
        }

        /// <summary>
        /// 构造函数，初始化当前的显示主要数据信息
        /// </summary>
        /// <param name="dataService"></param>
        public MainViewModel(IDataService dataService)
        {
            string name;
            string installPath;
            string installPath1;
            this._dataService = dataService;

            //注册消息，打开指定的URL
            base.MessengerInstance.Register<int>(this, MessageToken.ToOpenUrl, (int pageIndex) => {
                ImagesItem imagesItem = this.SelectedNavbar.Images[pageIndex];
                if ((imagesItem.OpenLink ? true : imagesItem.IsNew))
                {
                    imagesItem.IsNew = false;
                    if (imagesItem.OpenLink)
                    {
                        MainViewModel.OpenUrl(imagesItem.Link);
                    }
                }
            }, false);

            //注册消息，打开指定URL
            base.MessengerInstance.Register<string>(this, MessageToken.ToOpenStrUrl, new Action<string>(MainViewModel.OpenUrl), false);

            //注册消息，改变当前的图片
            base.MessengerInstance.Register<int>(this, MessageToken.OnChangeImage, (int pageIndex) => {
                try
                {
                    string smallImage = this.SelectedNavbar.Images[pageIndex].SmallImage;
                    if (!this.SmallImageUrl.Equals(smallImage))
                    {
                        this.SmallImageUrl = smallImage;
                    }
                }
                catch (Exception exception)
                {
                    this._logger.Error<Exception>(exception);
                }
            }, false);

            //注册消息，启动Revit
            base.MessengerInstance.Register<string>(this, MessageToken.ToOpenRevit, (string filePath) => {
                try
                {
                    string str = string.Concat(this.SelectedRevitProduct.InstallLocation, "\\Revit.exe");
                    string[] strArrays = null;
                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        strArrays = new string[] { filePath };
                    }
                    ProcessHelper.StartProcess(str, strArrays);
                    this.Config.LocalImageLinkData = this.ImageLinkData;
                    this.Config.BeePCProductList = (
                        from item in this.BeePCProducts
                        select item.InstallPath).ToList<string>();
                    GlobalData.Config = this.Config;
                    GlobalData.Save();
                }
                catch (Exception exception)
                {
                    this._logger.Error<Exception>(exception);
                }
            }, false);

            //注册消息
            base.MessengerInstance.Register<string>(this, MessageToken.ToAddBeePC, (string path) => path.InitBeePCProduct((BeePCProduct beepc, Exception ex) => {
                if (ex != null)
                {
                    this._logger.Error<Exception>(ex);
                }
                else if (this.BeePCProducts.FirstOrDefault<BeePCProduct>((BeePCProduct item) => item.InstallPath.Equals(beepc.InstallPath)) == null)
                {
                    this.BeePCProducts.Add(beepc);
                    if (this.SelectedBeePCProduct == null)
                    {
                        this.SelectedBeePCProduct = this.BeePCProducts.FirstOrDefault<BeePCProduct>();
                        this.RaisePropertyChanged<BeePCProduct>(Expression.Lambda<Func<BeePCProduct>>(Expression.Property(Expression.Constant(this, typeof(MainViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(MainViewModel).GetMethod("get_SelectedBeePCProduct").MethodHandle)), new ParameterExpression[0]));
                    }
                    this.RaisePropertyChanged<ObservableCollection<BeePCProduct>>(Expression.Lambda<Func<ObservableCollection<BeePCProduct>>>(Expression.Property(Expression.Constant(this, typeof(MainViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(MainViewModel).GetMethod("get_BeePCProducts").MethodHandle)), new ParameterExpression[0]));
                }
            }), false);

            //注册消息，删除BeePC
            base.MessengerInstance.Register<string>(this, MessageToken.ToDelBeePC, (string path) => {
                BeePCProduct beePCProduct = this.BeePCProducts.FirstOrDefault<BeePCProduct>((BeePCProduct item) => item.InstallPath.Equals(path));
                if (beePCProduct != null)
                {
                    this.BeePCProducts.Remove(beePCProduct);
                }
                if (this.SelectedBeePCProduct == null)
                {
                    this.SelectedBeePCProduct = this.BeePCProducts.FirstOrDefault<BeePCProduct>();
                    this.RaisePropertyChanged<BeePCProduct>(Expression.Lambda<Func<BeePCProduct>>(Expression.Property(Expression.Constant(this, typeof(MainViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(MainViewModel).GetMethod("get_SelectedBeePCProduct").MethodHandle)), new ParameterExpression[0]));
                }
                this.RaisePropertyChanged<ObservableCollection<BeePCProduct>>(Expression.Lambda<Func<ObservableCollection<BeePCProduct>>>(Expression.Property(Expression.Constant(this, typeof(MainViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(MainViewModel).GetMethod("get_BeePCProducts").MethodHandle)), new ParameterExpression[0]));
            }, false);

            //注册消息，版本改变
            base.MessengerInstance.Register<PropertyChangedMessage<BeePCProduct>>(this, (PropertyChangedMessage<BeePCProduct> argument0) => this.ChangeBeePCVersion(), false);
           
            //注册消息Revit产品信息
            base.MessengerInstance.Register<PropertyChangedMessage<RevitProduct>>(this, (PropertyChangedMessage<RevitProduct> message) => {
                this.ChangeBeePCVersion();
                this.HistoryFiles.Clear();
                if (message.NewValue != null)
                {
                    message.NewValue.ReadRecentFiles().ForEach((HistoryFile item) => this.HistoryFiles.Add(item));
                }
            }, false);


            this.ImageLinkData = this._dataService.GetLocalImageLinkData();
            this.RevitProducts.Clear();

            //开始获取Revit版本
            foreach (RevitProduct revitProduct in this._dataService.GetRevitProducts())
            {
                this.RevitProducts.Add(revitProduct);
            }
            this.BeePCProducts.Clear();

            //开始获取BeePC版本
            foreach (BeePCProduct beePCProduct1 in this._dataService.GetBeePCProducts())
            {
                this.BeePCProducts.Add(beePCProduct1);
            }
            //获取当前的配置信息
            this.Config = this._dataService.GetAppConfig();

            if (!this.RevitProducts.Any<RevitProduct>())
            {
                MessageBox.Show(new MessageBoxInfo()
                {
                    Caption = "提示",
                    Message = "请先安装Revit,再使用本软件!"
                });
            }
            //获取当前的产品信息和配置路径
            if ((!this.BeePCProducts.Any<BeePCProduct>() ? false : string.IsNullOrWhiteSpace(this.Config.SelectBeePC)))
            {
                AppConfig config = this.Config;
                BeePCProduct beePCProduct2 = this.BeePCProducts.FirstOrDefault<BeePCProduct>();
                if (beePCProduct2 != null)
                {
                    installPath1 = beePCProduct2.InstallPath;
                }
                else
                {
                    installPath1 = null;
                }
                config.SelectBeePC = installPath1;
            }

            //当前的安装路径
            if (this.BeePCProducts.All<BeePCProduct>((BeePCProduct item) => item.InstallPath != this.Config.SelectBeePC))
            {
                AppConfig appConfig = this.Config;
                BeePCProduct beePCProduct3 = this.BeePCProducts.FirstOrDefault<BeePCProduct>();
                if (beePCProduct3 != null)
                {
                    installPath = beePCProduct3.InstallPath;
                }
                else
                {
                    installPath = null;
                }
                appConfig.SelectBeePC = installPath;
            }
            if ((!this.RevitProducts.Any<RevitProduct>() ? false : string.IsNullOrWhiteSpace(this.Config.SelectRevit)))
            {
                AppConfig config1 = this.Config;
                RevitProduct revitProduct1 = this.RevitProducts.FirstOrDefault<RevitProduct>();
                if (revitProduct1 != null)
                {
                    name = revitProduct1.Name;
                }
                else
                {
                    name = null;
                }
                config1.SelectRevit = name;
            }

            //获取图片连接数据
            this._dataService.GetImageLinkData((ImageLinkData imgData, Exception ex) => {
                if (ex == null)
                {
                    try
                    {
                        if (this.ImageLinkData.Version < imgData.Version)
                        {
                            imgData.DownLoadImages();
                            if ((!imgData.CheckLocalImages() || !imgData.ConvertServer2Local() ? false : this.ImageLinkData.SetIsNew(imgData)))
                            {
                                this.ImageLinkData.DeleteImages();
                                DispatcherHelper.CheckBeginInvokeOnUI(() => {
                                    this.ImageLinkData = imgData;
                                    this.SelectedNavbar = this.ImageLinkData.Navbars.FirstOrDefault<NavbarsItem>();
                                });
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception exception)
                    {
                        this._logger.Error<Exception>(exception);
                    }
                }
                else
                {
                    this._logger.Error<Exception>(ex);
                }
            });

            //获取连接信息
            this._dataService.GetLikeNumber((string number, Exception ex) => {
                if (ex != null)
                {
                    this._logger.Error<Exception>(ex);
                    this.LikeNumber = "暂无数据";
                }
                else
                {
                    this.LikeNumber = number;
                }
            });
            //获取天气信息
            this._dataService.GetWeather(this.Config.WeatherUrl, (WeatherInfo weather, Exception ex) => {
                if (ex != null)
                {
                    this._logger.Error<Exception>(ex);
                }
                else
                {
                    this.LocationWeatherInfo = weather;
                }
            });

            //获取当前的选择菜单
            this.SelectedNavbar = this.ImageLinkData.Navbars.FirstOrDefault<NavbarsItem>();

            //
            this.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(MainViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(MainViewModel).GetMethod("get_CanOpen").MethodHandle)), new ParameterExpression[0]));
        }

        public void ChangeBeePCVersion()
        {
            this.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(MainViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle(typeof(MainViewModel).GetMethod("get_CanOpen").MethodHandle)), new ParameterExpression[0]));
            if ((this.SelectedRevitProduct == null ? false : this.SelectedBeePCProduct != null))
            {
                this.RevitProducts.ClearBeePCProduct();
                string str = string.Concat(Directory.GetParent(this.SelectedRevitProduct.AllUsersAddInFolder).FullName, "\\", this.SelectedRevitProduct.IntVersion());
                string str1 = string.Concat(str, "\\BeePC.addin");
                string str2 = this.SelectedBeePCProduct.BuildXmlString(this.SelectedRevitProduct.IntVersion());
                File.WriteAllText(str1, str2);
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();
            this.Config.LocalImageLinkData = this.ImageLinkData;
            this.RevitProducts.ClearBeePCProduct();
            this.Config.BeePCProductList = (
                from item in this.BeePCProducts
                select item.InstallPath).ToList<string>();
            GlobalData.Config = this.Config;
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}