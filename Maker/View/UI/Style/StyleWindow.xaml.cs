﻿using Maker.Business.Model.OperationModel;
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
    public partial class StyleWindow : MakerDialog
    {
        public ScriptUserControl mw;
        public StyleWindow(ScriptUserControl mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void lbCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //svMain.Children.Clear();
            if (lbCatalog.SelectedIndex == -1)
            {
                return;
            }
            else {
                //BaseOperationModel baseOperationModel = operationModels[lbCatalog.SelectedIndex];
                //if (baseOperationModel is VerticalFlippingOperationModel)
                //    {
                //        svMain.Children.Add(new VerticalFlippingOperationChild());
                //    }
                //else if (baseOperationModel is HorizontalFlippingOperationModel)
                //{
                //    svMain.Children.Add(new HorizontalFlippingOperationChild());
                //}
                //else if (baseOperationModel is LowerLeftSlashFlippingOperationModel)
                //{
                //    svMain.Children.Add(new LowerLeftSlashFlippingOperationChild());
                //}
                //else if (baseOperationModel is LowerRightSlashFlippingOperationModel)
                //{
                //    svMain.Children.Add(new LowerRightSlashFlippingOperationChild());
                //}
                //else if (baseOperationModel is ClockwiseOperationModel)
                //{
                //    svMain.Children.Add(new ClockwiseOperationChild());
                //}
                //else if (baseOperationModel is AntiClockwiseOperationModel)
                //{
                //    svMain.Children.Add(new AntiClockwiseOperationChild());
                //}
                //else if (baseOperationModel is RemoveBorderOperationModel)
                //{
                //    svMain.Children.Add(new RemoveBorderOperationChild());
                //}
                //else if (baseOperationModel is ReversalOperationModel)
                //{
                //    svMain.Children.Add(new ReversalOperationChild());
                //}
                //else if (baseOperationModel is ChangeTimeOperationModel)
                //{
                //    svMain.Children.Add(new ChangeTimeOperationChild(baseOperationModel as ChangeTimeOperationModel));
                //}
                //else if (baseOperationModel is FoldOperationModel)
                //{
                //    svMain.Children.Add(new FoldOperationChild(baseOperationModel as FoldOperationModel));
                //}
                //else if (baseOperationModel is SetEndTimeOperationModel)
                //{
                //    svMain.Children.Add(new SetEndTimeOperationChild(baseOperationModel as SetEndTimeOperationModel));
                //}
                //else if (baseOperationModel is ShapeColorOperationModel)
                //{
                //    svMain.Children.Add(new ShapeColorOperationChild(baseOperationModel as ShapeColorOperationModel));
                //}
                //else if (baseOperationModel is OneNumberOperationModel)
                //{
                //    svMain.Children.Add(new OneNumberOperationChild(baseOperationModel as OneNumberOperationModel));
                //}
                //else if (baseOperationModel is ChangeColorOperationModel)
                //{
                //    svMain.Children.Add(new ColorOperationChild(baseOperationModel as ChangeColorOperationModel));
                //}
                //else if (baseOperationModel is CopyToTheEndOperationModel)
                //{
                //    svMain.Children.Add(new ColorOperationChild(baseOperationModel as CopyToTheEndOperationModel));
                //}
                //else if (baseOperationModel is CopyToTheFollowOperationModel)
                //{
                //    svMain.Children.Add(new ColorOperationChild(baseOperationModel as CopyToTheFollowOperationModel));
                //}
                //else if (baseOperationModel is AccelerationOrDecelerationOperationModel)
                //{
                //    svMain.Children.Add(new ColorOperationChild(baseOperationModel as AccelerationOrDecelerationOperationModel));
                //}
                //else if (baseOperationModel is AnimationDisappearOperationModel)
                //{
                //    svMain.Children.Add(new AnimationDisappearOperationChild(baseOperationModel as AnimationDisappearOperationModel));
                //}
                //else if (baseOperationModel is ColorWithCountOperationModel)
                //{
                //    svMain.Children.Add(new ColorOperationChild(baseOperationModel as ColorWithCountOperationModel));
                //}
                //else if (baseOperationModel is AnimationDisappearOperationModel)
                //{
                //    svMain.Children.Add(new AnimationDisappearOperationChild(baseOperationModel as AnimationDisappearOperationModel));
                //}
                //else if (baseOperationModel is InterceptTimeOperationModel)
                //{
                //    svMain.Children.Add(new InterceptTimeOperationChild(baseOperationModel as InterceptTimeOperationModel));
                //}
                //else if (baseOperationModel is ThirdPartyOperationModel)
                //{
                //    svMain.Children.Add(new ThirdPartyOperationChild(baseOperationModel as ThirdPartyOperationModel));
                //}
            }       
        }
       public List<BaseOperationModel> operationModels;
        public void SetData(List<BaseOperationModel> operationModels)
        {
            lbCatalog.Items.Clear();
            svMain.Children.Clear();

            this.operationModels = operationModels;

            foreach (var item in operationModels)
            {
               
                //if (item is SetAttributeOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "FastGeneration");
                //}
                //else if (item is CreateFromStepOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "FastGeneration");
                //}
                //else if (item is CreateFromQuickOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "FastGeneration");
                //}
                //else if (item is VerticalFlippingOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "VerticalFlipping");
                //}
                //else if (item is HorizontalFlippingOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "HorizontalFlipping");
                //}
                //else if (item is LowerLeftSlashFlippingOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "LowerLeftSlashFlipping");
                //}
                //else if (item is LowerRightSlashFlippingOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "LowerRightSlashFlipping");
                //}
                //else if (item is ClockwiseOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ClockwiseRotation");
                //}
                //else if (item is AntiClockwiseOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "AntiClockwiseRotation");
                //}
                //else if (item is ChangeTimeOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ChangeTime");
                //}
                //else if (item is RemoveBorderOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "RemoveTheBorder");
                //}
                //else if (item is ReversalOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "Reversal");
                //}
                //else if (item is FoldOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "Fold");
                //}
                //else if (item is ChangeColorOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ChangeColor");
                //}
                //else if (item is SetEndTimeOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "EndTime");
                //}
                //else if (item is CopyToTheEndOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ColorSuperposition");
                //}
                //else if (item is CopyToTheFollowOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ColorSuperpositionFollow");
                //}
                //else if (item is AccelerationOrDecelerationOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "AccelerationOrDeceleration");
                //}
                //else if (item is InterceptTimeOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "InterceptTime");
                //}
                //else if (item is AnimationDisappearOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "Disappear");
                //}
                //else if (item is ShapeColorOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ShapeColor");
                //}
                //else if (item is ColorWithCountOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ColorWithCount");
                //}
                //else if (item is OneNumberOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, (item as OneNumberOperationModel).Identifier);
                //}
                //else if (item is ThirdPartyOperationModel)
                //{
                //    box.SetResourceReference(TextBlock.TextProperty, "ThirdParty");
                //}
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
            if (svMain.Children.Count != 0){
                (svMain.Children[svMain.Children.Count - 1] as BaseStyle).Margin = new Thickness(0,0,0,20);
            }
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

     
    }
}
