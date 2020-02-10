namespace Hao.Launcher.Data
{
	public class MessageToken
	{
		public readonly static string ToOpenUrl;

		public readonly static string ToOpenStrUrl;

		public readonly static string OnChangeImage;

		public readonly static string ToOpenWindow;

		public readonly static string ToOpenRevit;

		public readonly static string ToAddBeePC;

		public readonly static string ToDelBeePC;

		static MessageToken()
		{
			MessageToken.ToOpenUrl = "ToOpenUrl";
			MessageToken.ToOpenStrUrl = "ToOpenStrUrl";
			MessageToken.OnChangeImage = "OnChangeImage";
			MessageToken.ToOpenWindow = "ToOpenWindow";
			MessageToken.ToOpenRevit = "ToOpenRevit";
			MessageToken.ToAddBeePC = "ToAddBeePC";
			MessageToken.ToDelBeePC = "ToDelBeePC";
		}

		public MessageToken()
		{
		}
	}
}