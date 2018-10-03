using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;
using System.Xml.Linq;

namespace Maker.View
{
    public class BaseUserControl : UserControl
    {
        public NewMainWindow mw;
        public String filePath = String.Empty;
        protected FileBusiness fileBusiness = new FileBusiness();
        protected StackPanel spHint;
        protected Panel mainView;
        protected bool bMakerLightUserControl;

        /// <summary>
        /// 是否是制作灯光的控件
        /// </summary>
        /// <returns></returns>
        public bool IsMakerLightUserControl() {
            return bMakerLightUserControl;
        }

        public BaseUserControl()
        {
            //Closing += BaseLightWindow_Closing;
        }
        /// <summary>
        /// 隐藏控制 - 新建或者打开之前隐藏界面
        /// </summary>
        protected void HideControl()
        {
            //隐藏内部容器
            mainView.Children[0].Visibility = Visibility.Collapsed;
            //隐藏容器
            spHint = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Orientation = Orientation.Horizontal
            };
            //提示
            TextBlock tbOr = new TextBlock
            {
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20)
            };
            tbOr.SetResourceReference(TextBlock.TextProperty, "NoFileWasOpened");
            //打开
            Button btnOpen = new Button
            {
                Width = 180
            };
            btnOpen.SetResourceReference(ContentProperty, "Open");
            btnOpen.Click += OpenFile;
            //容器添加控件
            spHint.Children.Add(tbOr);
            spHint.Children.Add(btnOpen);
            //总容器添加隐藏容器
            mainView.Children.Add(spHint);
        }

        protected virtual void BaseLightWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
        }
        public String _fileExtension
        {
            get;
            set;
        }
        public String _fileType
        {
            get;
            set;
        }
       
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OpenFile(object sender, RoutedEventArgs e)
        {
            mw.cuc.OpenFile();
            //System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ////openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
            //openFileDialog1.Filter = _fileExtension.Substring(1) + "文件(*" + _fileExtension + ")|*" + _fileExtension + "|All files(*.*)|*.*";
            //openFileDialog1.RestoreDirectory = true;
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    filePath = openFileDialog1.FileName;
            //    LoadFile();
            //}
        }
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="childFileName"></param>
        public virtual void LoadFile(String childFileName)
        {
            filePath = GetFileDirectory() + childFileName;
            LoadFileContent();
            spHint.Visibility = Visibility.Collapsed;
            mainView.Children[0].Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 加载文件具体操作
        /// </summary>
        protected virtual void LoadFileContent()
        {
            
        }
        /// <summary>
        /// 文件保存具体操作
        /// </summary>
        protected virtual void SaveFile()
        {
           
        }
        /// <summary>
        /// 文件保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }
        /// <summary>
        /// 文件另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveAsFile(object sender, RoutedEventArgs e)
        { }
        /// <summary>
        /// 得到文件目录
        /// </summary>
        /// <returns></returns>
        public virtual String GetFileDirectory() {
            return mw.lastProjectPath + _fileType + @"\";
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public virtual void DeleteFile(object sender, RoutedEventArgs e)
        {
        }
      
        /// <summary>
        /// 添加文件
        /// </summary>
        public virtual void NewFile(object sender, RoutedEventArgs e)
        {
            String _filePath = GetFileDirectory();
            GetStringDialog2 dialog = new GetStringDialog2(mw, _fileExtension, fileBusiness.GetFilesName(filePath, new List<string>() { _fileExtension }), _fileExtension);
                if (dialog.ShowDialog() == true)
                {
                    filePath = _filePath + dialog.fileName;
                    if (File.Exists(filePath))
                    {
                        new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                        return;
                    }
                    else
                    {
                    CreateFile(filePath);
                    LoadFile(dialog.fileName);
                    mw.cuc.lbMain.Items.Add(dialog.fileName);
                }
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        protected virtual void CreateFile(String filePath)
        {
            File.Create(filePath).Close();
        }
       
    }
}
