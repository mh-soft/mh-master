using HandyControl.Interactivity;
using HandyControl.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hao.Launcher.Controls
{
	public class FloatingBlock : Control
	{
		private readonly static double Double0Box;

		public readonly static DependencyProperty ToXProperty;

		public readonly static DependencyProperty ToYProperty;

		public readonly static DependencyProperty DurationProperty;

		public readonly static DependencyProperty HorizontalOffsetProperty;

		public readonly static DependencyProperty VerticalOffsetProperty;

		public readonly static DependencyProperty ContentTemplateProperty;

		private readonly static DependencyProperty ReadyToFloatProperty;

		public readonly static DependencyProperty ContentProperty;

		public object Content
		{
			get
			{
				return base.GetValue(FloatingBlock.ContentProperty);
			}
			set
			{
				base.SetValue(FloatingBlock.ContentProperty, value);
			}
		}

		public DataTemplate ContentTemplate
		{
			get
			{
				return (DataTemplate)base.GetValue(FloatingBlock.ContentTemplateProperty);
			}
			set
			{
				base.SetValue(FloatingBlock.ContentTemplateProperty, value);
			}
		}

		static FloatingBlock()
		{
			FloatingBlock.Double0Box = 0;
			FloatingBlock.ToXProperty = DependencyProperty.RegisterAttached("ToX", typeof(double), typeof(FloatingBlock), new PropertyMetadata((object)FloatingBlock.Double0Box));
			FloatingBlock.ToYProperty = DependencyProperty.RegisterAttached("ToY", typeof(double), typeof(FloatingBlock), new PropertyMetadata((object)-100));
			FloatingBlock.DurationProperty = DependencyProperty.RegisterAttached("Duration", typeof(Duration), typeof(FloatingBlock), new PropertyMetadata((object)(new Duration(TimeSpan.FromSeconds(2)))));
			FloatingBlock.HorizontalOffsetProperty = DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double), typeof(FloatingBlock), new PropertyMetadata((object)FloatingBlock.Double0Box));
			FloatingBlock.VerticalOffsetProperty = DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(FloatingBlock), new PropertyMetadata((object)FloatingBlock.Double0Box));
			FloatingBlock.ContentTemplateProperty = DependencyProperty.RegisterAttached("ContentTemplate", typeof(DataTemplate), typeof(FloatingBlock), new PropertyMetadata(null, new PropertyChangedCallback(FloatingBlock.OnDataChanged)));
			FloatingBlock.ReadyToFloatProperty = DependencyProperty.RegisterAttached("ReadyToFloat", typeof(bool), typeof(FloatingBlock), new PropertyMetadata(false));
			FloatingBlock.ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(object), typeof(FloatingBlock), new PropertyMetadata(null, new PropertyChangedCallback(FloatingBlock.OnDataChanged)));
		}

		public FloatingBlock()
		{
		}

		private static FloatingBlock CreateBlock(Visual element, AdornerContainer adorner)
		{
			Point position = Mouse.GetPosition(adorner.AdornedElement);
			TranslateTransform translateTransform = new TranslateTransform()
			{
				X = position.X + FloatingBlock.GetHorizontalOffset(element),
				Y = position.Y + FloatingBlock.GetVerticalOffset(element)
			};
			FloatingBlock floatingBlock = new FloatingBlock()
			{
				Content = FloatingBlock.GetContent(element),
				ContentTemplate = FloatingBlock.GetContentTemplate(element)
			};
			TransformGroup transformGroup = new TransformGroup();
			transformGroup.Children.Add(translateTransform);
			floatingBlock.RenderTransform = transformGroup;
			FloatingBlock floatingBlock1 = floatingBlock;
			double totalMilliseconds = FloatingBlock.GetDuration(element).TimeSpan.TotalMilliseconds;
			DoubleAnimation doubleAnimation = AnimationHelper.CreateAnimation(FloatingBlock.GetToX(element) + translateTransform.X, totalMilliseconds);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)", new object[0]));
			Storyboard.SetTarget(doubleAnimation, floatingBlock1);
			DoubleAnimation doubleAnimation1 = AnimationHelper.CreateAnimation(FloatingBlock.GetToY(element) + translateTransform.Y, totalMilliseconds);
			Storyboard.SetTargetProperty(doubleAnimation1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)", new object[0]));
			Storyboard.SetTarget(doubleAnimation1, floatingBlock1);
			DoubleAnimation doubleAnimation2 = AnimationHelper.CreateAnimation(0, totalMilliseconds);
			Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("Opacity", new object[0]));
			Storyboard.SetTarget(doubleAnimation2, floatingBlock1);
			Storyboard storyboard = new Storyboard();
			storyboard.Completed += new EventHandler((object s, EventArgs e) =>
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(element);
				if (adornerLayer != null)
				{
					adorner.Child = null;
					adornerLayer.Remove(adorner);
				}
			});
			storyboard.Children.Add(doubleAnimation);
			storyboard.Children.Add(doubleAnimation1);
			storyboard.Children.Add(doubleAnimation2);
			storyboard.Begin();
			return floatingBlock1;
		}

		public static object GetContent(DependencyObject element)
		{
			return element.GetValue(FloatingBlock.ContentProperty);
		}

		public static DataTemplate GetContentTemplate(DependencyObject element)
		{
			return (DataTemplate)element.GetValue(FloatingBlock.ContentTemplateProperty);
		}

		public static Duration GetDuration(DependencyObject element)
		{
			return (Duration)element.GetValue(FloatingBlock.DurationProperty);
		}

		public static double GetHorizontalOffset(DependencyObject element)
		{
			return (double)element.GetValue(FloatingBlock.HorizontalOffsetProperty);
		}

		private static bool GetReadyToFloat(DependencyObject element)
		{
			return (bool)element.GetValue(FloatingBlock.ReadyToFloatProperty);
		}

		public static double GetToX(DependencyObject element)
		{
			return (double)element.GetValue(FloatingBlock.ToXProperty);
		}

		public static double GetToY(DependencyObject element)
		{
			return (double)element.GetValue(FloatingBlock.ToYProperty);
		}

		public static double GetVerticalOffset(DependencyObject element)
		{
			return (double)element.GetValue(FloatingBlock.VerticalOffsetProperty);
		}

		private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			UIElement uIElement = d as UIElement;
			if (uIElement != null)
			{
				uIElement.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(FloatingBlock.Target_PreviewMouseLeftButtonDown));
				uIElement.RemoveHandler(UIElement.PreviewMouseLeftButtonUpEvent, new MouseButtonEventHandler(FloatingBlock.Target_PreviewMouseLeftButtonUp));
				if (e.NewValue != null)
				{
					uIElement.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(FloatingBlock.Target_PreviewMouseLeftButtonDown));
					uIElement.AddHandler(UIElement.PreviewMouseLeftButtonUpEvent, new MouseButtonEventHandler(FloatingBlock.Target_PreviewMouseLeftButtonUp));
				}
			}
		}

		public static void SetContent(DependencyObject element, object value)
		{
			element.SetValue(FloatingBlock.ContentProperty, value);
		}

		public static void SetContentTemplate(DependencyObject element, DataTemplate value)
		{
			element.SetValue(FloatingBlock.ContentTemplateProperty, value);
		}

		public static void SetDuration(DependencyObject element, Duration value)
		{
			element.SetValue(FloatingBlock.DurationProperty, value);
		}

		public static void SetHorizontalOffset(DependencyObject element, double value)
		{
			element.SetValue(FloatingBlock.HorizontalOffsetProperty, value);
		}

		private static void SetReadyToFloat(DependencyObject element, bool value)
		{
			element.SetValue(FloatingBlock.ReadyToFloatProperty, value);
		}

		public static void SetToX(DependencyObject element, double value)
		{
			element.SetValue(FloatingBlock.ToXProperty, value);
		}

		public static void SetToY(DependencyObject element, double value)
		{
			element.SetValue(FloatingBlock.ToYProperty, value);
		}

		public static void SetVerticalOffset(DependencyObject element, double value)
		{
			element.SetValue(FloatingBlock.VerticalOffsetProperty, value);
		}

		private static void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			FloatingBlock.SetReadyToFloat(sender as DependencyObject, true);
		}

		private static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			UIElement uIElement = sender as UIElement;
			if ((uIElement == null ? false : FloatingBlock.GetReadyToFloat(uIElement)))
			{
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(uIElement);
				if (adornerLayer != null)
				{
					AdornerContainer adornerContainer = new AdornerContainer(adornerLayer)
					{
						IsHitTestVisible = false,

					};
					adornerContainer.Child = FloatingBlock.CreateBlock(uIElement, adornerContainer);
					adornerLayer.Add(adornerContainer);
				}
				FloatingBlock.SetReadyToFloat(uIElement, false);
			}
		}
	}
}