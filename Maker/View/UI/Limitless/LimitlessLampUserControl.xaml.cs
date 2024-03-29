﻿using Maker.Model;
using Operation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ChangeIntoMotionDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LimitlessLampUserControl : BaseMakerLightUserControl, IMakerLight
    {
        public LimitlessLampUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            _fileExtension = ".limitlessLamp";
            _fileType = "LimitlessLamp";
            mainView = gMain;
            HideControl();

            completeColorPanel.SetSelectionChangedEvent(lbColor_SelectionChanged);

            previewLaunchpad.Size = 410;
            previewLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(28,30,31)));
            mLaunchpad.SetParent(this);

        }

        private void lbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mLaunchpad.NowBrushNumber = completeColorPanel.NowColor;
            mLaunchpad.NowBrush = StaticConstant.brushList[mLaunchpad.NowBrushNumber];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Width = mw.ActualWidth - 300;
            Height = mw.gMost.ActualHeight - 100;
        }

            private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
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
            XElement xInterval = new XElement("Interval");
            xInterval.Value = "12";
            xRoot.Add(xInterval);
            XElement xPoints = new XElement("Points");
            xRoot.Add(xPoints);
            // 保存该文档  
            xDoc.Save(filePath);
        }
        private List<Point> points = new List<Point>();
        protected override void LoadFileContent()
        {
            mLaunchpad.Reset();
            points.Clear();
         
            Operation.LimitlessLampModel limitlessLampModel = Operation.FileBusiness.CreateInstance().ReadLimitlessLamp(filePath);
            for (int i = 0; i < limitlessLampModel.Columns - 1; i++)
            {
                mLaunchpad.AddColumn();
            }
            for (int i = 0; i < limitlessLampModel.Rows - 1; i++)
            {
                mLaunchpad.AddRow();
            }
            mLaunchpad.SetData(limitlessLampModel.Data);
            points = limitlessLampModel.Points;
            lbPoint.Items.Clear();
            foreach (var point in points) {
                lbPoint.Items.Add(point.X+","+point.Y);
            }
            tbInterval.Text = limitlessLampModel.Interval.ToString();
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

              XElement xnData = new XElement("Data")
            {
                Value = mLaunchpad.GetData()
            };
            xnroot.Add(xnData);

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

            if (!int.TryParse(tbInterval.Text, out int interval))
            {
                return;
            }
            XElement xnInterval = new XElement("Interval")
            {
               
                Value = interval.ToString()
            };
            xnroot.Add(xnInterval);

            XElement xnPoints = new XElement("Points");
            foreach (var point in points)
            {
                XElement xnPoint = new XElement("Point");
                xnPoint.SetAttributeValue("x",point.X);
                xnPoint.SetAttributeValue("y", point.Y);
                xnPoints.Add(xnPoint);
            }
            xnroot.Add(xnPoints);
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

        private void AddPoint(object sender, RoutedEventArgs e)
        {
            GetNumberDialog dialog = new GetNumberDialog(mw,"",true);
            if (dialog.ShowDialog() == true) {
                points.Add(new Point(dialog.MultipleNumber[0], dialog.MultipleNumber[1]));
                lbPoint.Items.Add(points[points.Count-1].X+","+ points[points.Count - 1].Y);
            }
        }
        private void RemovePoint(object sender, RoutedEventArgs e)
        {
            if (lbPoint.SelectedIndex == -1)
                return;
            points.RemoveAt(lbPoint.SelectedIndex);
            lbPoint.Items.RemoveAt(lbPoint.SelectedIndex);
        }

        private void lbPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbPoint.SelectedIndex == -1)
                return;
            String[] strs = lbPoint.SelectedItem.ToString().Split(',');
            if (strs.Length != 2)
                return;
            mLaunchpad.SetDataToPreviewLaunchpad(int.Parse(strs[0]), int.Parse(strs[1]));
        }

        public override List<Light> GetData()
        {
            List<Light> ll = new List<Light>();
            if (int.TryParse(tbInterval.Text, out int interval)) {
               
                for (int i = 0; i < points.Count; i++)
                {
                    List<Light> mLl = mLaunchpad.SetDataToPreviewLaunchpadFromXY((int)points[i].X, (int)points[i].Y);
                    for (int j = 0; j < mLl.Count; j++)
                        mLl[j].Time = i * interval;
                    ll.AddRange(mLl);
                }
            }
            return ll;
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mw.RemoveChildren();
        }

        private void TextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbPoint.Visibility == Visibility.Collapsed)
            {
                lbPoint.Visibility = Visibility.Visible;
                tbEdit.Visibility = Visibility.Collapsed;

                lbPoint.Items.Clear();
                String[] str = tbEdit.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                for (int i = 0; i < str.Length; i++) {
                    if (str[i].Equals(String.Empty))
                        continue;
                    String[] _str = tbEdit.Text.Split(',');
                    if (_str.Length >= 2) {
                        lbPoint.Items.Add(_str[0].Trim() + ','+_str[1].Trim());
                    }
                }
            }
            else {
                lbPoint.Visibility = Visibility.Collapsed;
                tbEdit.Visibility = Visibility.Visible;

                tbEdit.Clear();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lbPoint.Items.Count; i++)
                {
                    sb.Append(lbPoint.Items[i] + Environment.NewLine);
                }
                tbEdit.Text = sb.ToString();
                tbEdit.Focus();
                tbEdit.SelectionStart = tbEdit.Text.Length;
            }
        }

        public override void OnDismiss()
        {
            SaveFile();
        }
    }
}
