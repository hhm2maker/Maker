using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Maker.View.Style.Child;

namespace Maker.View.UI.Style.Child
{
    public class OperationStyle : BaseSettingUserControl
    {
        public virtual bool ToSave()
        {
            return true;
        }
    }
}
