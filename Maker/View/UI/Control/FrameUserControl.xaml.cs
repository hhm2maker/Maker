﻿using Maker.Business;
using Maker.Model;
using Maker.Utils;
using Maker.View.Control;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.Utils;
using MakerUI.Device;
using Operation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Maker.View.Control
{
    /// <summary>
    /// FrameUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class FrameUserControl : UserControl,ICanDraw
    {
        private MainControlWindow mw;
        private MainControlUserControl mw2;

        public FrameUserControl(MainControlUserControl mw)
        {
            InitializeComponent();
            InitLaunchpadEvent();

            mw2 = mw;

            //FileBusiness file = new FileBusiness();
            //ColorList = file.ReadColorFile(mw.mw.strColortabPath);
        }
        public FrameUserControl(MainControlWindow mw)
        {
            InitializeComponent();
            InitLaunchpadEvent();

            this.mw = mw;

            //FileBusiness file = new FileBusiness();
            //ColorList = file.ReadColorFile(mw.mw.strColortabPath);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lbColor.SelectedIndex = 5;
            tbNowColor.Text = (lbColor.SelectedIndex).ToString();
            tbNowColor.Background = NumToBrush(lbColor.SelectedIndex);
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromArgb(255, 53, 53, 53)));
            mLaunchpad.SetCanDraw(true);
        }
        private void InitLaunchpadEvent()
        {
            mLaunchpad.SetMouseEnter(ChangeColor);
            mLaunchpad.SetMouseLeftButtonDown(SetColor);
            mLaunchpad.SetMouseRightButtonDown(ClearColor);
            mLaunchpad.SetICanDraw(this);           
        }
        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;

            if (mouseType == 1)
            {
                if (LeftOrRight == 0)
                {
                    for (int i = 0; i < mLaunchpad.Count; i++)
                    {
                        if (sender == mLaunchpad.GetButton(i))
                        {
                            dic[liTime[nowTimePoint - 1]][i] = nowColor;
                            break;
                        }
                    }
                }
                else if (LeftOrRight == 1)
                {
                    for (int i = 0; i < mLaunchpad.Count; i++)
                    {
                        if (sender == mLaunchpad.GetButton(i))
                        {
                            dic[liTime[nowTimePoint - 1]][i] = 0;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 0左键1右键
        /// </summary>
        private int LeftOrRight = -1;
        private void SetColor(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            for (int i = 0; i < mLaunchpad.Count; i++)
            {
                if (sender == mLaunchpad.GetButton(i))
                {
                    dic[liTime[nowTimePoint - 1]][i] = nowColor;
                    break;
                }
            }
            LeftOrRight = 0;
        }
        private void ClearColor(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            for (int i = 0; i < mLaunchpad.Count; i++)
            {
                if (sender == mLaunchpad.GetButton(i))
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                    break;
                }
            }
            LeftOrRight = 1;
        }
        private List<int> liTime = new List<int>();
        private Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
        //private List<FrameworkElement> lfe = new List<FrameworkElement>();
        private List<String> ColorList = new List<string>();
        private int nowTimePoint = -1;
        private int mouseType = 0;//0没按下 1按下
        private int nowColor = 5;//当前颜色
        private Boolean bLeftUp = false;//左上角区域是否被选中
        private Boolean bRightUp = false;
        private Boolean bLeftDown = false;
        private Boolean bRightDown = false;

        /// <summary>
        /// 获取主窗口数据
        /// </summary>
        public void SetData(List<Light> mActionBeanList)
        {
            ClearFrame();
            liTime = Business.LightBusiness.GetTimeList(mActionBeanList);
            dic = Business.LightBusiness.GetParagraphLightIntList(mActionBeanList);
            if (liTime.Count == 0)
            {
                tbTimeNow.Text = "0";
                nowTimePoint = 0;
                tbTimePointCountLeft.Text = "0";
                tbTimePointCount.Text = " / " + 0;
            }
            else
            {
                tbTimeNow.Text = liTime[0].ToString();
                nowTimePoint = 1;
                tbTimePointCountLeft.Text = nowTimePoint.ToString();
                tbTimePointCount.Text = " / " + liTime.Count;
                LoadFrame();
            }
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns>ActionBean集合</returns>
        public List<Light> GetData()
        {
            List<Light> mActionBeanList = new List<Light>();
            Boolean[] b = new Boolean[96];
            for (int i = 0; i < 96; i++)
            {
                b[i] = false;
            }
            for (int i = 0; i < liTime.Count; i++)
            {    
                for (int j = 0; j < 96; j++) {
                    if (dic[liTime[i]][j] == 0)
                    {
                        if (b[j]) {
                            mActionBeanList.Add(new Light(liTime[i], 128, j + 28, 64));
                            b[j] = false;
                        }
                    }
                    if (dic[liTime[i]][j] != 0) {
                        if (b[j])
                        {
                            mActionBeanList.Add(new Light(liTime[i], 128, j + 28, 64));
                        }
                        mActionBeanList.Add(new Light(liTime[i], 144, j + 28, dic[liTime[i]][j]));
                        b[j] = true;
                    }
                }
            }
            return mActionBeanList;
        }

        private void ClearFrame()
        {
            //清空
            mLaunchpad.ClearAllColorExceptMembrane();
        }

        private void LoadFrame()
        {
            ClearFrame();
            int[] x = dic[liTime[nowTimePoint - 1]];

            for (int i = 0; i < x.Count(); i++)
            {
                //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
                if (x[i] == 0)
                {
                    continue;
                }
                RoundedCornersPolygon rcp = mLaunchpad.GetButton(i) as RoundedCornersPolygon;
                if (rcp != null)
                {
                    rcp.Fill = NumToBrush(x[i]);
                }
                Ellipse e = mLaunchpad.GetButton(i) as Ellipse;
                if (e != null)
                {
                    e.Fill = NumToBrush(x[i]);
                }
                Rectangle r = mLaunchpad.GetButton(i) as Rectangle;
                if (r != null)
                {
                    r.Fill = NumToBrush(x[i]);
                }
            }
        }

     
        public void ToLastTime() {
            if (nowTimePoint <= 1) return;
            nowTimePoint--;
            tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
            tbTimePointCountLeft.Text = nowTimePoint.ToString();
            tbTimePointCount.Text = " / " + liTime.Count;
            LoadFrame();
        }
        public void ToNextTime()
        {
            if (nowTimePoint > dic.Count - 1) return;
            nowTimePoint++;
            tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
            tbTimePointCountLeft.Text = nowTimePoint.ToString();
            tbTimePointCount.Text =  " / " + liTime.Count;
            LoadFrame();
        }
        private void btnLastTimePoint_Click(object sender, RoutedEventArgs e)
        {
            ToLastTime();
        }

        private void btnNextTimePoint_Click(object sender, RoutedEventArgs e)
        {
            ToNextTime();
        }

        private void btnInsertTimePoint_Click(object sender, RoutedEventArgs e)
        {
            GetNumberDialog dialog = new GetNumberDialog(mw, "TheFrameOfTheNewNodeColon", false, liTime, false);
            if (dialog.ShowDialog() == true) {
                int[] x = new int[96];
                for (int j = 0; j < 96; j++)
                {
                    x[j] = 0;
                }
                InsertTimePoint(dialog.OneNumber, x);
            }
        }
        /// <summary>
        /// 插入时间点
        /// </summary>
        /// <param name="time">插入时间</param>
        /// <param name="shape">插入形状</param>
        private void InsertTimePoint(int time, int[] shape)
        {
            if (liTime.Count == 0)
            {
                liTime.Insert(0, time);
                dic.Add(time, shape);
                nowTimePoint = 1;
                tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                tbTimePointCountLeft.Text = nowTimePoint.ToString();
                tbTimePointCount.Text = " / " + liTime.Count;
                LoadFrame();
            }
            else
            {
                //如果比最大的小，比较大小插入合适的位置
                if (liTime[liTime.Count - 1] > time)
                {
                    for (int i = 0; i < liTime.Count; i++)
                    {
                        if (liTime[i] > time)
                        {
                            liTime.Insert(i, time);
                            dic.Add(time, shape);
                            nowTimePoint = i + 1;
                            tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                            tbTimePointCountLeft.Text = nowTimePoint.ToString();
                            tbTimePointCount.Text = " / " + liTime.Count;
                            LoadFrame();
                            break;
                        }
                    }
                }
                //比最大的大，插入最后
                else
                {
                    liTime.Add(time);
                    dic.Add(time, shape);
                    nowTimePoint = liTime.Count;
                    tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                    tbTimePointCountLeft.Text = nowTimePoint.ToString();
                    tbTimePointCount.Text = " / " + liTime.Count;
                    LoadFrame();
                }
            }

        }

        private void btnDeleteTimePoint_Click(object sender, RoutedEventArgs e)
        {
            if (liTime.Count == 0)
                return;

            OkOrCancelDialog oocd = new OkOrCancelDialog(mw, "WhetherToDeleteTheTimeNode");
            if (oocd.ShowDialog() == true)
            {
                dic.Remove(liTime[nowTimePoint - 1]);
                liTime.RemoveAt(nowTimePoint - 1);
                if (liTime.Count == 0)
                {
                    nowTimePoint = 0;
                    tbTimeNow.Text = "0";
                    tbTimePointCountLeft.Text = "0";
                    tbTimePointCount.Text = " / " + 0;
                    ClearFrame();
                }
                else
                {
                    if (nowTimePoint == 1)
                    {
                        nowTimePoint = 1;
                    }
                    else
                    {
                        nowTimePoint--;
                    }

                    tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                    tbTimePointCountLeft.Text = nowTimePoint.ToString();
                    tbTimePointCount.Text =  " / " + liTime.Count;
                    LoadFrame();
                }
            }
        }

        /// <summary>
        /// 数字转笔刷
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        private SolidColorBrush NumToBrush(int i)
        {
            if (i == 0)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorList[i - 1]));
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseType = 1;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseType = 0;
        }

        private void lbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbNowColor.Text = (lbColor.SelectedIndex).ToString();
            tbNowColor.Background = NumToBrush(lbColor.SelectedIndex);
            nowColor = lbColor.SelectedIndex;
            if (lbColor.SelectedIndex == 0)
            {
                mLaunchpad.SetNowBrush(StaticConstant.closeBrush);
            }
            else {
                mLaunchpad.SetNowBrush(StaticConstant.brushList[lbColor.SelectedIndex - 1]);
            }
         
        }

        private void btnLeftUp_Click(object sender, RoutedEventArgs e)
        {
            if (!bLeftUp)
            {
                //如果没被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.Yellow;
                btn.BorderThickness = new Thickness(3);
            }
            else
            {
                //如果被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.White;
                btn.BorderThickness = new Thickness(1);
            }
            bLeftUp = !bLeftUp;
        }

        private void btnRightUp_Click(object sender, RoutedEventArgs e)
        {
            if (!bRightUp)
            {
                //如果没被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.Yellow;
                btn.BorderThickness = new Thickness(3);
            }
            else
            {
                //如果被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.White;
                btn.BorderThickness = new Thickness(1);
            }
            bRightUp = !bRightUp;
        }

        private void btnLeftDown_Click(object sender, RoutedEventArgs e)
        {
            if (!bLeftDown)
            {
                //如果没被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.Yellow;
                btn.BorderThickness = new Thickness(3);
            }
            else
            {
                //如果被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.White;
                btn.BorderThickness = new Thickness(1);
            }
            bLeftDown = !bLeftDown;
        }

        private void btnRightDown_Click(object sender, RoutedEventArgs e)
        {
            if (!bRightDown)
            {
                //如果没被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.Yellow;
                btn.BorderThickness = new Thickness(3);
            }
            else
            {
                //如果被选中
                Button btn = (Button)sender;
                btn.BorderBrush = Brushes.White;
                btn.BorderThickness = new Thickness(1);
            }
            bRightDown = !bRightDown;
        }

        private void btnRegionHorizontalFlipping_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            if (bLeftDown)
            {
                int[] x = new int[16];
                for (int i = 8; i < 24; i++)
                {
                    x[i - 8] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][8] = x[12];
                dic[liTime[nowTimePoint - 1]][9] = x[13];
                dic[liTime[nowTimePoint - 1]][10] = x[14];
                dic[liTime[nowTimePoint - 1]][11] = x[15];

                dic[liTime[nowTimePoint - 1]][12] = x[8];
                dic[liTime[nowTimePoint - 1]][13] = x[9];
                dic[liTime[nowTimePoint - 1]][14] = x[10];
                dic[liTime[nowTimePoint - 1]][15] = x[11];

                dic[liTime[nowTimePoint - 1]][16] = x[4];
                dic[liTime[nowTimePoint - 1]][17] = x[5];
                dic[liTime[nowTimePoint - 1]][18] = x[6];
                dic[liTime[nowTimePoint - 1]][19] = x[7];

                dic[liTime[nowTimePoint - 1]][20] = x[0];
                dic[liTime[nowTimePoint - 1]][21] = x[1];
                dic[liTime[nowTimePoint - 1]][22] = x[2];
                dic[liTime[nowTimePoint - 1]][23] = x[3];
            }
            if (bLeftUp)
            {
                int[] x = new int[16];
                for (int i = 24; i < 40; i++)
                {
                    x[i - 24] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][24] = x[12];
                dic[liTime[nowTimePoint - 1]][25] = x[13];
                dic[liTime[nowTimePoint - 1]][26] = x[14];
                dic[liTime[nowTimePoint - 1]][27] = x[15];

                dic[liTime[nowTimePoint - 1]][28] = x[8];
                dic[liTime[nowTimePoint - 1]][29] = x[9];
                dic[liTime[nowTimePoint - 1]][30] = x[10];
                dic[liTime[nowTimePoint - 1]][31] = x[11];

                dic[liTime[nowTimePoint - 1]][32] = x[4];
                dic[liTime[nowTimePoint - 1]][33] = x[5];
                dic[liTime[nowTimePoint - 1]][34] = x[6];
                dic[liTime[nowTimePoint - 1]][35] = x[7];

                dic[liTime[nowTimePoint - 1]][36] = x[0];
                dic[liTime[nowTimePoint - 1]][37] = x[1];
                dic[liTime[nowTimePoint - 1]][38] = x[2];
                dic[liTime[nowTimePoint - 1]][39] = x[3];
            }
            if (bRightDown)
            {
                int[] x = new int[16];
                for (int i = 40; i < 56; i++)
                {
                    x[i - 40] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][40] = x[12];
                dic[liTime[nowTimePoint - 1]][41] = x[13];
                dic[liTime[nowTimePoint - 1]][42] = x[14];
                dic[liTime[nowTimePoint - 1]][43] = x[15];

                dic[liTime[nowTimePoint - 1]][44] = x[8];
                dic[liTime[nowTimePoint - 1]][45] = x[9];
                dic[liTime[nowTimePoint - 1]][46] = x[10];
                dic[liTime[nowTimePoint - 1]][47] = x[11];

                dic[liTime[nowTimePoint - 1]][48] = x[4];
                dic[liTime[nowTimePoint - 1]][49] = x[5];
                dic[liTime[nowTimePoint - 1]][50] = x[6];
                dic[liTime[nowTimePoint - 1]][51] = x[7];

                dic[liTime[nowTimePoint - 1]][52] = x[0];
                dic[liTime[nowTimePoint - 1]][53] = x[1];
                dic[liTime[nowTimePoint - 1]][54] = x[2];
                dic[liTime[nowTimePoint - 1]][55] = x[3];
            }
            if (bRightUp)
            {
                int[] x = new int[16];
                for (int i = 56; i < 72; i++)
                {
                    x[i - 56] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][56] = x[12];
                dic[liTime[nowTimePoint - 1]][57] = x[13];
                dic[liTime[nowTimePoint - 1]][58] = x[14];
                dic[liTime[nowTimePoint - 1]][59] = x[15];

                dic[liTime[nowTimePoint - 1]][60] = x[8];
                dic[liTime[nowTimePoint - 1]][61] = x[9];
                dic[liTime[nowTimePoint - 1]][62] = x[10];
                dic[liTime[nowTimePoint - 1]][63] = x[11];

                dic[liTime[nowTimePoint - 1]][64] = x[4];
                dic[liTime[nowTimePoint - 1]][65] = x[5];
                dic[liTime[nowTimePoint - 1]][66] = x[6];
                dic[liTime[nowTimePoint - 1]][67] = x[7];

                dic[liTime[nowTimePoint - 1]][68] = x[0];
                dic[liTime[nowTimePoint - 1]][69] = x[1];
                dic[liTime[nowTimePoint - 1]][70] = x[2];
                dic[liTime[nowTimePoint - 1]][71] = x[3];
            }
            LoadFrame();
            #endregion
        }

        private void btnRegionVerticalFlipping_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            if (bLeftDown)
            {
                int[] x = new int[16];
                for (int i = 8; i < 24; i++)
                {
                    x[i - 8] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][8] = x[3];
                dic[liTime[nowTimePoint - 1]][9] = x[2];
                dic[liTime[nowTimePoint - 1]][10] = x[1];
                dic[liTime[nowTimePoint - 1]][11] = x[0];

                dic[liTime[nowTimePoint - 1]][12] = x[7];
                dic[liTime[nowTimePoint - 1]][13] = x[6];
                dic[liTime[nowTimePoint - 1]][14] = x[5];
                dic[liTime[nowTimePoint - 1]][15] = x[4];

                dic[liTime[nowTimePoint - 1]][16] = x[11];
                dic[liTime[nowTimePoint - 1]][17] = x[10];
                dic[liTime[nowTimePoint - 1]][18] = x[9];
                dic[liTime[nowTimePoint - 1]][19] = x[8];

                dic[liTime[nowTimePoint - 1]][20] = x[15];
                dic[liTime[nowTimePoint - 1]][21] = x[14];
                dic[liTime[nowTimePoint - 1]][22] = x[13];
                dic[liTime[nowTimePoint - 1]][23] = x[12];
            }
            if (bLeftUp)
            {
                int[] x = new int[16];
                for (int i = 24; i < 40; i++)
                {
                    x[i - 24] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][24] = x[3];
                dic[liTime[nowTimePoint - 1]][25] = x[2];
                dic[liTime[nowTimePoint - 1]][26] = x[1];
                dic[liTime[nowTimePoint - 1]][27] = x[0];

                dic[liTime[nowTimePoint - 1]][28] = x[7];
                dic[liTime[nowTimePoint - 1]][29] = x[6];
                dic[liTime[nowTimePoint - 1]][30] = x[5];
                dic[liTime[nowTimePoint - 1]][31] = x[4];

                dic[liTime[nowTimePoint - 1]][32] = x[11];
                dic[liTime[nowTimePoint - 1]][33] = x[10];
                dic[liTime[nowTimePoint - 1]][34] = x[9];
                dic[liTime[nowTimePoint - 1]][35] = x[8];

                dic[liTime[nowTimePoint - 1]][36] = x[15];
                dic[liTime[nowTimePoint - 1]][37] = x[14];
                dic[liTime[nowTimePoint - 1]][38] = x[13];
                dic[liTime[nowTimePoint - 1]][39] = x[12];
            }
            if (bRightDown)
            {
                int[] x = new int[16];
                for (int i = 40; i < 56; i++)
                {
                    x[i - 40] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][40] = x[3];
                dic[liTime[nowTimePoint - 1]][41] = x[2];
                dic[liTime[nowTimePoint - 1]][42] = x[1];
                dic[liTime[nowTimePoint - 1]][43] = x[0];

                dic[liTime[nowTimePoint - 1]][44] = x[7];
                dic[liTime[nowTimePoint - 1]][45] = x[6];
                dic[liTime[nowTimePoint - 1]][46] = x[5];
                dic[liTime[nowTimePoint - 1]][47] = x[4];

                dic[liTime[nowTimePoint - 1]][48] = x[11];
                dic[liTime[nowTimePoint - 1]][49] = x[10];
                dic[liTime[nowTimePoint - 1]][50] = x[9];
                dic[liTime[nowTimePoint - 1]][51] = x[8];

                dic[liTime[nowTimePoint - 1]][52] = x[15];
                dic[liTime[nowTimePoint - 1]][53] = x[14];
                dic[liTime[nowTimePoint - 1]][54] = x[13];
                dic[liTime[nowTimePoint - 1]][55] = x[12];
            }
            if (bRightUp)
            {
                int[] x = new int[16];
                for (int i = 56; i < 72; i++)
                {
                    x[i - 56] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][56] = x[3];
                dic[liTime[nowTimePoint - 1]][57] = x[2];
                dic[liTime[nowTimePoint - 1]][58] = x[1];
                dic[liTime[nowTimePoint - 1]][59] = x[0];

                dic[liTime[nowTimePoint - 1]][60] = x[7];
                dic[liTime[nowTimePoint - 1]][61] = x[6];
                dic[liTime[nowTimePoint - 1]][62] = x[5];
                dic[liTime[nowTimePoint - 1]][63] = x[4];

                dic[liTime[nowTimePoint - 1]][64] = x[11];
                dic[liTime[nowTimePoint - 1]][65] = x[10];
                dic[liTime[nowTimePoint - 1]][66] = x[9];
                dic[liTime[nowTimePoint - 1]][67] = x[8];

                dic[liTime[nowTimePoint - 1]][68] = x[15];
                dic[liTime[nowTimePoint - 1]][69] = x[14];
                dic[liTime[nowTimePoint - 1]][70] = x[13];
                dic[liTime[nowTimePoint - 1]][71] = x[12];
            }
            LoadFrame();
            #endregion
        }

        private void btnRegionClockwise_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            if (bLeftDown)
            {
                int[] x = new int[16];
                for (int i = 8; i < 24; i++)
                {
                    x[i - 8] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][8] = x[3];
                dic[liTime[nowTimePoint - 1]][9] = x[7];
                dic[liTime[nowTimePoint - 1]][10] = x[11];
                dic[liTime[nowTimePoint - 1]][11] = x[15];

                dic[liTime[nowTimePoint - 1]][12] = x[2];
                dic[liTime[nowTimePoint - 1]][13] = x[6];
                dic[liTime[nowTimePoint - 1]][14] = x[10];
                dic[liTime[nowTimePoint - 1]][15] = x[14];

                dic[liTime[nowTimePoint - 1]][16] = x[1];
                dic[liTime[nowTimePoint - 1]][17] = x[5];
                dic[liTime[nowTimePoint - 1]][18] = x[9];
                dic[liTime[nowTimePoint - 1]][19] = x[13];

                dic[liTime[nowTimePoint - 1]][20] = x[0];
                dic[liTime[nowTimePoint - 1]][21] = x[4];
                dic[liTime[nowTimePoint - 1]][22] = x[8];
                dic[liTime[nowTimePoint - 1]][23] = x[12];
            }
            if (bLeftUp)
            {
                int[] x = new int[16];
                for (int i = 24; i < 40; i++)
                {
                    x[i - 24] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][24] = x[3];
                dic[liTime[nowTimePoint - 1]][25] = x[7];
                dic[liTime[nowTimePoint - 1]][26] = x[11];
                dic[liTime[nowTimePoint - 1]][27] = x[15];

                dic[liTime[nowTimePoint - 1]][28] = x[2];
                dic[liTime[nowTimePoint - 1]][29] = x[6];
                dic[liTime[nowTimePoint - 1]][30] = x[10];
                dic[liTime[nowTimePoint - 1]][31] = x[14];

                dic[liTime[nowTimePoint - 1]][32] = x[1];
                dic[liTime[nowTimePoint - 1]][33] = x[5];
                dic[liTime[nowTimePoint - 1]][34] = x[9];
                dic[liTime[nowTimePoint - 1]][35] = x[13];

                dic[liTime[nowTimePoint - 1]][36] = x[0];
                dic[liTime[nowTimePoint - 1]][37] = x[4];
                dic[liTime[nowTimePoint - 1]][38] = x[8];
                dic[liTime[nowTimePoint - 1]][39] = x[12];
            }
            if (bRightDown)
            {
                int[] x = new int[16];
                for (int i = 40; i < 56; i++)
                {
                    x[i - 40] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][40] = x[3];
                dic[liTime[nowTimePoint - 1]][41] = x[7];
                dic[liTime[nowTimePoint - 1]][42] = x[11];
                dic[liTime[nowTimePoint - 1]][43] = x[15];

                dic[liTime[nowTimePoint - 1]][44] = x[2];
                dic[liTime[nowTimePoint - 1]][45] = x[6];
                dic[liTime[nowTimePoint - 1]][46] = x[10];
                dic[liTime[nowTimePoint - 1]][47] = x[14];

                dic[liTime[nowTimePoint - 1]][48] = x[1];
                dic[liTime[nowTimePoint - 1]][49] = x[5];
                dic[liTime[nowTimePoint - 1]][50] = x[9];
                dic[liTime[nowTimePoint - 1]][51] = x[13];

                dic[liTime[nowTimePoint - 1]][52] = x[0];
                dic[liTime[nowTimePoint - 1]][53] = x[4];
                dic[liTime[nowTimePoint - 1]][54] = x[8];
                dic[liTime[nowTimePoint - 1]][55] = x[12];
            }
            if (bRightUp)
            {
                int[] x = new int[16];
                for (int i = 56; i < 72; i++)
                {
                    x[i - 56] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][56] = x[3];
                dic[liTime[nowTimePoint - 1]][57] = x[7];
                dic[liTime[nowTimePoint - 1]][58] = x[11];
                dic[liTime[nowTimePoint - 1]][59] = x[15];

                dic[liTime[nowTimePoint - 1]][60] = x[2];
                dic[liTime[nowTimePoint - 1]][61] = x[6];
                dic[liTime[nowTimePoint - 1]][62] = x[10];
                dic[liTime[nowTimePoint - 1]][63] = x[14];

                dic[liTime[nowTimePoint - 1]][64] = x[1];
                dic[liTime[nowTimePoint - 1]][65] = x[5];
                dic[liTime[nowTimePoint - 1]][66] = x[9];
                dic[liTime[nowTimePoint - 1]][67] = x[13];

                dic[liTime[nowTimePoint - 1]][68] = x[0];
                dic[liTime[nowTimePoint - 1]][69] = x[4];
                dic[liTime[nowTimePoint - 1]][70] = x[8];
                dic[liTime[nowTimePoint - 1]][71] = x[12];
            }
            LoadFrame();
            #endregion
        }

        private void btnRegionAntiClockwise_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            if (bLeftDown)
            {
                int[] x = new int[16];
                for (int i = 8; i < 24; i++)
                {
                    x[i - 8] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][8] = x[12];
                dic[liTime[nowTimePoint - 1]][9] = x[8];
                dic[liTime[nowTimePoint - 1]][10] = x[4];
                dic[liTime[nowTimePoint - 1]][11] = x[0];

                dic[liTime[nowTimePoint - 1]][12] = x[13];
                dic[liTime[nowTimePoint - 1]][13] = x[9];
                dic[liTime[nowTimePoint - 1]][14] = x[5];
                dic[liTime[nowTimePoint - 1]][15] = x[1];

                dic[liTime[nowTimePoint - 1]][16] = x[14];
                dic[liTime[nowTimePoint - 1]][17] = x[10];
                dic[liTime[nowTimePoint - 1]][18] = x[6];
                dic[liTime[nowTimePoint - 1]][19] = x[2];

                dic[liTime[nowTimePoint - 1]][20] = x[15];
                dic[liTime[nowTimePoint - 1]][21] = x[11];
                dic[liTime[nowTimePoint - 1]][22] = x[7];
                dic[liTime[nowTimePoint - 1]][23] = x[3];
            }
            if (bLeftUp)
            {
                int[] x = new int[16];
                for (int i = 24; i < 40; i++)
                {
                    x[i - 24] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][24] = x[12];
                dic[liTime[nowTimePoint - 1]][25] = x[8];
                dic[liTime[nowTimePoint - 1]][26] = x[4];
                dic[liTime[nowTimePoint - 1]][27] = x[0];

                dic[liTime[nowTimePoint - 1]][28] = x[13];
                dic[liTime[nowTimePoint - 1]][29] = x[9];
                dic[liTime[nowTimePoint - 1]][30] = x[5];
                dic[liTime[nowTimePoint - 1]][31] = x[1];

                dic[liTime[nowTimePoint - 1]][32] = x[14];
                dic[liTime[nowTimePoint - 1]][33] = x[10];
                dic[liTime[nowTimePoint - 1]][34] = x[6];
                dic[liTime[nowTimePoint - 1]][35] = x[2];

                dic[liTime[nowTimePoint - 1]][36] = x[15];
                dic[liTime[nowTimePoint - 1]][37] = x[11];
                dic[liTime[nowTimePoint - 1]][38] = x[7];
                dic[liTime[nowTimePoint - 1]][39] = x[3];
            }
            if (bRightDown)
            {
                int[] x = new int[16];
                for (int i = 40; i < 56; i++)
                {
                    x[i - 40] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][40] = x[12];
                dic[liTime[nowTimePoint - 1]][41] = x[8];
                dic[liTime[nowTimePoint - 1]][42] = x[4];
                dic[liTime[nowTimePoint - 1]][43] = x[0];

                dic[liTime[nowTimePoint - 1]][44] = x[13];
                dic[liTime[nowTimePoint - 1]][45] = x[9];
                dic[liTime[nowTimePoint - 1]][46] = x[5];
                dic[liTime[nowTimePoint - 1]][47] = x[1];

                dic[liTime[nowTimePoint - 1]][48] = x[14];
                dic[liTime[nowTimePoint - 1]][49] = x[10];
                dic[liTime[nowTimePoint - 1]][50] = x[6];
                dic[liTime[nowTimePoint - 1]][51] = x[2];

                dic[liTime[nowTimePoint - 1]][52] = x[15];
                dic[liTime[nowTimePoint - 1]][53] = x[11];
                dic[liTime[nowTimePoint - 1]][54] = x[7];
                dic[liTime[nowTimePoint - 1]][55] = x[3];
            }
            if (bRightUp)
            {
                int[] x = new int[16];
                for (int i = 56; i < 72; i++)
                {
                    x[i - 56] = dic[liTime[nowTimePoint - 1]][i];
                }
                dic[liTime[nowTimePoint - 1]][56] = x[12];
                dic[liTime[nowTimePoint - 1]][57] = x[8];
                dic[liTime[nowTimePoint - 1]][58] = x[4];
                dic[liTime[nowTimePoint - 1]][59] = x[0];

                dic[liTime[nowTimePoint - 1]][60] = x[13];
                dic[liTime[nowTimePoint - 1]][61] = x[9];
                dic[liTime[nowTimePoint - 1]][62] = x[5];
                dic[liTime[nowTimePoint - 1]][63] = x[1];

                dic[liTime[nowTimePoint - 1]][64] = x[14];
                dic[liTime[nowTimePoint - 1]][65] = x[10];
                dic[liTime[nowTimePoint - 1]][66] = x[6];
                dic[liTime[nowTimePoint - 1]][67] = x[2];

                dic[liTime[nowTimePoint - 1]][68] = x[15];
                dic[liTime[nowTimePoint - 1]][69] = x[11];
                dic[liTime[nowTimePoint - 1]][70] = x[7];
                dic[liTime[nowTimePoint - 1]][71] = x[3];
            }
            LoadFrame();
            #endregion
        }

        private void btnRegionCopy_Click(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            CopyRegionDialog rc = new CopyRegionDialog(mw,dic[liTime[nowTimePoint - 1]]);
            if (rc.ShowDialog() == true)
            {
                dic[liTime[nowTimePoint - 1]] = rc.x;
                LoadFrame();
            }
        }
        private void btnRegionClear_Click(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            if (bLeftDown)
            {
                for (int i = 8; i < 24; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                /*
                for (int i = 84; i < 92; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                */
            }
            if (bLeftUp)
            {
                for (int i = 24; i < 40; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                /*
              for (int i = 80; i < 84; i++)
              {
                  dic[liTime[nowTimePoint - 1]][i] = 0;
              }
              for (int i = 0; i < 4; i++)
              {
                  dic[liTime[nowTimePoint - 1]][i] = 0;
              }
              */
            }
            if (bRightDown)
            {
                for (int i = 40; i < 56; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                /*
              for (int i = 76; i < 80; i++)
              {
                  dic[liTime[nowTimePoint - 1]][i] = 0;
              }
              for (int i = 92; i < 96; i++)
              {
                  dic[liTime[nowTimePoint - 1]][i] = 0;
              }
                */
            }
            if (bRightUp)
            {
                for (int i = 56; i < 72; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                /*
                for (int i = 72; i < 76; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                for (int i = 4; i < 8; i++)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                */
            }
            LoadFrame();
        }

        private void btnHorizontalFlipping_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            int[] x = new int[96];
            for (int i = 0; i < 96; i++)
            {
                x[i] = dic[liTime[nowTimePoint - 1]][i];
            }
            dic[liTime[nowTimePoint - 1]][0] = x[88];
            dic[liTime[nowTimePoint - 1]][1] = x[89];
            dic[liTime[nowTimePoint - 1]][2] = x[90];
            dic[liTime[nowTimePoint - 1]][3] = x[91];
            dic[liTime[nowTimePoint - 1]][4] = x[92];
            dic[liTime[nowTimePoint - 1]][5] = x[93];
            dic[liTime[nowTimePoint - 1]][6] = x[94];
            dic[liTime[nowTimePoint - 1]][7] = x[95];

            dic[liTime[nowTimePoint - 1]][8] = x[36];
            dic[liTime[nowTimePoint - 1]][9] = x[37];
            dic[liTime[nowTimePoint - 1]][10] = x[38];
            dic[liTime[nowTimePoint - 1]][11] = x[39];
            dic[liTime[nowTimePoint - 1]][12] = x[32];
            dic[liTime[nowTimePoint - 1]][13] = x[33];
            dic[liTime[nowTimePoint - 1]][14] = x[34];
            dic[liTime[nowTimePoint - 1]][15] = x[35];

            dic[liTime[nowTimePoint - 1]][16] = x[28];
            dic[liTime[nowTimePoint - 1]][17] = x[29];
            dic[liTime[nowTimePoint - 1]][18] = x[30];
            dic[liTime[nowTimePoint - 1]][19] = x[31];
            dic[liTime[nowTimePoint - 1]][20] = x[24];
            dic[liTime[nowTimePoint - 1]][21] = x[25];
            dic[liTime[nowTimePoint - 1]][22] = x[26];
            dic[liTime[nowTimePoint - 1]][23] = x[27];

            dic[liTime[nowTimePoint - 1]][24] = x[20];
            dic[liTime[nowTimePoint - 1]][25] = x[21];
            dic[liTime[nowTimePoint - 1]][26] = x[22];
            dic[liTime[nowTimePoint - 1]][27] = x[23];
            dic[liTime[nowTimePoint - 1]][28] = x[16];
            dic[liTime[nowTimePoint - 1]][29] = x[17];
            dic[liTime[nowTimePoint - 1]][30] = x[18];
            dic[liTime[nowTimePoint - 1]][31] = x[19];

            dic[liTime[nowTimePoint - 1]][32] = x[12];
            dic[liTime[nowTimePoint - 1]][33] = x[13];
            dic[liTime[nowTimePoint - 1]][34] = x[14];
            dic[liTime[nowTimePoint - 1]][35] = x[15];
            dic[liTime[nowTimePoint - 1]][36] = x[8];
            dic[liTime[nowTimePoint - 1]][37] = x[9];
            dic[liTime[nowTimePoint - 1]][38] = x[10];
            dic[liTime[nowTimePoint - 1]][39] = x[11];

            dic[liTime[nowTimePoint - 1]][40] = x[68];
            dic[liTime[nowTimePoint - 1]][41] = x[69];
            dic[liTime[nowTimePoint - 1]][42] = x[70];
            dic[liTime[nowTimePoint - 1]][43] = x[71];
            dic[liTime[nowTimePoint - 1]][44] = x[64];
            dic[liTime[nowTimePoint - 1]][45] = x[65];
            dic[liTime[nowTimePoint - 1]][46] = x[66];
            dic[liTime[nowTimePoint - 1]][47] = x[67];

            dic[liTime[nowTimePoint - 1]][48] = x[60];
            dic[liTime[nowTimePoint - 1]][49] = x[61];
            dic[liTime[nowTimePoint - 1]][50] = x[62];
            dic[liTime[nowTimePoint - 1]][51] = x[63];
            dic[liTime[nowTimePoint - 1]][52] = x[56];
            dic[liTime[nowTimePoint - 1]][53] = x[57];
            dic[liTime[nowTimePoint - 1]][54] = x[58];
            dic[liTime[nowTimePoint - 1]][55] = x[59];

            dic[liTime[nowTimePoint - 1]][56] = x[52];
            dic[liTime[nowTimePoint - 1]][57] = x[53];
            dic[liTime[nowTimePoint - 1]][58] = x[54];
            dic[liTime[nowTimePoint - 1]][59] = x[55];
            dic[liTime[nowTimePoint - 1]][60] = x[48];
            dic[liTime[nowTimePoint - 1]][61] = x[49];
            dic[liTime[nowTimePoint - 1]][62] = x[50];
            dic[liTime[nowTimePoint - 1]][63] = x[51];

            dic[liTime[nowTimePoint - 1]][64] = x[44];
            dic[liTime[nowTimePoint - 1]][65] = x[45];
            dic[liTime[nowTimePoint - 1]][66] = x[46];
            dic[liTime[nowTimePoint - 1]][67] = x[47];
            dic[liTime[nowTimePoint - 1]][68] = x[40];
            dic[liTime[nowTimePoint - 1]][69] = x[41];
            dic[liTime[nowTimePoint - 1]][70] = x[42];
            dic[liTime[nowTimePoint - 1]][71] = x[43];

            dic[liTime[nowTimePoint - 1]][72] = x[79];
            dic[liTime[nowTimePoint - 1]][73] = x[78];
            dic[liTime[nowTimePoint - 1]][74] = x[77];
            dic[liTime[nowTimePoint - 1]][75] = x[76];
            dic[liTime[nowTimePoint - 1]][76] = x[75];
            dic[liTime[nowTimePoint - 1]][77] = x[74];
            dic[liTime[nowTimePoint - 1]][78] = x[73];
            dic[liTime[nowTimePoint - 1]][79] = x[72];

            dic[liTime[nowTimePoint - 1]][80] = x[87];
            dic[liTime[nowTimePoint - 1]][81] = x[86];
            dic[liTime[nowTimePoint - 1]][82] = x[85];
            dic[liTime[nowTimePoint - 1]][83] = x[84];
            dic[liTime[nowTimePoint - 1]][84] = x[83];
            dic[liTime[nowTimePoint - 1]][85] = x[82];
            dic[liTime[nowTimePoint - 1]][86] = x[81];
            dic[liTime[nowTimePoint - 1]][87] = x[80];

            dic[liTime[nowTimePoint - 1]][88] = x[0];
            dic[liTime[nowTimePoint - 1]][89] = x[1];
            dic[liTime[nowTimePoint - 1]][90] = x[2];
            dic[liTime[nowTimePoint - 1]][91] = x[3];
            dic[liTime[nowTimePoint - 1]][92] = x[4];
            dic[liTime[nowTimePoint - 1]][93] = x[5];
            dic[liTime[nowTimePoint - 1]][94] = x[6];
            dic[liTime[nowTimePoint - 1]][95] = x[7];

            LoadFrame();
            #endregion
        }

        private void btnVerticalFlipping_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            int[] x = new int[96];
            for (int i = 0; i < 96; i++)
            {
                x[i] = dic[liTime[nowTimePoint - 1]][i];
            }
            dic[liTime[nowTimePoint - 1]][0] = x[7];
            dic[liTime[nowTimePoint - 1]][1] = x[6];
            dic[liTime[nowTimePoint - 1]][2] = x[5];
            dic[liTime[nowTimePoint - 1]][3] = x[4];
            dic[liTime[nowTimePoint - 1]][4] = x[3];
            dic[liTime[nowTimePoint - 1]][5] = x[2];
            dic[liTime[nowTimePoint - 1]][6] = x[1];
            dic[liTime[nowTimePoint - 1]][7] = x[0];

            dic[liTime[nowTimePoint - 1]][8] = x[43];
            dic[liTime[nowTimePoint - 1]][9] = x[42];
            dic[liTime[nowTimePoint - 1]][10] = x[41];
            dic[liTime[nowTimePoint - 1]][11] = x[40];
            dic[liTime[nowTimePoint - 1]][12] = x[47];
            dic[liTime[nowTimePoint - 1]][13] = x[46];
            dic[liTime[nowTimePoint - 1]][14] = x[45];
            dic[liTime[nowTimePoint - 1]][15] = x[44];

            dic[liTime[nowTimePoint - 1]][16] = x[51];
            dic[liTime[nowTimePoint - 1]][17] = x[50];
            dic[liTime[nowTimePoint - 1]][18] = x[49];
            dic[liTime[nowTimePoint - 1]][19] = x[48];
            dic[liTime[nowTimePoint - 1]][20] = x[55];
            dic[liTime[nowTimePoint - 1]][21] = x[54];
            dic[liTime[nowTimePoint - 1]][22] = x[53];
            dic[liTime[nowTimePoint - 1]][23] = x[52];

            dic[liTime[nowTimePoint - 1]][24] = x[59];
            dic[liTime[nowTimePoint - 1]][25] = x[58];
            dic[liTime[nowTimePoint - 1]][26] = x[57];
            dic[liTime[nowTimePoint - 1]][27] = x[56];
            dic[liTime[nowTimePoint - 1]][28] = x[63];
            dic[liTime[nowTimePoint - 1]][29] = x[62];
            dic[liTime[nowTimePoint - 1]][30] = x[61];
            dic[liTime[nowTimePoint - 1]][31] = x[60];

            dic[liTime[nowTimePoint - 1]][32] = x[67];
            dic[liTime[nowTimePoint - 1]][33] = x[66];
            dic[liTime[nowTimePoint - 1]][34] = x[65];
            dic[liTime[nowTimePoint - 1]][35] = x[64];
            dic[liTime[nowTimePoint - 1]][36] = x[71];
            dic[liTime[nowTimePoint - 1]][37] = x[70];
            dic[liTime[nowTimePoint - 1]][38] = x[69];
            dic[liTime[nowTimePoint - 1]][39] = x[68];

            dic[liTime[nowTimePoint - 1]][40] = x[11];
            dic[liTime[nowTimePoint - 1]][41] = x[10];
            dic[liTime[nowTimePoint - 1]][42] = x[9];
            dic[liTime[nowTimePoint - 1]][43] = x[8];
            dic[liTime[nowTimePoint - 1]][44] = x[15];
            dic[liTime[nowTimePoint - 1]][45] = x[14];
            dic[liTime[nowTimePoint - 1]][46] = x[13];
            dic[liTime[nowTimePoint - 1]][47] = x[12];

            dic[liTime[nowTimePoint - 1]][48] = x[19];
            dic[liTime[nowTimePoint - 1]][49] = x[18];
            dic[liTime[nowTimePoint - 1]][50] = x[17];
            dic[liTime[nowTimePoint - 1]][51] = x[16];
            dic[liTime[nowTimePoint - 1]][52] = x[23];
            dic[liTime[nowTimePoint - 1]][53] = x[22];
            dic[liTime[nowTimePoint - 1]][54] = x[21];
            dic[liTime[nowTimePoint - 1]][55] = x[20];

            dic[liTime[nowTimePoint - 1]][56] = x[27];
            dic[liTime[nowTimePoint - 1]][57] = x[26];
            dic[liTime[nowTimePoint - 1]][58] = x[25];
            dic[liTime[nowTimePoint - 1]][59] = x[24];
            dic[liTime[nowTimePoint - 1]][60] = x[31];
            dic[liTime[nowTimePoint - 1]][61] = x[30];
            dic[liTime[nowTimePoint - 1]][62] = x[29];
            dic[liTime[nowTimePoint - 1]][63] = x[28];

            dic[liTime[nowTimePoint - 1]][64] = x[35];
            dic[liTime[nowTimePoint - 1]][65] = x[34];
            dic[liTime[nowTimePoint - 1]][66] = x[33];
            dic[liTime[nowTimePoint - 1]][67] = x[32];
            dic[liTime[nowTimePoint - 1]][68] = x[39];
            dic[liTime[nowTimePoint - 1]][69] = x[38];
            dic[liTime[nowTimePoint - 1]][70] = x[37];
            dic[liTime[nowTimePoint - 1]][71] = x[36];

            dic[liTime[nowTimePoint - 1]][72] = x[80];
            dic[liTime[nowTimePoint - 1]][73] = x[81];
            dic[liTime[nowTimePoint - 1]][74] = x[82];
            dic[liTime[nowTimePoint - 1]][75] = x[83];
            dic[liTime[nowTimePoint - 1]][76] = x[84];
            dic[liTime[nowTimePoint - 1]][77] = x[85];
            dic[liTime[nowTimePoint - 1]][78] = x[86];
            dic[liTime[nowTimePoint - 1]][79] = x[87];

            dic[liTime[nowTimePoint - 1]][80] = x[72];
            dic[liTime[nowTimePoint - 1]][81] = x[73];
            dic[liTime[nowTimePoint - 1]][82] = x[74];
            dic[liTime[nowTimePoint - 1]][83] = x[75];
            dic[liTime[nowTimePoint - 1]][84] = x[76];
            dic[liTime[nowTimePoint - 1]][85] = x[77];
            dic[liTime[nowTimePoint - 1]][86] = x[78];
            dic[liTime[nowTimePoint - 1]][87] = x[79];

            dic[liTime[nowTimePoint - 1]][88] = x[95];
            dic[liTime[nowTimePoint - 1]][89] = x[94];
            dic[liTime[nowTimePoint - 1]][90] = x[93];
            dic[liTime[nowTimePoint - 1]][91] = x[92];
            dic[liTime[nowTimePoint - 1]][92] = x[91];
            dic[liTime[nowTimePoint - 1]][93] = x[90];
            dic[liTime[nowTimePoint - 1]][94] = x[89];
            dic[liTime[nowTimePoint - 1]][95] = x[88];

            LoadFrame();
            #endregion
        }

        private void btnClockwise_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            int[] x = new int[96];
            for (int i = 0; i < 96; i++)
            {
                x[i] = dic[liTime[nowTimePoint - 1]][i];
            }
            dic[liTime[nowTimePoint - 1]][0] = x[87];
            dic[liTime[nowTimePoint - 1]][1] = x[86];
            dic[liTime[nowTimePoint - 1]][2] = x[85];
            dic[liTime[nowTimePoint - 1]][3] = x[84];
            dic[liTime[nowTimePoint - 1]][4] = x[83];
            dic[liTime[nowTimePoint - 1]][5] = x[82];
            dic[liTime[nowTimePoint - 1]][6] = x[81];
            dic[liTime[nowTimePoint - 1]][7] = x[80];

            dic[liTime[nowTimePoint - 1]][8] = x[43];
            dic[liTime[nowTimePoint - 1]][9] = x[47];
            dic[liTime[nowTimePoint - 1]][10] = x[51];
            dic[liTime[nowTimePoint - 1]][11] = x[55];
            dic[liTime[nowTimePoint - 1]][12] = x[42];
            dic[liTime[nowTimePoint - 1]][13] = x[46];
            dic[liTime[nowTimePoint - 1]][14] = x[50];
            dic[liTime[nowTimePoint - 1]][15] = x[54];

            dic[liTime[nowTimePoint - 1]][16] = x[41];
            dic[liTime[nowTimePoint - 1]][17] = x[45];
            dic[liTime[nowTimePoint - 1]][18] = x[49];
            dic[liTime[nowTimePoint - 1]][19] = x[53];
            dic[liTime[nowTimePoint - 1]][20] = x[40];
            dic[liTime[nowTimePoint - 1]][21] = x[44];
            dic[liTime[nowTimePoint - 1]][22] = x[48];
            dic[liTime[nowTimePoint - 1]][23] = x[52];

            dic[liTime[nowTimePoint - 1]][24] = x[11];
            dic[liTime[nowTimePoint - 1]][25] = x[15];
            dic[liTime[nowTimePoint - 1]][26] = x[19];
            dic[liTime[nowTimePoint - 1]][27] = x[23];
            dic[liTime[nowTimePoint - 1]][28] = x[10];
            dic[liTime[nowTimePoint - 1]][29] = x[14];
            dic[liTime[nowTimePoint - 1]][30] = x[18];
            dic[liTime[nowTimePoint - 1]][31] = x[22];

            dic[liTime[nowTimePoint - 1]][32] = x[9];
            dic[liTime[nowTimePoint - 1]][33] = x[13];
            dic[liTime[nowTimePoint - 1]][34] = x[17];
            dic[liTime[nowTimePoint - 1]][35] = x[21];
            dic[liTime[nowTimePoint - 1]][36] = x[8];
            dic[liTime[nowTimePoint - 1]][37] = x[12];
            dic[liTime[nowTimePoint - 1]][38] = x[16];
            dic[liTime[nowTimePoint - 1]][39] = x[20];

            dic[liTime[nowTimePoint - 1]][40] = x[59];
            dic[liTime[nowTimePoint - 1]][41] = x[63];
            dic[liTime[nowTimePoint - 1]][42] = x[67];
            dic[liTime[nowTimePoint - 1]][43] = x[71];
            dic[liTime[nowTimePoint - 1]][44] = x[58];
            dic[liTime[nowTimePoint - 1]][45] = x[62];
            dic[liTime[nowTimePoint - 1]][46] = x[66];
            dic[liTime[nowTimePoint - 1]][47] = x[70];

            dic[liTime[nowTimePoint - 1]][48] = x[57];
            dic[liTime[nowTimePoint - 1]][49] = x[61];
            dic[liTime[nowTimePoint - 1]][50] = x[65];
            dic[liTime[nowTimePoint - 1]][51] = x[69];
            dic[liTime[nowTimePoint - 1]][52] = x[56];
            dic[liTime[nowTimePoint - 1]][53] = x[60];
            dic[liTime[nowTimePoint - 1]][54] = x[64];
            dic[liTime[nowTimePoint - 1]][55] = x[68];

            dic[liTime[nowTimePoint - 1]][56] = x[27];
            dic[liTime[nowTimePoint - 1]][57] = x[31];
            dic[liTime[nowTimePoint - 1]][58] = x[35];
            dic[liTime[nowTimePoint - 1]][59] = x[39];
            dic[liTime[nowTimePoint - 1]][60] = x[26];
            dic[liTime[nowTimePoint - 1]][61] = x[30];
            dic[liTime[nowTimePoint - 1]][62] = x[34];
            dic[liTime[nowTimePoint - 1]][63] = x[38];

            dic[liTime[nowTimePoint - 1]][64] = x[25];
            dic[liTime[nowTimePoint - 1]][65] = x[29];
            dic[liTime[nowTimePoint - 1]][66] = x[33];
            dic[liTime[nowTimePoint - 1]][67] = x[37];
            dic[liTime[nowTimePoint - 1]][68] = x[24];
            dic[liTime[nowTimePoint - 1]][69] = x[28];
            dic[liTime[nowTimePoint - 1]][70] = x[32];
            dic[liTime[nowTimePoint - 1]][71] = x[36];

            dic[liTime[nowTimePoint - 1]][72] = x[0];
            dic[liTime[nowTimePoint - 1]][73] = x[1];
            dic[liTime[nowTimePoint - 1]][74] = x[2];
            dic[liTime[nowTimePoint - 1]][75] = x[3];
            dic[liTime[nowTimePoint - 1]][76] = x[4];
            dic[liTime[nowTimePoint - 1]][77] = x[5];
            dic[liTime[nowTimePoint - 1]][78] = x[6];
            dic[liTime[nowTimePoint - 1]][79] = x[7];

            dic[liTime[nowTimePoint - 1]][80] = x[88];
            dic[liTime[nowTimePoint - 1]][81] = x[89];
            dic[liTime[nowTimePoint - 1]][82] = x[90];
            dic[liTime[nowTimePoint - 1]][83] = x[91];
            dic[liTime[nowTimePoint - 1]][84] = x[92];
            dic[liTime[nowTimePoint - 1]][85] = x[93];
            dic[liTime[nowTimePoint - 1]][86] = x[94];
            dic[liTime[nowTimePoint - 1]][87] = x[95];

            dic[liTime[nowTimePoint - 1]][88] = x[79];
            dic[liTime[nowTimePoint - 1]][89] = x[78];
            dic[liTime[nowTimePoint - 1]][90] = x[77];
            dic[liTime[nowTimePoint - 1]][91] = x[76];
            dic[liTime[nowTimePoint - 1]][92] = x[75];
            dic[liTime[nowTimePoint - 1]][93] = x[74];
            dic[liTime[nowTimePoint - 1]][94] = x[73];
            dic[liTime[nowTimePoint - 1]][95] = x[72];

            LoadFrame();
            #endregion
        }

        private void btnAntiClockwise_Click(object sender, RoutedEventArgs e)
        {
            #region
            if (nowTimePoint == 0)
                return;
            int[] x = new int[96];
            for (int i = 0; i < 96; i++)
            {
                x[i] = dic[liTime[nowTimePoint - 1]][i];
            }
            dic[liTime[nowTimePoint - 1]][0] = x[72];
            dic[liTime[nowTimePoint - 1]][1] = x[73];
            dic[liTime[nowTimePoint - 1]][2] = x[74];
            dic[liTime[nowTimePoint - 1]][3] = x[75];
            dic[liTime[nowTimePoint - 1]][4] = x[76];
            dic[liTime[nowTimePoint - 1]][5] = x[77];
            dic[liTime[nowTimePoint - 1]][6] = x[78];
            dic[liTime[nowTimePoint - 1]][7] = x[79];

            dic[liTime[nowTimePoint - 1]][8] = x[36];
            dic[liTime[nowTimePoint - 1]][9] = x[32];
            dic[liTime[nowTimePoint - 1]][10] = x[28];
            dic[liTime[nowTimePoint - 1]][11] = x[24];
            dic[liTime[nowTimePoint - 1]][12] = x[37];
            dic[liTime[nowTimePoint - 1]][13] = x[33];
            dic[liTime[nowTimePoint - 1]][14] = x[29];
            dic[liTime[nowTimePoint - 1]][15] = x[25];

            dic[liTime[nowTimePoint - 1]][16] = x[38];
            dic[liTime[nowTimePoint - 1]][17] = x[34];
            dic[liTime[nowTimePoint - 1]][18] = x[30];
            dic[liTime[nowTimePoint - 1]][19] = x[26];
            dic[liTime[nowTimePoint - 1]][20] = x[39];
            dic[liTime[nowTimePoint - 1]][21] = x[35];
            dic[liTime[nowTimePoint - 1]][22] = x[31];
            dic[liTime[nowTimePoint - 1]][23] = x[27];

            dic[liTime[nowTimePoint - 1]][24] = x[68];
            dic[liTime[nowTimePoint - 1]][25] = x[64];
            dic[liTime[nowTimePoint - 1]][26] = x[60];
            dic[liTime[nowTimePoint - 1]][27] = x[56];
            dic[liTime[nowTimePoint - 1]][28] = x[69];
            dic[liTime[nowTimePoint - 1]][29] = x[65];
            dic[liTime[nowTimePoint - 1]][30] = x[61];
            dic[liTime[nowTimePoint - 1]][31] = x[57];

            dic[liTime[nowTimePoint - 1]][32] = x[70];
            dic[liTime[nowTimePoint - 1]][33] = x[66];
            dic[liTime[nowTimePoint - 1]][34] = x[62];
            dic[liTime[nowTimePoint - 1]][35] = x[58];
            dic[liTime[nowTimePoint - 1]][36] = x[71];
            dic[liTime[nowTimePoint - 1]][37] = x[67];
            dic[liTime[nowTimePoint - 1]][38] = x[63];
            dic[liTime[nowTimePoint - 1]][39] = x[59];

            dic[liTime[nowTimePoint - 1]][40] = x[20];
            dic[liTime[nowTimePoint - 1]][41] = x[16];
            dic[liTime[nowTimePoint - 1]][42] = x[12];
            dic[liTime[nowTimePoint - 1]][43] = x[8];
            dic[liTime[nowTimePoint - 1]][44] = x[21];
            dic[liTime[nowTimePoint - 1]][45] = x[17];
            dic[liTime[nowTimePoint - 1]][46] = x[13];
            dic[liTime[nowTimePoint - 1]][47] = x[9];

            dic[liTime[nowTimePoint - 1]][48] = x[22];
            dic[liTime[nowTimePoint - 1]][49] = x[18];
            dic[liTime[nowTimePoint - 1]][50] = x[14];
            dic[liTime[nowTimePoint - 1]][51] = x[10];
            dic[liTime[nowTimePoint - 1]][52] = x[23];
            dic[liTime[nowTimePoint - 1]][53] = x[19];
            dic[liTime[nowTimePoint - 1]][54] = x[15];
            dic[liTime[nowTimePoint - 1]][55] = x[11];

            dic[liTime[nowTimePoint - 1]][56] = x[52];
            dic[liTime[nowTimePoint - 1]][57] = x[48];
            dic[liTime[nowTimePoint - 1]][58] = x[44];
            dic[liTime[nowTimePoint - 1]][59] = x[40];
            dic[liTime[nowTimePoint - 1]][60] = x[53];
            dic[liTime[nowTimePoint - 1]][61] = x[49];
            dic[liTime[nowTimePoint - 1]][62] = x[45];
            dic[liTime[nowTimePoint - 1]][63] = x[41];

            dic[liTime[nowTimePoint - 1]][64] = x[54];
            dic[liTime[nowTimePoint - 1]][65] = x[50];
            dic[liTime[nowTimePoint - 1]][66] = x[46];
            dic[liTime[nowTimePoint - 1]][67] = x[42];
            dic[liTime[nowTimePoint - 1]][68] = x[55];
            dic[liTime[nowTimePoint - 1]][69] = x[51];
            dic[liTime[nowTimePoint - 1]][70] = x[47];
            dic[liTime[nowTimePoint - 1]][71] = x[43];

            dic[liTime[nowTimePoint - 1]][72] = x[95];
            dic[liTime[nowTimePoint - 1]][73] = x[94];
            dic[liTime[nowTimePoint - 1]][74] = x[93];
            dic[liTime[nowTimePoint - 1]][75] = x[92];
            dic[liTime[nowTimePoint - 1]][76] = x[91];
            dic[liTime[nowTimePoint - 1]][77] = x[90];
            dic[liTime[nowTimePoint - 1]][78] = x[89];
            dic[liTime[nowTimePoint - 1]][79] = x[88];

            dic[liTime[nowTimePoint - 1]][80] = x[7];
            dic[liTime[nowTimePoint - 1]][81] = x[6];
            dic[liTime[nowTimePoint - 1]][82] = x[5];
            dic[liTime[nowTimePoint - 1]][83] = x[4];
            dic[liTime[nowTimePoint - 1]][84] = x[3];
            dic[liTime[nowTimePoint - 1]][85] = x[2];
            dic[liTime[nowTimePoint - 1]][86] = x[1];
            dic[liTime[nowTimePoint - 1]][87] = x[0];

            dic[liTime[nowTimePoint - 1]][88] = x[80];
            dic[liTime[nowTimePoint - 1]][89] = x[81];
            dic[liTime[nowTimePoint - 1]][90] = x[82];
            dic[liTime[nowTimePoint - 1]][91] = x[83];
            dic[liTime[nowTimePoint - 1]][92] = x[84];
            dic[liTime[nowTimePoint - 1]][93] = x[85];
            dic[liTime[nowTimePoint - 1]][94] = x[86];
            dic[liTime[nowTimePoint - 1]][95] = x[87];

            LoadFrame();
            #endregion
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            GetNumberDialog c = new GetNumberDialog(mw, "CopyToFrameNumberColon", false, liTime, true);
            if (c.ShowDialog() == true)
            {
                int[] x = new int[96];
                for (int i = 0; i < dic[liTime[nowTimePoint - 1]].Count(); i++)
                {
                    x[i] = dic[liTime[nowTimePoint - 1]][i];
                }
                //如果已经有该时间点，替换
                if (liTime.Contains(c.OneNumber))
                {
                    dic[c.OneNumber] = x;
                    for (int i = 0; i < liTime.Count; i++)
                    {
                        if (liTime[i] == c.OneNumber)
                        {
                            nowTimePoint = i + 1;
                            tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                            tbTimePointCountLeft.Text = nowTimePoint.ToString();
                            tbTimePointCount.Text = " / " + liTime.Count;
                            LoadFrame();
                            break;
                        }
                    }
                }
                else
                {
                    InsertTimePoint(c.OneNumber, x);
                }
                #region
                /*
                //如果没有该时间点，新建
                else {
                    //如果比最大的小，比较大小插入合适的位置
                    if (liTime[liTime.Count - 1] > c.time)
                    {
                        for (int i = 0; i < liTime.Count; i++)
                        {
                            if (liTime[i] > c.time)
                            {
                                 liTime.Insert(i, c.time);
                                dic.Add(c.time, x);
                                nowTimePoint = i + 1;
                                tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                                tbTimePointCount.Text = nowTimePoint + " / " + liTime.Count;
                                LoadFrame();
                                break;
                            }
                        }
                    }
                    //比最大的大，插入最后
                    else
                    {
                        liTime.Add(c.time);             
                        dic.Add(c.time, x);
                        nowTimePoint = liTime.Count;
                        tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
                        tbTimePointCount.Text = nowTimePoint + " / " + liTime.Count;
                        LoadFrame();
                    }
                }
                */
                #endregion
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            int[] x = new int[96];
            for (int i = 0; i < 96; i++)
            {
                x[i] = 0;
            }
            dic[liTime[nowTimePoint - 1]] = x;
            LoadFrame();
        }

        private void btnInsertStartTimePoint_Click(object sender, RoutedEventArgs e)
        {
            //如果已经有该时间点，报错
            if (liTime.Contains(0))
            {
                new MessageDialog(mw, "TheFrameHasATimeNode").ShowDialog();
            }
            else
            {
                int[] x = new int[96];
                for (int i = 0; i < 96; i++)
                {
                    x[i] = 0;
                }
                InsertTimePoint(0, x);
            }
        }

        private void btnInsertDiyTimePoint_Click(object sender, RoutedEventArgs e)
        {
            String str = tbInsertDiyTimePoint.Text.Trim();
            if (str.Trim().Equals(""))
            {
                return;
            }
            int time = 0;
            try
            {
                if (str.Contains("+"))
                {
                    //当前时间 +
                    time = int.Parse(tbTimeNow.Text) + int.Parse(str.Substring(1));
                }
                else if (str.Contains("-"))
                {
                    //当前时间 -
                    time = int.Parse(tbTimeNow.Text) - int.Parse(str.Substring(1));
                }
                else
                {
                    //当前时间
                    time = int.Parse(str);
                }

                if (time < 0)
                {
                    new MessageDialog(mw, "TheInputFormatIsIncorrect").ShowDialog();
                    return;
                }

            }
            catch
            {
                new MessageDialog(mw, "TheInputFormatIsIncorrect").ShowDialog();
                return;
            }
            //如果已经有该时间点，报错
            if (liTime.Contains(time))
            {
                new MessageDialog(mw, "TheFrameHasATimeNode").ShowDialog();
            }
            else
            {
                int[] x = new int[96];
                for (int i = 0; i < 96; i++)
                {
                    x[i] = 0;
                }
                InsertTimePoint(time, x);
            }
        }

        private void tbTimePointCountLeft_TextChanged(object sender, TextChangedEventArgs e)
        {
            try  {
                ClearFrame();
                if (liTime.Count == 0)
                    return;
                int  position = Convert.ToInt32(tbTimePointCountLeft.Text);
                if (liTime.Count == 0)
                {
                    nowTimePoint = 0;
                    tbTimePointCountLeft.Text = "0";
                }
                else
                {
                    nowTimePoint = position;
                    LoadFrame();
                }
                LoadFrame();
            }
            catch {
                if (liTime.Count == 0)
                {
                    nowTimePoint = 0;
                    tbTimePointCountLeft.Text = "0";
                }
                else {
                    nowTimePoint = 1;
                    tbTimePointCountLeft.Text = "1";
                    LoadFrame();
                }
            }
        }

        public bool IsCanDraw()
        {
            return nowTimePoint != 0;
        }
    }
}
