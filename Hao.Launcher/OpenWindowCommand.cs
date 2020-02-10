using Hao.Launcher.Data;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Input;

namespace Hao.Launcher
{
	public class OpenWindowCommand : ICommand
	{
		public OpenWindowCommand()
		{
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			Messenger.Default.Send<string>(parameter.ToString(), MessageToken.ToOpenWindow);
		}

		public event EventHandler CanExecuteChanged;
	}
}