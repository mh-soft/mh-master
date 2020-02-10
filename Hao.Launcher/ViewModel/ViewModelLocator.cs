/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Hao.Launcher"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using Hao.Launcher.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Diagnostics.CodeAnalysis;

namespace Hao.Launcher.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public static BeePCManageViewModel BeePCManage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BeePCManageViewModel>();
            }
        }

        /// <summary>
        /// 当前的主页面
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public static MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// 当前建泰构造
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider( () => SimpleIoc.Default));
            //注册当前的数据模型
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BeePCManageViewModel>();
        }

        /// <summary>
        /// 当前的实例对象
        /// </summary>
        public ViewModelLocator()
        {
        }

        public static void Cleanup()
        {
            ViewModelLocator.Main.Cleanup();
        }
    }
}