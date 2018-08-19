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
    public class BaseLightWindow : Window, ILightWindow
    {
        protected NewMainWindow mw;
        protected String filePath = String.Empty;
        private FileBusiness fileBusiness = new FileBusiness();
        private StackPanel spHint;
        protected Panel mainView;

        public BaseLightWindow() {
            Closing += BaseLightWindow_Closing;
        }
        /// <summary>
        /// 隐藏控制 - 新建或者打开之前隐藏界面
        /// </summary>
        protected void HideControl() {
            //隐藏内部容器
            mainView.Children[0].Visibility = Visibility.Collapsed;
            //隐藏容器
            spHint = new StackPanel();
            spHint.HorizontalAlignment = HorizontalAlignment.Center;
            spHint.VerticalAlignment = VerticalAlignment.Center;
            spHint.Orientation = Orientation.Horizontal;
            //新建
            Button btnNew = new Button();
            btnNew.Width = 180;
            btnNew.SetResourceReference(ContentProperty, "New");
            btnNew.Click += NewFile;
            //或者
            TextBlock tbOr = new TextBlock();
            tbOr.VerticalAlignment = VerticalAlignment.Center;
            tbOr.Margin = new Thickness(20);
            tbOr.SetResourceReference(TextBlock.TextProperty, "Or");
            //打开
            Button btnOpen = new Button();
            btnOpen.Width = 180;
            btnOpen.SetResourceReference(ContentProperty, "Open");
            btnOpen.Click += OpenFile;
            //容器添加控件
            spHint.Children.Add(btnNew);
            spHint.Children.Add(tbOr);
            spHint.Children.Add(btnOpen);
            //总容器添加隐藏容器
            mainView.Children.Add(spHint);
        }

        private void BaseLightWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            SaveFile();
            Hide();      // Programmatically hides the window
        }

        protected void NewFile(object sender, RoutedEventArgs e)
        {
            GetStringDialog2 dialog = new GetStringDialog2(this, "NewFileNameColon", fileBusiness.GetFilesName(mw.LightFilePath, new List<string>() { ".light" }));
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
            SetData(fileBusiness.ReadLightFile(filePath));
            spHint.Visibility = Visibility.Collapsed;
            mainView.Children[0].Visibility = Visibility.Visible;
        }
        protected void OpenFile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";

            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                LoadFile();
            }
       
        }
    
    private void SaveFile()
    {
            fileBusiness.WriteLightFile(filePath,GetData());
    }
    protected void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }
        protected void SaveAsFile(object sender, RoutedEventArgs e)
        {

        }

        public virtual void SetData(List<Light> lightList) { }

        public virtual List<Light> GetData() { return null; }
        
    }
}
