using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.UI.Style.Child
{
    public partial class AnimationDisappearOperationChild : OperationStyle
    {
        private AnimationDisappearOperationModel animationDisappearOperationModel;
        public AnimationDisappearOperationChild(AnimationDisappearOperationModel animationDisappearOperationModel)
        {
            this.animationDisappearOperationModel =  animationDisappearOperationModel;
            //构建对话框
            cbType = GetComboBox(new List<string>() { "Serpentine" }, null);
            AddTitleAndControl("TypeColon", cbType);
            
            AddTopHintTextBlock("StartTimeColon");
            AddTextBox();
            AddTopHintTextBlock("IntervalColon");
            AddTextBox();
            CreateDialog();
            tbStartTime = Get(3) as TextBox;
            tbInterval = Get(5) as TextBox;

            tbStartTime.Text = animationDisappearOperationModel.StartTime.ToString();
            tbInterval.Text = animationDisappearOperationModel.Interval.ToString();
        }
      

        public TextBox tbStartTime, tbInterval;
        public ComboBox cbType;

        public override bool ToSave() {
            if (tbStartTime.Text.Equals(String.Empty))
            {
                tbStartTime.Focus();
                return false;
            }
            if (int.TryParse(tbStartTime.Text, out int iStartTime))
            {
                animationDisappearOperationModel.StartTime = iStartTime;
            }
            else
            {
                tbStartTime.Focus();
                return false;
            }
            if (tbInterval.Text.Equals(String.Empty))
            {
                tbInterval.Focus();
                return false;
            }
            if (int.TryParse(tbInterval.Text, out int iInterval))
            {
                animationDisappearOperationModel.Interval = iInterval;
                return true;
            }
            else
            {
                tbInterval.Focus();
                return false;
            }
        }
    }
}
