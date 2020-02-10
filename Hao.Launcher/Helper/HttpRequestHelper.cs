using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hao.Launcher.Helper
{
	public class HttpRequestHelper
	{
		public HttpRequestHelper()
		{
		}

		private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			return (errors != SslPolicyErrors.None ? false : true);
		}

		public static HttpWebResponse CreateGetHttpResponse(string url)
		{
			HttpWebRequest version10 = null;
			if (!url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
			{
				version10 = WebRequest.Create(url) as HttpWebRequest;
			}
			else
			{
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(HttpRequestHelper.CheckValidationResult);
				version10 = WebRequest.Create(url) as HttpWebRequest;
				version10.ProtocolVersion = HttpVersion.Version10;
			}
			version10.Method = "GET";
			return version10.GetResponse() as HttpWebResponse;
		}

		public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters)
		{
			HttpWebRequest httpWebRequest = null;
			httpWebRequest = (!url.StartsWith("https", StringComparison.OrdinalIgnoreCase) ? WebRequest.Create(url) as HttpWebRequest : WebRequest.Create(url) as HttpWebRequest);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			if ((parameters == null ? false : parameters.Count != 0))
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				foreach (string key in parameters.Keys)
				{
					if (num <= 0)
					{
						stringBuilder.AppendFormat("{0}={1}", key, parameters[key]);
						num++;
					}
					else
					{
						stringBuilder.AppendFormat("&{0}={1}", key, parameters[key]);
					}
				}
				byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(bytes, 0, (int)bytes.Length);
				}
			}
			httpWebRequest.Headers.GetValues("Content-Type");
			return httpWebRequest.GetResponse() as HttpWebResponse;
		}

		public static string GetResponseString(HttpWebResponse webresponse)
		{
			string end;
			using (Stream responseStream = webresponse.GetResponseStream())
			{
				end = (new StreamReader(responseStream, Encoding.UTF8)).ReadToEnd();
			}
			return end;
		}

		public static bool HttpDownload(string url, string path)
		{
			bool flag;
			string str = string.Concat(Path.GetDirectoryName(path), "\\temp");
			Directory.CreateDirectory(str);
			string str1 = string.Concat(str, "\\", Path.GetFileName(path), ".temp");
			if (File.Exists(str1))
			{
				File.Delete(str1);
			}
			try
			{
				FileStream fileStream = new FileStream(str1, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				Stream responseStream = (httpWebRequest.GetResponse() as HttpWebResponse).GetResponseStream();
				byte[] numArray = new byte[1024];
				for (int i = responseStream.Read(numArray, 0, (int)numArray.Length); i > 0; i = responseStream.Read(numArray, 0, (int)numArray.Length))
				{
					fileStream.Write(numArray, 0, i);
				}
				fileStream.Close();
				responseStream.Close();
				File.Move(str1, path);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		public static string HttpGet(string Url, string postDataStr)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(Url, (postDataStr == "" ? "" : "?"), postDataStr));
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentType = "application/json";
			Stream responseStream = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
			string end = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			return end;
		}

		public static async Task<string> HttpGetAsync(string Url, string postDataStr)
		{
			string str = await Task.Run<string>(() => HttpRequestHelper.HttpGet(Url, postDataStr));
			return str;
		}

		public static string HttpPost(string Url, string postDataStr, ref bool isSuccess)
		{
			string message;
			try
			{
				HttpWebRequest byteCount = (HttpWebRequest)WebRequest.Create(Url);
				byteCount.Method = "POST";
				byteCount.ContentType = "application/json";
				byteCount.ContentLength = (long)Encoding.UTF8.GetByteCount(postDataStr);
				Stream requestStream = byteCount.GetRequestStream();
				StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.GetEncoding("gb2312"));
				streamWriter.Write(postDataStr);
				streamWriter.Close();
				Stream responseStream = ((HttpWebResponse)byteCount.GetResponse()).GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
				string end = streamReader.ReadToEnd();
				streamReader.Close();
				responseStream.Close();
				isSuccess = true;
				message = end;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				isSuccess = false;
				Console.Write(exception.Message);
				message = exception.Message;
			}
			return message;
		}
	}
}