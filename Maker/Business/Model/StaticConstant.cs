using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Maker.Model
{
    public static class StaticConstant
    {
        /// <summary>
        /// 笔刷列表
        /// </summary>
        public static List<SolidColorBrush> brushList = new List<SolidColorBrush>();
        /// <summary>
        /// 数字转笔刷
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        public static SolidColorBrush NumToBrush(int i)
        {
            if (i == -1)
                return closeBrush;
            return brushList[i];
        }
        /// <summary>
        /// 主窗体
        /// </summary>
        public static MainWindow mw3;
        /// <summary>
        /// 关闭笔刷
        /// </summary>
        public static SolidColorBrush closeBrush {
            get {
                //return new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
                if (brushList == null || brushList.Count == 0)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
                }
                else
                {
                    return brushList[0];
                }
            }
        } 
        /// <summary>
        /// 主窗体
        /// </summary>
        public static NewMainWindow mw;

        public static String NowVersion = "20200505";

        public static bool IsNowVersion = false;
    }
}
