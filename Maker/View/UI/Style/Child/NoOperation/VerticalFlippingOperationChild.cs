using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.View.LightScriptUserControl;

namespace Maker.View.UI.Style.Child
{
    public partial class VerticalFlippingOperationChild : NoOperationStyle
    {
        public VerticalFlippingOperationChild(ScriptUserControl suc) : base(suc)
        {
        }

        public override string Title { get; set; } = "VerticalFlipping";
    }
}
