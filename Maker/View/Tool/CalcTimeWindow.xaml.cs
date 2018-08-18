using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Maker.View.Tool
{
    /// <summary>
    /// CalcTimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalcTimeWindow : Window
    {
        public CalcTimeWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
   
        public int time = 0;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbFilePath.Focus();
        }

      
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbFilePath.Text = openFileDialog.FileName;
            }
        }

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, StringBuilder m_strReceive, int m_v1, int m_v2);

        private string getasfTime(string filePath)
        {
            StringBuilder shortpath = new StringBuilder(80);
            GetShortPathName(filePath, shortpath, shortpath.Capacity);
            string name = shortpath.ToString();
            StringBuilder buf = new StringBuilder(80);
            mciSendString("close all", buf, buf.Capacity, 0);
            mciSendString("open " + name + " alias media", buf, buf.Capacity, 0);
            mciSendString("status media length", buf, buf.Capacity, 0);
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)Convert.ToDouble(buf.ToString().Trim()));
            //return ts.ToString();
            return buf.ToString().Trim();
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(tbFilePath.Text))
            {
                return;
            }
            long l = long.Parse(getasfTime(tbFilePath.Text));
            tbResult.Text = (l * double.Parse(tbBPM.Text) / 1000).ToString();
        }
    }
}
