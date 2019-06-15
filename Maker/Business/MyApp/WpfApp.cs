using log4net;
using Maker.View.Control;
using Maker.View.UI;
using Maker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maker.MyApp
{
    public class WpfApp : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //添加Setting资源
            ResourceDictionary settingResourceDictionary = new ResourceDictionary
            {
                Source = new Uri(@"SettingDictionary.xaml", UriKind.Relative)
            };
            //添加英文资源
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(settingResourceDictionary);
            ResourceDictionary englishResourceDictionary = new ResourceDictionary
            {
                Source = new Uri(@"View\Resources\Language\StringResource.xaml", UriKind.Relative)
            };
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(englishResourceDictionary);
            //初始化MVVMLight
            ViewModelLocator viewModelLocator = new ViewModelLocator();
            System.Windows.Application.Current.Resources.Add("Locator", viewModelLocator);

            NewMainWindow mw = new NewMainWindow();
            this.mw = mw;
            //设置主窗口
            MainWindow = mw;
            mw.Show();
            //if (e.Args.Length > 0)
            //    //带文件路径打开
            //    mw.ShowFromFile(e.Args[0]);
            //else
            //    mw.Show();

        }
        public void ShowDocument(String filename)
        {
            //查看App/xaml.cs
        }
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NewMainWindow mw { get; set; }
        public WpfApp()
        {
            // 在异常由应用程序引发但未进行处理时发生。主要指的是UI线程。
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //  当某个异常未被捕获时出现。主要指的是非UI线程
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //可以记录日志并转向错误bug窗口友好提示用户
            if (e.ExceptionObject is System.Exception)
            {
                Exception ex = (System.Exception)e.ExceptionObject;
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //可以记录日志并转向错误bug窗口友好提示用户
            e.Handled = true;
           System.Windows.MessageBox.Show("消息:" + e.Exception.Message + "\r\n" + e.Exception.StackTrace);

            ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Error(e.Exception);
            //mw.ShowMsg();
        }
        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
            //mw.Window_Closing();
        }
    }
}
