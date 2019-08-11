using Maker.View.LightScriptUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Maker.ViewBusiness
{
    public class InputUserControlViewBusiness
    {
        private ScriptUserControl iuc;
        public InputUserControlViewBusiness(ScriptUserControl iuc) {
            this.iuc = iuc;
        }
        /// <summary>
        /// 清空输出框
        /// </summary>
        /// <param name="sender"></param>
        public void ClearInput(object sender)
        {
            if (sender == iuc.tbIfThenClear)
            {
                iuc.tbIfTime.Clear();
                iuc.tbIfPosition.Clear();
                iuc.tbIfColor.Clear();
                iuc.tbThenTime.Clear();
                iuc.tbThenPosition.Clear();
                iuc.tbThenColor.Clear();
            }
            else if (sender == iuc.btnSelectEditorClear)
            {
                iuc.tbSelectEditorTime.Clear();
                iuc.tbSelectEditorPosition.Clear();
                iuc.tbSelectEditorColor.Clear();
            }
        }
        
    }
}
