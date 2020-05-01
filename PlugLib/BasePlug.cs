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

        public virtual List<PermissionsClass.Permissions> GetPermissions()
        {
            return new List<PermissionsClass.Permissions>();
        }

        public virtual string GetTitle()
        {
            return "";
        }

        public virtual UserControl GetView()
        {
            return new UserControl();
        }

        public virtual string Version()
        {
            return "0.0.0";
        }
    }
}
