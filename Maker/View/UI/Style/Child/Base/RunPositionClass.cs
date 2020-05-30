using Maker.Model;
using Maker.View.Dialog;
using Maker.View.Style.Child;
using Maker.View.UIBusiness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace Maker.View.UI.Style.Child.Base
{
    public class RunPositionClass : RunContentClass
    {

        public override UIElement GetContent()
        {
            return new DrawRangeDialog(StaticConstant.mw,contextMenu, Result);
        }

        private void Result(List<int> result)
        {
            needClose = false;

            StringBuilder sb = new StringBuilder();
            foreach(int i in result) {
                if (sb.ToString().Equals(String.Empty))
                {
                    sb.Append(i);
                }
                else {
                    sb.Append(Os.suc.StrInputFormatDelimiter).Append(i);
                }
            }
            RunCombo.Text = sb.ToString();
            contextMenu.IsOpen = false;
            Os.ToRefresh();
        }
    }
}
