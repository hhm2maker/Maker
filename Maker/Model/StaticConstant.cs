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
        /// 主窗体
        /// </summary>
        public static MainWindow mw;
        /// <summary>
        /// 关闭笔刷
        /// </summary>
        public static SolidColorBrush closeBrush = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));
    }
}
