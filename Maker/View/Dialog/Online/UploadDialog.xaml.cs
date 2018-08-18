using Maker.View.Control;
using Maker.View.Dialog;
using Sharer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maker.View.Dialog.Online
{
    /// <summary>
    /// UploadDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UploadDialog : Window
    {
        private MainWindow mw;
        public UploadDialog(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            Owner = mw;
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender as TextBox != null)
            {
                (sender as TextBox).Background = Brushes.White;
            }
            if (sender as PasswordBox != null)
            {
                (sender as PasswordBox).Background = Brushes.White;
            }

        }
        private void Input_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender as TextBox != null)
            {
                (sender as TextBox).Background = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            }
            if (sender as PasswordBox != null)
            {
                (sender as PasswordBox).Background = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            m_tbUploader.Text = mw.strUserName;
        }

        private void m_btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //文件名
            if (m_tbProjectName.Text.Equals("") || m_tbProjectName.Text.Length > 32)
            {
                m_tbProjectNameHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 90, 90));
                return;
            }
            else
            {
                m_tbProjectNameHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            }
            //文件路径
            if (!m_tbProjectName.Text.Equals("") || !File.Exists(m_tbProjectName.Text))
            {
                m_tbProjectNameHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 90, 90));
            }
            else
            {
                m_tbProjectNameHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                return;
            }
            //文件类型
            if (m_rbUploadTypeMy.IsChecked == false && m_rbUploadTypeOther.IsChecked == false)
            {
                m_tbUploadTypeOtherHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 90, 90));
                return;
            }
            else
            {
                m_tbUploadTypeOtherHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            }
            //文件简介
            if (m_tbProjectRemarks.Text.Length > 256)
            {
                m_tbProjectRemarksHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 90, 90));
                return;
            }
            else
            {
                m_tbProjectRemarksHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            }

            FileInfo fileInfo = new System.IO.FileInfo(m_tbProjectPath.Text);
            //KB为单位
            if (System.Math.Ceiling(fileInfo.Length / 1024.0) > 4096)
            {
                //大于1M
                new MessageDialog(mw, "TheFileIsTooLarge").ShowDialog();
                return;
            }
            //扩展名 ".mid"
            if (!System.IO.Path.GetExtension(m_tbProjectPath.Text).Equals(".lightScript"))
            {
                new MessageDialog(mw, "NonLightScriptFile").ShowDialog();
                return;
            }
            //上传文件
            var url = "http://www.launchpadlight.com/sharer/UploadProject";

            var formDatas = new List<FormItemModel>();
            //添加文本  
            formDatas.Add(new FormItemModel()
            {
                Key = "UserName",
                Value = mw.strUserName
            });
            formDatas.Add(new FormItemModel()
            {
                Key = "UserPassword",
                Value = mw.strUserPassword
            });
            formDatas.Add(new FormItemModel()
            {
                Key = "UserId",
                Value = mw.mUser.UserId.ToString()
            });
            formDatas.Add(new FormItemModel()
            {
                Key = "ProjectName",
                Value = m_tbProjectName.Text
            });
            int type = 0;
            if (m_rbUploadTypeOther.IsChecked == true)
            {
                type = 1;
            }
            formDatas.Add(new FormItemModel()
            {
                Key = "ProjectType",
                Value = type.ToString()
            });
            formDatas.Add(new FormItemModel()
            {
                Key = "ProjectRemarks",
                Value = m_tbProjectRemarks.Text
            });
            formDatas.Add(new FormItemModel()
            {
                Key = "UploadTime",
                Value = DateTime.Now.ToString()
            });
            //添加文件  
            formDatas.Add(new FormItemModel()
            {
                Key = "ScriptFile",
                Value = "",
                FileName = "my.lightScript",
                FileContent = File.OpenRead(m_tbProjectPath.Text)
            });
            //提交表单  
            var result = Util.PostForm(url, formDatas);
            if (result.Equals("success"))
            {
                mw.tbUploadCount.Text = (int.Parse(mw.tbUploadCount.Text) + 1).ToString();
                System.Windows.Forms.MessageBox.Show("上传成功");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(result.Substring(5));
            }
        }

        private void btnOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "语句文件(*.lightScript)|*.lightScript|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_tbProjectPath.Text = openFileDialog1.FileName;
                if (m_tbProjectName.Text.Equals(""))
                {
                    m_tbProjectName.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                }
            }
        }
    
}
}
