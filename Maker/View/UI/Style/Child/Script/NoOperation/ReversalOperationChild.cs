using Maker.View.LightScriptUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.View.UI.Style.Child
{
    public partial class ReversalOperationChild : NoOperationStyle
    {
        public override string Title { get; set; } = "Reversal";
        public ReversalOperationChild(ScriptUserControl suc) : base(suc)
        {

        }
    }
}
