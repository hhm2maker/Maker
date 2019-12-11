using Maker.Business.Utils;
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
        private NewMainWindow mw;
        public CalcTimeWindow(NewMainWindow mw)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.mw = mw;
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
            tbBPM.Text = mw.NowProjectModel.Bpm.ToString();

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

       


        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(tbFilePath.Text))
            {
                return;
            }
            tbResult.Text = MediaFileTimeUtil.GetAsfTime(tbFilePath.Text,double.Parse(tbBPM.Text));
        }
    }
}
