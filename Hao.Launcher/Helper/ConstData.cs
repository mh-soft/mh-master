using System;
using System.IO;

namespace Hao.Launcher.Helper
{
	public class ConstData
	{
		public readonly static string FolderName;

		public readonly static string FullFolder;

		public readonly static string IniFileName;

		public readonly static string FullIniFilePath;

		public readonly static string JsonFileName;

		public readonly static string FullJsonFilePath;

		public readonly static string BeePCFolder;

		public const string STARTER = "STARTER";

		public const string SELECT_REVIT = "SELECT_REVIT";

		public const string SELECT_BEEPC = "SELECT_BEEPC";

		public const string LAST_UPDATE_TIME = "LAST_UPDATE_TIME";

		public const string BIG_IMAGE = "bigImage";

		public const string SMALL_IMAGE = "smallImage";

		public const string IMAGE_SUFFIX = ".img";

		public const string BEEPC = "BEEPC";

		public const string InstallPath = "InstallPath";

		public const string Version = "Version";

		public const string ProductAmount = "ProductAmount";

		public const string ServerUrl = "http://192.168.0.157:65145/";

		static ConstData()
		{
			ConstData.FolderName = "BeePC";
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			ConstData.FullFolder = string.Concat(folderPath, directorySeparatorChar.ToString(), ConstData.FolderName);
			ConstData.IniFileName = "BeePCStarter.dat";
			string fullFolder = ConstData.FullFolder;
			directorySeparatorChar = Path.DirectorySeparatorChar;
			ConstData.FullIniFilePath = string.Concat(fullFolder, directorySeparatorChar.ToString(), ConstData.IniFileName);
			ConstData.JsonFileName = "ServerData.dat";
			string str = ConstData.FullFolder;
			directorySeparatorChar = Path.DirectorySeparatorChar;
			ConstData.FullJsonFilePath = string.Concat(str, directorySeparatorChar.ToString(), ConstData.JsonFileName);
			ConstData.BeePCFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
			if (!Directory.Exists(ConstData.FullFolder))
			{
				Directory.CreateDirectory(ConstData.FullFolder);
			}
		}

		public ConstData()
		{
		}
	}
}