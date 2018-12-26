using Maker.Business;
using Maker.Model;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.UI.Tool.Paved;
using Maker.View.Utils;
using Maker.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace Maker.View.LightUserControl
{
    /// <summary>
    /// FrameUserControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FrameUserControl : BaseLightUserControl, ICanDraw
    {
        public FrameUserControl(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            mainView = gMain;
            HideControl();
            selectView = bDraw;

            mLaunchpad.SetSize(600);
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(46, 48, 51)));

            //初始化贴膜
            mLaunchpad.AddMembrane();
            mLaunchpad.ShowOrHideMembrane();
            //初始化事件
            InitLaunchpadEvent();
            //初始化绘制事件
            mLaunchpad.SetCanDraw(true);

            completeColorPanel.SetSelectionChangedEvent(lbColor_SelectionChanged);

            InitPosition();

            

        }
        private List<int> leftDown, leftUp, rightDown, rightUp;
        private void InitPosition()
        {
            leftDown = new List<int>();
            for (int i = 8; i < 24; i++)
            {
                leftDown.Add(i);
            }
            leftUp = new List<int>();
            for (int i = 24; i < 40; i++)
            {
                leftUp.Add(i);
            }
            rightDown = new List<int>();
            for (int i = 40; i < 56; i++)
            {
                rightDown.Add(i);
            }
            rightUp = new List<int>();
            for (int i = 56; i < 72; i++)
            {
                rightUp.Add(i);
            }
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
            if (IsCanDraw())
            {
                if (LeftOrRight == 0)
                {
                    int i = mLaunchpad.GetNumber(sender as Shape);
                    if (i < 96)
                    {
                        dic[liTime[nowTimePoint - 1]][i] = nowColor;
                    }
                    else
                    {
                        dic[liTime[nowTimePoint - 1]][i - 96] = nowColor;
                    }
                }
                else if (LeftOrRight == 1)
                {
                    int i = mLaunchpad.GetNumber(sender as Shape);
                    if (i < 96)
                    {
                        dic[liTime[nowTimePoint - 1]][i] = 0;
                    }
                    else
                    {
                        dic[liTime[nowTimePoint - 1]][i - 96] = 0;
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
            if (IsCanDraw())
            {
                int i = mLaunchpad.GetNumber(sender as Shape);
                if (i < 96)
                {
                    dic[liTime[nowTimePoint - 1]][i] = nowColor;
                }
                else
                {
                    dic[liTime[nowTimePoint - 1]][i - 96] = nowColor;
                }
                LeftOrRight = 0;
            }
        }
        private void ClearColor(object sender, RoutedEventArgs e)
        {
            if (IsCanDraw())
            {
                int i = mLaunchpad.GetNumber(sender as Shape);
                if (i < 96)
                {
                    dic[liTime[nowTimePoint - 1]][i] = 0;
                }
                else
                {
                    dic[liTime[nowTimePoint - 1]][i - 96] = 0;
                }
                LeftOrRight = 1;
            }
        }
        public List<int> liTime {
            set
            {
                (DataContext as FrameUserControlViewModel).Welcome.LiTime = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Welcome.LiTime;
            }
        }
        public Dictionary<int, int[]> dic {
            set
            {
                (DataContext as FrameUserControlViewModel).Welcome.NowData = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Welcome.NowData;
            }
        }
        //private List<FrameworkElement> lfe = new List<FrameworkElement>();
        private List<String> ColorList = new List<string>();
        public int nowTimePoint {
            set {
                (DataContext as FrameUserControlViewModel).Welcome.NowTimePoint = value;
            }
            get {
                return (DataContext as FrameUserControlViewModel).Welcome.NowTimePoint;
            }
        }
        public int allTimePoint
        {
            set
            {
                (DataContext as FrameUserControlViewModel).Welcome.AllTimePoint = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Welcome.AllTimePoint;
            }
        }
        private int mouseType = 0;//0没按下 1按下
        private int nowColor = 5;//当前颜色

        /// <summary>
        /// 获取主窗口数据
        /// </summary>
        public override void SetData(List<Light> mActionBeanList)
        {
            ClearFrame();
            liTime = LightBusiness.GetTimeList(mActionBeanList);
            dic = LightBusiness.GetParagraphLightIntList(mActionBeanList);
            allTimePoint = liTime.Count;

            if (liTime.Count == 0)
            {
                //tbTimeNow.Text = "0";
                nowTimePoint = 0;
                //tbTimePointCountLeft.Text = "0";
                //tbTimePointCount.Text = 0.ToString();
            }
            else
            {
                //tbTimeNow.Text = liTime[0].ToString();
                nowTimePoint = 1;
                //tbTimePointCountLeft.Text = nowTimePoint.ToString();
                //tbTimePointCount.Text = " / " + liTime.Count;
                //LoadFrame();
            }
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <returns>ActionBean集合</returns>
        public override List<Light> GetData()
        {
            List<Light> mActionBeanList = new List<Light>();
            Boolean[] b = new Boolean[96];
            for (int i = 0; i < 96; i++)
            {
                b[i] = false;
            }
            for (int i = 0; i < liTime.Count; i++)
            {
                for (int j = 0; j < 96; j++)
                {
                    if (dic[liTime[i]][j] == 0 || dic[liTime[i]][j] == -1)
                    {
                        if (b[j])
                        {
                            mActionBeanList.Add(new Light(liTime[i], 128, j + 28, 64));
                            b[j] = false;
                        }
                    }
                    if (dic[liTime[i]][j] != 0 && dic[liTime[i]][j] != -1)
                    {
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

        public void LoadFrame()
        {
            if (nowTimePoint == 0)
                return;
            ClearFrame();

            List<Light> mLightList = new List<Light>();

            int[] x = dic[liTime[nowTimePoint - 1]];

            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i] == 0 || x[i] == -1)
                {
                    continue;
                }
                mLightList.Add(new Light(0,144, i+28, x[i]));
            }
            (DataContext as FrameUserControlViewModel).Welcome.NowLightLight = mLightList;
            LoadNowText();
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
        

        private Point point = new Point();
        private Rectangle rectangle = new Rectangle();
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseType = 1;
            if (e.ChangedButton == MouseButton.Left)
            {
                LeftOrRight = 0;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                LeftOrRight = 1;
            }
            point = e.GetPosition(mLaunchpad);
        }

        List<int> selects = new List<int>();
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseType = 0;
            RemoveSelectRectangle();
            if (tcLeft.SelectedIndex == 1)
            {
                SelectPosition(mLaunchpad.GetSelectPosition(point, e.GetPosition(mLaunchpad)));
            }
        }

        private void mLaunchpad_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseType == 1 && tcLeft.SelectedIndex == 1)
            {
                if (!mLaunchpad.Children.Contains(rectangle))
                {
                    rectangle = new Rectangle()
                    {
                        Width = Math.Abs(e.GetPosition(mLaunchpad).X - point.X),
                        Height = Math.Abs(e.GetPosition(mLaunchpad).Y - point.Y),
                        StrokeThickness = 2,
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeDashArray = new DoubleCollection(new List<Double> { 2, 2 }),
                    };
                    mLaunchpad.Children.Add(rectangle);
                    Canvas.SetLeft(rectangle, Math.Min(e.GetPosition(mLaunchpad).X, point.X));
                    Canvas.SetTop(rectangle, Math.Min(e.GetPosition(mLaunchpad).Y, point.Y));
                }
                else
                {
                    Canvas.SetLeft(rectangle, Math.Min(e.GetPosition(mLaunchpad).X, point.X));
                    Canvas.SetTop(rectangle, Math.Min(e.GetPosition(mLaunchpad).Y, point.Y));
                    rectangle.Width = Math.Abs(e.GetPosition(mLaunchpad).X - point.X);
                    rectangle.Height = Math.Abs(e.GetPosition(mLaunchpad).Y - point.Y);
                }
            }
        }
        private void lbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nowColor = completeColorPanel.NowColor;
            mLaunchpad.SetNowBrush(StaticConstant.brushList[completeColorPanel.NowColor]);
            LoadNowText();
        }

        private void btnRegionCopy_Click(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            CopyRegionDialog rc = new CopyRegionDialog(mw, dic[liTime[nowTimePoint - 1]]);
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
            for (int i = 0; i < selects.Count; i++)
            {
                dic[liTime[nowTimePoint - 1]][selects[i]] = 0;
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

        public bool IsCanDraw()
        {
            return mouseType == 1 && nowTimePoint != 0 && tcLeft.SelectedIndex == 0;
        }

        private ControlType nowControlType = ControlType.Draw;
        private Border selectView;
        private enum ControlType
        {
            Draw = 0,
            Select = 1,
            Picture = 2,
            Fire = 3
        }

        private LinearGradientBrush selectBrush = new LinearGradientBrush
        {
            StartPoint = new Point(0.5, 0),
            EndPoint = new Point(0.5, 1),
            GradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(94, 106, 134), 0),
                        new GradientStop(Color.FromRgb(64, 77, 108), 1)
                    }
        };
        private LinearGradientBrush noSelectBrush = new LinearGradientBrush
        {
            StartPoint = new Point(0.5, 0),
            EndPoint = new Point(0.5, 1),
            GradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(236, 241, 244), 0),
                        new GradientStop(Color.FromRgb(236, 241, 244), 0.5),
                       new GradientStop(Color.FromRgb(208, 224, 234), 0.5),
                        new GradientStop(Color.FromRgb(208, 224, 234), 1)
                    }
        };

        private bool isShowPicture = true;
        private void ShowImageControl(object sender, MouseButtonEventArgs e)
        {
            if (isShowPicture)
            {
                spRight.Visibility = Visibility.Collapsed;
                bPicture.Background = noSelectBrush;
                iPicture.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_black.png", UriKind.RelativeOrAbsolute));
            }
            else {
                spRight.Visibility = Visibility.Visible;
                iPicture.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_white.png", UriKind.RelativeOrAbsolute));
                bPicture.Background = selectBrush;
            }
            isShowPicture = !isShowPicture;
        }

        private void ChangeControlType(object sender, MouseButtonEventArgs e)
        {
            selectView.Background = noSelectBrush;
            if (selectView == bDraw)
            {
                iDraw.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/pen_black.png", UriKind.RelativeOrAbsolute));
            }
            else if (selectView == bSelect)
            {
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_black.png", UriKind.RelativeOrAbsolute));
            }
           
            selectView = sender as Border;
            selectView.Background = selectBrush;

            if (sender == bDraw)
            {
                iDraw.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/pen_white.png", UriKind.RelativeOrAbsolute));
                nowControlType = ControlType.Draw;
                tcLeft.SelectedIndex = 0;
            }
            else if (sender == bSelect)
            {
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_white.png", UriKind.RelativeOrAbsolute));
                nowControlType = ControlType.Select;
                tcLeft.SelectedIndex = 1;
            }
          
        }

        private void OpenFileControl(object sender, RoutedEventArgs e)
        {
            popFile.IsOpen = true;
        }

        private void mLaunchpad_MouseEnter(object sender, MouseEventArgs e)
        {
            if (nowTimePoint == 0)
            {
                mLaunchpad.Cursor = Cursors.No;
            }
            else
            {
                if (nowControlType == ControlType.Draw)
                {
                    mLaunchpad.Cursor = Cursors.Pen;
                }
                else if (nowControlType == ControlType.Select)
                {
                    mLaunchpad.Cursor = Cursors.Cross;
                }
                else
                {
                    mLaunchpad.Cursor = Cursors.Arrow;
                }
            }
        }
        private SolidColorBrush popSelectBrush = new SolidColorBrush(Color.FromRgb(0, 255, 255));
        private SolidColorBrush popNoSelectBrush = new SolidColorBrush(Colors.White);
        private void PopSpMouseEnter(object sender, MouseEventArgs e)
        {
            Image img = (sender as StackPanel).Children[0] as Image;

            if (sender == spNewFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/file_blue.png", UriKind.RelativeOrAbsolute));
            }
            else if (sender == spOpenFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/open_blue.png", UriKind.RelativeOrAbsolute));
            }
            else if (sender == spSaveFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/save_blue.png", UriKind.RelativeOrAbsolute));
            }
            else if (sender == spSaveAsFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/saveas_blue.png", UriKind.RelativeOrAbsolute));
            }
            TextBlock tb = (sender as StackPanel).Children[1] as TextBlock;
            tb.Foreground = popSelectBrush;
        }

        private void PopSpMouseLeave(object sender, MouseEventArgs e)
        {
            Image img = (sender as StackPanel).Children[0] as Image;

            if (sender == spNewFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/file_white.png", UriKind.RelativeOrAbsolute));
            }
            else if (sender == spOpenFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/open_white.png", UriKind.RelativeOrAbsolute));
            }
            else if (sender == spSaveFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/save_white.png", UriKind.RelativeOrAbsolute));
            }
            else if (sender == spSaveAsFile)
            {
                img.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/saveas_white.png", UriKind.RelativeOrAbsolute));
            }
            TextBlock tb = (sender as StackPanel).Children[1] as TextBlock;
            tb.Foreground = popNoSelectBrush;
        }

        private void ShowMembrane(object sender, MouseButtonEventArgs e)
        {
            mLaunchpad.ShowOrHideMembrane();
            if (mLaunchpad.isMembrane)
            {
                bShowMembrane.Background = selectBrush;
            }
            else
            {
                bShowMembrane.Background = noSelectBrush;
            }
        }

        private void spSaveFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popFile.IsOpen = false;
            SaveFile();
        }

        private void tcLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tcLeft.SelectedIndex != 1)
                mLaunchpad.ClearSelect();
            if (tcLeft.SelectedIndex == 1)
            {

            }
        }

        private void mLaunchpad_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseType = 0;
            RemoveSelectRectangle();
        }

        private void SelectMove(object sender, RoutedEventArgs e)
        {
            if (sender == btnToLeft)
                SelectMove(ToWhere.toLeft);
            if (sender == btnToRight)
                SelectMove(ToWhere.toRight);
            if (sender == btnToUp)
                SelectMove(ToWhere.toUp);
            if (sender == btnToDown)
                SelectMove(ToWhere.toDown);
        }
        private enum ToWhere
        {
            toLeft = 0,
            toUp = 1,
            toRight = 2,
            toDown = 3
        }

        private void SelectMove(ToWhere toWhere)
        {
            List<int> newSelect = new List<int>();
            List<List<int>> ints;

            ints = new List<List<int>>();

            if (toWhere == ToWhere.toLeft || toWhere == ToWhere.toRight)
                ints.AddRange(Operation.IntCollection.VerticalIntList);
            if (toWhere == ToWhere.toUp || toWhere == ToWhere.toDown)
                ints.AddRange(Operation.IntCollection.HorizontalIntList);

            if (toWhere == ToWhere.toRight)
            {
                ints.Reverse();
                selects.Reverse();
            }
            if (toWhere == ToWhere.toDown)
            {
                ints.Reverse();
            }
            if (toWhere == ToWhere.toUp)
            {
                selects.Reverse();
            }
            for (int i = 0; i < selects.Count; i++)
            {
                for (int j = 0; j < ints.Count; j++)
                {
                    if (ints[j].Contains(selects[i] + 28))
                    {
                        int oldJ = j;
                        int contraryJ = 0;

                        if (oldJ == 0)
                        {
                            j = ints.Count - 1;
                        }
                        else
                        {
                            j -= 1;
                        }

                        if (oldJ == ints.Count - 1)
                        {
                            contraryJ = 0;
                        }
                        else
                        {
                            contraryJ = oldJ + 1;
                        }
                        if (ints[j][ints[oldJ].IndexOf(selects[i] + 28)] < 0)
                            break;

                        dic[liTime[nowTimePoint - 1]][ints[j][ints[oldJ].IndexOf(selects[i] + 28)] - 28] = dic[liTime[nowTimePoint - 1]][selects[i]];
                        newSelect.Add(ints[j][ints[oldJ].IndexOf(selects[i] + 28)] - 28);
                        if (!selects.Contains(ints[contraryJ].IndexOf(selects[i] + 28)))
                        {
                            dic[liTime[nowTimePoint - 1]][selects[i]] = 0;
                        }
                        break;
                    }
                }
            }
            selects.Clear();
            selects.AddRange(newSelect);
            mLaunchpad.SetSelectPosition(selects);
            if (toWhere == ToWhere.toRight)
                selects.Reverse();
            if (toWhere == ToWhere.toUp)
            {
                selects.Reverse();
            }
            LoadFrame();

            mLaunchpad.Focus();
        }

        private void LbText_PrevieKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (lbText.SelectedIndex == -1)
                return;
            TextBlock tb = cMain.Children[lbText.SelectedIndex] as TextBlock;
            if (e.Key == Key.Left)
                Canvas.SetLeft(tb, Canvas.GetLeft(tb) - 1);
            else if (e.Key == Key.Right)
                Canvas.SetLeft(tb, Canvas.GetLeft(tb) + 1);
            else if (e.Key == Key.Up)
                Canvas.SetTop(tb, Canvas.GetTop(tb) - 1);
            else if (e.Key == Key.Down)
                Canvas.SetTop(tb, Canvas.GetTop(tb) + 1);

            points[nowTimePoint].Texts[lbText.SelectedIndex].Point.X = Canvas.GetLeft(tb);
            points[nowTimePoint].Texts[lbText.SelectedIndex].Point.Y = Canvas.GetTop(tb);
        }
        private void LbText_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void BaseLightUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.Key == Key.Left)
                SelectMove(ToWhere.toLeft);
            else if (e.Key == Key.Right)
                SelectMove(ToWhere.toRight);
            else if (e.Key == Key.Up)
                SelectMove(ToWhere.toUp);
            else if (e.Key == Key.Down)
                SelectMove(ToWhere.toDown);
        }
        private void BaseLightUserControl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void SaveCanvas(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "png(*.png)|*.png"
            };
            var rst = dlg.ShowDialog();
            if (rst == true)
            {
                String fileName = dlg.FileName;
                SaveRTBAsPNG(GetBitmapStream(mLaunchpad, 96), fileName);
            }
        }

        private void SaveLongCanvas(object sender, RoutedEventArgs e)
        {
            if (nowTimePoint == 0)
                return;
            new ShowPavedWindow(mw, GetData(), 1,points, StaticConstant.brushList[nowColor]).ShowDialog();
        }


        public RenderTargetBitmap GetBitmapStream(Canvas canvas, int dpi)
        {
            //Size size = new Size(canvas.Width, canvas.Height);
            //canvas.Measure(size);
            //canvas.Arrange(new Rect(size));
            var rtb = new RenderTargetBitmap(
                (int)canvas.Width, //width
                (int)canvas.Height, //height
                dpi, //dpi x
                dpi, //dpi y
                PixelFormats.Pbgra32 // pixelformat
                );
            rtb.Render(canvas);
            return rtb;
        }

        private void SaveRTBAsPNG(RenderTargetBitmap bmp, string filename)
        {
            var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));

            using (var stm = System.IO.File.Create(filename))
            {
                enc.Save(stm);
            }
        }
        private void NewTextFile(object sender, RoutedEventArgs e)
        {
            String _filePath = GetFileDirectory();
            GetStringDialog2 dialog = new GetStringDialog2(mw, ".text", fileBusiness.GetFilesName(mw.lastProjectPath + @"\Text\", new List<string>() { ".text" }), ".text");
            if (dialog.ShowDialog() == true)
            {
                filePath = mw.lastProjectPath + @"\Text\" + dialog.fileName;
                if (File.Exists(filePath))
                {
                    new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                    return;
                }
                else
                {
                    CreateTextFile(filePath);
                    LoadText(dialog.fileName);
                }
            }
        }
        private void NewText(object sender, RoutedEventArgs e)
        {
            GetStringDialog getString = new GetStringDialog(mw, "", "", "");
            if (getString.ShowDialog() == true)
            {
                TextBlock tb = new TextBlock()
                {
                    Text = getString.mString,
                    Foreground = StaticConstant.brushList[nowColor],
                };
                if (points.ContainsKey(nowTimePoint))
                {
                    points[nowTimePoint].Texts.Add(new FramePointModel.Text()
                    {
                        Value = getString.mString,
                        Point = new Point(0, 0)
                    });
                }
                else
                {
                    points.Add(nowTimePoint,
                    new FramePointModel()
                    {
                        Value = nowTimePoint,
                        Texts = new List<FramePointModel.Text>() {
                          new FramePointModel.Text(){
                                Value = getString.mString,
                                Point = new Point(0, 0)
                          }
                        }
                    });

                }
                Canvas.SetLeft(tb, 0);
                Canvas.SetTop(tb, 0);
                cMain.Children.Add(tb);
                lbText.Items.Add(getString.mString);
            }
        }
        private void EditText(object sender, RoutedEventArgs e)
        {
            if (!points.ContainsKey(nowTimePoint))
                return;

            GetStringDialog getString = new GetStringDialog(mw, "", "", "");
            if (getString.ShowDialog() == true)
            {
                TextBlock tb = cMain.Children[lbText.SelectedIndex] as TextBlock;
                tb.Text = getString.mString;
                points[nowTimePoint].Texts[lbText.SelectedIndex].Value = getString.mString;
                lbText.Items[lbText.SelectedIndex] = getString.mString;
            }
        }
        private void DeleteText(object sender, RoutedEventArgs e)
        {
            if (cMain.Children.Count > 1)
            {
                lbText.Items.Clear();
                cMain.Children.RemoveRange(1, cMain.Children.Count - 1);
            }
            if (!points.ContainsKey(nowTimePoint))
                return;

            int selectedIndex = lbText.SelectedIndex;
            cMain.Children.RemoveAt(selectedIndex);
            points[nowTimePoint].Texts.RemoveAt(selectedIndex);
            lbText.Items.RemoveAt(selectedIndex);
        }

        private void CreateTextFile(String filePath)
        {
            //获取对象
            XDocument xDoc = new XDocument();
            // 添加根节点
            XElement xRoot = new XElement("Root");
            xDoc.Add(xRoot);
            // 保存该文档  
            xDoc.Save(filePath);
        }

       public Dictionary<int, FramePointModel> points
        {
            set
            {
                (DataContext as FrameUserControlViewModel).Welcome.Points = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Welcome.Points;
            }
        }
        private void LoadText(String fileName)
        {
            points.Clear();
            XDocument doc = XDocument.Load(fileName);

            foreach (XElement element in doc.Elements("Point"))
            {
                FramePointModel framePointModel = new FramePointModel();
                framePointModel.Value = int.Parse(element.Attribute("value").Value);
                List<FramePointModel.Text> texts = new List<FramePointModel.Text>();

                foreach (XElement _element in element.Elements("Text"))
                {
                    texts.Add(new FramePointModel.Text()
                    {
                        Value = _element.Attribute("value").Value,
                        Point = new Point(Double.Parse(_element.Attribute("x").Value), Double.Parse(_element.Attribute("y").Value))
                    });
                }
                framePointModel.Texts = texts;
                points.Add(int.Parse(element.Attribute("value").Value), framePointModel);
            }

            LoadNowText();
        }

        private void LoadNowText()
        {
            return;
            if (cMain.Children.Count > 1)
            {
                lbText.Items.Clear();
                cMain.Children.RemoveRange(1, cMain.Children.Count - 1);
            }

            if (!points.ContainsKey(nowTimePoint))
                return;
            //加载
            foreach (var item in points[nowTimePoint].Texts)
            {
                lbText.Items.Add(item.Value);
                TextBlock tb = new TextBlock()
                {
                    Text = item.Value,
                    Foreground = StaticConstant.brushList[nowColor],
                };
                Canvas.SetLeft(tb, item.Point.X);
                Canvas.SetTop(tb, item.Point.Y);
                cMain.Children.Add(tb);
            }
        }

        private void LoadTextFile(object sender, RoutedEventArgs e)
        {
            List<String> fileNames = new List<string>();
            FileBusiness business = new FileBusiness();
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\Text", new List<string>() { ".text" }));

            ShowLightListDialog dialog = new ShowLightListDialog(mw, "", fileNames);
            if (dialog.ShowDialog() == true)
            {
                nowTextFilePath = mw.lastProjectPath + @"\Text\" + dialog.selectItem;
                LoadText(nowTextFilePath);
            }

        }

        private String nowTextFilePath = "";
        private void SaveTextFile(object sender, RoutedEventArgs e)
        {
            if (nowTextFilePath.Equals(String.Empty))
            {
                return;
            }
            XDocument doc = new XDocument();
            foreach (FramePointModel framePointModel in points.Values)
            {
                XElement element = new XElement("Point");
                element.SetAttributeValue("value", framePointModel.Value);
                doc.Add(element);
                foreach (FramePointModel.Text text in framePointModel.Texts)
                {
                    XElement _element = new XElement("Text");
                    _element.SetAttributeValue("value", text.Value);
                    _element.SetAttributeValue("x", text.Point.X);
                    _element.SetAttributeValue("y", text.Point.Y);
                    element.Add(_element);
                }
            }
            doc.Save(nowTextFilePath);
        }

        /// <summary>
        /// 移除选择框
        /// </summary>
        private void RemoveSelectRectangle()
        {
            if (mLaunchpad.Children.Contains(rectangle) && tcLeft.SelectedIndex == 1)
            {
                mLaunchpad.Children.Remove(rectangle);
            }
        }

        private void FourAreaClick(object sender, RoutedEventArgs e)
        {
            if (sender == btnLeftDown)
                SelectPosition(leftDown);
            if (sender == btnLeftUp)
                SelectPosition(leftUp);
            if (sender == btnRightDown)
                SelectPosition(rightDown);
            if (sender == btnRightUp)
                SelectPosition(rightUp);
        }

        public void SelectPosition(List<int> positions)
        {
            if (rbSelect.IsChecked == true)
            {
                selects.Clear();
                selects.AddRange(positions);
                mLaunchpad.SetSelectPosition(selects);
            }
            if (rbAdd.IsChecked == true)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (!selects.Contains(positions[i]))
                    {
                        selects.Add(positions[i]);
                    }
                }
                selects.AddRange(positions);
                mLaunchpad.SetSelectPosition(selects);
            }
            if (rbIntersection.IsChecked == true)
            {
                List<int> nowSelects = new List<int>();
                nowSelects.AddRange(selects);
                selects.Clear();
                for (int i = 0; i < positions.Count; i++)
                {
                    if (nowSelects.Contains(positions[i]))
                    {
                        selects.Add(positions[i]);
                    }
                }
                mLaunchpad.SetSelectPosition(selects);
            }
            if (rbComplement.IsChecked == true)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (selects.Contains(positions[i]))
                    {
                        selects.Remove(positions[i]);
                    }
                    else
                    {
                        selects.Add(positions[i]);
                    }
                }
                mLaunchpad.SetSelectPosition(selects);
            }
            mLaunchpad.Focus();
        }
    }
}
