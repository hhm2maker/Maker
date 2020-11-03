using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Maker.View.Style.Child;
using Maker.View.LightScriptUserControl;

namespace Maker.View.UI.Style.Child
{
    public class NoOperationStyle : OperationStyle
    {

        protected override bool OnlyTitle
        {
            get;
            set;
        } = true;

        public NoOperationStyle(ScriptUserControl suc) : base(suc)
        {
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>();
        }

        protected override void RefreshView()
        {
            UpdateData();
        }
    }
}
