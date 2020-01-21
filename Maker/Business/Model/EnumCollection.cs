using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
   public static class EnumCollection
    {  
        /// <summary>
       ///  主窗口当前显示的内容
       /// </summary>
        public enum MainWindowMode
        {
            None = 0,//未选择
            Light = 1,//Light文件
            Input = 2,//输入
            Page = 3,//页文件
            Play = 4,//演奏
            Makerpj = 5,//属性
        }
        /// <summary>
        ///  集合类型
        /// </summary>
        public enum CollectionType
        {
            Intersection = 0,//交集
            Complement = 1,//补集
        }
        /// <summary>
        ///  集合类型
        /// </summary>
        public enum PageUCSelectType
        {
            Down = 0,//按下
            Loop = 1,//循环
            Up = 2,//抬起
        }
        /// <summary>
        ///  项目类型
        /// </summary>
        public enum ProjectType
        {
            launchpadlightproject = 0,//Launchpad灯光项目
            launchpadlightlibrary = 1,//Launchpad灯光库
        }
        /// <summary>
        ///  播放器类型
        /// </summary>
        public enum PlayerType
        {
            ParagraphLightList = 0,//段落Light
            Accurate = 1,//精确
            ParagraphIntList = 2,//段落Int
            Fast = 3,//快速
        }
    }
}
