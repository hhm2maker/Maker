using Maker.Business;
using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.Model;
using Maker.View.Device;
using Maker.View.Dialog;
using Maker.View.UI.Tool.Paved;
using Maker.ViewModel;
using Operation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public partial class FrameUserControl2 : BaseLightUserControl, ICanDraw
    {
        public FrameUserControl2(NewMainWindow mw)
        {
            InitializeComponent();

            this.mw = mw;

            mainView = gMain;
            HideControl();

            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Color.FromRgb(46, 48, 51)));

            //初始化贴膜
            mLaunchpad.AddMembrane();
            mLaunchpad.IsMembrane = false;
            //初始化事件
            InitLaunchpadEvent();
            //初始化绘制事件
            mLaunchpad.SetCanDraw(true);
            //初始化回调函数
            mLaunchpad.SetOnDataChange(OnLaunchpadDataChange);
            completeColorPanel.SetSelectionChangedEvent(lbColor_SelectionChanged);

            XmlSerializer serializer = new XmlSerializer(typeof(FrameConfigModel));
            FileStream stream = new FileStream("Config/frame.xml", FileMode.Open);
            frameModel = (FrameConfigModel)serializer.Deserialize(stream);
            stream.Close();
        }

        private FrameConfigModel frameModel;

        private List<int> leftDown = new List<int>() { 11,12,13,14,21,22,23,24,31,32,33,34,41,42,43,44};
        private List<int> leftUp = new List<int>() { 51, 52,53, 54, 61, 62, 63, 64, 71, 72, 73, 74, 81, 82, 83, 84 };
        private List<int> rightDown = new List<int>() { 15, 16, 17, 18, 25, 26, 27, 28, 35, 36, 37, 38, 45, 46, 47, 48 };
        private List<int> rightUp = new List<int>() { 55, 56, 57, 58, 65, 66, 67, 68, 75, 76, 77, 78, 85, 86, 87, 88 };

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
                    if (i < 100)
                    {
                        Dic[LiTime[NowTimePoint - 1]][i] = nowColor;
                    }
                    else
                    {
                        Dic[LiTime[NowTimePoint - 1]][i - 100] = nowColor;
                    }
                }
                else if (LeftOrRight == 1)
                {
                    int i = mLaunchpad.GetNumber(sender as Shape);
                    if (i < 100)
                    {
                        Dic[LiTime[NowTimePoint - 1]][i] = 0;
                    }
                    else
                    {
                        Dic[LiTime[NowTimePoint - 1]][i - 100] = 0;
                    }
                }
                (DataContext as FrameUserControlViewModel).Model.LoadFrame();
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
                if (i < 100)
                {
                    Dic[LiTime[NowTimePoint - 1]][i] = nowColor;
                }
                else
                {
                    Dic[LiTime[NowTimePoint - 1]][i - 100] = nowColor;
                }
                LeftOrRight = 0;
            }
            (DataContext as FrameUserControlViewModel).Model.LoadFrame();
        }
        private void ClearColor(object sender, RoutedEventArgs e)
        {
            if (IsCanDraw())
            {
                int i = mLaunchpad.GetNumber(sender as Shape);
                if (i < 100)
                {
                    Dic[LiTime[NowTimePoint - 1]][i] = 0;
                }
                else
                {
                    Dic[LiTime[NowTimePoint - 1]][i - 100] = 0;
                }
                LeftOrRight = 1;
            }
                (DataContext as FrameUserControlViewModel).Model.LoadFrame();
        }
        public List<int> LiTime {
            set
            {
                (DataContext as FrameUserControlViewModel).Model.LiTime = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Model.LiTime;
            }
        }
        public Dictionary<int, int[]> Dic {
            set
            {
                (DataContext as FrameUserControlViewModel).Model.NowData = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Model.NowData;
            }
        }

        private List<String> ColorList = new List<string>();
        public int NowTimePoint {
            set {
                (DataContext as FrameUserControlViewModel).Model.NowTimePoint = value;
            }
            get {
                return (DataContext as FrameUserControlViewModel).Model.NowTimePoint;
            }
        }
        public int allTimePoint
        {
            set
            {
                (DataContext as FrameUserControlViewModel).Model.AllTimePoint = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Model.AllTimePoint;
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
                mActionBeanList = Business.LightBusiness.Split(mActionBeanList);
                LiTime = Business.LightBusiness.GetTimeList(mActionBeanList);
                Dic = Business.LightBusiness.GetParagraphLightIntList(mActionBeanList);
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
            Boolean[] b = new Boolean[100];
            for (int i = 0; i < 100; i++)
            {
                b[i] = false;
            }
            for (int i = 0; i < LiTime.Count; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (Dic[LiTime[i]][j] == 0 || Dic[LiTime[i]][j] == -1)
                    {
                        if (b[j])
                        {
                            mActionBeanList.Add(new Light(LiTime[i], 128, j, 64));
                            b[j] = false;
                        }
                    }
                    if (Dic[LiTime[i]][j] != 0 && Dic[LiTime[i]][j] != -1)
                    {
                        if (b[j])
                        {
                            mActionBeanList.Add(new Light(LiTime[i], 128, j, 64));
                        }
                        mActionBeanList.Add(new Light(LiTime[i], 144, j, Dic[LiTime[i]][j]));
                        b[j] = true;
                    }
                }
            }

            return mActionBeanList;
        }

        /// <summary>
        /// 清空当前页面所有颜色
        /// </summary>
        private void ClearFrame()
        {
          
            mLaunchpad.ClearAllColorExceptMembrane();
        }

        public void LoadFrame()
        {
            if (NowTimePoint == 0)
                return;
            //ClearFrame();

            List<Light> mLightList = new List<Light>();

            int[] x = Dic[LiTime[NowTimePoint - 1]];

            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i] == 0 || x[i] == -1)
                {
                    continue;
                }
                mLightList.Add(new Light(0,144, i, x[i]));
            }
            (DataContext as FrameUserControlViewModel).Model.NowLightLight = mLightList;
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
                (DataContext as FrameUserControlViewModel).Model.Selects = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Model.Selects;
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

        private void btnRegionCopy_Click(object sender, MouseEventArgs e)
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
        private void btnRegionClear_Click(object sender, MouseEventArgs e)
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
            for (int i = 0; i < 100; i++)
            {
                int color = Dic[LiTime[NowTimePoint - 1]][i];
                if (color != 0)
                {
                    operationLightGroup.Add(new Operation.Light(0, 144, i, color));
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

            for (int i = 0; i < 100; i++)
            {
                Dic[LiTime[NowTimePoint - 1]][i] = 0;
            }
            foreach (var item in operationLightGroup)
            {
                Dic[LiTime[NowTimePoint - 1]][item.Position] = item.Color;
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
                int[] x = new int[100];
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
                            break;
                        }
                    }
                }
                else
                {
                    (DataContext as FrameUserControlViewModel).InsertTimePoint(c.OneNumber, x);
                }
            }
        }

        public bool IsCanDraw()
        {
            return mouseType == 1 && nowTimePointValidationRule.IsCanDraw && nowControlType == ControlType.Draw && NowTimePoint != 0;
        }

        private ControlType nowControlType = ControlType.Draw;
        private enum ControlType
        {
            Style = 0,
            Draw = 1,
            Select = 2,
            Picture = 3,
        }
        private SelectType nowSelectType = SelectType.Select;
        private enum SelectType
        {
            Select = 0,
            Add = 1,
            Intersection = 2,
            Complement =3
        }
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
            mLaunchpad.IsMembrane = !mLaunchpad.IsMembrane;
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
                    if (ints[j].Contains(selects[i]))
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
                        if (ints[j][ints[oldJ].IndexOf(selects[i])] < 0)
                            break;

                        Dic[LiTime[NowTimePoint - 1]][ints[j][ints[oldJ].IndexOf(selects[i])]] = Dic[LiTime[NowTimePoint - 1]][selects[i]];
                        newSelect.Add(ints[j][ints[oldJ].IndexOf(selects[i])] );
                        if (!selects.Contains(ints[contraryJ].IndexOf(selects[i])))
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

        private void SaveCanvas(object sender, MouseEventArgs e)
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
                SaveRTBAsPNG(GetBitmapStream(mLaunchpad, 100), fileName);
            }
        }

        private void SaveLongCanvas(object sender, MouseEventArgs e)
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
        private void NewTextFile(object sender, MouseEventArgs e)
        {
            String _filePath = GetFileDirectory();
            UI.UserControlDialog.NewFileDialog newFileDialog = new UI.UserControlDialog.NewFileDialog(mw,false, ".text", Business.FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + @"\Text\", new List<string>() { ".text" }), ".text","", NewTextFile);
            mw.ShowMakerDialog(newFileDialog);
        }

        public void NewTextFile(String result) {
            mw.RemoveDialog();
            String _filePath = mw.LastProjectPath + @"\Text\" + result;
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

        private void NewText(object sender, MouseEventArgs e)
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

                ((DataContext as FrameUserControlViewModel).Model.ListBoxData as ObservableCollection<dynamic>).Add(getString.mString);
            }
        }
        private void EditText(object sender, MouseEventArgs e)
        {
            if (!points.ContainsKey(NowTimePoint))
                return;

            GetStringDialog getString = new GetStringDialog(mw, "", "", "");
            if (getString.ShowDialog() == true)
            {
                TextBlock tb = cText.Children[lbText.SelectedIndex] as TextBlock;
                tb.Text = getString.mString;
                points[NowTimePoint].Texts[lbText.SelectedIndex].Value = getString.mString;

                ((DataContext as FrameUserControlViewModel).Model.ListBoxData as ObservableCollection<dynamic>)[lbText.SelectedIndex] = getString.mString;
            }
        }
        private void DeleteText(object sender, MouseEventArgs e)
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

            ((DataContext as FrameUserControlViewModel).Model.ListBoxData as ObservableCollection<dynamic>).RemoveAt(selectedIndex);
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
                (DataContext as FrameUserControlViewModel).Model.Points = value;
            }
            get
            {
                return (DataContext as FrameUserControlViewModel).Model.Points;
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

      
        private void LoadTextFile(object sender, MouseEventArgs e)
        {
            List<String> fileNames = new List<string>();
            fileNames.AddRange(Business.FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + @"\Text", new List<string>() { ".text" }));

            ShowLightListDialog dialog = new ShowLightListDialog(mw, "", fileNames);
            if (dialog.ShowDialog() == true)
            {
                nowTextFilePath = mw.LastProjectPath + @"\Text\" + dialog.selectItem;
                LoadText(nowTextFilePath);
            }
        }

        private String nowTextFilePath = "";
        private void SaveTextFile(object sender, MouseEventArgs e)
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
            if (mLaunchpad.Children.Contains(rectangle) && nowControlType == ControlType.Select)
            {
                mLaunchpad.Children.Remove(rectangle);
            }
        }

        public int filePosition = -1;
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int position = spLeft.Children.IndexOf(sender as UIElement);

            for (int i = 0; i < spLeft.Children.Count; i++)
            {
                TextBlock textBlock = ((Panel)((Panel)((Border)spLeft.Children[i]).Child).Children[0]).Children[1] as TextBlock;
                if (i == position)
                {
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
                else
                {
                    textBlock.Foreground = new SolidColorBrush(Color.FromRgb(168, 169, 169));
                }
            }

            if (sender == bStyle)
            {
                nowControlType = ControlType.Style;
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_blue.png", UriKind.RelativeOrAbsolute));
                iColor.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/color_black.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_gray.png", UriKind.RelativeOrAbsolute));
                iPicture2.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_gray.png", UriKind.RelativeOrAbsolute));
            }
            if (sender == bColor)
            {
                nowControlType = ControlType.Draw;
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_gray.png", UriKind.RelativeOrAbsolute));
                iColor.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/color_blue.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_gray.png", UriKind.RelativeOrAbsolute));
                iPicture2.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_gray.png", UriKind.RelativeOrAbsolute));
            }
            if (sender == bSelect)
            {
                nowControlType = ControlType.Select;
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_gray.png", UriKind.RelativeOrAbsolute));
                iColor.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/color_black.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_blue.png", UriKind.RelativeOrAbsolute));
                iPicture2.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_gray.png", UriKind.RelativeOrAbsolute));
            }
            if (sender == bPicture)
            {
                nowControlType = ControlType.Picture;
                iStyle.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/style_gray.png", UriKind.RelativeOrAbsolute));
                iColor.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/color_black.png", UriKind.RelativeOrAbsolute));
                iSelect.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/select_gray.png", UriKind.RelativeOrAbsolute));
                iPicture2.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/picture_blue.png", UriKind.RelativeOrAbsolute));
            }

            if (filePosition == position)
                return;

            if (filePosition != -1)
            {
                (spLeft.Children[filePosition] as Border).Background = new SolidColorBrush(Colors.Transparent);
                (spLeft.Children[filePosition] as Border).BorderBrush = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                //(((spLeft.Children[filePosition] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            }

            (spLeft.Children[position] as Border).Background = new SolidColorBrush(Color.FromRgb(184, 191, 198));
            (spLeft.Children[position] as Border).BorderBrush = new SolidColorBrush(Colors.Transparent);
            //(((spLeft.Children[position] as Border).Child as Grid).Children[0] as TextBlock).Foreground = new SolidColorBrush(Color.FromRgb(85, 85, 85));
            if (nowControlType == ControlType.Style)
            {
                bMyStyle.Visibility = Visibility.Visible;
            }
            else
            {
                bMyStyle.Visibility = Visibility.Collapsed;
            }

            if (nowControlType == ControlType.Draw)
            {
                bCompleteColor.Visibility = Visibility.Visible;
            }
            else
            {
                bCompleteColor.Visibility = Visibility.Collapsed;
            }

            if (nowControlType == ControlType.Select)
            {
                bFullSelect.Visibility = Visibility.Visible;
            }
            else
            {
                bFullSelect.Visibility = Visibility.Collapsed;
            }

            if (nowControlType == ControlType.Picture)
            {
                spRight.Visibility = Visibility.Visible;
            }
            else
            {
                spRight.Visibility = Visibility.Collapsed;
            }
            filePosition = position;
        }

        private void dpColor_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void iSelect2_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            popSelect.Visibility = Visibility.Visible;
        }

        private void BaseLightUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Width = mw.gRight.ActualWidth;

            if (mw.basicConfigModel.Model == BasicConfigModel.ModelType.PC)
            {
                Height = mw.gRight.ActualHeight;

                dpCenter.Width = mw.ActualWidth / 4;
                mLaunchpad.Size = mw.ActualWidth / 4 - 30;
                cLaunchpad.Width = mLaunchpad.Width;
                cLaunchpad.Height = mLaunchpad.Height;
            }
            else {
                //Height = mw.gRight.ActualHeight;

                //dpCenter.Width = mw.ActualWidth /2;
                //mLaunchpad.Size = mw.ActualWidth/2 - 30;
                //cLaunchpad.Width = mLaunchpad.Width;
                //cLaunchpad.Height = mLaunchpad.Height;
            }
         

            Canvas_MouseLeftButtonUp(bStyle,null);
        }

        private void FourAreaClick(object sender, MouseButtonEventArgs e)
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
            if (nowSelectType == SelectType.Select)
            {
                selects.Clear();
                selects.AddRange(positions);
                mLaunchpad.SetSelectPosition(selects);
            }
            if (nowSelectType == SelectType.Add)
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
            if (nowSelectType == SelectType.Intersection)
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
            if (nowSelectType == SelectType.Complement)
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
            Business.Currency.XmlSerializerBusiness.Save(frameModel, "Config/frame.xml");
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void  OnLaunchpadDataChange(List<Light> data) {
            //当前页的回调
            //LightBusiness.Print(data);
        }

        public override void OnDismiss() {
            //SaveFile();
            Business.Currency.XmlSerializerBusiness.Save(frameModel, "Config/frame.xml");
        }

        public void SetButton(int position)
        {
            if (NowTimePoint == 0)
            {
                return;
            }
            else {
                if (Dic[LiTime[NowTimePoint - 1]][position] == 0)
                    Dic[LiTime[NowTimePoint - 1]][position] = nowColor;
                else 
                    Dic[LiTime[NowTimePoint - 1]][position] = 0;
                (DataContext as FrameUserControlViewModel).Model.LoadFrame();
            }
        }
    }
}
