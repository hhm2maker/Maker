using System;
using System.Windows;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class HintDialog : MakerDialog
    {
        public HintDialog(String title,String content, RoutedEventHandler okEventHandler, RoutedEventHandler cancelEventHandler, RoutedEventHandler notHintEventHandler)
        {
            InitializeComponent();

            tbTitle.Text = title;
            tbContent.Text = content;

            btnOk.Click += okEventHandler;
            btnCancel.Click += cancelEventHandler;
            btnNotHint.Click += notHintEventHandler;
        }
    }
}
