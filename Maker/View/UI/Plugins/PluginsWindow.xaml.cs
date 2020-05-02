using Maker.Business.Currency;
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
            if (lbMain.SelectedIndex == -1)
            {
                iBigIcon.Source = null;
                tbTitle.Text = "";
                tbPermissions.Text = "";
                return;
            }
            IBasePlug plug = mw.Plugs[lbMain.SelectedIndex];
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
                else {
                    tbPermissions.Text = PermissionsClass.PermissionsExplain(plug.GetPermissions()[i]) + Environment.NewLine;
                }
            }
            if (mw.plugsConfigModel.Plugs[lbMain.SelectedIndex].Enable) {
                bEnable.Visibility = Visibility.Collapsed;
                bDisable.Visibility = Visibility.Visible;
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
        }
    }
}
