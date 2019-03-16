using Maker.Business;
using Maker.View.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Maker.View.Dialog
{
    /// <summary>
    /// ImportPictureDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ImportPictureDialog : Window
    {
        private  String _imagePath;
        private MainWindow mw;
        public ImportPictureDialog(MainWindow mw, String imagePath)
        {
            InitializeComponent();
            _imagePath = imagePath;
            Owner = mw;
            this.mw = mw;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri(_imagePath));
            //b.ImageSource = new BitmapImage(new Uri(tbBgPath.Text));
            mLaunchpad.Background = b;

            //FileBusiness file = new FileBusiness();
            //ColorList = file.ReadColorFile(mw.strColortabPath);
            for (int x = 0; x < ColorList.Count; x++)
            {
                _ColorList.Add(NumToColor(x));
            }

            mLaunchpad.SetSize(750);
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(System.Windows.Media.Color.FromArgb(0,83,83,83)));
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(_imagePath, true);
            img = KiResizeImage(img,750, 750);

            for (int i = 0; i < mLaunchpad.Children.Count; i++)
            {
                FrameworkElement fe = (FrameworkElement)mLaunchpad.Children[i];
                dictonary.Clear();
                //X
                for (Double j = Canvas.GetLeft(fe); j <= Canvas.GetLeft(fe) + fe.ActualWidth; j++)
                {
                    //Y
                    for (Double k = Canvas.GetTop(fe); k <= Canvas.GetTop(fe) + fe.ActualHeight; k++)
                    {
                        System.Drawing.Color pixelColor = img.GetPixel((int)j, (int)k);
                        if (dictonary.ContainsKey(pixelColor))
                        {
                            dictonary[pixelColor]++;
                        }
                        else
                        {
                            dictonary.Add(pixelColor, 1);
                        }
                        //Console.WriteLine(pixelColor.R+"---"+pixelColor.G+"---"+pixelColor.B);
                    }
                }
                Dictionary<System.Drawing.Color, int> _dictonary = dictonary.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, o => o.Value);
                Dictionary<int, int> mDictonary = new Dictionary<int, int>();
                //Console.WriteLine(_dictonary.First().Key);
                foreach (var item in _dictonary)
                {
                    int _position = 0;
                    double cha = 1000;
                    for (int x = 0; x < _ColorList.Count; x++)
                    {
                        //Double nowCha = Math.Abs(item.Key.GetHue() - _ColorList[x].GetHue());
                        Double nowCha = Math.Abs(item.Key.GetHue() - _ColorList[x].GetHue()) + Math.Abs(item.Key.GetSaturation() - _ColorList[x].GetSaturation()) + Math.Abs(item.Key.GetBrightness() - _ColorList[x].GetBrightness());
                        if (nowCha < cha)
                        {
                            cha = nowCha;
                            _position = x;
                        }
                    }
                    if (mDictonary.ContainsKey(_position + 1))
                    {
                        mDictonary[_position + 1] += item.Value;
                    }
                    else
                    {
                        mDictonary.Add(_position + 1, item.Value);
                    }
                }
                Dictionary<int, int> _mDictonary = mDictonary.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, o => o.Value);

                nowColor.Add(_mDictonary.First().Key - 1);
                System.Drawing.Color color = NumToColor(_mDictonary.First().Key - 1);
                mLaunchpad.SetButtonBackground(i, new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, color.R, color.G, color.B)));
                //Console.WriteLine(dictonary.First().Key.R+"---"+ dictonary.First().Key.G+ "---"+dictonary.First().Key.B);
            }
        }
        
        Dictionary<System.Drawing.Color, int> dictonary = new Dictionary<System.Drawing.Color, int>();
        List<int> nowColor = new List<int>();

        private List<String> ColorList = new List<string>();
        private List<System.Drawing.Color> _ColorList = new List<System.Drawing.Color>();

        /// <summary>
        /// 数字转笔刷
        /// </summary>
        /// <param name="i">颜色数值</param>
        /// <returns>SolidColorBrush笔刷</returns>
        private System.Drawing.Color NumToColor(int i)
        {
            return System.Drawing.ColorTranslator.FromHtml(ColorList[i]);
        }

        public static System.Drawing.Bitmap KiResizeImage(System.Drawing.Bitmap bmp, int newW, int newH)
        {
            try
            {
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(newW, newH);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, newW, newH), new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int noColorPosition = int.Parse(tbNoColor.Text);
            int intNoColor = nowColor[noColorPosition];
            for (int i = 0; i < nowColor.Count; i++)
            {
                if (nowColor[i] == intNoColor)
                {
                    mLaunchpad.SetButtonBackground(i, new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 245)));
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int noColorPosition = int.Parse(tbNoColor.Text);
            int intNoColor = nowColor[noColorPosition];
            int intMyColor = int.Parse(tbNoColor.Text);

            for (int i = 0; i < nowColor.Count; i++)
            {
                if (nowColor[i] == intNoColor)
                {
                    mLaunchpad.SetButtonBackground(i, new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 245)));
                }
                else
                {
                    //mLaunchpad.SetButtonBackground(i, new SolidColorBrush(NumToColor(intMyColor - 1)));

                }
            }
        }
    }
}
