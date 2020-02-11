using System;

namespace Hao.Shell
{
    /// <summary>
    /// 定义插件的属性描述
    /// </summary>
    public class PluginInfoAttribute : System.Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string _Name = string.Empty;

        /// <summary>
        /// 版本
        /// </summary>
        private string _Version = string.Empty;
        /// <summary>
        /// 作者
        /// </summary>
        private string _Author = string.Empty;

        /// <summary>
        /// 网址
        /// </summary>
        private string _Webpage = string.Empty;
        /// <summary>
        /// 存储信息
        /// </summary>
        private object _Tag = null;
        /// <summary>
        /// 索引
        /// </summary>
        private int _Index = 0;
        /// <summary>
        /// 是否加载启动
        /// </summary>
        private bool _LoadWhenStart = true;
        /// <summary>
        /// 用于描述插件的属性信息
        /// </summary>
        public PluginInfoAttribute() { }

        /// <summary>
        /// 构造函数，初始化当前插件的描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="author"></param>
        /// <param name="webpage"></param>
        /// <param name="loadWhenStart"></param>
        /// <param name="index"></param>
        public PluginInfoAttribute(string name, string version, string author, string webpage, bool loadWhenStart, int index)
        {
            _Name = name;
            _Version = version;
            _Author = author;
            _Webpage = webpage;
            _LoadWhenStart = loadWhenStart;
            _Index = index;
        }

        /// <summary>
        /// 插件的名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }
        /// <summary>
        /// 插件的版本
        /// </summary>
        public string Version
        {
            get
            {
                return _Version;
            }
        }

        /// <summary>
        /// 插件的作者
        /// </summary>
        public string Author
        {
            get
            {
                return _Author;
            }
        }

        /// <summary>
        /// 插件的访问网页
        /// </summary>
        public string Webpage
        {
            get
            {
                return _Webpage;
            }
        }

        /// <summary>
        /// 加载是否执行
        /// </summary>
        public bool LoadWhenStart
        {
            get
            {
                return _LoadWhenStart;
            }
        }

        /// <summary>
        /// 用来存储一些有用的信息
        /// </summary>
        public object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }

        /// <summary>
        /// 插件的索引信息
        /// </summary>
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }


    }
}
