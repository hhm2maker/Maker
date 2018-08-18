using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AddTheOpenModeOfTheSpecifiedFile(".makerpj");
            Close();
        }

        /// <summary>
        /// 添加指定文件的打开模式 
        /// </summary>
        /// <param name="fileFormat">文件格式(".mid")</param>
        public void AddTheOpenModeOfTheSpecifiedFile(String fileFormat)
        {
            try
            {
                string strExtension = fileFormat;
                string strProject = "Maker";
                //删除
                try {Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(strExtension);}catch { }
                Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(strExtension).SetValue("", strProject, Microsoft.Win32.RegistryValueKind.String);
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(strProject))
                {
                    //设置默认图标
                    //Microsoft.Win32.RegistryKey iconKey = key.CreateSubKey("DefaultIcon");
                    //iconKey.SetValue("", System.Windows.Forms.Application.StartupPath + @"\Images\midifile.ico");
                    string strExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    strExePath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(strExePath));
                    strExePath += @"\Maker.exe";
                    Console.WriteLine(strExePath);
                    key.CreateSubKey(@"Shell\Open\Command").SetValue("", strExePath + " \"%1\"", Microsoft.Win32.RegistryValueKind.ExpandString);
                }
                System.Windows.Forms.MessageBox.Show("Success_成功!");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Fail_失败!");
            }
        }
    }
}
