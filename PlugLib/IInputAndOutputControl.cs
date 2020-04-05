using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static PlugLib.InputAndOutputControlEnum;
using static PlugLib.PermissionsClass;

namespace PlugLib
{
    public interface IInputAndOutputControl : IControl
    {
        /// <summary>
        /// 按下设备按钮时发生的事件
        /// </summary>
        void OnInput(int position, KeyModel keyModel);

        /// <summary>
        /// 发送灯光
        /// </summary>
        /// <param name="lights"></param>
        void OutputLight(SendLight sendLight);
    }
}
