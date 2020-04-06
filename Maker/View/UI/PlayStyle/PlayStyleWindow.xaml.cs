using Maker.Business.Model.OperationModel;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.PageWindow;
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
    public partial class PlayStyleWindow : BaseStyleUserControl
    {
        public PageMainUserControl mw;
        public PlayStyleWindow(PageMainUserControl mw)
        {
            InitializeComponent();
            this.mw = mw;

            cbMain = lbCatalog;
            spMain = svMain;
        }
      
        public override void OnRefresh() {
            
        }

        public void SetData(List<BaseOperationModel> operationModels)
        {
            lbCatalog.Items.Clear();
            svMain.Children.Clear();
            this.operationModels = operationModels;

            foreach (var item in operationModels)
            {
                lbCatalog.SelectedIndex = 0;

                if (item is LightFilePlayModel)
                {
                    svMain.Children.Add(new LightFilePlayChild(item as LightFilePlayModel));
                }
                else if (item is GotoPagePlayModel)
                {
                    svMain.Children.Add(new GotoPagePlayChild(item as GotoPagePlayModel));
                }
                else if (item is AudioFilePlayModel)
                {
                    svMain.Children.Add(new AudioFilePlayChild(item as AudioFilePlayModel));
                }


                (svMain.Children[svMain.Children.Count - 1] as BaseStyle).sw = this;

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

     
    }
}
