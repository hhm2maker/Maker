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
        public PluginsWindow(NewMainWindow mw)
        {
            InitializeComponent();

            DataContext = new People(mw.Plugs);
        }

        class People : List<Person>
        {
            public People(List<IBasePlug> plugs)
            {

                for (int i = 0; i < plugs.Count; i++) {
                    Add(new Person() { Icon = plugs[i].GetIcon(),
                        Title = plugs[i].GetTitle()
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

    }
}
