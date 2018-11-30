﻿using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ChangeIntoMotionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LimitlessLampUserControl : BaseUserControl
    {
        public LimitlessLampUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            _fileExtension = ".limitlessLamp";
            _fileType = "LimitlessLamp";
            mainView = gMain;
            HideControl();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public List<int> NumberList {
            get;
            set;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
            NumberList = mLaunchpad.GetNumber();
            if (!NumberList.Contains(1))
            {
                //啥也没有
                //DialogResult = false;
            }
            else {
                for (int i = 0; i < NumberList.Count; i++) {
                    System.Console.WriteLine(NumberList[i]);
                }
                //DialogResult = true;
            }
        }

        public void AA() {
            //ChangeIntoMotionDialog dialog = new ChangeIntoMotionDialog(mw);
            //if (dialog.ShowDialog() == true)
            //{
            //    StringBuilder builder = new StringBuilder();
            //    foreach (int number in dialog.NumberList)
            //    {
            //        builder.Append(number.ToString() + " ");
            //    }
            //    String stepName = GetUsableStepName();
            //    if (stepName == null)
            //    {
            //        new MessageDialog(mw, "ThereIsNoProperName").ShowDialog();
            //        return;
            //    }
            //    String commandLine = "\tLightGroup " + stepName + "LightGroup = Create.Animation(Translation,\""
            //        + builder.ToString().Trim() + "\");";

            //    lightScriptDictionary.Add(stepName, commandLine);
            //    //visibleDictionary.Add(stepName, true);
            //    containDictionary.Add(stepName, new List<string>() { stepName });
               
            //}
        }

        private void AddColumn(object sender, RoutedEventArgs e)
        {
            mLaunchpad.AddColumn();
        }
        private void RemoveColumn(object sender, RoutedEventArgs e)
        {
            mLaunchpad.RemoveColumn();
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {
            mLaunchpad.AddRow();
        }
        private void RemoveRow(object sender, RoutedEventArgs e)
        {
            mLaunchpad.RemoveRow();
        }

        protected override void CreateFile(String filePath)
        {
            //获取对象
            XDocument xDoc = new XDocument();
            // 添加根节点
            XElement xRoot = new XElement("Root");
            // 添加节点使用Add
            xDoc.Add(xRoot);
            // 创建一个按钮加到root中
            XElement xData = new XElement("Data");
            xRoot.Add(xData);
            XElement xColumns = new XElement("Columns");
            xColumns.Value = "1";
            xRoot.Add(xColumns);
            XElement xRows = new XElement("Rows");
            xRows.Value = "1";
            xRoot.Add(xRows);
            XElement xPoints = new XElement("Points");
            xRoot.Add(xPoints);
            // 保存该文档  
            xDoc.Save(filePath);
        }

        protected override void LoadFileContent()
        {
            XDocument doc = XDocument.Load(filePath);
            XElement xnroot = doc.Element("Root");
            int columns = int.Parse(xnroot.Element("Columns").Value);
            for (int i = 0; i < columns - 1; i++) {
                mLaunchpad.AddColumn();
            }
            int rows = int.Parse(xnroot.Element("Rows").Value);
            for (int i = 0; i < rows - 1; i++)
            {
                mLaunchpad.AddRow();
            }
          

            //pageNames.Clear();
            //XElement xnPages = xnroot.Element("Pages");
            //foreach (XElement pageElement in xnPages.Elements("Page"))
            //{
            //    pageNames.Add(pageElement.Value);
            //}
            //for (int i = 0; i < pageNames.Count; i++)
            //    lbPages.Items.Add(pageNames[i]);
        }

        public override void SaveFile()
        {
            XDocument doc = new XDocument();
            XElement xnroot = new XElement("Root");
            doc.Add(xnroot);

            XElement xnColumns = new XElement("Columns")
            {
                Value = mLaunchpad.ColumnsCount.ToString()
            };
            xnroot.Add(xnColumns);

            XElement xnRows = new XElement("Rows")
            {
                Value = mLaunchpad.RowsCount.ToString()
            };
            xnroot.Add(xnRows);

            //XElement xnPages = new XElement("Pages");
            //foreach (XElement pageElement in xnPages.Elements("Page"))
            //{
            //    pageNames.Add(pageElement.Value);
            //}
            //for (int i = 0; i < pageNames.Count; i++)
            //{
            //    XElement xnPage = new XElement("Page")
            //    {
            //        Value = pageNames[i]
            //    };
            //    xnPages.Add(xnPage);
            //}
            //xnroot.Add(xnPages);

            doc.Save(filePath);
        }
    }
}