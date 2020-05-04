using Maker.Business.Currency;
using Maker.View.UI.BottomDialog;
using Maker.View.UI.Dialog.WindowDialog;
using PlugLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Maker.View.UI.Plugins
{
    /// <summary>
    /// PluginsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PluginsWindow : Window
    {
        private NewMainWindow mw;
        public PluginsWindow(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;
            DataContext = new People(mw.Plugs);
        }

        class People : List<Person>
        {
            public People(List<IBasePlug> plugs)
            {
                for (int i = 0; i < plugs.Count; i++) {
                    Add(new Person() { Icon = plugs[i].GetIcon(),
                        Title = plugs[i].GetInfo().Title
                    });
                }
            }
        }

        public class Person : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private void Notify(String propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private ImageSource icon;
            public ImageSource Icon
            {
                get { return icon; }
                set
                {
                    icon = value;
                    Notify("Icon");
                }
            }

            private String title;
            public String Title
            {
                get { return title; }
                set
                {
                    title = value;
                    Notify("Title");
                }
            }
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Change();
        }

        private void Change()
        {
            if (lbMain.SelectedIndex == -1)
            {
                UpdateRight(null);
                return;
            }
            IBasePlug plug = mw.Plugs[lbMain.SelectedIndex];
            UpdateRight(plug);

            if (mw.plugsConfigModel.Plugs[lbMain.SelectedIndex].Enable)
            {
                bEnable.Visibility = Visibility.Collapsed;
                bDisable.Visibility = Visibility.Visible;
            }
            else
            {
                bEnable.Visibility = Visibility.Visible;
                bDisable.Visibility = Visibility.Collapsed;
            }

            bUnInstall.Visibility = Visibility.Visible;
        }

        public void UpdateRight(IBasePlug plug) {
            if (plug == null)
            {
                iBigIcon.Source = null;
                tbTitle.Text = "";
                tbPermissions.Text = "";
                tbAuthor.Text = "";
                tbDescribe.Text = "";

                bEnable.Visibility = Visibility.Collapsed;
                bDisable.Visibility = Visibility.Collapsed;
                bInstall.Visibility = Visibility.Collapsed;
                bUnInstall.Visibility = Visibility.Collapsed;
            }
            else {
                iBigIcon.Source = plug.GetIcon();
                PlugInfo plugInfo = plug.GetInfo();
                tbTitle.Text = plugInfo.Title;
                tbAuthor.Text = plugInfo.Author;
                tbDescribe.Text = plugInfo.Describe;
                for (int i = 0; i < plug.GetPermissions().Count; i++)
                {
                    if (i == plug.GetPermissions().Count - 1)
                    {
                        tbPermissions.Text = PermissionsClass.PermissionsExplain(plug.GetPermissions()[i]);
                    }
                    else
                    {
                        tbPermissions.Text = PermissionsClass.PermissionsExplain(plug.GetPermissions()[i]) + Environment.NewLine;
                    }
                }
            }
        }

        private void B_Disable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetEnable(false);
        }

        private void B_Enable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetEnable(true);
        }

        private void SetEnable(bool isEnable)
        {
            if (lbMain.SelectedIndex == -1)
            {
                return;
            }

            mw.plugsConfigModel.Plugs[lbMain.SelectedIndex].Enable = isEnable;
            XmlSerializerBusiness.Save(mw.plugsConfigModel, "Config/plugs.xml");
            Change();

            Restart();
        }


        private String installPath = String.Empty;
        private void I_File_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String fileName = openFileDialog.FileName;
                String basePath = AppDomain.CurrentDomain.BaseDirectory + @"Plugs\";
                if (fileName.StartsWith(basePath))
                {
                    fileName = fileName.Substring(basePath.Length);

                    for (int i = 0; i < mw.plugsConfigModel.Plugs.Count; i++)
                    {
                        if (mw.plugsConfigModel.Plugs[i].Path.Equals(fileName))
                        {
                            lbMain.SelectedIndex = i;
                            return;
                        }
                    }

                    installPath = fileName;

                    lbMain.SelectedIndex = -1;
                    UpdateRight(mw.FilePathToPlug(installPath));

                    bInstall.Visibility = Visibility.Visible;
                }
                else {
                    //选择的不是Plugs文件夹下的文件
                    mw.AddMessageBottomDialog(new ErrorMessageBottomClass(mw, 40001));
                }
            }
            else {
               
            }
        }

        private void B_Install_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var plug = new Business.Model.Config.PlugsConfigModel.Plug
            {
                Path = installPath,
                Enable = true
            };
            mw.plugsConfigModel.Plugs.Add(plug);
            XmlSerializerBusiness.Save(mw.plugsConfigModel, "Config/plugs.xml");

            mw.Plugs.Add(mw.FilePathToPlug(installPath));
            DataContext = new People(mw.Plugs);
            lbMain.SelectedIndex = lbMain.Items.Count - 1;

            bInstall.Visibility = Visibility.Collapsed;

            Restart();
        }

        private void B_UnInstall_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //目前为 直接删除xml文件中的内容
            //之后改为弹窗可以勾选是否删除本地文件
            //new ConfirmWindowDialog(this).Show();
            if (lbMain.SelectedIndex == -1)
            {
                return;
            }

            mw.plugsConfigModel.Plugs.RemoveAt(lbMain.SelectedIndex);
            XmlSerializerBusiness.Save(mw.plugsConfigModel, "Config/plugs.xml");

            mw.Plugs.RemoveAt(lbMain.SelectedIndex);

            //lbMain.SelectedIndex = -1;
            //Change();
            DataContext = new People(mw.Plugs);
            Change();

            Restart();
        }

        /// <summary>
        /// 显示重启弹窗
        /// </summary>
        private void Restart() {
            mw.AddMessageBottomDialog(new RestartMessageBottomClass(mw));
        }
    }
}
