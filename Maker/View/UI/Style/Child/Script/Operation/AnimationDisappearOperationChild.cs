using Maker.Business.Model.OperationModel;
using Maker.View.LightScriptUserControl;
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
        public override StyleType FunType { get; set; } = StyleType.Animation;

        private AnimationDisappearOperationModel animationDisappearOperationModel;

        private List<String> types = new List<String>() { "Serpentine"};

        public AnimationDisappearOperationChild(AnimationDisappearOperationModel animationDisappearOperationModel, ScriptUserControl suc) : base(suc)
        {
            this.animationDisappearOperationModel =  animationDisappearOperationModel;
            ToCreate();
        }

        protected override List<RunModel> UpdateData()
        {
            return new List<RunModel>
            {
                new RunModel("TypeColon", (string)Application.Current.FindResource(types[0]), RunModel.RunType.Combo, types),
                new RunModel("StartTimeColon", animationDisappearOperationModel.StartTime.ToString()),
                new RunModel("IntervalColon", animationDisappearOperationModel.Interval.ToString()),
            };
        }

        protected override void RefreshView()
        {
            //StartTime
            String strStartTime = runs[5].Text;
            if (!int.TryParse(strStartTime, out int iStartTime))
            {
                iStartTime = animationDisappearOperationModel.StartTime;
            }

            //Interval
            String strInterval = runs[5].Text;
            if (!int.TryParse(strInterval, out int iInterval))
            {
                iInterval = animationDisappearOperationModel.Interval;
            }

            animationDisappearOperationModel.StartTime = iStartTime;
            animationDisappearOperationModel.Interval = iInterval;

            UpdateData();
        }
    }
}
