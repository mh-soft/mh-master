using Hao.Launcher.Data;
using Hao.Launcher.Helper;
using Hao.Launcher.Model;
using Hao.Launcher.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Hao.Launcher.Window
{
	/// <summary>
	/// 启动当前的主窗体
	/// </summary>
	public partial class MainWindow : BlurWindow
	{
		private bool AutoDeactivate;

		private bool CanLike = false;

		private int times = 0;

		private readonly string path = string.Concat(ConstData.FullFolder, Path.DirectorySeparatorChar.ToString(), "like.dat");

		private readonly static object LockObj;

		/// <summary>
		/// 当前的主窗体信息
		/// </summary>
		static MainWindow()
		{
			MainWindow.LockObj = new object();
		}
		/// <summary>
		/// 当前的主窗体的构造函数
		/// </summary>
		public MainWindow()
		{
			//初始化当前的组件
			this.InitializeComponent();

			//读取连接数据
			this.readLikeData();

			//注册当前的消息
			Messenger.Default.Register<string>(this, MessageToken.ToOpenWindow, (string windowName) =>
			{
				if ("BeePCManage".Equals(windowName))
				{
					(new BeePCManage()).ShowDialog();
				}
			}, false);

			//注册一个新的消息
			Messenger.Default.Register<string>(this, MessageToken.ToOpenRevit, (string argument0) =>
			{
				this.AutoDeactivate = true;
				base.WindowState = System.Windows.WindowState.Minimized;
			}, false);



			base.Closing += new CancelEventHandler((object argument1, CancelEventArgs argument2) => ViewModelLocator.Cleanup());
			base.Deactivated += new EventHandler(this.MainWindow_Deactivated);
			base.Activated += new EventHandler(this.MainWindow_Activated);

			//初始化一个定时器
			DispatcherTimer dispatcherTimer = new DispatcherTimer()
			{
				Interval = TimeSpan.FromSeconds(5)
			};
			dispatcherTimer.Tick += new EventHandler((object argument3, EventArgs argument4) =>
			{
				if ((this.Carousel.IsMouseOver ? false : ((bool?)(this.Carousel.Tag as bool?)).GetValueOrDefault()))
				{
					HandyControl.Controls.Carousel carousel = this.Carousel;
					int pageIndex = carousel.PageIndex + 1;
					carousel.PageIndex = pageIndex;
				}
			});
			dispatcherTimer.Start();



			base.Activate();
			base.Loaded += new RoutedEventHandler((object s, RoutedEventArgs e) => this.StackPanelGif.BeginAnimation(FrameworkElement.WidthProperty, Helper.AnimationHelper.CreateAnimation(0, 3500)));
			base.KeyDown += new KeyEventHandler((object s, KeyEventArgs e) =>
			{
				if ((!Keyboard.IsKeyDown(Key.LeftCtrl) || !Keyboard.IsKeyDown(Key.LeftAlt) ? false : Keyboard.IsKeyDown(Key.F10)))
				{
					(new BeePCManage()).ShowDialog();
				}
			});
		}

		private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void Carousel_OnClick(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send<int>(this.Carousel.PageIndex, MessageToken.OnChangeImage);
		}

		private void Carousel_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Messenger.Default.Send<int>(this.Carousel.PageIndex, MessageToken.ToOpenUrl);
		}

		private void ChromiumWebBrowser_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		private void DealSliderValueChange(System.Windows.Controls.Slider slider)
		{
			double? from;
			Duration duration;
			if (slider.Value < 0.1)
			{
				if (Math.Abs(slider.Value - slider.Minimum) < 0.0038)
				{
					slider.Value = 0;
					this.TopGrid.Focus();
					return;
				}
				DoubleAnimation doubleAnimation = new DoubleAnimation()
				{
					From = new double?(slider.Value),
					To = new double?(0)
				};
				DoubleAnimation doubleAnimation1 = doubleAnimation;
				if (doubleAnimation.From.Value <= 0)
				{
					duration = TimeSpan.FromSeconds(0);
				}
				else
				{
					double value = doubleAnimation.To.Value;
					from = doubleAnimation.From;
					duration = new Duration(TimeSpan.FromSeconds(Math.Abs(value - from.Value) / 2));
				}
				doubleAnimation1.Duration = duration;
				Storyboard.SetTarget(doubleAnimation, slider);
				Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Value", new object[0]));
				Storyboard storyboard = new Storyboard();
				storyboard.Children.Add(doubleAnimation);
				storyboard.Completed += new EventHandler((object argument0, EventArgs argument1) =>
				{
					slider.Value = 0;
					storyboard.Stop();
					this.TopGrid.Focus();
				});
				storyboard.Begin();
			}
			else
			{
				if (Math.Abs(slider.Value - slider.Maximum) < 0.0038)
				{
					Messenger.Default.Send<string>(null, MessageToken.ToOpenRevit);
					slider.Value = 0;
					this.TopGrid.Focus();
					return;
				}
				DoubleAnimation duration1 = new DoubleAnimation()
				{
					From = new double?(slider.Value),
					To = new double?(1)
				};
				double num = duration1.To.Value;
				from = duration1.From;
				duration1.Duration = new Duration(TimeSpan.FromSeconds(Math.Abs(num - from.Value) / 2));
				Storyboard.SetTarget(duration1, slider);
				Storyboard.SetTargetProperty(duration1, new PropertyPath("Value", new object[0]));
				Storyboard storyboard1 = new Storyboard();
				storyboard1.Children.Add(duration1);
				storyboard1.Completed += new EventHandler((object s, EventArgs e) =>
				{
					Messenger.Default.Send<string>(null, MessageToken.ToOpenRevit);
					slider.Value = 0;
					storyboard1.Stop();
					this.TopGrid.Focus();
				});
				storyboard1.Begin();
			}
		}

		private void Like_OnClick(object sender, RoutedEventArgs e)
		{
			
		}

		private void MainWindow_Activated(object sender, EventArgs e)
		{
			this.AutoDeactivate = false;
		}

		private void MainWindow_Deactivated(object sender, EventArgs e)
		{
			if (!this.AutoDeactivate)
			{
				this.DealSliderValueChange(this.Slider);
			}
		}

		private void Min_OnClick(object sender, RoutedEventArgs e)
		{
			base.WindowState = System.Windows.WindowState.Minimized;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
		}

		private void readLikeData()
		{
		
		}

		private void Share_OnClick(object sender, RoutedEventArgs e)
		{
			ShareWindow shareWindow = SingleOpenHelper.CreateControl<ShareWindow>();
			(new PopupWindow()
			{
				PopupElement = shareWindow
			}).ShowDialog(this.Share, false);
		}

		private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.Slider.CaptureMouse();
			e.Handled = true;
		}

		private void StackPanel_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			e.Handled = true;
			if ((e.LeftButton == MouseButtonState.Pressed ? true : e.RightButton == MouseButtonState.Pressed))
			{
				Point position = Mouse.GetPosition(this.Slider);
				Console.WriteLine(position.X / this.Slider.ActualWidth);
				System.Windows.Controls.Slider slider = this.Slider;
				position = Mouse.GetPosition(this.Slider);
				slider.Value = position.X / this.Slider.ActualWidth;
				Console.WriteLine(string.Concat("Value:", this.Slider.Value));
			}
		}

		private void StackPanel_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			this.Slider.IsEnabled = false;
			e.Handled = true;
			this.Slider.ReleaseMouseCapture();
			this.DealSliderValueChange(this.Slider);
			this.Slider.IsEnabled = true;
		}

		private void Timeline_OnCompleted(object sender, EventArgs e)
		{
		}

		private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Badge badge = sender as Badge;
			if ((badge != null ? badge.Tag : null) is List<ImagesItem>)
			{
				int num = ((List<ImagesItem>)badge.Tag).FindIndex((ImagesItem item) => item.IsNew);
				if (num != -1)
				{
					this.Carousel.PageIndex = num;
				}
			}
		}

		private void writeLike2File()
		{
			DateTime today = DateTime.Today;
			string str = string.Format("{0}${1}", today.ToShortDateString(), this.times);
			if (!File.Exists(this.path))
			{
				File.Create(this.path).Close();
			}
			File.WriteAllText(this.path, str);
		}
	}
}