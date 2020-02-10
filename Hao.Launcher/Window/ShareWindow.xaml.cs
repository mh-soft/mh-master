using Hao.Launcher.Data;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Hao.Launcher.Window
{
	public partial class ShareWindow : Grid, ISingleOpen, IDisposable
	{
		private bool _disposed;

		public bool CanDispose { get; } = true;

		public ShareWindow()
		{
			this.InitializeComponent();
		}

		private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				System.Windows.Threading.Dispatcher dispatcher = base.Dispatcher;
				if (dispatcher != null)
				{
					dispatcher.BeginInvoke(new Action(() =>
					{
						System.Windows.Window window = System.Windows.Window.GetWindow(this);
						if (window != null)
						{
							window.Close();
						}
						else
						{
						}
					}), new object[0]);
				}
				else
				{
				}
				this._disposed = true;
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void QQ_OnClick(object sender, RoutedEventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("https://connect.qq.com/widget/shareqq/index.html?");
			stringBuilder.Append("url=").Append("http://www.wengwengkeji.com/");
			stringBuilder.Append("&title=").Append("BeePC，最好用的装配式深化设计软件~");
			stringBuilder.Append("&desc=").Append("快来试试BeePC!");
			stringBuilder.Append("&pics=").Append("http://www.wengwengkeji.com/wengweng/lib/images/news/18052802/1.jpg");
			stringBuilder.Append("&summary").Append("123");
			Messenger.Default.Send<string>(stringBuilder.ToString(), MessageToken.ToOpenStrUrl);
		}

		private void Sina_OnClick(object sender, RoutedEventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("http://service.weibo.com/share/share.php?");
			stringBuilder.Append("url=").Append("http://www.wengwengkeji.com/");
			stringBuilder.Append("&type=").Append("button");
			stringBuilder.Append("&style=").Append("number");
			stringBuilder.Append("&appkey=").Append("");
			stringBuilder.Append("&title=").Append("BeePC，最好用的装配式深化设计软件~");
			stringBuilder.Append("&pic=").Append("http://www.wengwengkeji.com/wengweng/lib/images/news/18052802/1.jpg");
			stringBuilder.Append("&ralateUid=").Append("");
			stringBuilder.Append("&language=").Append("zh_cn");
			Messenger.Default.Send<string>(stringBuilder.ToString(), MessageToken.ToOpenStrUrl);
		}

		private void Zone_OnClick(object sender, RoutedEventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("https://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?");
			stringBuilder.Append("url=").Append("http://www.wengwengkeji.com/");
			stringBuilder.Append("&title=").Append("BeePC，最好用的装配式深化设计软件~");
			Messenger.Default.Send<string>(stringBuilder.ToString(), MessageToken.ToOpenStrUrl);
		}
	}
}