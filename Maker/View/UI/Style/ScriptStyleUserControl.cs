using Maker.View.LightScriptUserControl;
using Maker.View.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Business.Model.OperationModel;
using Maker.View.Dialog;
using Maker.View.Style.Child;
using Maker.View.UI.Style.Child;
using Maker.View.UI.Style.Child.Operation;
using Maker.View.UI.UserControlDialog;
using Operation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Maker.View.UI.Style
{
    public class ScriptStyleUserControl : BaseStyleWindow
    {
        public ScriptUserControl mw;
        public ScriptStyleUserControl(ScriptUserControl mw) : base()
        {
            this.mw = mw;
        }

        public override void OnRefresh()
        {
            mw.Test();
        }

        public void SetData(List<BaseOperationModel> operationModels, bool isNew)
        {
            SetData(operationModels);
        }

        public void SetData(List<BaseOperationModel> operationModels)
        {
            svMain.Children.Clear();

            this.operationModels = operationModels;

            foreach (var item in operationModels)
            {

                if (item is ConditionJudgmentOperationModel)
                {
                    svMain.Children.Add(new ConditionJudgmentOperationChild(item as ConditionJudgmentOperationModel, mw));
                }
                else if (item is SetAttributeOperationModel)
                {
                    svMain.Children.Add(new SetAttributeOperationChild(item as SetAttributeOperationModel, mw));
                }
                else if (item is CreateFromAutomaticOperationModel)
                {
                    svMain.Children.Add(new CreateFromAutomaticOperationChild(item as CreateFromAutomaticOperationModel, mw));
                }
                else if (item is CreateFromFileOperationModel)
                {
                    svMain.Children.Add(new CreateFromFileOperationChild(item as CreateFromFileOperationModel, mw));
                }
                else if (item is CreateFromStepOperationModel)
                {
                    svMain.Children.Add(new CreateFromStepOperationChild(item as CreateFromStepOperationModel, mw));
                }
                else if (item is CreateFromQuickOperationModel)
                {
                    svMain.Children.Add(new CreateFromQuickOperationChild(item as CreateFromQuickOperationModel, mw));
                }
                else if (item is VerticalFlippingOperationModel)
                {
                    svMain.Children.Add(new VerticalFlippingOperationChild(mw));
                }
                else if (item is HorizontalFlippingOperationModel)
                {
                    svMain.Children.Add(new HorizontalFlippingOperationChild(mw));
                }
                else if (item is LowerLeftSlashFlippingOperationModel)
                {
                    svMain.Children.Add(new LowerLeftSlashFlippingOperationChild(mw));
                }
                else if (item is LowerRightSlashFlippingOperationModel)
                {
                    svMain.Children.Add(new LowerRightSlashFlippingOperationChild(mw));
                }
                else if (item is ClockwiseOperationModel)
                {
                    svMain.Children.Add(new ClockwiseOperationChild(mw));
                }
                else if (item is AntiClockwiseOperationModel)
                {
                    svMain.Children.Add(new AntiClockwiseOperationChild(mw));
                }
                else if (item is RemoveBorderOperationModel)
                {
                    svMain.Children.Add(new RemoveBorderOperationChild(mw));
                }
                else if (item is ReversalOperationModel)
                {
                    svMain.Children.Add(new ReversalOperationChild(mw));
                }
                else if (item is ChangeTimeOperationModel)
                {
                    svMain.Children.Add(new ChangeTimeOperationChild(item as ChangeTimeOperationModel, mw));
                }
                else if (item is FoldOperationModel)
                {
                    svMain.Children.Add(new FoldOperationChild(item as FoldOperationModel, mw));
                }
                else if (item is SetEndTimeOperationModel)
                {
                    svMain.Children.Add(new SetEndTimeOperationChild(item as SetEndTimeOperationModel, mw));
                }
                else if (item is ShapeColorOperationModel)
                {
                    svMain.Children.Add(new ShapeColorOperationChild(item as ShapeColorOperationModel, mw));
                }
                else if (item is OneNumberOperationModel)
                {
                    svMain.Children.Add(new OneNumberOperationChild(item as OneNumberOperationModel, mw));
                }
                else if (item is ChangeColorOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as ChangeColorOperationModel, mw));
                }
                else if (item is CopyToTheEndOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as CopyToTheEndOperationModel, mw));
                }
                else if (item is CopyToTheFollowOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as CopyToTheFollowOperationModel, mw));
                }
                else if (item is AccelerationOrDecelerationOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as AccelerationOrDecelerationOperationModel, mw));
                }
                else if (item is AnimationDisappearOperationModel)
                {
                    svMain.Children.Add(new AnimationDisappearOperationChild(item as AnimationDisappearOperationModel, mw)); ;
                }
                else if (item is ColorWithCountOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as ColorWithCountOperationModel, mw));
                }
                else if (item is AnimationDisappearOperationModel)
                {
                    svMain.Children.Add(new AnimationDisappearOperationChild(item as AnimationDisappearOperationModel, mw));
                }
                else if (item is InterceptTimeOperationModel)
                {
                    svMain.Children.Add(new InterceptTimeOperationChild(item as InterceptTimeOperationModel, mw));
                }
                else if (item is ThirdPartyOperationModel)
                {
                    svMain.Children.Add(new ThirdPartyOperationChild(item as ThirdPartyOperationModel, mw));
                }
                (svMain.Children[svMain.Children.Count - 1] as BaseStyle).sw = this;
            }
            //if (svMain.Children.Count != 0){
            //    (svMain.Children[svMain.Children.Count - 1] as BaseStyle).Margin = new Thickness(0,0,0,20);
            //}
        }

        private bool CanSave()
        {
            if (svMain.Children.Count == 0)
                return true;
            if (svMain.Children[0] is NoOperationStyle)
            {
                return true;
            }
            else
            {
                return (svMain.Children[0] as OperationStyle).ToSave();
            }
        }

    }
}
