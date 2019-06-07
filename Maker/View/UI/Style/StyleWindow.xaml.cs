using Maker.Business.Model.OperationModel;
using Maker.View.Style.Child;
using Maker.View.UI.Style.Child;
using Maker.View.UI.UserControlDialog;
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
        private NewMainWindow mw;
        public StyleWindow(NewMainWindow mw)
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
       private List<BaseOperationModel> operationModels;
        public void SetData(List<BaseOperationModel> operationModels)
        {
            this.operationModels = operationModels;

            foreach (var item in operationModels)
            {
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
                if (item is VerticalFlippingOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "VerticalFlipping");
                }
                else if (item is HorizontalFlippingOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "HorizontalFlipping");
                }
                else if (item is LowerLeftSlashFlippingOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "LowerLeftSlashFlipping");
                }
                else if (item is LowerRightSlashFlippingOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "LowerRightSlashFlipping");
                }
                else if (item is ClockwiseOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ClockwiseRotation");
                }
                else if (item is AntiClockwiseOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "AntiClockwiseRotation");
                }
                else if (item is ChangeTimeOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ChangeTime");
                }
                else if (item is RemoveBorderOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "RemoveTheBorder");
                }
                else if (item is ReversalOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "Reversal");
                }
                else if (item is FoldOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "Fold");
                }
                else if (item is ChangeColorOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ChangeColor");
                }
                else if (item is SetEndTimeOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "EndTime");
                }
                else if (item is CopyToTheEndOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ColorSuperposition");
                }
                else if (item is CopyToTheFollowOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ColorSuperpositionFollow");
                }
                else if (item is AccelerationOrDecelerationOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "AccelerationOrDeceleration");
                }
                else if (item is InterceptTimeOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "InterceptTime");
                }
                else if (item is AnimationDisappearOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "Disappear");
                }
                else if (item is ShapeColorOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ShapeColor");
                }
                else if (item is ColorWithCountOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ColorWithCount");
                }
                else if (item is OneNumberOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, (item as OneNumberOperationModel).Identifier);
                }
                else if (item is ThirdPartyOperationModel)
                {
                    box.SetResourceReference(TextBlock.TextProperty, "ThirdParty");
                }
            }
            lbCatalog.SelectedIndex = 0;

            for(int i = 0;i<operationModels.Count;i++ )
            {
            BaseOperationModel baseOperationModel = operationModels[i];
            if (baseOperationModel is VerticalFlippingOperationModel)
            {
                svMain.Children.Add(new VerticalFlippingOperationChild());
            }
            else if (baseOperationModel is HorizontalFlippingOperationModel)
            {
                svMain.Children.Add(new HorizontalFlippingOperationChild());
            }
            else if (baseOperationModel is LowerLeftSlashFlippingOperationModel)
            {
                svMain.Children.Add(new LowerLeftSlashFlippingOperationChild());
            }
            else if (baseOperationModel is LowerRightSlashFlippingOperationModel)
            {
                svMain.Children.Add(new LowerRightSlashFlippingOperationChild());
            }
            else if (baseOperationModel is ClockwiseOperationModel)
            {
                svMain.Children.Add(new ClockwiseOperationChild());
            }
            else if (baseOperationModel is AntiClockwiseOperationModel)
            {
                svMain.Children.Add(new AntiClockwiseOperationChild());
            }
            else if (baseOperationModel is RemoveBorderOperationModel)
            {
                svMain.Children.Add(new RemoveBorderOperationChild());
            }
            else if (baseOperationModel is ReversalOperationModel)
            {
                svMain.Children.Add(new ReversalOperationChild());
            }
            else if (baseOperationModel is ChangeTimeOperationModel)
            {
                svMain.Children.Add(new ChangeTimeOperationChild(baseOperationModel as ChangeTimeOperationModel));
            }
            else if (baseOperationModel is FoldOperationModel)
            {
                svMain.Children.Add(new FoldOperationChild(baseOperationModel as FoldOperationModel));
            }
            else if (baseOperationModel is SetEndTimeOperationModel)
            {
                svMain.Children.Add(new SetEndTimeOperationChild(baseOperationModel as SetEndTimeOperationModel));
            }
            else if (baseOperationModel is ShapeColorOperationModel)
            {
                svMain.Children.Add(new ShapeColorOperationChild(baseOperationModel as ShapeColorOperationModel));
            }
            else if (baseOperationModel is OneNumberOperationModel)
            {
                svMain.Children.Add(new OneNumberOperationChild(baseOperationModel as OneNumberOperationModel));
            }
            else if (baseOperationModel is ChangeColorOperationModel)
            {
                svMain.Children.Add(new ColorOperationChild(baseOperationModel as ChangeColorOperationModel));
            }
            else if (baseOperationModel is CopyToTheEndOperationModel)
            {
                svMain.Children.Add(new ColorOperationChild(baseOperationModel as CopyToTheEndOperationModel));
            }
            else if (baseOperationModel is CopyToTheFollowOperationModel)
            {
                svMain.Children.Add(new ColorOperationChild(baseOperationModel as CopyToTheFollowOperationModel));
            }
            else if (baseOperationModel is AccelerationOrDecelerationOperationModel)
            {
                svMain.Children.Add(new ColorOperationChild(baseOperationModel as AccelerationOrDecelerationOperationModel));
            }
            else if (baseOperationModel is AnimationDisappearOperationModel)
            {
                svMain.Children.Add(new AnimationDisappearOperationChild(baseOperationModel as AnimationDisappearOperationModel));
            }
            else if (baseOperationModel is ColorWithCountOperationModel)
            {
                svMain.Children.Add(new ColorOperationChild(baseOperationModel as ColorWithCountOperationModel));
            }
            else if (baseOperationModel is AnimationDisappearOperationModel)
            {
                svMain.Children.Add(new AnimationDisappearOperationChild(baseOperationModel as AnimationDisappearOperationModel));
            }
            else if (baseOperationModel is InterceptTimeOperationModel)
            {
                svMain.Children.Add(new InterceptTimeOperationChild(baseOperationModel as InterceptTimeOperationModel));
            }
            else if (baseOperationModel is ThirdPartyOperationModel)
            {
                svMain.Children.Add(new ThirdPartyOperationChild(baseOperationModel as ThirdPartyOperationModel));
            }
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


        private void BtnRemoveFx_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lbCatalog.SelectedIndex == -1)
                return;
            int position = lbCatalog.SelectedIndex;
            lbCatalog.Items.RemoveAt(position);
            operationModels.RemoveAt(position);
            //svMain.Children.RemoveAt(position);
        }

        private void ImgUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lbCatalog.SelectedIndex <= 0) {
                return;
            }
            else
            {
                int i = lbCatalog.SelectedIndex;

                List<CheckBox> checkboxs = new List<CheckBox>();
                foreach (Object o in lbCatalog.Items) {
                    checkboxs.Add((CheckBox)o);
                }

                lbCatalog.Items.Clear();

                CheckBox box = checkboxs[i-1];
                checkboxs[i-1]  = checkboxs[i];
                checkboxs[i] = box;

                foreach (CheckBox c in checkboxs) {
                    lbCatalog.Items.Add(c);
                }

                List<BaseChild> basechilds = new List<BaseChild>();
                foreach (Object o in svMain.Children)
                {
                    basechilds.Add((BaseChild)o);
                }

                svMain.Children.Clear();

                BaseChild child = basechilds[i - 1];
                basechilds[i - 1] = basechilds[i];
                basechilds[i] = child;

                foreach (BaseChild b in basechilds)
                {
                    svMain.Children.Add(b);
                }
                lbCatalog.SelectedIndex = i - 1;
            }
        }
        private void ImgDown_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lbCatalog.SelectedIndex == -1 || lbCatalog.SelectedIndex == lbCatalog.Items.Count-1)
            {
                return;
            }
            else
            {
                int i = lbCatalog.SelectedIndex;

                List<CheckBox> checkboxs = new List<CheckBox>();
                foreach (Object o in lbCatalog.Items)
                {
                    checkboxs.Add((CheckBox)o);
                }

                lbCatalog.Items.Clear();

                CheckBox box = checkboxs[i + 1];
                checkboxs[i + 1] = checkboxs[i];
                checkboxs[i] = box;

                foreach (CheckBox c in checkboxs)
                {
                    lbCatalog.Items.Add(c);
                }

                List<BaseChild> basechilds = new List<BaseChild>();
                foreach (Object o in svMain.Children)
                {
                    basechilds.Add((BaseChild)o);
                }

                svMain.Children.Clear();

                BaseChild child = basechilds[i + 1];
                basechilds[i + 1] = basechilds[i];
                basechilds[i] = child;

                foreach (BaseChild b in basechilds)
                {
                    svMain.Children.Add(b);
                }
                lbCatalog.SelectedIndex = i + 1;
            }
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!CanSave())
                return;
            mw.RemoveSetting();
            mw.projectUserControl.suc.Test();
        }
    }
}
