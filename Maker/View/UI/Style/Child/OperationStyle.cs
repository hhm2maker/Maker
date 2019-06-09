using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Maker.View.Style.Child;
using Maker.Model;

namespace Maker.View.UI.Style.Child
{
    public class OperationStyle : BaseSettingUserControl
    {
        protected List<Light> NowData {
            get {
                return StaticConstant.mw.projectUserControl.suc.mLaunchpadData;
            }
        }

        protected List<Light> MyData;
       

        public virtual void Refresh() {

        }

        public virtual bool ToSave()
        {
            return true;
        }
    }
}
