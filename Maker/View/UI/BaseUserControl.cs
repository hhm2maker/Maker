using Maker.Business;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.Windows;
using Maker.Model;
using System.Windows.Controls;
using System.IO;
using System.Xml.Linq;
using System.Windows.Media;
using Maker.View.UI.UserControlDialog;

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
            this.MouseLeftButtonDown += BaseUserControl_MouseLeftButtonDown;
            //Closing += BaseLightWindow_Closing;
        }

        private void BaseUserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// 隐藏控制 - 新建或者打开之前隐藏界面
        /// </summary>
        public void HideControl()
        {
            //隐藏内部容器
            mainView.Children[0].Visibility = Visibility.Collapsed;
            if (spHint == null)
            {
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
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20)
                };
                tbOr.SetResourceReference(TextBlock.TextProperty, "NoFileWasOpened");
              
                //容器添加控件
                spHint.Children.Add(tbOr);
                //总容器添加隐藏容器
                mainView.Children.Add(spHint);
            }
            else {
                spHint.Visibility = Visibility.Visible;
            }
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
        /// 加载文件
        /// </summary>
        /// <param name="childFileName"></param>
        public virtual void LoadFile(String childFileName)
        {
            filePath = GetFileDirectory() + childFileName;
            LoadFileContent();
            if(spHint != null) { 
                  spHint.Visibility = Visibility.Collapsed;
                  mainView.Children[0].Visibility = Visibility.Visible;
            }
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
        public virtual void SaveFile()
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
            return mw.LastProjectPath + _fileType + @"\";
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public virtual void DeleteFile(object sender, RoutedEventArgs e)
        {
            File.Delete(GetFileDirectory() + filePath);
        }
      
        /// <summary>
        /// 添加文件
        /// </summary>
        public virtual void NewFile(object sender, RoutedEventArgs e)
        {
            String _filePath = GetFileDirectory();
            UI.UserControlDialog.NewFileDialog newFileDialog = new UI.UserControlDialog.NewFileDialog(mw,false, _fileExtension, fileBusiness.GetFilesName(filePath, new List<string>() { _fileExtension }), _fileExtension, NewFileResult);
            mw.ShowMakerDialog(newFileDialog);
        }

        public void NewFileResult(String filePath)
        {
            mw.RemoveDialog();
            String _filePath = GetFileDirectory();

            _filePath = _filePath + filePath;
            if (File.Exists(_filePath))
            {
                new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                return;
            }
            else
            {
                CreateFile(_filePath);
                if (_filePath.EndsWith(".playExport"))
                {
                    //mw.lbPlay.Items.Add(item);
                    this.filePath = _filePath;
                    SaveFile();
                    return;
                }
                LoadFile(filePath);
                TreeViewItem item = new TreeViewItem
                {
                    Header = filePath,
                };
                item.FontSize = 16;
                item.ContextMenu = mw.contextMenu;
                if (filePath.EndsWith(".light")) {
                    mw.tvLight.Items.Add(item);
                }
                else if (filePath.EndsWith(".lightScript"))
                {
                    mw.tvLightScript.Items.Add(item);
                }
                else if (filePath.EndsWith(".limitlessLamp"))
                {
                    mw.tvLimitlessLamp.Items.Add(item);
                }
                item.IsSelected = true;
            }
            //mw.projectUserControl.suc.InitMyContent();
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        protected virtual void CreateFile(String filePath)
        {
            File.Create(filePath).Close();
        }

        /// <summary>
        /// 当窗口消失触发事件
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void OnDismiss()
        {
           
        }
    }
}
