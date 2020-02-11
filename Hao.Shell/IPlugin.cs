using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Hao.Shell
{
    /// <summary>
    /// 当前插件的定义
    /// </summary>
    public interface IPlugin
    {

        /// <summary>
        /// 当前面板的名称
        /// </summary>
        string TabName { get; }
        /// <summary>
        /// 当前面板的名称
        /// </summary>
        string PanelName { get; }

        /// <summary>
        /// 当前的次序的信息
        /// </summary>
        int PanelIndex { get; }

        /// <summary>
        /// 当前下拉名称
        /// </summary>
        string SplitName { get; }

        /// <summary>
        /// 显示大图标
        /// </summary>
        bool LargeIcon { get; }

        /// <summary>
        /// 假如是下拉菜单，则下拉菜单的次序
        /// </summary>
        int MenuIndex { get; }

        /// <summary>
        /// 当前菜单的名称信息
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 别名
        /// </summary>
        string AlaisName { get; }
        /// <summary>
        /// 类名
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// 模块名称
        /// </summary>
        string ModelName { get; }
        /// <summary>
        /// 快捷键
        /// </summary>
        string ShortCutKey { get; }

        /// <summary>
        /// 当前插件对应的图片
        /// </summary>
        BitmapImage ImageURI { get; }

        /// <summary>
        /// 当前的插件的基本属性
        /// </summary>
        PluginInfoAttribute PluginInfo { get; }

    }
}
