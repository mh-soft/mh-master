using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hao.Shell
{
    public class Unity
    {

        /// <summary>
        /// 获取当前应用程序的指定路径
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyPath() {
            Unity u = new Unity();
            if (u.StartupFolder != null) {

                return u.StartupFolder.LocalPath;
            }
            return null;
        }

        /// <summary>
        /// 当前的程序启动目录
        /// </summary>
        public Uri StartupFolder
        {
            get
            {
                Uri baseUri = new Uri(ExecutablePath);
      
                return new Uri(baseUri, ".");
            }
        }
        /// <summary>
        /// 当前应用程序执行的路径信息
        /// </summary>
        public string ExecutablePath
        {
            get
            {
                string codeBase = string.Empty;
                var entryAssembly = Assembly.GetExecutingAssembly();
                codeBase = entryAssembly.CodeBase;
                return codeBase;
            }
        }


    }
}
