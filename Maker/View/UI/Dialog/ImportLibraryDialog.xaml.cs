using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.UI.UserControlDialog;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ImportLibraryDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImportLibraryDialog : MakerDialog
    {
        private String fileName;
        private NewMainWindow mw;
        public ImportLibraryDialog(NewMainWindow mw, String fileName)
        {
            InitializeComponent();
            this.mw = mw;
            this.fileName = fileName;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            FileInfo fileInfo = new FileInfo(fileName);
            if (lbMain.SelectedItem.ToString().Equals("Main"))
            {
                mw.editUserControl.suc.NewFromImport(fileInfo.Name, "");
            }
            else
            {
                mw.editUserControl.suc.NewFromImport(fileInfo.Name, lbMain.SelectedItem.ToString());
            }
            mw.RemoveDialog();
        }
      

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mw.RemoveDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<String, ScriptModel> stepsDictionary = ScriptFileBusiness.GetScriptModelDictionary(fileName,out string introduce,out string audioResources);
            foreach (ScriptModel step in stepsDictionary.Values) {
                if (step.Visible) {
                    lbMain.Items.Add(step.Name);
                }
                lbMain.Items.Add("Main");
            }
            //LightScriptBusiness scriptBusiness = new LightScriptBusiness();
            //String command = scriptBusiness.LoadLightScript(fileName);
            //Dictionary<String, String> dictionary = scriptBusiness.GetCatalog(command, out Dictionary<String, List<String>> extendsDictionary, out Dictionary<String, List<String>> intersectionDictionary, out Dictionary<String, List<String>> complementDictionary);
            //foreach (var item in dictionary) {
            //    if (!item.Key.Trim().Equals("NoVisible") && !item.Key.Trim().Equals("Contain") && !item.Key.Trim().Equals("Introduce") && !item.Key.Trim().Equals("Final") && !item.Key.Trim().Equals("Locked"))
            //    {
            //        lbMain.Items.Add(item.Key);
            //    }
            //}
            //lbMain.Items.Add("Main");
        }
    }
}
