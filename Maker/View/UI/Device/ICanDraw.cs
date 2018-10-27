using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.Device
{
    public interface ICanDraw
    {
        /// <summary>
        /// 判断是否能够绘制事件
        /// </summary>
        bool IsCanDraw();
    }
}
