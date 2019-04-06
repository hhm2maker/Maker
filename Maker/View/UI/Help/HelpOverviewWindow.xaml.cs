using Maker.View.Dialog;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;


namespace Maker.View.Help
{
    /// <summary>
    /// HelpOverviewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HelpOverviewWindow : System.Windows.Window
    {
        public HelpOverviewWindow(System.Windows.Window window)
        {
            InitializeComponent();
            Owner = window;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbLeft != null) {
                switch (lbLeft.SelectedIndex)
                {
                    case -1:
                        AllCollapsed();
                        lbiNone.Visibility = Visibility.Visible;
                        break;
                    case 0:
                        AllCollapsed();
                        lbiOldHelpDocument.Visibility = Visibility.Visible;
                        lbiInstanceDocument.Visibility = Visibility.Visible;
                        lbiFlowChart.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        AllCollapsed();
                        lbiDeveloperDocumentation.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
        }

        private void AllCollapsed()
        {
            lbiNone.Visibility = Visibility.Collapsed;
            lbiOldHelpDocument.Visibility = Visibility.Collapsed;
            lbiInstanceDocument.Visibility = Visibility.Collapsed;
            lbiFlowChart.Visibility = Visibility.Collapsed;
            lbiDeveloperDocumentation.Visibility = Visibility.Collapsed;
        }
        private void DefaultOpenFlowChart(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\Help\\flowchart.png"))
            {
                new MessageDialog(this, "PictureFilesDoNotExist").ShowDialog();
                return;
            }
            //建立新的系统进程      
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //设置图片的真实路径和文件名      
            process.StartInfo.FileName = System.Windows.Forms.Application.StartupPath + "\\Help\\flowchart.png";
            //设置进程运行参数，这里以最大化窗口方法显示图片。      
            process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
            //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true      
            process.StartInfo.UseShellExecute = true;
            //此处可以更改进程所打开窗体的显示样式，可以不设      
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.Close();
        }

        private void ToHelpWindow(object sender, RoutedEventArgs e)
        {
            if (sender == lbiOldHelpDocument)
            {
                new HelpWindow().Show();
            }
            else if (sender == lbiInstanceDocument)
            {
                wbMain.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"Help\InstanceDocument\InstanceDocument.html");
                //System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Help\InstanceDocument.docx");
            }
            else if (sender == lbiDeveloperDocumentation)
            {
                wbMain.Navigate(AppDomain.CurrentDomain.BaseDirectory + @"Help\DeveloperDocumentation\DeveloperDocumentation.html");
                //System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Operation\开发者文档.docx");
            }
        }

        private void wbMain_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            wbMain.Visibility = Visibility.Visible;
        }
    }
}
