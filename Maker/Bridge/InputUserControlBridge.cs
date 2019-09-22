using Maker.Business;
using Maker.MethodSet;
using Maker.Model;
using Maker.Utils;
using Maker.View.Dialog;
using Maker.View.LightScriptUserControl;
using Maker.View.Utils;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.Bridge
{
    public class InputUserControlBridge
    {
        private ScriptUserControl iuc;
        private PointCollection polygonPC = new PointCollection();
        public List<int> mColor = new List<int>();

        public InputUserControlBridge(ScriptUserControl iuc)
        {
            this.iuc = iuc;
            RefreshColor();

            InitPC();
        }

        private void InitPC()
        {
            polygonPC.Add(new Point(8, 0));
            polygonPC.Add(new Point(0, 15));
            polygonPC.Add(new Point(16, 15));
        }
        /// <summary>
        /// 更新面板颜色
        /// </summary>
        /// <param name="mLightList"></param>
        public void UpdateForColor(List<Light> mLightList,bool isChild) {
            iuc.cColor.Children.RemoveRange(1, iuc.cColor.Children.Count - 1);
            if (isChild) {
                mLightList = GetRealData(mLightList);
            }
            Dictionary<int, int> _color = new Dictionary<int, int>();
            int count = mLightList.Count;
            if (count == 0)
            {
                iuc.rColor.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                for (int i = 0; i < mLightList.Count; i++)
                {
                    int c = mLightList[i].Color;
                    if (c < 0 || c > 127 || mLightList[i].Action == 128)
                    {
                        count--;
                        continue;
                    }
                    if (_color.ContainsKey(c))
                    {
                        _color[c]++;
                    }
                    else
                    {
                        _color.Add(c, 1);
                    }
                }
                if (_color.Count > 0)
                {
                    LinearGradientBrush brush = new LinearGradientBrush();
                    brush.StartPoint = new Point(0, 0.5);
                    brush.EndPoint = new Point(1, 0.5);
                    double cPosition = 0;
                    double lPosition = 0;
                    GradientStopCollection collection = new GradientStopCollection();
                    mColor.Clear();
                    foreach (var item in _color)
                    {
                        mColor.Add(item.Key);

                        GradientStop stop = new GradientStop(StaticConstant.brushList[item.Key-1].Color, cPosition);
                        collection.Add(stop);
                        cPosition += (double)item.Value / count;
                        brush.GradientStops = collection;
                        iuc.rColor.Fill = brush;

                        Polygon polygon = new Polygon();
                        polygon.Points = polygonPC;
                        polygon.Fill = StaticConstant.brushList[item.Key-1];
                        polygon.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        polygon.MouseLeftButtonUp += Polygon_MouseLeftButtonUp;
                        Canvas.SetLeft(polygon, 8 + lPosition);
                        lPosition += (double)item.Value / count * 270;
                        Canvas.SetTop(polygon, 30);
                        iuc.cColor.Children.Add(polygon);
                    }
                }
            }
        }

        private List<Light> GetRealData(List<Light> mLightList)
        {
            if (!iuc.finalDictionary.ContainsKey(iuc.GetStepName()))
                return mLightList;
            //没有可操作的灯光组
            if (!iuc.lightScriptDictionary[iuc.GetStepName()].Contains(iuc.GetStepName() + "LightGroup"))
            {
                return mLightList;
            }
            String[] contents = iuc.finalDictionary[iuc.GetStepName()].Split(';');
            StringBuilder command = new StringBuilder();
            foreach (String str in contents)
            {
                if (str.Equals(String.Empty))
                    continue;
                String[] strs = str.Split('=');
                String type = strs[0];
                String[] _contents = strs[1].Split(',');

                if (type.Equals("Color"))
                {
                    foreach (String _str in _contents)
                    {
                        String[] mContents = _str.Split('-');
                        if (mContents[0].Equals("Format"))
                        {
                            if (mContents[1].Equals("Green"))
                            {
                                //mLightList = LightGroupMethod.SetColor(mLightList,new List<int>() { 73,74,75,76 });
                            }
                            if (mContents[1].Equals("Blue"))
                            {
                                //mLightList = LightGroupMethod.SetColor(mLightList, new List<int>() { 33, 37, 41, 45 });
                            }
                            if (mContents[1].Equals("Pink"))
                            {
                                //mLightList = LightGroupMethod.SetColor(mLightList, new List<int>() { 4, 94, 53, 57 });
                            }
                            if (mContents[1].Equals("Diy"))
                            {
                                List<int> intColors = new List<int>(); 
                                String[] strColors = mContents[2].Split(' ');
                                for (int i = 0; i < strColors.Length; i++) {
                                    intColors.Add(int.Parse(strColors[i]));
                                }
                                //mLightList = LightGroupMethod.SetColor(mLightList, intColors);
                            }
                        }
                        else if (mContents[0].Equals("Shape"))
                        {
                            if (mContents[1].Equals("Square"))
                            {
                                //mLightList = EditMethod.ShapeColor(mLightList,EditMethod.ShapeColorType.Square, mContents[2]);
                            }
                            else if (mContents[1].Equals("RadialVertical"))
                            {
                                //mLightList = EditMethod.ShapeColor(mLightList, EditMethod.ShapeColorType.RadialVertical, mContents[2]);
                            }
                            else if (mContents[1].Equals("RadialHorizontal"))
                            {
                                //mLightList = EditMethod.ShapeColor(mLightList, EditMethod.ShapeColorType.RadialHorizontal, mContents[2]);
                            }
                        }
                    }
                }
                if (type.Equals("Shape"))
                {
                    foreach (String _str in _contents)
                    {
                        if (_str.Equals("HorizontalFlipping"))
                        {
                            //mLightList = EditMethod.HorizontalFlipping(mLightList);
                        }
                        if (_str.Equals("VerticalFlipping"))
                        {
                            //mLightList = EditMethod.VerticalFlipping(mLightList);
                        }
                        if (_str.Equals("Clockwise"))
                        {
                            //mLightList = EditMethod.Clockwise(mLightList);
                        }
                        if (_str.Equals("AntiClockwise"))
                        {
                            //mLightList = EditMethod.AntiClockwise(mLightList);
                        }
                    }
                }
                if (type.Equals("Time"))
                {
                    foreach (String _str in _contents)
                    {
                        if (_str.Equals("Reversal"))
                        {
                            //mLightList = EditMethod.Reversal(mLightList);
                        }
                        String[] mContents = _str.Split('-');
                        if (mContents[0].Equals("ChangeTime"))
                        {
                            //mLightList = EditMethod.ChangeTime(mLightList,(EditMethod.Operator)int.Parse(mContents[1]),double.Parse(mContents[2]));
                        }
                        else if (mContents[0].Equals("StartTime"))
                        {
                            //mLightList = LightGroupMethod.SetStartTime(mLightList, int.Parse(mContents[1]));
                        }
                        else if (mContents[0].Equals("AllTime"))
                        {
                            //mLightList = LightGroupMethod.SetAllTime(mLightList, int.Parse(mContents[1]));
                        }
                    }
                }
                if (type.Equals("ColorOverlay"))
                {
                    foreach (String _str in _contents)
                    {
                        String[] mContents = _str.Split('-');
                        List<int> intColors = new List<int>();
                        String[] strColors = mContents[1].Split(' ');
                        for (int x = 0; x < strColors.Length; x++)
                        {
                            intColors.Add(int.Parse(strColors[x]));
                        }
                        if (mContents[0].Equals("true"))
                        {
                            //mLightList = EditMethod.CopyToTheFollow(mLightList, intColors);
                        }
                        else
                        {
                            //mLightList = EditMethod.CopyToTheEnd(mLightList, intColors);
                        }
                    }
                }
                if (type.Equals("SportOverlay"))
                {
                    foreach (String _str in _contents)
                    {
                        String[] mContents = _str.Split('-');

                        List<int> intColors = new List<int>();
                        String[] strColors = mContents[0].Split(' ');
                        for (int x = 0; x < strColors.Length; x++)
                        {
                            intColors.Add(int.Parse(strColors[x]));
                        }
                        //mLightList = EditMethod.AccelerationOrDeceleration(mLightList, intColors);
                    }
                }
                if (type.Equals("Other"))
                {
                    foreach (String _str in _contents)
                    {
                        if (_str.Equals("RemoveBorder"))
                        {
                           // mLightList = EditMethod.RemoveBorder(mLightList);
                        }
                    }
                }
            }
            return mLightList;
        }

        private void Polygon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Polygon p = sender as Polygon;
            iuc.needChangeColor = mColor[iuc.cColor.Children.IndexOf(p)-1];
            iuc.pColor.PlacementTarget = p;
            //设置选中项
            iuc.lbColor.SelectedIndex = mColor[iuc.cColor.Children.IndexOf(p)-1];
            iuc.pColor.IsOpen = true;
        }
        public void UpdateData()
        {
            UpdateData(mLightList);
        }
          private Dictionary<string, List<Light>> mLightList ;
        public void UpdateData(Dictionary<string, List<Light>> mLightList)
        {
            if (mLightList == null) {
                mLightList = new Dictionary<string, List<Light>>();
            }
              
            iuc.mLightDictionary = mLightList;
            this.mLightList = mLightList;
            List<Light> colorLightList = new List<Light>();

            //颜色面板
            List<Light> lights = new List<Light>();
            foreach (var item in mLightList)
            {
                lights.AddRange(item.Value);
            }
            colorLightList = lights;
            //if (iuc.lbStep.SelectedIndex == -1)
            //{
            //    colorLightList = mLightList;
            //}
            //else {
            //    colorLightList = iuc.RefreshData(iuc.GetStepName());
            //    colorLightList = GetRealData(colorLightList);
            //}
            iuc.cColor.Children.RemoveRange(1, iuc.cColor.Children.Count - 1);
            Dictionary<int,int> _color = new Dictionary<int, int>();
            int count = colorLightList.Count;
            if (count == 0)
            {
                iuc.rColor.Fill = new SolidColorBrush(Color.FromArgb(255,0,0,0));
            }
            else { 
            for (int i = 0; i < colorLightList.Count; i++) {
                int c = colorLightList[i].Color;
                if (c < 0 || c > 127 || colorLightList[i].Action == 128) {
                    count--;
                    continue;
                }
                if (_color.ContainsKey(c))
                {
                    _color[c]++;
                }
                else {
                    _color.Add(c,1);
                }
            }
            if (_color.Count > 0) {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0, 0.5);
                brush.EndPoint = new Point(1, 0.5);
                double cPosition = 0;
                double lPosition = 0;
                GradientStopCollection collection = new GradientStopCollection();
                    mColor.Clear();
                foreach (var item in _color)
                {
                    mColor.Add(item.Key);
                    GradientStop stop = new GradientStop(StaticConstant.brushList[item.Key-1].Color, cPosition);
                    collection.Add(stop);
                    cPosition += (double)item.Value / count;
                    brush.GradientStops = collection;
                    iuc.rColor.Fill = brush;

                    Polygon polygon = new Polygon();
                    polygon.Points = polygonPC;
                    polygon.Fill = StaticConstant.brushList[item.Key-1];
                    polygon.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    polygon.MouseLeftButtonUp += Polygon_MouseLeftButtonUp;
                    Canvas.SetLeft(polygon, 8 + lPosition);
                    lPosition += (double)item.Value / count * 270;
                    Canvas.SetTop(polygon, 30);
                    iuc.cColor.Children.Add(polygon);
                    }
                }
            }

            //时间轴
            iuc.cTime.Children.Clear();
            double actualWidth = iuc.cTime.ActualWidth;
            
            int lightMaxTime = -1;

            foreach (var item in mLightList) {
                int nowMax = LightBusiness.GetMax(item.Value);
                if (lightMaxTime < nowMax) {
                    lightMaxTime = nowMax;
                }
            }

            if (lightMaxTime != -1 && lightMaxTime != 0)
            { 
            int stepNum = 0;
            foreach (var item in iuc.scriptModelDictionary) {
                if (mLightList.ContainsKey(item.Key)) {
                    int min = LightBusiness.GetMin(mLightList[item.Key]);
                    int max = LightBusiness.GetMax(mLightList[item.Key]);

                    Rectangle myRect = new Rectangle
                    {
                        Stroke = Brushes.Black,
                        Fill = Brushes.Gray,
                        Height = 30+2,
                        Width = (max - min)/ (lightMaxTime*1.0) * actualWidth
                    };
                    Canvas.SetTop(myRect, ((30 +2)) * stepNum + 2);
                    Canvas.SetLeft(myRect, min / (lightMaxTime*1.0) * actualWidth);
                   
                    iuc.cTime.Children.Add(myRect);
                }
                stepNum++;
            }
            }
            iuc.cTime.Height = iuc.scriptModelDictionary.Count * (30+2);

            if (iuc.mShow == ScriptUserControl.ShowMode.Launchpad)
            {
                //清空
                SetDataToLaunchpad(lights);
            }
            else if (iuc.mShow == ScriptUserControl.ShowMode.DataGrid)
            {
                SetDataToDataGrid(lights);
            }
        }
        public List<int> liTime = new List<int>();
        private Dictionary<int, List<Light>> dic = new Dictionary<int, List<Light>>();
        private List<String> ColorList = new List<string>();
        public int nowTimePoint = 1;

        public void RefreshColor()
        {
            //FileBusiness file = new FileBusiness();
            //ColorList = file.ReadColorFile(iuc.mw.strColortabPath);
        }

        public void SetDataToDataGrid(List<Light> mActionBeanList)
        {
            //iuc.dgMain.ItemsSource = mActionBeanList;
        }
        /// <summary>
        /// 获取主窗口数据
        /// </summary>
        public void SetDataToLaunchpad(List<Light> mActionBeanList)
        {
            //切割
            mActionBeanList = LightBusiness.Split(mActionBeanList);

            liTime.Clear();
            dic = LightBusiness.GetParagraphLightLightList(mActionBeanList);
            liTime = dic.Keys.ToList();

            //dic.Clear();
            //int time = -1;
            //for (int i = 0; i < mActionBeanList.Count; i++)
            //{
            //    if (mActionBeanList[i].Time != time)
            //    {
            //        time = mActionBeanList[i].Time;
            //        liTime.Add(time);
            //        int[] x = new int[100];
            //        for (int j = 0; j < 100; j++)
            //        {
            //            x[j] = 0;
            //        }
            //        dic.Add(time, x);
            //        if (mActionBeanList[i].Action == 144)
            //        {
            //            dic[time][mActionBeanList[i].Position] = mActionBeanList[i].Color;
            //        }
            //        else if (mActionBeanList[i].Action == 128)
            //        {
            //            dic[time][mActionBeanList[i].Position] = 0;//关闭为黑色
            //        }
            //    }
            //    else
            //    {
            //        if (mActionBeanList[i].Action == 144)
            //        {
            //            dic[time][mActionBeanList[i].Position] = mActionBeanList[i].Color;
            //        }
            //        else if (mActionBeanList[i].Action == 128)
            //        {
            //            dic[time][mActionBeanList[i].Position] = 0;//关闭为黑色
            //        }
            //    }
            //}
            if (liTime.Count == 0)
            {
                iuc.tbTimeNow.Text = "0";
                nowTimePoint = 0;
                iuc.tbTimePointCountLeft.Text = "0";
                iuc.tbTimePointCount.Text = "0";

                iuc.mLaunchpadData = new List<Light>();
                iuc.mLaunchpad.SetData(iuc.mLaunchpadData);
            }
            else
            {
                iuc.tbTimeNow.Text = liTime[0].ToString();
              
                if (nowTimePoint > liTime.Count || nowTimePoint == 0)
                {
                    nowTimePoint = 1;
                }
              
                iuc.tbTimePointCountLeft.Text = nowTimePoint.ToString();
                iuc.tbTimePointCount.Text = liTime.Count.ToString();
                LoadFrame();
            }
        }
      

        public void LoadFrame()
        {
            iuc.mLaunchpadData = dic[liTime[nowTimePoint - 1]];
            iuc.mLaunchpad.SetData(iuc.mLaunchpadData);

            iuc.tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();

            //for (int i = 0; i < x.Count(); i++)
            //{
            //    //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
            //    if (x[i] == 0)
            //    {
            //        iuc.mLaunchpad.SetButtonBackground(i, StaticConstant.closeBrush);
            //        continue;
            //    }
            //    iuc.mLaunchpadData.Add(new Light(0,144,i,x[i]));
            //}
            iuc.OnDrawTimeLine();

            if (nowTimePoint == 1)
            {
                iuc.btnLastTimePoint.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/toleft_gray.png", UriKind.RelativeOrAbsolute));
            }
            else {
                iuc.btnLastTimePoint.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/toleft_blue.png", UriKind.RelativeOrAbsolute));
            }
            if (nowTimePoint == liTime.Count)
            {
                iuc.btnNextTimePoint.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/toright_gray.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                iuc.btnNextTimePoint.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/toright_blue.png", UriKind.RelativeOrAbsolute));
            }
        }

        public void ToLastTime()
        {
            if (nowTimePoint <= 1) return;
            nowTimePoint--;
            //iuc.tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
            iuc.tbTimePointCountLeft.Text = nowTimePoint.ToString();
            //iuc.tbTimePointCount.Text = liTime.Count.ToString();
            //LoadFrame();
        }
        public void ToNextTime()
        {
            if (nowTimePoint > dic.Count - 1) return;
            nowTimePoint++;
            //iuc.tbTimeNow.Text = liTime[nowTimePoint - 1].ToString();
            iuc.tbTimePointCountLeft.Text = nowTimePoint.ToString();
            //iuc.tbTimePointCount.Text = liTime.Count.ToString();
            //LoadFrame();
        }
        public void tbTimePointCountLeft_TextChanged()
        {
            try
            {
                int position = Convert.ToInt32(iuc.tbTimePointCountLeft.Text);
                if (liTime.Count == 0)
                {
                    nowTimePoint = 0;
                    iuc.tbTimePointCountLeft.Text = "0";

                    iuc.btnLastTimePoint.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/toleft_gray.png", UriKind.RelativeOrAbsolute));
                    iuc.btnNextTimePoint.Source = new BitmapImage(new Uri("pack://application:,,,/View/Resources/Image/toright_gray.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    nowTimePoint = position;
                    LoadFrame();
                }
            }
            catch
            {
                if (liTime.Count == 0)
                {
                    nowTimePoint = 0;
                    iuc.tbTimePointCountLeft.Text = "0";
                }
                else
                {
                    nowTimePoint = 1;
                    iuc.tbTimePointCountLeft.Text = "1";
                    LoadFrame();
                }
            }
        }
        public void LoadRangeFile()
        {
            iuc.rangeDictionary = FileBusiness.CreateInstance().ReadRangeFile(AppDomain.CurrentDomain.BaseDirectory + @"RangeList\test.Range");
            if (iuc.rangeDictionary == null)
            {
                new MessageDialog(iuc.mw, "TheReadRangeFileFailed").ShowDialog();
            }
        }

        /// <summary>
        /// 将库文件加载到界面
        /// </summary>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        public void InitLibrary(List<String> librarys, RoutedEventHandler clickEvent)
        {
            //GeneralViewBusiness.SetStringsAndClickEventToMenuItem(iuc.miChildLibrary, librarys, clickEvent,false,14);
        }

        /// <summary>
        /// 将我的内容加载到界面
        /// </summary>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        //public void InitMyContent(List<String> contents, MouseButtonEventHandler clickEvent)
        //{
        //    GeneralViewBusiness.SetStringsAndClickEventToListBox(iuc.miChildMycontent, contents, clickEvent,true,16);
        //}

        /// <summary>
        /// 获取库文件列表
        /// </summary>
        /// <returns></returns>
        public List<String> GetLibrary()
        {
            List<String> librarys = new List<String>();
            DirectoryInfo folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Library");
            foreach (FileInfo file in folder.GetFiles("*.lightScript"))
            {
                librarys.Add(System.IO.Path.GetFileNameWithoutExtension(file.FullName));
            }
            return librarys;
        }
        /// <summary>
        /// 获取我的文件列表
        /// </summary>
        /// <returns></returns>
        //public List<String> GetMyContent(String exceptStr)
        //{
        //    List<String> contents = new List<String>();
        //    if (iuc.cbMyContent.SelectedIndex == 0)
        //    {
        //        DirectoryInfo folder = new DirectoryInfo(iuc.mw.LastProjectPath + @"\Light");
        //        foreach (FileInfo file in folder.GetFiles("*.light"))
        //        {
        //            if (!file.Name.Equals(exceptStr))
        //                contents.Add(System.IO.Path.GetFileName(file.FullName));
        //        }
        //        foreach (FileInfo file in folder.GetFiles("*.mid"))
        //        {
        //            if (!file.Name.Equals(exceptStr))
        //                contents.Add(System.IO.Path.GetFileName(file.FullName));
        //        }
        //    }
        //    if (iuc.cbMyContent.SelectedIndex == 1) {
        //        DirectoryInfo folder = new DirectoryInfo(iuc.mw.LastProjectPath + @"\LightScript");
        //        foreach (FileInfo file in folder.GetFiles("*.lightScript"))
        //        {
        //            if (!file.Name.Equals(exceptStr))
        //                contents.Add(System.IO.Path.GetFileName(file.FullName));
        //        }
        //    }
        //    if (iuc.cbMyContent.SelectedIndex == 2)
        //    {
        //        DirectoryInfo folder = new DirectoryInfo(iuc.mw.LastProjectPath + @"\LimitlessLamp");
        //        foreach (FileInfo file in folder.GetFiles("*.LimitlessLamp"))
        //        {
        //            if (!file.Name.Equals(exceptStr))
        //                contents.Add(System.IO.Path.GetFileName(file.FullName));
        //        }
        //    }
        //    return contents;
        //}

        ///// <summary>
        ///// 获取主窗口数据
        ///// </summary>
        //public void SetData(List<Light> mActionBeanList,String delimiter, String range) {
        //    this.delimiter = delimiter;
        //    this.range = range;

        //    //tbMain.Clear();
        //    StringBuilder sb = new StringBuilder();
        //    for (int i=0;i< mActionBeanList.Count; i++) {
        //        sb.Append(mActionBeanList[i].Time+",");
        //        if (mActionBeanList[i].Action == 144)
        //        {
        //            sb.Append("o" + ",");
        //        }
        //        else if (mActionBeanList[i].Action == 128)
        //        {
        //            sb.Append("c" + ",");
        //        }
        //        sb.Append(mActionBeanList[i].Position + ",");
        //        sb.Append(mActionBeanList[i].Color + ";");
        //        if (i != mActionBeanList.Count - 1) {
        //            sb.Append(Environment.NewLine);
        //        }
        //    }
        //    //tbMain.Text = sb.ToString();
        //}

    }
}
