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

namespace Maker.View.UI.Play
{
    /// <summary>
    /// LogCatUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class LogCatUserControl : UserControl
    {
        public LogCatUserControl()
        {
            InitializeComponent();
        }

        public enum Level{
            Normal,
            Warning,
            Error,
        }

        public enum LogTag
        {
            Device,
            Plug,
            Input_Output,
        }

        public void SetLog(String tag,String content,Level level) {
            tbMain.Text += tag + "---" + content + content + Environment.NewLine;
            UpdateValue();
        }

        public void SetLog(LogTag tag, String content, Level level)
        {
            tbMain.Text += tag.ToString() + "---" + content+Environment.NewLine;
            UpdateValue();
        }

        private void UpdateValue()
        {
            if (tbMain.Text.Length > 10000) {
                tbMain.Text = "";
            }
        }


    }
}
