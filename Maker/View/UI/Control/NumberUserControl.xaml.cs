using Maker.Business;
using Maker.Model;
using Operation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.Control
{
    /// <summary>
    /// NumberUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class NumberUserControl : UserControl
    {
        public NumberUserControl()
        {
            InitializeComponent();
        }
        private List<Light> lab = new List<Light>();
        /// <summary>
        /// 获取主窗口数据
        /// </summary>
        public void SetData(List<Light> mActionBeanList)
        {
            rbTime.IsChecked = false;
            rbAction.IsChecked = false;
            rbPosition.IsChecked = false;
            rbColor.IsChecked = false;

            lab = Business.LightBusiness.Copy(mActionBeanList);
            Print();
        }
        private void SortWith(String str)
        {
            List<Light> mActionBeanList = new List<Light>();
            String ActionStr = tbMain.Text;
            String LingShi = "";
            List<String> LingShiActionStrList = new List<String>();
            for (int j = 0; j < ActionStr.Length; j++)
            {
                if (ActionStr[j] == ';')
                {
                    LingShi = LingShi.Replace("\n", "");
                    LingShi = LingShi.Replace("\r", "");
                    LingShiActionStrList.Add(LingShi);
                    LingShi = "";
                    continue;
                }
                LingShi += ActionStr[j];
            }
            for (int j = 0; j < LingShiActionStrList.Count; j++)
            {
                String[] x = LingShiActionStrList[j].Split(',');
                Light ab = new Light();
                ab.Time = int.Parse(x[0]);
                if (Char.Parse(x[1]) == 'o')
                {
                    ab.Action = 144;
                }
                else if (Char.Parse(x[1]) == 'c')
                {
                    ab.Action = 128;
                }
                ab.Position = int.Parse(x[2]);
                ab.Color = int.Parse(x[3]);
                mActionBeanList.Add(ab);
            }
            lab = mActionBeanList;
            if (str.Equals("time"))
            {
                //根据时间排序
                for (int j = 0; j < lab.Count - 1; j++)
                {
                    for (int k = 0; k < lab.Count - j - 1; k++)
                    {
                        if (lab[k].Time > lab[k + 1].Time)
                        {
                            Light ab = new Light();
                            ab.Time = lab[k].Time;
                            ab.Action = lab[k].Action;
                            ab.Position = lab[k].Position;
                            ab.Color = lab[k].Color;

                            lab[k] = lab[k + 1];
                            lab[k + 1] = ab;
                        }
                    }
                }
            }
            if (str.Equals("action"))
            {
                //根据行为排序
                for (int j = 0; j < lab.Count - 1; j++)
                {
                    for (int k = 0; k < lab.Count - j - 1; k++)
                    {
                        if (lab[k].Action < lab[k + 1].Action)//行为是开比关大
                        {
                            Light ab = new Light();
                            ab.Time = lab[k].Time;
                            ab.Action = lab[k].Action;
                            ab.Position = lab[k].Position;
                            ab.Color = lab[k].Color;

                            lab[k] = lab[k + 1];
                            lab[k + 1] = ab;
                        }
                    }
                }
            }
            if (str.Equals("position"))
            {
                //根据位置排序
                for (int j = 0; j < lab.Count - 1; j++)
                {
                    for (int k = 0; k < lab.Count - j - 1; k++)
                    {
                        if (lab[k].Position > lab[k + 1].Position)
                        {
                            Light ab = new Light();
                            ab.Time = lab[k].Time;
                            ab.Action = lab[k].Action;
                            ab.Position = lab[k].Position;
                            ab.Color = lab[k].Color;

                            lab[k] = lab[k + 1];
                            lab[k + 1] = ab;
                        }
                    }
                }
            }
            if (str.Equals("color"))
            {
                //根据颜色排序
                for (int j = 0; j < lab.Count - 1; j++)
                {
                    for (int k = 0; k < lab.Count - j - 1; k++)
                    {
                        if (lab[k].Color > lab[k + 1].Color)
                        {
                            Light ab = new Light();
                            ab.Time = lab[k].Time;
                            ab.Action = lab[k].Action;
                            ab.Position = lab[k].Position;
                            ab.Color = lab[k].Color;

                            lab[k] = lab[k + 1];
                            lab[k + 1] = ab;
                        }
                    }
                }
            }
        }
        private void Print()
        {
            if (tbMain == null)
            {
                return;
            }
            StringBuilder line = new StringBuilder();
            for (int k = 0; k < lab.Count; k++)
            {
                if (lab[k].Action == 144) {
                    line.Append(lab[k].Time + ",o," + lab[k].Position + "," + lab[k].Color + ";" + Environment.NewLine);
                }
                else if (lab[k].Action == 128)
                {
                    line.Append(lab[k].Time + ",c," + lab[k].Position + "," + lab[k].Color + ";" + Environment.NewLine);
                }
            }
            tbMain.Text = line.ToString();
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns>ActionBean集合</returns>
        public List<Light> GetData()
        {
            List<Light> mActionBeanList = new List<Light>();
            String ActionStr = tbMain.Text;
            String LingShi = "";
            List<String> LingShiActionStrList = new List<String>();
            for (int j = 0; j < ActionStr.Length; j++)
            {
                if (ActionStr[j] == ';')
                {
                    LingShi = LingShi.Replace("\n", "");
                    LingShi = LingShi.Replace("\r", "");
                    LingShiActionStrList.Add(LingShi);
                    LingShi = "";
                    continue;
                }
                LingShi += ActionStr[j];
            }
            for (int j = 0; j < LingShiActionStrList.Count; j++)
            {
                String[] x = LingShiActionStrList[j].Split(',');
                Light ab = new Light();
                ab.Time = int.Parse(x[0]);
                if (Char.Parse(x[1]) == 'o')
                {
                    ab.Action = 144;
                }
                else if (Char.Parse(x[1]) == 'c')
                {
                    ab.Action = 128;
                }
                ab.Position = int.Parse(x[2]);
                ab.Color = int.Parse(x[3]);
                mActionBeanList.Add(ab);
            }
            return mActionBeanList;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbError.Items.Clear();
            //获取AB集合
            string ActionStr = tbMain.Text;
            string LingShi = "";
            List<String> mActionStrList = new List<String>();
            List<Light> mActionBeanList = new List<Light>();
            for (int j = 0; j < ActionStr.Length; j++)
            {
                if (ActionStr[j] == ';')
                {
                    LingShi = LingShi.Replace("\n", "");
                    LingShi = LingShi.Replace("\r", "");
                    mActionStrList.Add(LingShi);
                    LingShi = "";
                    continue;
                }
                LingShi += ActionStr[j];
            }
            for (int j = 0; j < mActionStrList.Count; j++)
            {
                String[] x = mActionStrList[j].Split(',');

                try
                {

                    Light ab = new Light();
                    ab.Time = int.Parse(x[0]);
                    if (Char.Parse(x[1]) == 'o')
                    {
                        ab.Action = 144;
                    }
                    else if (Char.Parse(x[1]) == 'c')
                    {
                        ab.Action = 128;
                    }
                    ab.Position = int.Parse(x[2]);
                    ab.Color = int.Parse(x[3]);
                    mActionBeanList.Add(ab);
                }
                catch
                {
                    lbError.Items.Add("第" + (j + 1) + "个动作有误：" + "格式不正确");
                }
            }

            //检测动作、位置、颜色是否正常
            for (int j = 0; j < mActionBeanList.Count; j++)
            {
                StringBuilder errorStr = new StringBuilder();
                if (mActionBeanList[j].Action != 144 && mActionBeanList[j].Action != 128)
                {
                    errorStr.Append("行为有误;");
                }
                if (mActionBeanList[j].Position < 28 || mActionBeanList[j].Position > 123)
                {
                    errorStr.Append("位置有误;");
                }
                if (mActionBeanList[j].Color < 1 || mActionBeanList[j].Color > 127)
                {
                    errorStr.Append("颜色有误;");
                }
                if (!errorStr.ToString().Equals(""))
                {
                    lbError.Items.Add("第" + (j + 1) + "个动作有误：" + errorStr);
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender==rbTime)
            {
                SortWith("time");
            }
            else if (sender == rbAction)
            {
                SortWith("action");
            }
            else if (sender == rbPosition)
            {
                SortWith("position");
            }
            else if (sender == rbColor)
            {
                SortWith("color");
            }
            Print();
        }

        //private void GoToErrorLine(object sender, EventArgs e)
        //{
        //    int errorLine = -1;
        //    for (int i = 0; i < mLabelList_Error.Count; i++)
        //    {
        //        if (sender == mLabelList_Error[i])
        //        {
        //            errorLine = mErrorList_int[i];
        //        }
        //    }
        //    if (errorLine == -1)
        //    {
        //        return;
        //    }

        //    String str = textBox1.Text;
        //    int linshi = 0;
        //    int start = -1;
        //    Boolean open = false;
        //    int end = -1;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        if (linshi == errorLine && start == -1)
        //        {
        //            open = true;
        //        }
        //        if (open && str[i] != '\r' && str[i] != '\n' && str[i] != ';')
        //        {
        //            start = i;
        //            open = false;
        //        }
        //        if (str[i] == ';')
        //        {
        //            linshi++;
        //        }
        //        if (linshi == errorLine + 1)
        //        {
        //            end = i + 1;
        //            break;
        //        }
        //    }

        //    textBox1.Select(start, end - start);
        //    textBox1.Focus();
        //    textBox1.ScrollToCaret();
        //}
    }
}
