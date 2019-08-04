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
        public override string Title { get; set; } = "Disappear";
        private AnimationDisappearOperationModel animationDisappearOperationModel;
        public AnimationDisappearOperationChild(AnimationDisappearOperationModel animationDisappearOperationModel)
        {
            this.animationDisappearOperationModel =  animationDisappearOperationModel;
            //构建对话框
            cbType = GetComboBox(new List<string>() { "Serpentine" }, null);
            AddTitleAndControl("TypeColon", cbType);

            tbStartTime = GetTexeBox(animationDisappearOperationModel.StartTime.ToString());
            AddTitleAndControl("StartTimeColon", tbStartTime);
            tbInterval = GetTexeBox(animationDisappearOperationModel.Interval.ToString());
            AddTitleAndControl("IntervalColon", tbInterval);
        
            CreateDialog();
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
