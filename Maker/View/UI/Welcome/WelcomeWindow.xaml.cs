using Maker.Business;
using Maker.Business.Currency;
using Maker.Business.Model.Config;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.Help;
using Maker.View.UI.Game;
using Maker.View.UI.UserControlDialog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maker.View.UI
{
    /// <summary>
    /// WelcomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();

            Left = 0;
            Top = 0;
            Height = SystemParameters.WorkArea.Height;//获取屏幕的宽高  使之不遮挡任务栏
            Width = SystemParameters.WorkArea.Width;

            TransformGroup transformGroup = new TransformGroup();
            RotateTransform rotateTransform = new RotateTransform(45);   //其中180是旋转180度
            transformGroup.Children.Add(rotateTransform);
            mBorderLaunchpad.RenderTransform = transformGroup;
            mLaunchpad.SetLaunchpadBackground(new SolidColorBrush(Colors.Transparent));
            //mLaunchpad.SetButtonBorderBackground(3,new SolidColorBrush(Colors.White));
            mLaunchpad.SetButtonBackground(new SolidColorBrush(Colors.White));

            gMain.Width = Width * 0.8;
            gMain.Height = Height * 0.8;

            mLaunchpad.Size = gMain.Width / 6;
            tbDevice.Margin = new Thickness(0, gMain.Height / 8, 0, 0);
            tbHelp.Margin = new Thickness(0, gMain.Height / 8, 0, 0);

            InitStaticConstant();


            //new TestW().Show();
        }

        /// <summary>
        /// 初始化静态常量
        /// </summary>
        public void InitStaticConstant()
        {
            String strColortabPath = AppDomain.CurrentDomain.BaseDirectory + @"Color\color.color";
            StaticConstant.brushList = FileBusiness.CreateInstance().ReadColorFile(strColortabPath);
        }

        public void ShowMakerDialog(MakerDialog makerdialog)
        {
            gMost.Children.Add(new Grid()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            });

            gMost.Children.Add(makerdialog);

            ThicknessAnimation marginAnimation = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, 30, 0, 0),
                //To = new Thickness(0, (ActualHeight - makerdialog.Height) / 2, 0, 0),
                Duration = TimeSpan.FromSeconds(0.3)
            };

            makerdialog.BeginAnimation(MarginProperty, marginAnimation);
        }

        public void RemoveDialog()
        {
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
            gMost.Children.RemoveAt(gMost.Children.Count - 1);
        }
    
       

      
    }
}
