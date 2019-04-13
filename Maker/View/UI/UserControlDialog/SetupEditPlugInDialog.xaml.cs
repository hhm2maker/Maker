using Maker.Business.Model;
using System;
using System.IO;
using System.Windows;
using static Maker.Business.Model.ThirdPartySetupsModel;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class SetupEditPlugInDialog : MakerDialog
    {
        private NewMainWindow mw;
        private String setupFilePath;
        private ThirdPartySetupModel thirdPartySetupModel;
        public SetupEditPlugInDialog(NewMainWindow mw, String setupFilePath, ThirdPartySetupModel thirdPartySetupModel)
        {
            InitializeComponent();
            this.mw = mw;
            this.setupFilePath = setupFilePath;
            this.thirdPartySetupModel = thirdPartySetupModel;
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            //覆盖View和DLL文件
            FileInfo fileInfo = new FileInfo(setupFilePath);
            File.Copy(fileInfo.Directory+@"\"+ thirdPartySetupModel.View+".xml", AppDomain.CurrentDomain.BaseDirectory + @"Operation\View\" + thirdPartySetupModel.View + ".xml",true);
            File.Copy(fileInfo.Directory + @"\" + thirdPartySetupModel.Dll + ".dll", AppDomain.CurrentDomain.BaseDirectory + @"Operation\Dll\" + thirdPartySetupModel.Dll + ".dll", true);

            //安装插件
            mw.suc.SetupEditPlugIn(thirdPartySetupModel);

            mw.RemoveDialog();
            mw.RemoveDialog();
        }

        private void btnNoReplacement_Click(object sender, RoutedEventArgs e)
        {
            //安装插件
            mw.suc.SetupEditPlugIn(thirdPartySetupModel);

            mw.RemoveDialog();
            mw.RemoveDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
            mw.RemoveDialog();
        }
    }
}
