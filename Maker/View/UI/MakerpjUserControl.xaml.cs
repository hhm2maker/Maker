using Maker.Business;
using Maker.View.Control;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maker.View
{
    /// <summary>
    /// MakerpjUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MakerpjUserControl : UserControl
    {
        private MainWindow mw;
        public MakerpjUserControl(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            List<String> fileNames = new List<string>();
            FileBusiness business = new FileBusiness();
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\Light", new List<string>() { ".light" }));
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\LightScript", new List<string>() { ".lightScript" }));
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\Midi", new List<string>() { ".mid" }));
            ShowLightListDialog dialog = new ShowLightListDialog(mw, tbTutorialPath.Text, fileNames);
            if (dialog.ShowDialog() == true)
            {
                tbTutorialPath.Text = dialog.selectItem;
                mw.tutorial = tbTutorialPath.Text;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           tbTutorialPath.Text = mw.tutorial;
        }

        private void btnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            tbTutorialPath.Text = String.Empty;
            mw.tutorial = String.Empty;
        }
    }
}
