using Autodesk.RevitAddIns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hao.Shell
{
    /// <summary>
    /// 当前的版本信息
    /// </summary>
    public class VersionChecked
    {

        /// <summary>
        /// 获取当前的安装的所有revit的版本信息
        /// </summary>
        /// <returns></returns>
        public List<RevitProduct> GetAllInstalledRevitProducts()
        {
            return RevitProductUtility.GetAllInstalledRevitProducts();
        }

    }
}
