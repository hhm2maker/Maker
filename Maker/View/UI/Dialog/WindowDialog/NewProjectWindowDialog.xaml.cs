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
        public bool bClose;

        public TextBox tbFileName;
        public TextBox tbBPM;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="hint"></param>
        /// <param name="notContains"></param>
        /// <param name="fileType"></param>
        public NewProjectWindowDialog(Window window, String TheFolder,bool bClose)
        {
            InitializeComponent();
            Owner = window;
            this.window = window;
            this.bClose = bClose;

            directorys = FileBusiness.CreateInstance().GetDirectorysName(TheFolder);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BaseStyle baseStyle = new BaseStyle();
            baseStyle.VerticalAlignment = VerticalAlignment.Center;
            //构建对话框
            tbFileName = baseStyle.GetTexeBox("");
            baseStyle.AddTitleAndControl("NewFileNameColon", tbFileName, Orientation.Vertical);

            tbBPM = baseStyle.GetTexeBox("96");
            baseStyle.AddTitleAndControl("BPMColon", tbBPM, Orientation.Vertical)
                .GetButton("Cancel", btnCancel_Click, out Button btnClose)
                .GetDockPanel(out DockPanel dp, GeneralMainViewBusiness.CreateInstance().GetButton("Ok", btnOk_Click), btnClose);

            dp.HorizontalAlignment = HorizontalAlignment.Center;
            baseStyle.AddUIElement(dp);

            baseStyle.CreateDialogNormal();

            spMain.Children.Add(baseStyle);

            baseStyle.Margin = new Thickness(30);

            if (!bClose)
            {
                btnClose.Visibility = Visibility.Collapsed;
            }

            tbFileName.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbFileName.Text.Equals(String.Empty)) {
                tbFileName.Focus();
                return;
            }
            fileName = tbFileName.Text;
            if (directorys.Contains(fileName)) {
                tbFileName.Focus();
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

      
    }
}
