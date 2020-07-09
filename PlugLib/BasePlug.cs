using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlugLib
{
    public class BasePlug : IBasePlug
    {
        public virtual List<IControl> GetControl()
        {
            return new List<IControl>();
        }

        public virtual ImageSource GetIcon()
        {
            return null;
        }

        public virtual PlugInfo GetInfo()
        {
            PlugInfo plugInfo = new PlugInfo
            {
                Title = "",
                Author = "",
                Version = "0.0.0"
            };
            return plugInfo;
        }

        public virtual List<PermissionsClass.Permissions> GetPermissions()
        {
            return new List<PermissionsClass.Permissions>();
        }

        public virtual ShowModel GetShowModel()
        {
            return ShowModel.Popup;
        }

        public virtual UserControl GetView()
        {
            return new UserControl();
        }


    }
}
