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
        private InputUserControl iuc;
        public InputUserControlViewBusiness(InputUserControl iuc) {
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
            else if (sender == iuc.btnFastGenerationrClear)
            {
                iuc.tbFastGenerationrTime.Clear();
                iuc.tbFastGenerationrRange.Clear();
                iuc.tbFastGenerationrInterval.Clear();
                iuc.tbFastGenerationrContinued.Clear();
                iuc.tbFastGenerationrColor.Clear();
            }
            else if (sender == iuc.btnSelectEditorClear)
            {
                iuc.tbSelectEditorTime.Clear();
                iuc.tbSelectEditorPosition.Clear();
                iuc.tbSelectEditorColor.Clear();
            }
        }
        public void SetBackGroundFromWidth(double width) {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = Color.FromArgb(255, 40, 40, 40);
            stop1.Offset = 0;
            GradientStop stop2 = new GradientStop();
            stop2.Color = Color.FromArgb(255, 40, 40, 40);
            stop2.Offset = (width - 17.33) / width;
            GradientStop stop3 = new GradientStop();
            stop3.Color = Color.FromArgb(255, 74, 74, 74);
            stop3.Offset = (width - 17.33) / width;
            brush.GradientStops.Add(stop1);
            brush.GradientStops.Add(stop2);
            brush.GradientStops.Add(stop3);
            iuc.dgMain.Background = brush;
        }
    }
}
