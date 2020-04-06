using Maker.Business.Model.OperationModel;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.Style.Child;
using Maker.View.UI.Style.Child;
using Maker.View.UI.Style.Child.Operation;
using Maker.View.UI.UserControlDialog;
using Operation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Maker.View.Style
{
    /// <summary>
    /// StyleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StyleWindow : BaseStyleUserControl
    {
        public ScriptUserControl mw;
        public StyleWindow(ScriptUserControl mw)
        {
            InitializeComponent();
            this.mw = mw;

            cbMain = lbCatalog;
            spMain = svMain;
        }

        public override void OnRefresh() {
            mw.Test();
        }

        public void SetData(List<BaseOperationModel> operationModels)
        {
            lbCatalog.Items.Clear();
            svMain.Children.Clear();

            this.operationModels = operationModels;

            foreach (var item in operationModels)
            {
                lbCatalog.SelectedIndex = 0;

                if (item is ConditionJudgmentOperationModel)
                {
                    svMain.Children.Add(new ConditionJudgmentOperationChild(item as ConditionJudgmentOperationModel,mw));
                }
                else if (item is SetAttributeOperationModel)
                {
                    svMain.Children.Add(new SetAttributeOperationChild(item as SetAttributeOperationModel));
                }
                else if (item is CreateFromAutomaticOperationModel)
                {
                    svMain.Children.Add(new CreateFromAutomaticOperationChild(item as CreateFromAutomaticOperationModel));
                }
                else if (item is CreateFromFileOperationModel)
                {
                    svMain.Children.Add(new CreateFromFileOperationChild(item as CreateFromFileOperationModel));
                }
                else if (item is CreateFromStepOperationModel)
                {
                    svMain.Children.Add(new CreateFromStepOperationChild(item as CreateFromStepOperationModel));
                }
                else if (item is CreateFromQuickOperationModel)
                {
                    svMain.Children.Add(new CreateFromQuickOperationChild(item as CreateFromQuickOperationModel,mw));
                }
                else if (item is VerticalFlippingOperationModel)
                {
                    svMain.Children.Add(new VerticalFlippingOperationChild());
                }
                else if (item is HorizontalFlippingOperationModel)
                {
                    svMain.Children.Add(new HorizontalFlippingOperationChild());
                }
                else if (item is LowerLeftSlashFlippingOperationModel)
                {
                    svMain.Children.Add(new LowerLeftSlashFlippingOperationChild());
                }
                else if (item is LowerRightSlashFlippingOperationModel)
                {
                    svMain.Children.Add(new LowerRightSlashFlippingOperationChild());
                }
                else if (item is ClockwiseOperationModel)
                {
                    svMain.Children.Add(new ClockwiseOperationChild());
                }
                else if (item is AntiClockwiseOperationModel)
                {
                    svMain.Children.Add(new AntiClockwiseOperationChild());
                }
                else if (item is RemoveBorderOperationModel)
                {
                    svMain.Children.Add(new RemoveBorderOperationChild());
                }
                else if (item is ReversalOperationModel)
                {
                    svMain.Children.Add(new ReversalOperationChild());
                }
                else if (item is ChangeTimeOperationModel)
                {
                    svMain.Children.Add(new ChangeTimeOperationChild(item as ChangeTimeOperationModel));
                }
                else if (item is FoldOperationModel)
                {
                    svMain.Children.Add(new FoldOperationChild(item as FoldOperationModel));
                }
                else if (item is SetEndTimeOperationModel)
                {
                    svMain.Children.Add(new SetEndTimeOperationChild(item as SetEndTimeOperationModel));
                }
                else if (item is ShapeColorOperationModel)
                {
                    svMain.Children.Add(new ShapeColorOperationChild(item as ShapeColorOperationModel));
                }
                else if (item is OneNumberOperationModel)
                {
                    svMain.Children.Add(new OneNumberOperationChild(item as OneNumberOperationModel));
                }
                else if (item is ChangeColorOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as ChangeColorOperationModel));
                }
                else if (item is CopyToTheEndOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as CopyToTheEndOperationModel));
                }
                else if (item is CopyToTheFollowOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as CopyToTheFollowOperationModel));
                }
                else if (item is AccelerationOrDecelerationOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as AccelerationOrDecelerationOperationModel));
                }
                else if (item is AnimationDisappearOperationModel)
                {
                    svMain.Children.Add(new AnimationDisappearOperationChild(item as AnimationDisappearOperationModel));
                }
                else if (item is ColorWithCountOperationModel)
                {
                    svMain.Children.Add(new ColorOperationChild(item as ColorWithCountOperationModel));
                }
                else if (item is AnimationDisappearOperationModel)
                {
                    svMain.Children.Add(new AnimationDisappearOperationChild(item as AnimationDisappearOperationModel));
                }
                else if (item is InterceptTimeOperationModel)
                {
                    svMain.Children.Add(new InterceptTimeOperationChild(item as InterceptTimeOperationModel));
                }
                else if (item is ThirdPartyOperationModel)
                {
                    svMain.Children.Add(new ThirdPartyOperationChild(item as ThirdPartyOperationModel));
                }
                (svMain.Children[svMain.Children.Count -1] as BaseStyle).sw = this;

                ListBoxItem mItem = new ListBoxItem()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40)),
                    Background = new SolidColorBrush(Colors.Transparent),
                };
                TextBlock box = new TextBlock
                {
                    FontSize = 16,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                mItem.Content = box;
                //mItem.MouseLeftButtonDown += Box_Click;
                lbCatalog.Items.Add(mItem);

                box.SetResourceReference(TextBlock.TextProperty, (svMain.Children[svMain.Children.Count - 1] as BaseStyle).Title);
            }
            //if (svMain.Children.Count != 0){
            //    (svMain.Children[svMain.Children.Count - 1] as BaseStyle).Margin = new Thickness(0,0,0,20);
            //}
    }

        public void SetData(List<BaseOperationModel> operationModels,bool isNew)
        {
            lbCatalog.Items.Clear();
            SetData(operationModels);
            if (isNew) {
                //是新增的
                lbCatalog.SelectedIndex = lbCatalog.Items.Count-1;
            }
        }

        private void Box_Click(object sender, MouseButtonEventArgs e)
        {
            lbCatalog.SelectedItem = sender;
        }

        private void lbCatalog_MouseEnter(object sender, MouseEventArgs e)
        {
            lbCatalog.IsEnabled = CanSave();
        }

        private bool CanSave() {
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

        private void BaseStyleUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            svMain.AllowDrop = true;
            svMain.Drop += SvMain_Drop;
            svMain.DragLeave += BaseStyle_DragLeave;
        }

        private void SvMain_Drop(object sender, DragEventArgs e)
        {

            BaseStyle bs = (BaseStyle)e.Data.GetData("this");
            //Point position = bs.TranslatePoint(new Point(0, 0), svMain);
            //Console.WriteLine(position.X + "---" + position.Y);

            int iPosition = -100;

            Point nowPoint = e.GetPosition(svMain);
            for (int i = 0; i < svMain.Children.Count; i++)
            {
                Point position = svMain.Children[i].TranslatePoint(new Point(0, 0), svMain);
                if (position.Y > nowPoint.Y)
                {
                    iPosition = i - 1;
                    break;
                }
            }
            if (iPosition == -100)
            {
                //将控件移至最后
            }
            else {
                //将控件移至 iPosition
            }

            //Console.WriteLine(nowPoint.X + "---" + nowPoint.Y);
        }

        private void BaseStyle_DragLeave(object sender, DragEventArgs e)
        {
        }
    }
}
