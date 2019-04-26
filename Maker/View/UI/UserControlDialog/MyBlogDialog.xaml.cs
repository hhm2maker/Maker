using System;
using System.Windows;

namespace Maker.View.UI.UserControlDialog
{
    /// <summary>
    /// ChangeLanguage.xaml 的交互逻辑
    /// </summary>
    public partial class MyBlogDialog : MakerDialog
    {
        private WelcomeWindow mw;
        public MyBlogDialog(WelcomeWindow mw, String content)
        {
            InitializeComponent();

            this.mw = mw;
            Width = mw.ActualWidth * 0.6;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mw.RemoveDialog();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //System.Diagnostics.Process.Start(@"E:\Sharer\Maker\Maker\bin\Debug\matrix uploader\dfu-util.exe",
            //    "dfu-util -v -d 0203:0100,0203:0003 -t 2048 -a 0 -R -D \" "+ @"E:\Sharer\Maker\Maker\bin\Debug\matrix uploader\MatrixFW 0.1.3.3b 4-25-1.mxfw" + "\"");
         
            System.Diagnostics.Process.Start(@"E:\Sharer\Maker\Maker\bin\Debug\matrixuploader\Matrix Firmware Uploader.bat",
              "\"E:\\Sharer\\Maker\\Maker\\bin\\Debug\\matrixuploader\\MatrixFW 0.1.3.3 b4-25-1.mxfw\"");

            //细节优化。
            //如是否要再次确认
            //echo Make sure Matrix is pluged in. Press Any Key to continue.
        }
    }
}
