using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static PlugLib.PermissionsClass;

namespace PlugLib
{
    public interface IBasePlug
    {
        /// <summary>
        /// 返回一个界面
        /// </summary>
        UserControl GetView();

        List<Permissions> GetPermissions();

        ImageSource GetIcon();

        /// <summary>
        /// 获取控制器
        /// </summary>
        List<IControl> GetControl();
    }
}
