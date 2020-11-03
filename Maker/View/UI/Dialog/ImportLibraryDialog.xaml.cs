using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.LightScriptUserControl;
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
        private ScriptUserControl suc;
        public delegate void FinishEvent(String fileName,String stepName);
        private FinishEvent finishEvent;

        public ImportLibraryDialog(NewMainWindow mw, ScriptUserControl suc, String fileName)
        {
            InitializeComponent();
            this.mw = mw;
            this.suc = suc;
            this.fileName = fileName;
        }

        public ImportLibraryDialog(NewMainWindow mw, ScriptUserControl suc ,String fileName, FinishEvent finishEvent)
        {
            InitializeComponent();
            this.mw = mw;
            this.suc = suc;
            this.fileName = fileName;
            this.finishEvent = finishEvent;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (lbMain.SelectedIndex == -1)
                return;
            FileInfo fileInfo = new FileInfo(fileName);
            if (lbMain.SelectedItem.ToString().Equals("Main"))
            {
                if (finishEvent == null)
                {
                    suc.NewFromImport(fileInfo.Name, "");
                }
                else {
                    finishEvent(fileInfo.Name, "");
                }
            }
            else
            {
                if (finishEvent == null)
                {
                    suc.NewFromImport(fileInfo.Name, lbMain.SelectedItem.ToString());
                }
                else
                {
                    finishEvent(fileInfo.Name, lbMain.SelectedItem.ToString());
                }
                
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
            }
            lbMain.Items.Add("Main");
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
