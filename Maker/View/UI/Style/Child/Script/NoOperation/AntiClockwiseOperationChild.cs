using Maker.View.LightScriptUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.UI.Style.Child
{
    public partial class AntiClockwiseOperationChild : NoOperationStyle
    {
        public override string Title { get; set; } = "AntiClockwiseRotation";
        public AntiClockwiseOperationChild(ScriptUserControl suc):base(suc)
        {

        }
    }
}
