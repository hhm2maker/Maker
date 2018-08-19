using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;

namespace Maker.View.LightWindow
{
    public class BaseLightWindow : Window,ILightWindow
    {
        protected NewMainWindow mw;
        protected String filePath = String.Empty;
        private FileBusiness business = new FileBusiness();
        protected StackPanel hintView;
        protected Panel mainView;
        protected void NewFile(object sender, RoutedEventArgs e)
        {
            GetStringDialog2 dialog = new GetStringDialog2(this, "NewFileNameColon", business.GetFilesName(mw.LightFilePath, new List<string>() { ".light" }));
            if (dialog.ShowDialog() == true) {
                filePath = mw.LightFilePath + dialog.fileName;
                if (File.Exists(filePath))
                {
                    new MessageDialog(this, "ExistingSameNameFile").ShowDialog();
                    return;
                }
                else {
                    File.Create(filePath).Close();
                }
                LoadFile();
            }
        }
        protected void LoadFile() {
            SetData(business.ReadLightFile(filePath));
            hintView.Visibility = Visibility.Collapsed;
            mainView.Visibility = Visibility.Visible;
        }
        protected void OpenFile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";

            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
            }
            LoadFile();
        }
        protected void SaveFile(object sender, RoutedEventArgs e)
        {

        }
        protected void SaveAsFile(object sender, RoutedEventArgs e)
        {

        }

        public virtual void SetData(List<Light> lightList) { }

        public virtual List<Light> GetData() { return null; }
        
    }
}
