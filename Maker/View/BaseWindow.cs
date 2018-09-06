﻿using Maker.Business;
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
    public class BaseWindow : Window
    {
        protected NewMainWindow mw;
        protected String filePath = String.Empty;
        protected FileBusiness fileBusiness = new FileBusiness();
        protected StackPanel spHint;
        protected Panel mainView;

        public BaseWindow()
        {
            Closing += BaseLightWindow_Closing;
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
            //新建
            Button btnNew = new Button
            {
                Width = 180
            };
            btnNew.SetResourceReference(ContentProperty, "New");
            btnNew.Click += NewFile;
            //或者
            TextBlock tbOr = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20)
            };
            tbOr.SetResourceReference(TextBlock.TextProperty, "Or");
            //打开
            Button btnOpen = new Button
            {
                Width = 180
            };
            btnOpen.SetResourceReference(ContentProperty, "Open");
            btnOpen.Click += OpenFile;
            //容器添加控件
            spHint.Children.Add(btnNew);
            spHint.Children.Add(tbOr);
            spHint.Children.Add(btnOpen);
            //总容器添加隐藏容器
            mainView.Children.Add(spHint);
        }

        protected virtual void BaseLightWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            Hide();      // Programmatically hides the window
        }
        protected String _fileType
        {
            get;
            set;
        }

        /// <summary>
        /// 新建文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NewFile(object sender, RoutedEventArgs e)
        {
            GetStringDialog2 dialog = null;
            String _filePath = String.Empty;
            if (_fileType.Equals(".light"))
            {
                _filePath = mw.LightFilePath;
            }
            else if (_fileType.Equals(".lightScript"))
            {
                _filePath = mw.LightScriptFilePath;
            }
            else if (_fileType.Equals(".lightPage"))
            {
                _filePath = mw.LightPageFilePath;
            }
            else if (_fileType.Equals(".playExport"))
            {
                _filePath = mw.PlayFilePath;
            }
            dialog = new GetStringDialog2(this, "NewFileNameColon", fileBusiness.GetFilesName(filePath, new List<string>() { _fileType }), _fileType);
            if (dialog != null)
            {
                if (dialog.ShowDialog() == true)
                {
                    filePath = _filePath + dialog.fileName;
                    if (File.Exists(filePath))
                    {
                        new MessageDialog(this, "ExistingSameNameFile").ShowDialog();
                        return;
                    }
                    else
                    {
                        if (_fileType.Equals(".lightPage")) {
                            //获取对象
                            XDocument xDoc = new XDocument();
                            // 添加根节点
                            XElement xRoot = new XElement("Page");
                            // 添加节点使用Add
                            xDoc.Add(xRoot);
                            for (int i = 0; i < 96; i++)
                            {
                                // 创建一个按钮加到root中
                                XElement xButton = new XElement("Buttons");
                                xRoot.Add(xButton);
                            }
                            // 保存该文档  
                            xDoc.Save(filePath);
                        }
                        else { 
                            File.Create(filePath).Close();
                        }
                    }
                    LoadFile();
                }
            }
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OpenFile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
            openFileDialog1.Filter = _fileType.Substring(1) + "文件(*" + _fileType + ")|*" + _fileType + "|All files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                LoadFile();
            }
        }

        protected virtual void LoadFile()
        {
            LoadFileContent();
            spHint.Visibility = Visibility.Collapsed;
            mainView.Children[0].Visibility = Visibility.Visible;
        }

        protected virtual void LoadFileContent()
        {
            
        }

        protected virtual void SaveFile()
        {
           
        }
        protected void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }
        protected void SaveAsFile(object sender, RoutedEventArgs e)
        { }

      
    }
}
