using Hao.Launcher.Data;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Hao.Launcher.Window
{
	public partial class BeePCManage : System.Windows.Window, ISingleOpen, IDisposable
	{
		private bool _disposed;

		public bool CanDispose { get; } = true;

		public BeePCManage()
		{
			this.InitializeComponent();
			base.Activate();
			base.KeyDown += new System.Windows.Input.KeyEventHandler((object s, System.Windows.Input.KeyEventArgs e) =>
			{
				if (e.Key == Key.Escape)
				{
					base.Close();
				}
			});
		}

		private void ButtonAddBeePC_OnClick(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
			{
				Description = "请选择到BeePC安装目录！"
			};
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				Messenger.Default.Send<string>(selectedPath, MessageToken.ToAddBeePC);
			}
		}

		private void Close_OnClick(object sender, RoutedEventArgs e)
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
	}
}