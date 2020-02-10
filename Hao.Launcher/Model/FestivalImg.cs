using GalaSoft.MvvmLight;

namespace Hao.Launcher.Model
{
	public class FestivalImg : ViewModelBase
	{
		public string FestivalName
		{
			get;
			set;
		}

		public string ImgUrl
		{
			get;
			set;
		}

		public string Replay
		{
			get;
			set;
		}

		public string StartTime
		{
			get;
			set;
		}

		public string StopTime
		{
			get;
			set;
		}

		public FestivalImg()
		{
		}
	}
}