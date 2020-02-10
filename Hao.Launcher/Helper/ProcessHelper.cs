using HandyControl.Controls;
using HandyControl.Data;
using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Hao.Launcher.Helper
{
	public class ProcessHelper
	{
		private readonly static Logger _logger;

		private const int WM_SYSCOMMAND = 274;

		private const int SC_CLOSE = 61536;

		private const int SC_MINIMIZE = 61472;

		private const int SC_MAXIMIZE = 61488;

		static ProcessHelper()
		{
			ProcessHelper._logger = LogManager.GetCurrentClassLogger();
		}

		public ProcessHelper()
		{
		}

		[DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
		private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
		private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

		[DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
		private static extern void SetForegroundWindow(IntPtr hwnd);

		public static bool StartProcess(string exePath, string[] args = null)
		{
			bool flag;
			if ((string.IsNullOrWhiteSpace(exePath) ? false : File.Exists(exePath)))
			{
				try
				{
					string str = "";
					if (args != null)
					{
						str = args.Aggregate<string, string>(str, (string current, string arg) => string.Concat(current, "\"", arg, "\" "));
						str = str.Trim();
					}
					flag = ProcessHelper.StartProcessStartInfo(new ProcessStartInfo(exePath, str));
				}
				catch (Exception exception)
				{
					ProcessHelper._logger.Error<Exception>(exception);
					flag = false;
				}
			}
			else
			{
				MessageBox.Show(new MessageBoxInfo()
				{
					Caption = "小蜜蜂提示",
					Message = "Revit查找失败,请关闭重试！若问题一直存在,请联系BeePC"
				});
				flag = false;
			}
			return flag;
		}

		public static bool StartProcessStartInfo(ProcessStartInfo startInfo)
		{
			bool flag;
			try
			{
				bool flag1 = (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
				if (!flag1)
				{
					startInfo.UseShellExecute = false;
					startInfo.RedirectStandardError = false;
					startInfo.Verb = "runas";
				}
				if (Process.Start(startInfo) == null)
				{
					flag = false;
					return flag;
				}
			}
			catch (Exception exception)
			{
				ProcessHelper._logger.Error<Exception>(exception);
			}
			flag = false;
			return flag;
		}
	}
}