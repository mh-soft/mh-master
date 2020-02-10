namespace Hao.Launcher.Model
{
	public class BeePCProduct
	{
		public bool CanRemove { get; set; } = true;

		public string CommandDllPath
		{
			get;
			set;
		}

		public string Guid
		{
			get;
			set;
		}

		public string InstallPath
		{
			get;
			set;
		}

		public string Version
		{
			get;
			set;
		}

		public BeePCProduct()
		{
		}
	}
}