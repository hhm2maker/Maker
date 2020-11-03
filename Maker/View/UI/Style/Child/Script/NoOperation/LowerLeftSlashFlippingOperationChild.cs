using Maker.View.LightScriptUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.UI.Style.Child
{
    public partial class LowerLeftSlashFlippingOperationChild : NoOperationStyle
    {
        public override string Title { get; set; } = "LowerLeftSlashFlipping";
        public LowerLeftSlashFlippingOperationChild(ScriptUserControl suc) : base(suc)
        {

        }
    }
}
