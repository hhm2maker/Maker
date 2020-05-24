using Maker.Business;
using Maker.View.Control;
using Maker.View.Style.Child;
using Maker.View.UIBusiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Forms;
using System.Windows.Media;

namespace Maker.View.Dialog
{
    /// <summary>
    /// GetOneNumberDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewProjectWindowDialog : Window
    {
        private Window window;
        public String fileName = String.Empty;
        public List<String> directorys;
        public double dBpm = 0;
        public bool isClose;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="hint"></param>
        /// <param name="notContains"></param>
        /// <param name="fileType"></param>
        public NewProjectWindowDialog(Window window, String TheFolder,bool isClose)
        {
            InitializeComponent();
            Owner = window;
            this.window = window;
            this.isClose = isClose;

            directorys = FileBusiness.CreateInstance().GetDirectorysName(TheFolder);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isClose)
            {
                bClose.Visibility = Visibility.Collapsed;
            }

            tbFileName.Focus();
        }

        private void B_Ok_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (tbFileName.Text.Equals(String.Empty)) {
                tbFileName.Focus();
                return;
            }
            fileName = tbFileName.Text;
            if (directorys.Contains(fileName)) {
                tbFileName.Focus();
                new MessageDialog(this, "ExistingSameNameFile").ShowDialog();
                return;
            }

            if (tbBPM.Text.Equals(String.Empty))
            {
                tbBPM.Focus();
                return;
            }

            if (double.TryParse(tbBPM.Text, out dBpm))
            {
                DialogResult = true;
            }
            else {
                tbBPM.Focus();
                return;
            }
        }

        private void B_Cancel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DialogResult = false;

        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog m_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                String filePath = m_Dialog.SelectedPath.Trim();

                DirectoryInfo info = new DirectoryInfo(filePath);
                tbFileName.Text = info.Name;
                tbUnipad.Text = filePath;
            }
        }
    }
}
