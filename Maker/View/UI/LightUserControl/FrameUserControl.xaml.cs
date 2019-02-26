using Maker.Business;
using Maker.Business.Model;
using Maker.Model;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.UI.Tool.Paved;
using Maker.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

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

            mLaunchpad.SetSize(600);
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(46, 48, 51)));

            //初始化贴膜
            mLaunchpad.AddMembrane();
            mLaunchpad.ShowOrHideMembrane();
            //初始化事件
            InitLaunchpadEvent();
            //初始化绘制事件
            mLaunchpad.SetCanDraw(true);
            //初始化回调函数
            mLaunchpad.SetOnDataChange(OnLaunchpadDataChange);
            completeColorPanel.SetSelectionChangedEvent(lbColor_SelectionChanged);

            InitPosition();

            XmlSerializer serializer = new XmlSerializer(typeof(FrameModel));
            FileStream stream = new FileStream("Config/frame.xml", FileMode.Open);
            dep = (FrameModel)serializer.Deserialize(stream);
            stream.Close();
            Canvas.SetLeft(cMain, dep.style.x);
            Canvas.SetTop(cMain, dep.style.y);

            sliderSize.Value = dep.style.size;
        }

        FrameModel dep;

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
                        Dic[LiTime[NowTimePoint - 1]][i] = nowColor;
                    }
                    else
                    {
                        Dic[LiTime[NowTimePoint - 1]][i - 96] = nowColor;
                    }
                }
                else if (LeftOrRight == 1)
                {
                    int i = mLaunchpad.GetNumber(sender as Shape);
                    if (i < 96)
                    {
                        Dic[LiTime[NowTimePoint - 1]][i] = 0;
                    }
                    else
                    {
                        Dic[LiTime[NowTimePoint - 1]][i - 96] = 0;
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
                    Dic[LiTime[NowTimePoint - 1]][i] = nowColor;
                }
                else
                {
                    Dic[LiTime[NowTimePoint - 1]][i - 96] = nowColor;
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
                    Dic[LiTime[NowTimePoint - 1]][i] = 0;
                }
                else
                {
                    Dic[LiTime[NowTimePoint - 1]][i - 96] = 0;
                }
                LeftOrRight = 1;
            }
        }
        public List<int> LiTime {
            set
            {
                (DataContext as FrameUserControlViewModel).Welcome.LiTime = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Welcome.LiTime;
            }
        }
        public Dictionary<int, int[]> Dic {
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
        public int NowTimePoint {
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
            LiTime = LightBusiness.GetTimeList(mActionBeanList);
            Dic = LightBusiness.GetParagraphLightIntList(mActionBeanList);
            allTimePoint = LiTime.Count;

            if (LiTime.Count == 0)
            {
                NowTimePoint = 0;
            }
            else
            {
                NowTimePoint = 1;
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
            for (int i = 0; i < LiTime.Count; i++)
            {
                for (int j = 0; j < 96; j++)
                {
                    if (Dic[LiTime[i]][j] == 0 || Dic[LiTime[i]][j] == -1)
                    {
                        if (b[j])
                        {
                            mActionBeanList.Add(new Light(LiTime[i], 128, j + 28, 64));
                            b[j] = false;
                        }
                    }
                    if (Dic[LiTime[i]][j] != 0 && Dic[LiTime[i]][j] != -1)
                    {
                        sliderSize.Value = dep.style.size;
                        if (b[j])
                        {
                            mActionBeanList.Add(new Light(LiTime[i], 128, j + 28, 64));
                        }
                        mActionBeanList.Add(new Light(LiTime[i], 144, j + 28, Dic[LiTime[i]][j]));
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
            if (NowTimePoint == 0)
                return;
            ClearFrame();

            List<Light> mLightList = new List<Light>();

            int[] x = Dic[LiTime[NowTimePoint - 1]];

            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i] == 0 || x[i] == -1)
                {
                    continue;
                }
                mLightList.Add(new Light(0,144, i+28, x[i]));
            }
            (DataContext as FrameUserControlViewModel).Welcome.NowLightLight = mLightList;
        }
      
        /// <summary>
        /// 插入时间点
        /// </summary>
        /// <param name="time">插入时间</param>
        /// <param name="shape">插入形状</param>
        private void InsertTimePoint(int time, int[] shape)
        {
            if (LiTime.Count == 0)
            {
                LiTime.Insert(0, time);
                Dic.Add(time, shape);
                NowTimePoint = 1;
                tbTimeNow.Text = LiTime[NowTimePoint - 1].ToString();
                tbTimePointCountLeft.Text = NowTimePoint.ToString();
                tbTimePointCount.Text = " / " + LiTime.Count;
                LoadFrame();
            }
            else
            {
                //如果比最大的小，比较大小插入合适的位置
                if (LiTime[LiTime.Count - 1] > time)
                {
                    for (int i = 0; i < LiTime.Count; i++)
                    {
                        if (LiTime[i] > time)
                        {
                            LiTime.Insert(i, time);
                            Dic.Add(time, shape);
                            NowTimePoint = i + 1;
                            tbTimeNow.Text = LiTime[NowTimePoint - 1].ToString();
                            tbTimePointCountLeft.Text = NowTimePoint.ToString();
                            tbTimePointCount.Text = " / " + LiTime.Count;
                            LoadFrame();
                            break;
                        }
                    }
                }
                //比最大的大，插入最后
                else
                {
                    LiTime.Add(time);
                    Dic.Add(time, shape);
                    NowTimePoint = LiTime.Count;
                    tbTimeNow.Text = LiTime[NowTimePoint - 1].ToString();
                    tbTimePointCountLeft.Text = NowTimePoint.ToString();
                    tbTimePointCount.Text = " / " + LiTime.Count;
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


        public List<int> selects
        {
            set
            {
                (DataContext as FrameUserControlViewModel).Welcome.Selects = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Welcome.Selects;
            }
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseType = 0;
            RemoveSelectRectangle();
            if (nowControlType == ControlType.Select)
            {
                SelectPosition(mLaunchpad.GetSelectPosition(point, e.GetPosition(mLaunchpad)));
            }
        }

        private void mLaunchpad_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseType == 1 && nowControlType == ControlType.Select)
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

            cText.NowColorNum = nowColor;
            popColor.IsOpen = false;

            mLaunchpad.SetNowBrush(StaticConstant.brushList[completeColorPanel.NowColor]);
            cColor.Background = StaticConstant.brushList[completeColorPanel.NowColor];
            if (nowControlType == ControlType.Select && nowTimePointValidationRule.IsCanDraw) {
                for (int i = 0; i < selects.Count; i++)
                {
                    Dic[LiTime[NowTimePoint - 1]][selects[i]] = completeColorPanel.NowColor;
                    LoadFrame();
                }
            }
        }

        private void btnRegionCopy_Click(object sender, RoutedEventArgs e)
        {
            if (NowTimePoint == 0)
                return;
            CopyRegionDialog rc = new CopyRegionDialog(mw, Dic[LiTime[NowTimePoint - 1]]);
            if (rc.ShowDialog() == true)
            {
                Dic[LiTime[NowTimePoint - 1]] = rc.x;
                LoadFrame();
            }
        }
        private void btnRegionClear_Click(object sender, RoutedEventArgs e)
        {
            if (NowTimePoint == 0)
                return;
            for (int i = 0; i < selects.Count; i++)
            {
                Dic[LiTime[NowTimePoint - 1]][selects[i]] = 0;
            }
            LoadFrame();
        }

        public void OperationControl(object sender, RoutedEventArgs e)
        {
            if (NowTimePoint == 0)
                return;
            Operation.LightGroup operationLightGroup = new Operation.LightGroup();
            for (int i = 0; i < 96; i++)
            {
                int color = Dic[LiTime[NowTimePoint - 1]][i];
                if (color != 0)
                {
                    operationLightGroup.Add(new Operation.Light(0, 144, i + 28, color));
                }
            }
            if (sender == miHorizontalFlipping) {
                operationLightGroup.HorizontalFlipping();
            }
            else if (sender == miHorizontalFlipping)
            {
                operationLightGroup.VerticalFlipping();
            }
            else if (sender == miClockwiseRotation)
            {
                operationLightGroup.Clockwise();
            }
            else if (sender == miClockwiseRotation)
            {
                operationLightGroup.Clockwise();
            }
            else if (sender == miAntiClockwiseRotation)
            {
                operationLightGroup.AntiClockwise();
            }

            for (int i = 0; i < 96; i++)
            {
                Dic[LiTime[NowTimePoint - 1]][i] = 0;
            }
            foreach (var item in operationLightGroup)
            {
                Dic[LiTime[NowTimePoint - 1]][item.Position - 28] = item.Color;
            }
            LoadFrame();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (NowTimePoint == 0)
                return;
            GetNumberDialog c = new GetNumberDialog(mw, "CopyToFrameNumberColon", false, LiTime, true);
            if (c.ShowDialog() == true)
            {
                int[] x = new int[96];
                for (int i = 0; i < Dic[LiTime[NowTimePoint - 1]].Count(); i++)
                {
                    x[i] = Dic[LiTime[NowTimePoint - 1]][i];
                }
                //如果已经有该时间点，替换
                if (LiTime.Contains(c.OneNumber))
                {
                    Dic[c.OneNumber] = x;
                    for (int i = 0; i < LiTime.Count; i++)
                    {
                        if (LiTime[i] == c.OneNumber)
                        {
                            NowTimePoint = i + 1;
                            tbTimeNow.Text = LiTime[NowTimePoint - 1].ToString();
                            tbTimePointCountLeft.Text = NowTimePoint.ToString();
                            tbTimePointCount.Text = " / " + LiTime.Count;
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
            if (NowTimePoint == 0)
                return;
            int[] x = new int[96];
            for (int i = 0; i < 96; i++)
            {
                x[i] = 0;
            }
            Dic[LiTime[NowTimePoint - 1]] = x;
            LoadFrame();
        }

        public bool IsCanDraw()
        {
            return mouseType == 1 && nowTimePointValidationRule.IsCanDraw && nowControlType == ControlType.Draw;
        }

        private ControlType nowControlType = ControlType.Draw;
        private enum ControlType
        {
            Style = 0,
            Draw = 1,
            Select = 2,
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


        private void mLaunchpad_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NowTimePoint == 0)
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

        private void ShowMembrane(object sender, MouseButtonEventArgs e)
        {
            mLaunchpad.ShowOrHideMembrane();
            mLaunchpad.SetValue(LaunchpadPro.IsMembraneProperty,!(bool)mLaunchpad.GetValue(LaunchpadPro.IsMembraneProperty));
        }

        private void spSaveFile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveFile();
        }

        private void tcLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nowControlType == ControlType.Draw)
                mLaunchpad.ClearSelect();
       
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

                        Dic[LiTime[NowTimePoint - 1]][ints[j][ints[oldJ].IndexOf(selects[i] + 28)] - 28] = Dic[LiTime[NowTimePoint - 1]][selects[i]];
                        newSelect.Add(ints[j][ints[oldJ].IndexOf(selects[i] + 28)] - 28);
                        if (!selects.Contains(ints[contraryJ].IndexOf(selects[i] + 28)))
                        {
                            Dic[LiTime[NowTimePoint - 1]][selects[i]] = 0;
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
            TextBlock tb = cText.Children[lbText.SelectedIndex] as TextBlock;
            if (e.Key == Key.Left)
                Canvas.SetLeft(tb, Canvas.GetLeft(tb) - 1);
            else if (e.Key == Key.Right)
                Canvas.SetLeft(tb, Canvas.GetLeft(tb) + 1);
            else if (e.Key == Key.Up)
                Canvas.SetTop(tb, Canvas.GetTop(tb) - 1);
            else if (e.Key == Key.Down)
                Canvas.SetTop(tb, Canvas.GetTop(tb) + 1);

            points[NowTimePoint].Texts[lbText.SelectedIndex].Point.X = Canvas.GetLeft(tb);
            points[NowTimePoint].Texts[lbText.SelectedIndex].Point.Y = Canvas.GetTop(tb);
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
            if (NowTimePoint == 0)
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
            if (NowTimePoint == 0)
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
            UI.UserControlDialog.NewFileDialog newFileDialog = new UI.UserControlDialog.NewFileDialog(mw,false, ".text", fileBusiness.GetFilesName(mw.lastProjectPath + @"\Text\", new List<string>() { ".text" }), ".text", NewTextFile);
            mw.ShowMakerDialog(newFileDialog);
        }

        public void NewTextFile(String result) {
            mw.RemoveDialog();
            String _filePath = mw.lastProjectPath + @"\Text\" + result;
            if (File.Exists(_filePath))
            {
                new MessageDialog(mw, "ExistingSameNameFile").ShowDialog();
                return;
            }
            else
            {
                CreateTextFile(_filePath);
                LoadText(_filePath);
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
                if (points.ContainsKey(NowTimePoint))
                {
                    points[NowTimePoint].Texts.Add(new FramePointModel.Text()
                    {
                        Value = getString.mString,
                        Point = new Point(0, 0)
                    });
                }
                else
                {
                    points.Add(NowTimePoint,
                    new FramePointModel()
                    {
                        Value = NowTimePoint,
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
                cText.Children.Add(tb);

                ((DataContext as FrameUserControlViewModel).Welcome.ListBoxData as ObservableCollection<dynamic>).Add(getString.mString);
            }
        }
        private void EditText(object sender, RoutedEventArgs e)
        {
            if (!points.ContainsKey(NowTimePoint))
                return;

            GetStringDialog getString = new GetStringDialog(mw, "", "", "");
            if (getString.ShowDialog() == true)
            {
                TextBlock tb = cText.Children[lbText.SelectedIndex] as TextBlock;
                tb.Text = getString.mString;
                points[NowTimePoint].Texts[lbText.SelectedIndex].Value = getString.mString;

                ((DataContext as FrameUserControlViewModel).Welcome.ListBoxData as ObservableCollection<dynamic>)[lbText.SelectedIndex] = getString.mString;
            }
        }
        private void DeleteText(object sender, RoutedEventArgs e)
        {
            if (cText.Children.Count > 1)
            {
                lbText.Items.Clear();
                cText.Children.RemoveRange(1, cText.Children.Count - 1);
            }
            if (!points.ContainsKey(NowTimePoint))
                return;

            int selectedIndex = lbText.SelectedIndex;
            cText.Children.RemoveAt(selectedIndex);
            points[NowTimePoint].Texts.RemoveAt(selectedIndex);

            ((DataContext as FrameUserControlViewModel).Welcome.ListBoxData as ObservableCollection<dynamic>).RemoveAt(selectedIndex);
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

        public object DoubleAnimaltion { get; private set; }

        private void LoadText(String fileName)
        {
            Dictionary<int, FramePointModel> mPoints = new Dictionary<int, FramePointModel>();
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
                mPoints.Add(int.Parse(element.Attribute("value").Value), framePointModel);
            }
            points = mPoints;
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

        private Point movePoint;
        private bool isMove = false;
        private void BaseLightUserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMove = true;
            movePoint = e.GetPosition(this);
        }

        private void BaseLightUserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMove = false;
        }

        private void BaseLightUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove && nowControlType == ControlType.Style) {
                dep.style.x += e.GetPosition(this).X - movePoint.X;
                dep.style.y += e.GetPosition(this).Y - movePoint.Y;

                Canvas.SetLeft(cMain, dep.style.x);
                Canvas.SetTop(cMain, dep.style.y);
                movePoint = e.GetPosition(this);
            }
        }
        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            isMove = false;
        }
        /// <summary>
        /// 移除选择框
        /// </summary>
        private void RemoveSelectRectangle()
        {
            if (mLaunchpad.Children.Contains(rectangle) && nowControlType == ControlType.Select)
            {
                mLaunchpad.Children.Remove(rectangle);
            }
        }

        private void sliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (dep != null)
                dep.style.size = sliderSize.Value;
        }

        private void MoveRLeft(int index)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = Canvas.GetTop(rLeft),
                To = 100 * index,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            rLeft.BeginAnimation(Canvas.TopProperty, doubleAnimation);

        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = spLeft.Children.IndexOf(sender as UIElement);
            MoveRLeft(index);
            if (sender != dpPicture) {
                HideImageControl();
            }

            for (int i = 0; i < spLeft.Children.Count; i++)
            {
                TextBlock textBlock = ((Panel)((Panel)spLeft.Children[i]).Children[0]).Children[1] as TextBlock;
                if (i == index)
                {
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
                else
                {
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(168, 169, 169));
                }
            }

            if (sender == dpStyle)
            {
                sliderSize.Visibility = Visibility.Visible;
                nowControlType = ControlType.Style;
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_blue.png", UriKind.RelativeOrAbsolute));
            }
            if (sender == dpColor)
            {
                sliderSize.Visibility = Visibility.Collapsed;

                if (nowControlType == ControlType.Draw)
                {
                    popColor.IsOpen = true;
                }
                else
                {
                    nowControlType = ControlType.Draw;
                }
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_gray.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_gray.png", UriKind.RelativeOrAbsolute));
            }
            if (sender == dpSelect)
            {
                sliderSize.Visibility = Visibility.Collapsed;
                nowControlType = ControlType.Select;
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_gray.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_blue.png", UriKind.RelativeOrAbsolute));
            }
            if (sender == dpPicture)
            {
                sliderSize.Visibility = Visibility.Collapsed;

                ShowImageControl();
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_gray.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_gray.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void dpColor_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            popColor.IsOpen = true;
        }
     
        private void ShowImageControl()
        {
            spRight.Visibility = Visibility.Visible;
            iPicture2.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_blue.png", UriKind.RelativeOrAbsolute));
        }
        private void HideImageControl() {
            spRight.Visibility = Visibility.Collapsed;
            iPicture2.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_gray.png", UriKind.RelativeOrAbsolute));
        }

        private void iSelect2_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            popSelect.IsOpen = true;
        }

        private void BaseLightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Width = mw.ActualWidth * 0.9;
            Height = mw.gMost.ActualHeight;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mw.RemoveChildren();
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
                        selects.Remove(positions[i]);
                    else
                        selects.Add(positions[i]);
                }
                mLaunchpad.SetSelectPosition(selects);
            }
            mLaunchpad.Focus();
        }

        public override void SaveFile()
        {
            base.SaveFile();
            XmlSerializer serializer = new XmlSerializer(dep.GetType());
            TextWriter writer = new StreamWriter("Config/frame.xml");
            serializer.Serialize(writer, dep);
            writer.Close();
        }

        private void  OnLaunchpadDataChange(List<Light> data) {
            //当前页的回调
            //LightBusiness.Print(data);
        }
    }
}
