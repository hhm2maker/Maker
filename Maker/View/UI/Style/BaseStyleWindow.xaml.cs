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
    public partial class BaseStyleWindow : BaseStyleUserControl
    {
        public ScriptUserControl mw;
        public BaseStyleWindow()
        {
            InitializeComponent();

            spMain = svMain;
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

            StackPanel spParent = bs.Parent as StackPanel;
            int oldPosition = spParent.Children.IndexOf(bs);
            int newPosition = -100;

            Point nowPoint = e.GetPosition(svMain);
            for (int i = 0; i < svMain.Children.Count; i++)
            {
                Point position = svMain.Children[i].TranslatePoint(new Point(0, 0), svMain);
                if (position.Y > nowPoint.Y)
                {
                    newPosition = i - 1;
                    break;
                }
            }

            //将控件移至最后
            if (newPosition == -100)
            {
                //已经是最后了
                if (oldPosition == spParent.Children.Count - 1)
                {
                    return;
                }

                newPosition = spParent.Children.Count - 1;
            }
            else
            {
                //将控件移至 newPosition - 已经在上面赋值
            }

            //如果是原位
            if (oldPosition == newPosition)
            {
                return;
            }

            //暂不支持移动到第一个
            if (newPosition == 0)
            {
                return;
            }

            //取出大的数，先移除
            int bigPosition = oldPosition > newPosition ? oldPosition : newPosition;
            int smallPosition = oldPosition < newPosition ? oldPosition : newPosition;

            BaseStyle bd = spMain.Children[smallPosition] as BaseStyle;
            BaseStyle bd2 = spMain.Children[bigPosition] as BaseStyle;
            spMain.Children.RemoveAt(bigPosition);
            spMain.Children.RemoveAt(smallPosition);
            spMain.Children.Insert(smallPosition, bd2);
            spMain.Children.Insert(bigPosition, bd);

            BaseOperationModel bom = operationModels[smallPosition] as BaseOperationModel;
            BaseOperationModel bom2 = operationModels[bigPosition] as BaseOperationModel;
            operationModels.RemoveAt(bigPosition);
            operationModels.RemoveAt(smallPosition);
            operationModels.Insert(smallPosition, bom2);
            operationModels.Insert(bigPosition, bom);

            OnRefresh();
        }

        private void BaseStyle_DragLeave(object sender, DragEventArgs e)
        {
        }
    }
}
