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

        /// <summary>
        /// 获取权限申请
        /// </summary>
        /// <returns></returns>
        List<Permissions> GetPermissions();

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <returns></returns>
        ImageSource GetIcon();

        /// <summary>
        /// 获取控制器
        /// </summary>
        List<IControl> GetControl();


        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <returns></returns>
        PlugInfo GetInfo();
     
    }
}
