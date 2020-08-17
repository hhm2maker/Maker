using Operation;
using Operation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static PlugLib.PageControlEnum;
using static PlugLib.PermissionsClass;

namespace PlugLib
{
    public interface IPageControl : IControl
    {
        /// <summary>
        /// 回传PageModel
        /// </summary>
        void GetResult(SendPage sendLight);
    }
}
