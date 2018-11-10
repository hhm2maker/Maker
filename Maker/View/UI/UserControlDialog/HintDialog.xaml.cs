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

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class HintDialog : UserControl
    {
        public HintDialog(String title,String content,RoutedEventHandler okEvent, RoutedEventHandler cancelEvent, RoutedEventHandler notHintEvent)
        {
            InitializeComponent();

            tbTitle.Text = title;
            tbContent.Text = content;
            btnOk.Click += okEvent;
            btnCancel.Click += cancelEvent;
            btnNotHint.Click += notHintEvent;
        }

        
    }
}
