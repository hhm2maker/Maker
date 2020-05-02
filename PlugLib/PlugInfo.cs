using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugLib
{
    public class PlugInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title = "";

        /// <summary>
        /// 作者
        /// </summary>
        public string Author = "";

        /// <summary>
        /// 版本
        /// </summary>
        public string Version = "";

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe = "";

        /// <summary>
        /// 超链接
        /// </summary>
        public List<Link> Links = new List<Link>();

        public class Link
        {
            public String Text = "";
            public String Url = "";
        }
    }
}
