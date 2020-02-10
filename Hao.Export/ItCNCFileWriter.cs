using System;
using System.IO;
using AdskLocalisation;
using AdskRevitUI;

namespace Hao.Export.MachineData
{
	// Token: 0x0200000E RID: 14
	public static class ItCNCFileWriter
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000411C File Offset: 0x0000231C
		public static void ResetSettings()
		{
			ItCNCFileWriter.bAlwaysOverwrite = false;
			ItCNCFileWriter.bNeverOverwrite = false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000412C File Offset: 0x0000232C
		public static bool dontOverwriteExistingFile(string sFullPath, CamIntOverwriteMode overwriteMode, out bool cancel)
		{
			cancel = false;
			bool flag = overwriteMode == CamIntOverwriteMode.Overwrite;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = false;
				bool flag3 = File.Exists(sFullPath);
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = overwriteMode == CamIntOverwriteMode.Cancel;
					if (flag5)
					{
						cancel = true;
						return true;
					}
					bool flag6 = overwriteMode == CamIntOverwriteMode.Skip;
					if (flag6)
					{
						return true;
					}
					bool flag7 = ItCNCFileWriter.bNeverOverwrite;
					if (flag7)
					{
						flag2 = true;
					}
					else
					{
						bool flag8 = !ItCNCFileWriter.bAlwaysOverwrite;
						if (flag8)
						{
							string fileName = Path.GetFileName(sFullPath);
							string lclTitleProceed = ItCNCFileWriter._lclTitleProceed;
							ModalDialog modalDialog = new ModalDialog(lclTitleProceed);
							modalDialog.MainContent = "msgProceedWithOverwrite".LocaliseFormat(new string[]
							{
								fileName
							});
							modalDialog.CommonButtons = (ModalDialogButtons.Yes | ModalDialogButtons.Cancel);
							modalDialog.DefaultButton = ModalDialogResult.Cancel;
							modalDialog.Id = "RevitPrecastCNCOverwriteFile";
							modalDialog.AddCommandLink(ModalDialogCommandLinkId.CommandLink1, ItCNCFileWriter._lclMsgYesToAll);
							ModalDialogResult modalDialogResult = modalDialog.Show();
							flag2 = (modalDialogResult == ModalDialogResult.Cancel);
							ItCNCFileWriter.bAlwaysOverwrite = (modalDialogResult == ModalDialogResult.CommandLink1);
							cancel = (modalDialogResult == ModalDialogResult.Cancel);
						}
					}
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x0400002F RID: 47
		private static bool bAlwaysOverwrite;

		// Token: 0x04000030 RID: 48
		private static bool bNeverOverwrite;

		// Token: 0x04000031 RID: 49
		private static readonly string _lclTitleProceed = "titleCamFileProceed".Localise();

		// Token: 0x04000032 RID: 50
		private static readonly string _lclMsgYesToAll = "msgYesToAll".Localise();
	}
}
