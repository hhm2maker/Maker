using Maker.Business.Model.OperationModel;
using Maker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.UI.Style.Child
{
    public partial class ColorOperationChild : OperationStyle
    {
        private ColorOperationModel changeColorOperationModel;
        public ColorOperationChild(ColorOperationModel changeColorOperationModel)
        {
            this.changeColorOperationModel = changeColorOperationModel;
            //构建对话框
            AddTopHintTextBlock(changeColorOperationModel.HintString);
            AddTextBox();
            CreateDialog();
            tbColors = Get(1) as TextBox;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < changeColorOperationModel.Colors.Count; i++) {
                if (i != changeColorOperationModel.Colors.Count - 1)
                {
                    sb.Append(changeColorOperationModel.Colors[i] + StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter.ToString());
                }
                else {
                    sb.Append(changeColorOperationModel.Colors[i]);
                }
            }
           
            tbColors.Text = sb.ToString();
        }

        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        public TextBox tbColors;
        public ComboBox cbOperation;

        public override bool ToSave() {
            if (tbColors.Text.Equals(String.Empty))
            {
                tbColors.Focus();
                return false;
            }
            List<int> colors = new List<int>();
            String[] strColors = tbColors.Text.Split(StaticConstant.mw.projectUserControl.suc.StrInputFormatDelimiter);
            foreach(var item in strColors) {
                if (int.TryParse(item, out int color))
                {
                    colors.Add(color);
                }
                else
                {
                    return false;
                }
            }
            changeColorOperationModel.Colors = colors;
            return true;
        }
    }
}
