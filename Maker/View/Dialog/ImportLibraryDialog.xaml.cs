using Maker.Business;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ImportLibraryDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImportLibraryDialog : Window
    {
        private MainWindow mw;
        private String fileName;
        public ImportLibraryDialog(MainWindow mw, String fileName)
        {
            InitializeComponent();
            this.mw = mw;
            this.fileName = fileName;
            Owner = mw;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LightScriptBusiness scriptBusiness = new LightScriptBusiness();
            String command = scriptBusiness.LoadLightScript(fileName);
            Dictionary<String, String> dictionary = scriptBusiness.GetCatalog(command, out Dictionary<String, List<String>> extendsDictionary, out Dictionary<String, List<String>> intersectionDictionary, out Dictionary<String, List<String>> complementDictionary);
            foreach (var item in dictionary) {
                if (!item.Key.Trim().Equals("NoVisible") && !item.Key.Trim().Equals("Contain") && !item.Key.Trim().Equals("Introduce") && !item.Key.Trim().Equals("Final") && !item.Key.Trim().Equals("Locked"))
                {
                    lbMain.Items.Add(item.Key);
                }
            }
            lbMain.Items.Add("Main");
        }
    }
}
