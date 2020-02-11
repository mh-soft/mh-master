using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using Autodesk.Revit.UI;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hao.Shell
{
    /// <summary>
    /// 插件加载器，用于加载指定的插件信息，插件的加载顺序为
    /// 首先 1、Tab分组；2、Panel分组；3、Stack分组；split分组
    /// tab代表主面板，panel代表小面板，stack代表排列，split代表下拉列表
    /// </summary>
    public static class PluginLoader
    {

        /// <summary>
        /// 验证当前类型是否是指定程序接口
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
       
        private static bool IsValidPlugin(Type t)
        {
            bool ret = false;
            Type[] interfaces = t.GetInterfaces();
            foreach( Type theInterface in interfaces ) {
                if (theInterface.FullName == "Hao.Shell.IPlugin")
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }



        private static SplitButton FindSplitByName(RibbonPanel panel, string name)
        {
            foreach(RibbonItem item in panel.GetItems())
            {
                if(item.ItemType==RibbonItemType.SplitButton)
                {
                    SplitButton splitbt = item as SplitButton;
                    if (splitbt.Name == name)
                        return splitbt;
                }
            }
            return null;
        }

        /// <summary>
        /// 创建一个按钮
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="panel"></param>
        private static void CreateButton(IFangCommand plugin, RibbonPanel panel)
        {
            if (plugin.SplitName != null && plugin.SplitName != "")
            {
                SplitButton splitbt = FindSplitByName(panel, plugin.SplitName);
                if (splitbt != null)
                {
                   
                    PushButtonData data = new PushButtonData(plugin.AlaisName, plugin.AlaisName, plugin.GetType().Assembly.Location, plugin.ModelName);
                    PushButton ps = splitbt.AddPushButton(data);
                    ps.LargeImage = plugin.ImageURI;
                    ps.Image = plugin.ImageURI;
                    
                    ps.SetShortCut(plugin.ShortCutKey);
                }
                else
                {
                    SplitButtonData splidata = new SplitButtonData(plugin.SplitName, plugin.SplitName);
                    SplitButton ps = panel.AddItem(splidata) as SplitButton;
                    ps.LargeImage = plugin.ImageURI;
                    ps.Image = plugin.ImageURI;
                    ps.SetShortCut(plugin.ShortCutKey);
                    ps.IsSynchronizedWithCurrentItem = false;
                    //ps.ItemText = plugin.SplitName;
                }
            }
            else
            {
      
            }
        }



        /// <summary>
        /// 加载所有的插件信息
        /// </summary>
        /// <param name="revitapp"></param>
        /// <param name="splash"></param>
        public static void LoadAllPlugins(UIControlledApplication revitapp, ISplash splash)
        {
            /// <summary>
            /// 所有的插件列表
            /// </summary>
            List<IFangCommand> plugins = new List<IFangCommand>();
            //获取当前的应用程序路径
            string appPath = Unity.GetAssemblyPath();

            if (appPath != null)
            {
                //获取当前所有的文件信息
                string[] files = Directory.GetFiles(appPath);
                //获取下面所有的dll
                foreach (string file in files)
                {
                    if (file.Contains("Fang.LGK.Plugin.") == false)
                        continue;
                    //获取扩展名
                    string ext = Path.GetExtension(file);
                    //判断是否是扩展名
                    if (ext.ToLower() != ".dll") continue;

                    try
                    {
                        //加载当前的程序集
                        Assembly tmp = Assembly.LoadFrom(file);
                        //获取当前所有类型信息
                        Type[] types = tmp.GetTypes();
                        //显示加载进度
                        splash.ProcessData=Path.GetFileName(file);
                        //获取当前的成员类型信息
                        foreach (Type t in types)
                        {
                            if (IsValidPlugin(t))
                            {
                                try
                                {
                                    //创建当前的插件信息
                                    IFangCommand plugin = (IFangCommand)tmp.CreateInstance(t.FullName);
                                    plugins.Add(plugin);
                                }
                                catch (Exception ex) {
                                    splash.ProcessData = "当前应用程序加载失败" + ex.Message;
                                    continue;

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        splash.ProcessData = "当前应用程序加载失败" + ex.Message;

                    }
                    //创建当前界面
                }

                CreateUI(revitapp, plugins, splash);
            }
            else {

            }
        }

        /// <summary>
        /// 创建当前的插件信息
        /// </summary>
        /// <param name="plugins"></param>
        private static void CreateUI(UIControlledApplication revitapp, List<IFangCommand> plugins, ISplash splash)
        {
            if (plugins.Count > 0) {

                //通过tab分组
                var tabs = plugins.GroupBy(x => x.TabName);
               
                //获取当前所有的tabs信息
                foreach (var t in tabs) {
                    if (t.Count() > 0)
                    {
                        //创建指定的面板
                        CreateTab(revitapp,t.Key);
                        //获取当前所有的面板组
                        var panels = t.GroupBy(x => x.PanelName);

                        //获取当前的面板的数量
                        if (panels.Count() > 0) {
                            //对当前的面板进行排序
                            var orderPanels = panels.OrderBy(x => x.ElementAtOrDefault(0).PanelIndex);

                            foreach (var p in orderPanels) {

                                //创建当前的面板
                                var panel = CreatePanel(revitapp, t.Key, p.Key);

                                if (panel != null) {

                                    //获取当前的stack分类
                                    Dictionary<string, List<IFangCommand>> splitValues = new Dictionary<string, List<IFangCommand>>();

                                    //遍历所有的插件
                                    foreach (var b in p)
                                    {
                                        //进行分组显示
                                        if (!string.IsNullOrEmpty(b.SplitName))
                                        {
                                            if (splitValues.Keys.Contains(b.SplitName))
                                            {
                                                splitValues[b.SplitName].Add(b);
                                            }
                                            else
                                            {
                                                splitValues[b.SplitName] = new List<IFangCommand>();
                                                splitValues[b.SplitName].Add(b);
                                            }
                                        }
                                        else
                                        {
                                            if (splitValues.Keys.Contains(b.ModelName))
                                            {
                                                splitValues[b.ModelName].Add(b);
                                            }
                                            else
                                            {

                                                splitValues[b.ModelName] = new List<IFangCommand>();
                                                splitValues[b.ModelName].Add(b);
                                            }
                                        }

                                    }
                                    CreateStack(panel, splitValues);
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                splash.ProcessData = "当前无任何插件可以加载";
            }
        }




        /// <summary>
        /// 创建当前的Tab信息
        /// </summary>
        /// <param name="revitapp"></param>
        /// <param name="key"></param>
        private static void CreateTab(UIControlledApplication revitapp, string tabname)
        {
            revitapp.CreateRibbonTab(tabname);
        }

        /// <summary>
        ///当前创建一个面板对象
        /// </summary>
        /// <param name="revitapp"></param>
        /// <param name="tabname"></param>
        /// <param name="panelname"></param>
        /// <returns></returns>
        private static RibbonPanel CreatePanel(UIControlledApplication revitapp, string tabname, string panelname)
        {
            //创建选项卡面板
            RibbonPanel panel = revitapp.CreateRibbonPanel(tabname, panelname);
            panel.Name = panelname;
            panel.Title = panelname;
            return panel;
        }

        /// <summary>
        /// 创建当前所有的堆栈次序
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="stackValues"></param>
        private static void CreateStack(RibbonPanel panel, Dictionary<string, List<IFangCommand>> splitValues)
        {
            //获取所有的小图标
            var smalls = splitValues.Where(x => !x.Value[0].LargeIcon);
            var largs = splitValues.Where(x => x.Value[0].LargeIcon).ToList();

            AddLargeButton(panel, largs);

            int n = smalls.Count();
            //假如当前的图标可以整除

            if (n >= 2)
            {
                if (n % 3 == 0)
                {
                    for (int i = 0; i < n; i += 3)
                    {

                        AddStackButton(panel, smalls.ElementAt(i), smalls.ElementAt(i + 1), smalls.ElementAt(i + 2));
                    }
                }
                else if (n % 3 == 1)
                {

                    for (int i = 0; i < n - 4; i += 3)
                    {
                        AddStackButton(panel, smalls.ElementAt(i), smalls.ElementAt(i + 1), smalls.ElementAt(i + 2));

                    }

                    for (int i = n - 4; i < n; i += 2)
                    {
                        AddStackButton(panel, smalls.ElementAt(i), smalls.ElementAt(i + 1));
                    }
                }
                else
                {
                    for (int i = 0; i < n - 2; i += 3)
                    {
                        AddStackButton(panel, smalls.ElementAt(i), smalls.ElementAt(i + 1), smalls.ElementAt(i + 2));
                    }
                    for (int i = n - 2; i < n; i += 2)
                    {
                        AddStackButton(panel, smalls.ElementAt(i), smalls.ElementAt(i + 1));
                    }
                }
          
            }
            else {
                AddLargeButton(panel, smalls.ToList());
            }

         

        }



        /// <summary>
        /// 当前的分特征
        /// </summary>
        /// <param name="keyValuePair1"></param>
        /// <param name="keyValuePair2"></param>
        private static void AddStackButton(RibbonPanel panel, KeyValuePair<string, List<IFangCommand>> keyValuePair1, KeyValuePair<string, List<IFangCommand>> keyValuePair2)
        {
            ButtonData b1 = null;
            ButtonData b2 = null;
            if (keyValuePair1.Value.Count > 1)
            {
                b1 = CreatePullButton(panel, keyValuePair1.Key, keyValuePair1.Value);
    
            }
            else {
                b1 = CreateButton(panel, keyValuePair1.Key, keyValuePair1.Value);

            }
            if (keyValuePair2.Value.Count > 1)
            {
                b2 = CreatePullButton(panel, keyValuePair2.Key, keyValuePair2.Value);
            }
            else
            {
                b2 = CreateButton(panel, keyValuePair2.Key, keyValuePair2.Value);
     
            }

            var list = panel.AddStackedItems(b1, b2);
            if (keyValuePair1.Value.Count > 1)
            {

                AppendPullButton(list[0] as PulldownButton, keyValuePair1.Value);
            }

            if (keyValuePair2.Value.Count > 1)
            {

                AppendPullButton(list[1] as PulldownButton, keyValuePair2.Value);
            }
        }



        /// <summary>
        /// 添加所有的堆栈按钮
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="keyValuePair1"></param>
        /// <param name="keyValuePair2"></param>
        /// <param name="keyValuePair3"></param>
        private static void AddStackButton(RibbonPanel panel, KeyValuePair<string, List<IFangCommand>> keyValuePair1, KeyValuePair<string, List<IFangCommand>> keyValuePair2, KeyValuePair<string, List<IFangCommand>> keyValuePair3)
        {
            ButtonData b1 = null;
            ButtonData b2 = null;
            ButtonData b3 = null;
            if (keyValuePair1.Value.Count > 1)
            {
                b1 = CreatePullButton(panel, keyValuePair1.Key, keyValuePair1.Value);
            }
            else
            {
                b1 = CreateButton(panel, keyValuePair1.Key, keyValuePair1.Value);
                b1.Text = keyValuePair1.Key;
            }
            if (keyValuePair2.Value.Count > 1)
            {
                b2 = CreatePullButton(panel, keyValuePair2.Key, keyValuePair2.Value);
                b2.Text = keyValuePair1.Key;
            }
            else
            {
                b2 = CreateButton(panel, keyValuePair2.Key, keyValuePair2.Value);
            }
            if (keyValuePair3.Value.Count > 1)
            {
                b3= CreatePullButton(panel, keyValuePair3.Key, keyValuePair3.Value);
            }
            else
            {
                b3 = CreateButton(panel, keyValuePair3.Key, keyValuePair3.Value);
            }
            var list = panel.AddStackedItems(b1, b2, b3);
            if (keyValuePair1.Value.Count > 1)
            {
                AppendPullButton(list[0] as PulldownButton, keyValuePair1.Value);
            }
            if (keyValuePair2.Value.Count > 1)
            {

                AppendPullButton(list[1] as PulldownButton, keyValuePair2.Value);
            }

            if (keyValuePair2.Value.Count > 1)
            {

                AppendPullButton(list[2] as PulldownButton, keyValuePair2.Value);
            }
        }

        /// <summary>
        /// 创建普通按钮
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="splitname"></param>
        /// <param name="plugins"></param>
        /// <returns></returns>
        private static PushButtonData CreateButton(RibbonPanel panel, string splitname, List<IFangCommand> plugins)
        {
            var plugin = plugins[0];
            PushButtonData pushData = new PushButtonData(plugin.AlaisName, plugin.AlaisName, plugin.GetType().Assembly.Location, plugin.ModelName);
            pushData.LargeImage = plugin.ImageURI;
            pushData.Image = plugin.ImageURI;
        
            return pushData;
        }

        /// <summary>
        /// 创建下拉按钮
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="splitname"></param>
        /// <param name="plugins"></param>
        /// <returns></returns>
        private static PulldownButtonData CreatePullButton(RibbonPanel panel, string splitname, List<IFangCommand> plugins)
        {
            PulldownButtonData pulldown = new PulldownButtonData(splitname, splitname);
            pulldown.Image = plugins[0].ImageURI;
            pulldown.LargeImage = plugins[0].ImageURI;
            
            return pulldown;
        }

        /// <summary>
        /// 附加按钮
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="plugins"></param>
        private static void AppendPullButton( PulldownButton b1, List<IFangCommand> plugins)
        {
            foreach (var plugin in plugins)
            {
                PushButtonData data = new PushButtonData(plugin.AlaisName, plugin.AlaisName, plugin.GetType().Assembly.Location, plugin.ModelName);
                PushButton ps = b1.AddPushButton(data);

                ps.LargeImage = plugin.ImageURI;
                ps.Image = plugin.ImageURI;
                ps.SetShortCut(plugin.ShortCutKey);
            }
        }

        /// <summary>
        /// 添加大按钮
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="largs"></param>
        private static void AddLargeButton(RibbonPanel panel, List<KeyValuePair<string, List<IFangCommand>>> largs)
        {
            foreach (var p in largs)
            {
                AddLargeButton(panel, p);
            }
        }

 

        /// <summary>
        /// 添加所有的大按钮
        /// </summary>
        /// <param name="splitValues"></param>
        private static void AddLargeButton(RibbonPanel panel, KeyValuePair<string, List<IFangCommand>> lb)
        {
            if (lb.Value.Count > 1)
            {

                var m = CreatePullButton(panel, lb.Key, lb.Value);
                var rb = panel.AddItem(m);
                AppendPullButton(rb as PulldownButton, lb.Value);
            }
            else
            {
                var m = CreateButton(panel, lb.Key, lb.Value);
                var rb = panel.AddItem(m);
            }

        }

    }
}
