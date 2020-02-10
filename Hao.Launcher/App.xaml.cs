using Hao.Launcher.Data;
using Hao.Launcher.Helper;
using GalaSoft.MvvmLight.Threading;
using NLog;
using SingleInstanceApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

/// <summary>
/// 当前的应用程序启动类
/// </summary>
namespace Hao.Launcher
{
    /// <summary>
    /// 当前的应用程序启动类
    /// </summary>
    public partial class App : Application, ISingleInstance
    {
        /// <summary>
        /// 当前的互斥对象
        /// </summary>
        private const string Unique = "Hao.Launcher";

        /// <summary>
        /// 当前的日志对象
        /// </summary>
        private readonly static Logger _logger;

        /// <summary>
        /// 当前的默认构造函数
        /// </summary>
        static App()
        {
            App._logger = LogManager.GetCurrentClassLogger();
        }

        public App()
        {
        }

        /// <summary>
        /// 应用程序报错
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            App._logger.Error("程序异常！");
            App._logger.Error<Exception>(e.Exception);
            SingleInstance<App>.Cleanup();
        }

        /// <summary>
        /// 应用程序的启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ProcessHelper.StartProcessStartInfo(new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase)
            {
                UseShellExecute = true,
                Verb = "runas"
            });
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            App._logger.Error("程序异常！");
            App._logger.Error(e.ExceptionObject);
            SingleInstance<App>.Cleanup();
        }

        /// <summary>
        /// 当前应用程序的main函数
        /// </summary>
        [STAThread]
        public static void Main()
        {
            App._logger.Error("程序启动！");
            //初始化当前程序，实现单例模式
            if (SingleInstance<App>.InitializeAsFirstInstance("Hao.Launcher"))
            {
                //启动当前的应用程序
                App._logger.Error("单一程序锁启动！");
                App app = new App();
                GlobalData.Init();
                //监听应用程序的图纸
                app.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App.App_DispatcherUnhandledException);
                TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(App.TaskSchedulerOnUnobservedTaskException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(App.CurrentDomainOnUnhandledException);
                
                //初始化当前的监听信息
                DispatcherHelper.Initialize();
                //初始化当前组件
                app.InitializeComponent();
                //启动应用程序
                app.Run();
                App._logger.Error("单一程序锁清除！");
                SingleInstance<App>.Cleanup();
            }
        }

        /// <summary>
        /// 当前应用程序的退出
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            SingleInstance<App>.Cleanup();
            GlobalData.Save();
            base.OnExit(e);
        }

        /// <summary>
        /// 当前应用程序的重启
        /// </summary>
        public void Restart()
        {
            GlobalData.Save();
            ProcessModule mainModule = Process.GetCurrentProcess().MainModule;
            if (mainModule != null)
            {
                SingleInstance<App>.Cleanup();
                Process.Start(mainModule.FileName);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 启动当前窗体
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            bool flag;
            System.Windows.Window mainWindow = base.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.Activate();
                flag = true;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 当前应用程序的异常信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            App._logger.Error("程序异常！");
            App._logger.Error<AggregateException>(e.Exception);
            SingleInstance<App>.Cleanup();
        }
    }
}