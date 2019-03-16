using Maker.Business;
using Maker.Model;
using Maker.Utils;
using Maker.View.Control;
using Maker.View.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.Dialog
{
    /// <summary>
    /// RecordingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class RecordingDialog : Window
    {
        private List<Light> mActionBeanList;
        public RecordingDialog(MainWindow mw, List<Light> mActionBeanList)
        {
            InitializeComponent();
            Owner = mw;
            this.mActionBeanList = mActionBeanList;

            //FileBusiness file = new FileBusiness();
            //ColorList = file.ReadColorFile(mw.strColortabPath);
        }

        //private int RecordingType = 0;//录制类型 0 - 低帧数 1 - 高帧数
        private List<int> liTime = new List<int>();
        private Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
        //private List<FrameworkElement> lfe = new List<FrameworkElement>();
        private List<String> ColorList = new List<string>();
        private int nowTimePoint = 1;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mLaunchpad.SetSize(600); 
            mLaunchpad.SetLaunchpadBackground(Brushes.White);

            if (dtimer == null)
            {
                dtimer = new System.Windows.Threading.DispatcherTimer();
                dtimer.Interval = TimeSpan.FromSeconds(0.5);
                dtimer.Tick += dtimer_Tick;
            }

            //切割
            mActionBeanList = LightBusiness.Split(mActionBeanList);
            liTime.Clear();
            dic.Clear();
            int time = -1;
            for (int i = 0; i < mActionBeanList.Count; i++)
            {
                if (mActionBeanList[i].Time != time)
                {
                    time = mActionBeanList[i].Time;
                    liTime.Add(time);
                    int[] x = new int[96];
                    for (int j = 0; j < 96; j++)
                    {
                        x[j] = 0;
                    }
                    dic.Add(time, x);
                    if (mActionBeanList[i].Action == 144)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = mActionBeanList[i].Color;
                    }
                    else if (mActionBeanList[i].Action == 128)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = 0;//关闭为黑色
                    }
                }
                else
                {
                    if (mActionBeanList[i].Action == 144)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = mActionBeanList[i].Color;
                    }
                    else if (mActionBeanList[i].Action == 128)
                    {
                        dic[time][mActionBeanList[i].Position - 28] = 0;//关闭为黑色
                    }
                }
            }
            nowTimePoint = 1;
            LoadFrame();
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
        private void ClearFrame()
        {
            //清空
            for (int i = 0; i < 96; i++)
            {
                RoundedCornersPolygon rcp = mLaunchpad.GetButton(i) as RoundedCornersPolygon;
                if (rcp != null)
                    rcp.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
                Ellipse e = mLaunchpad.GetButton(i) as Ellipse;
                if (e != null)
                    e.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
                Rectangle r = mLaunchpad.GetButton(i) as Rectangle;
                if (r != null)
                    r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F4F4F5"));
            }
        }
        System.Windows.Threading.DispatcherTimer dtimer;
        void dtimer_Tick(object sender, EventArgs e)
        {
            SaveCanvas(this, mLaunchpad, 100, AppDomain.CurrentDomain.BaseDirectory + "\\Recording\\Temp\\" + (nowTimePoint).ToString() + ".png");

            if (nowTimePoint > dic.Count - 1)
            {
                dtimer.Stop();
                Rendering();
                return;
            }

            nowTimePoint++;
            LoadFrame();
        }

        private void Rendering()
        {
            //if (RecordingType == 0)
            //{
            //    int width = 750;
            //    int height = 750;
            //    DateTime time = System.DateTime.Now;
            //    //create instance of video writer
            //    VideoFileWriter writer = new VideoFileWriter();
            //    //create new video file
            //    writer.Open("Recording/vedio/" + time.Year + "-" + time.Month + "-" + time.Day
            //        + " " + time.Hour + "." + time.Minute + "." + time.Second
            //        + ".avi", width, height, 96, VideoCodec.MPEG4);
            //    //create a bitmap to save into the video file
            //    int now = liTime[0];
            //    int liTimePosition = 0;
            //    for (int i = 0; i < liTime[liTime.Count - 1]; i++)
            //    {
            //        if (i > now)
            //        {
            //            liTimePosition++;
            //            now = liTime[liTimePosition];
            //        }
            //        System.Drawing.Bitmap image = new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Recording\\Temp\\" + (liTimePosition + 1) + ".png");
            //        writer.WriteVideoFrame(image);
            //    }
            //    //DirectoryInfo TheFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Recording\\Temp");
            //    //foreach (FileInfo NextFile in TheFolder.GetFiles())
            //    //{
            //    //    int nIndex = NextFile.Name.LastIndexOf('.');
            //    //    String filename = NextFile.Name;
            //    //    if (nIndex >= 0)
            //    //    {
            //    //        filename = filename.Substring(nIndex);
            //    //        if (filename.Equals(".png"))
            //    //        {
            //    //            System.Drawing.Bitmap image = new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Recording\\Temp\\" + NextFile.Name);
            //    //            //NextFile.Name
            //    //            writer.WriteVideoFrame(image);
            //    //        }
            //    //    }
            //    //}
            //    writer.Close();
            //}
     
                DateTime time = System.DateTime.Now;
                //org.loon.util.FileUtil file = new org.loon.util.FileUtil();
                AVIWriter aviWriter = new AVIWriter();
                //ps:avi中所有图像皆不能小于width及height
                System.Drawing.Bitmap avi_frame = aviWriter.Create(AppDomain.CurrentDomain.BaseDirectory + "/Recording/vedio/" + time.Year + "-" + time.Month + "-" + time.Day
                    + " " + time.Hour + "." + time.Minute + "." + time.Second
                    + ".avi", 96, 750, 750);
                int now = liTime[0];
                int liTimePosition = 0;

                for (int i = 0; i < liTime[liTime.Count - 1]; i++)
                {
                    if (i > now)
                    {
                        liTimePosition++;
                        now = liTime[liTimePosition];
                    }
                    //获得图像
                    System.Drawing.Bitmap image = new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\Recording\\Temp\\" + (liTimePosition + 1) + ".png");

                    //由于转化为avi后呈现相反，所以翻转
                    image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
                    //载入图像
                    aviWriter.LoadFrame(image);
                    aviWriter.AddFrame();
                }

                //释放资源
                aviWriter.Close();
                avi_frame.Dispose();
        }

        //private void btnLowFramesRecording_Click(object sender, RoutedEventArgs e)
        //{
        //    InitRecording();
        //    RecordingType = 0;
        //}

        private void btnHighFramesRecording_Click(object sender, RoutedEventArgs e)
        {
            InitRecording();
            //RecordingType = 1;
        }

        private void InitRecording()
        {
            //删除文件夹内所有文件
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Recording\\Temp");
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)            //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName);      //删除指定文件
                }
            }

            nowTimePoint = 1;
            LoadFrame();
            dtimer.Start();
        }

        public static void SaveCanvas(Window window, Canvas canvas, int dpi, string filename)
        {
            Size size = new Size(window.Width, window.Height);
            canvas.Measure(size);
            //canvas.Arrange(new Rect(size));

            var rtb = new RenderTargetBitmap(
                 //(int)window.Width, //width
                 //(int)window.Height, //height
                 750, //width
                750, //height
                dpi, //dpi x
                dpi, //dpi y
                PixelFormats.Pbgra32 // pixelformat
                );
            rtb.Render(canvas);

            SaveRTBAsPNG(rtb, filename);
        }
        private static void SaveRTBAsPNG(RenderTargetBitmap bmp, string filename)
        {
            var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));

            using (var stm = System.IO.File.Create(filename))
            {
                enc.Save(stm);
            }
        }

    }
}
