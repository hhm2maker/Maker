using Maker.Model;
using Maker.View.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.UI.Tool.Paved
{
    /// <summary>
    /// ShowPavedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowPavedWindow : Window
    {
        public ShowPavedWindow()
        {
            InitializeComponent();
        }

        private NewMainWindow mw;
        private List<Light> mLightList;
        private int pavedColumns;
        private Dictionary<int, FramePointModel> points;
        private Brush nowBrush;
        public ShowPavedWindow(NewMainWindow mw, List<Light> mLightList)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
            pavedColumns = mw.pavedConfigModel.Columns;
        }

        public ShowPavedWindow(NewMainWindow mw, List<Light> mLightList, int pavedColumns, Dictionary<int, FramePointModel> points,Brush nowBrush)
        {
            InitializeComponent();
            this.mw = mw;

            this.mLightList = mLightList;
            this.pavedColumns = pavedColumns;
            this.points = points;
            this.nowBrush = nowBrush;
        }
        public PavedUserControl puc;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            puc = new PavedUserControl(mw, mLightList, pavedColumns);
            gMain.Children.Add(puc);
            puc.svMain.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            foreach(var bigItem in points)
            foreach (var item in points[bigItem.Key].Texts)
            {
                TextBlock tb = new TextBlock()
                {
                    FontSize = 18,
                    Text = item.Value,
                    Foreground = nowBrush,
                };
                Canvas.SetLeft(tb, item.Point.X);
                Canvas.SetTop(tb, item.Point.Y);
                cMain.Children.Add(tb);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "png(*.png)|*.png"
            };
            var rst = dlg.ShowDialog();
            if (rst == true)
            {
                String fileName = dlg.FileName;
                SaveRTBAsPNG(GetBitmapStream(cMain, 96), fileName);
            }
        }
        public RenderTargetBitmap GetBitmapStream(Canvas canvas, int dpi)
        {
            //Size size = new Size(canvas.Width, canvas.Height);
            //canvas.Measure(size);
            //canvas.Arrange(new Rect(size));
       
             var rtb = new RenderTargetBitmap(
                600, //width
               600 * puc.Count, //height
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

    }
}
