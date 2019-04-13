using Maker.Business.Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Maker.Business.Model.ThirdPartySetupsModel;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class SetupEditPlugInsDialog : MakerDialog
    {
        private NewMainWindow mw;
        private String setupFilePath;
        private ThirdPartySetupsModel thirdPartySetupsModel;
        public SetupEditPlugInsDialog(NewMainWindow mw, String setupFilePath, ThirdPartySetupsModel thirdPartySetupsModel)
        {
            InitializeComponent();
            this.mw = mw;
            this.setupFilePath = setupFilePath;
            this.thirdPartySetupsModel = thirdPartySetupsModel;

            for (int i = 0; i < thirdPartySetupsModel.ThirdPartySetupModels.Count; i++) {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = thirdPartySetupsModel.ThirdPartySetupModels[i].Name;
                lbMain.Items.Add(listBoxItem);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }

        private void SetupEditPlugIn( ThirdPartySetupsModel thirdPartySetupModel)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Operation\View\" + thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].View + ".xml")
                         || File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Operation\Dll\" + thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].Dll + ".dll"))
            {
                //弹出提示框 - 是否覆盖
                mw.ShowMakerDialog(new SetupEditPlugInDialog(mw, setupFilePath, thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex]));
                lbMain.SelectedIndex = -1;
            }
            else
            {
                //直接安装
                mw.suc.SetupEditPlugIn(thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex]);
                lbMain.SelectedIndex = -1;
            }
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            for (int i = 0; i < mw.suc.thirdPartys.Count; i++)
            {
                if (mw.suc.thirdPartys[i].name.Equals(thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].Name))
                {
                    mw.ShowMakerDialog(new ErrorDialog(mw, "SuspectedInstalled"));
                    lbMain.SelectedIndex = -1;
                    return;
                }
            }

            HintDialog hintDialog = new HintDialog("安装编辑插件", thirdPartySetupsModel.ThirdPartySetupModels[lbMain.SelectedIndex].Text,
            delegate (System.Object _o, RoutedEventArgs _e)
            {
                //安装编辑插件
                SetupEditPlugIn( thirdPartySetupsModel);
                lbMain.SelectedIndex = -1;
            },
            delegate (System.Object _o, RoutedEventArgs _e)
            {
                mw.RemoveDialog();
                lbMain.SelectedIndex = -1;
            }
           );
            mw.ShowMakerDialog(hintDialog);
        }
    }
}
