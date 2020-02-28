using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        BitmapImage GetIcon();
    }
}
