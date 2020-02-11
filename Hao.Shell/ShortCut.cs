using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.Reflection;
using Autodesk.Windows;
using UIFrameworkServices;
using Autodesk.Revit.Attributes;
using UIFramework;

namespace Hao.Shell
{
    /// <summary>
    /// 定义当前的软件的扩展
    /// </summary>
    public static class ShortKeyExtension
    {
        /// <summary>
        /// 显示当前的扩展信息
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SetShortCut(this Autodesk.Revit.UI.RibbonButton btn, string key)
        {
            if (btn == null)
                return false;
            var method = btn.GetType().GetMethod("getRibbonItem", BindingFlags.NonPublic | BindingFlags.Instance);
            var adItem = method.Invoke(btn, null);
            if (adItem == null)
                return false;
            return ShortKeyExtension.SetShortCut(adItem as RibbonCommandItem, key);
        }

        /// <summary>
        /// 给指定按钮设置快捷键
        /// </summary>
        /// <param name="commandItem"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SetShortCut(this RibbonCommandItem commandItem, string key)
        {
            try
            {
                if (commandItem == null || string.IsNullOrEmpty(key))
                    return false;
                var parentTab = default(Autodesk.Windows.RibbonTab);

                var parentPanel = default(Autodesk.Windows.RibbonPanel);

                Autodesk.Windows.ComponentManager.Ribbon.FindItem(commandItem.Id, false, out parentPanel, out parentTab, true);
                if (parentTab == null || parentPanel == null)
                    return false;
                var path = string.Format("{0}>{1}", parentTab.Id, parentPanel.Source.Id);
                var commandId = ControlHelper.GetCommandId(commandItem);

                if (string.IsNullOrEmpty(commandId))
                {
                    commandId = Guid.NewGuid().ToString();
                    ControlHelper.SetCommandId(commandItem, commandId);
                }
                var shortcutItem = new ShortcutItem(commandItem.Text, commandId, key, path);

                shortcutItem.ShortcutType = StType.RevitAPI;

                KeyboardShortcutService.applyShortcutChanges(
                    new Dictionary<string, ShortcutItem>()
                        {
                        {
                            commandId,shortcutItem
                        }
                        }
                    );
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
