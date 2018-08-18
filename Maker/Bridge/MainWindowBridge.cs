using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.ViewBusiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using static Maker.Model.EnumCollection;

namespace Maker.Bridge
{
    public class MainWindowBridge
    {
        /// <summary>
        /// 窗口
        /// </summary>
        MainWindow window;
        /// <summary>
        /// 文件操作业务类
        /// </summary>
        FileBusiness fileBusiness = new FileBusiness();
        /// <summary>
        /// 传入窗口后续的业务由桥梁来连接业务和窗口
        /// </summary>
        /// <param name="mw"></param>
        public MainWindowBridge(MainWindow mw) {
            this.window = mw;
        }
        /// <summary>
        /// 获取语言
        /// </summary>
        public void GetLanguage() {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/language.xml");
            XmlNode languageRoot = doc.DocumentElement;
            XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
            window.strMyLanguage = languageMyLanguage.InnerText;
        }
        /// <summary>
        /// 加载语言
        /// </summary>
        public void LoadLanguage()
        {
            GetLanguage();
            if (window.strMyLanguage.Equals("zh-CN"))
            {
                ResourceDictionary dict = new ResourceDictionary
                {
                    Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative)
                };
                System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
                //System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(System.Windows.Application.Current.Resources.MergedDictionaries.Count - 1);
            }
            else if (window.strMyLanguage.Equals("en-US")) { }
            else
            {
                String mLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name;
                if (mLanguage.Equals("zh-CN"))
                {
                    ResourceDictionary dict = new ResourceDictionary
                    {
                        Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative)
                    };
                    System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
                }
                LanguageDialog dialog = new LanguageDialog();
                dialog.ShowDialog();

                XmlDocument doc = new XmlDocument();
                doc.Load("Config/language.xml");
                XmlNode languageRoot = doc.DocumentElement;
                XmlNode languageMyLanguage = languageRoot.SelectSingleNode("MyLanguage");
                if (dialog.cbValue.SelectedIndex == 0)
                {
                    languageMyLanguage.InnerText = "zh-CN";
                    window.strMyLanguage = "zh-CN";
                    ResourceDictionary dict = new ResourceDictionary
                    {
                        Source = new Uri(@"Resources\StringResource_zh-CN.xaml", UriKind.Relative)
                    };
                    System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
                }
                else if (dialog.cbValue.SelectedIndex == 1)
                {
                    languageMyLanguage.InnerText = "en-US";
                    window.strMyLanguage = "en-US";
                    ResourceDictionary dict = new ResourceDictionary
                    {
                        Source = new Uri(@"Resources\StringResource.xaml", UriKind.Relative)
                    };
                    System.Windows.Application.Current.Resources.MergedDictionaries[1] = dict;
                }
                doc.Save("Config/language.xml");
            }
        }
        ///// <summary>
        ///// 导入Midi文件
        ///// </summary>
        ///// <param name="filePath"></param>
        //public void ImportMidi(String filePath) {
        //    List<Light> list = fileBusiness.ReadMidiFile(filePath);
        //    window.mActionBeanList = list;
        //    if (window.mode == MainWindowMode.None  )
        //    {
        //        window.ToInputUserControl();
        //    }
        //    else
        //    {   
        //        window.SetDataToChildren();
        //    }
        //}
     /// <summary>
     /// 导出Midi文件
     /// </summary>
     /// <param name="filePath"></param>
     /// <param name="mActionBeanList"></param>
        public void ExportMidi(String filePath, String fileName,List<Light> mActionBeanList,bool isWriteToFile) {
            if (isWriteToFile)
            {
                fileBusiness.WriteMidiFile(filePath, fileName, mActionBeanList, isWriteToFile);
            }
            else {
                fileBusiness.WriteMidiFile(filePath, mActionBeanList);
            }
        }
        /// <summary>
        /// 导出Light文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mActionBeanList"></param>
        public void ExportLight(String filePath, List<Light> mActionBeanList)
        {
            fileBusiness.WriteLightFile(filePath, mActionBeanList);
        }
        /// <summary>
        /// 系统默认方式打开图片文件
        /// </summary>
        /// <param name="imgPath">图片文件路径</param>
        public void DefaultOpenImgFile(String imgPath) {
            if (!File.Exists(imgPath)) {
                new MessageDialog(window, "PictureFilesDoNotExist").ShowDialog();
                return;
            }
            //建立新的系统进程      
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //设置图片的真实路径和文件名      
            process.StartInfo.FileName = imgPath;
            //设置进程运行参数，这里以最大化窗口方法显示图片。      
            process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
            //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true      
            process.StartInfo.UseShellExecute = true;
            //此处可以更改进程所打开窗体的显示样式，可以不设      
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.Close();
        }
        /// <summary>
        /// 检查Save文件
        /// </summary>
        /// <param name="saveFilePath">Save文件路径</param>
        /// <returns></returns>
        public String CheckSaveFile(String saveFilePath)
        {
            return fileBusiness.CheckSaveFile(saveFilePath);
        }
        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="exeFilePath">可执行文件路径</param>
        /// <param name="description">描述</param>
        public void CreateShortCut(String exeFilePath,String description) {
            string DesktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);//得到桌面文件夹  
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(DesktopPath + "\\Maker.lnk");
            shortcut.TargetPath = exeFilePath;
            shortcut.Arguments = "";// 参数  
            shortcut.Description = description;
            shortcut.WorkingDirectory = Path.GetDirectoryName(exeFilePath);//程序所在文件夹，在快捷方式图标点击右键可以看到此属性  
            //shortcut.IconLocation = @"D:\software\cmpc\zy.exe,0";//图标  
            shortcut.Hotkey = "CTRL+SHIFT+Z";//热键  
            shortcut.WindowStyle = 1;
            shortcut.Save();
        }
        /// <summary>
        /// 设置MainWindow全屏
        /// </summary>
        public void SetFullScreen()
        {
            GeneralViewBusiness.SetFullScreen(window);
        }
        /// <summary>
        /// 设置MainWindow全屏
        /// </summary>
        public void SetPercentageOfScreen(double percentage)
        {
            GeneralViewBusiness.SetPercentageOfScreen(window,percentage);
        }
        /// <summary>
        /// 获取历史记录列表
        /// </summary>
        /// <returns></returns>
        public List<HistoricalModel> GetHistorical()
        {
            List<HistoricalModel> historicals = new List<HistoricalModel>();
            XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Config\historical.xml");
            foreach (XElement element in doc.Element("Historical").Elements())
            {
                //var name = element.Element("name").Value;
                historicals.Add(new HistoricalModel(element.Attribute("path").Value));
            }
            return historicals;
        }
        /// <summary>
        /// 获取第三方插件列表
        /// </summary>
        /// <returns></returns>
        public List<ThirdPartyModel> GetThirdParty()
        {
            List<ThirdPartyModel> thirdPartys = new List<ThirdPartyModel>();
            XDocument doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Operation\DetailedList.xml");
            foreach (XElement element in doc.Element("Operations").Elements())
            {
                //var name = element.Element("name").Value;
                thirdPartys.Add(new ThirdPartyModel(element.Attribute("name").Value,
                    element.Attribute("entext").Value,
                    element.Attribute("zhtext").Value,
                    element.Attribute("view").Value,
                    element.Attribute("dll").Value));
            }
            return thirdPartys;
        }
        /// <summary>
        /// 将第三方扩展加载到界面
        /// </summary>
        /// <param name="clickEvent"></param>
        /// <returns></returns>
        public void InitThirdParty(List<ThirdPartyModel> thirdPartys, RoutedEventHandler clickEvent)
        {
            if (thirdPartys.Count != 0)
            {
                List<String> strs = new List<string>();
                if (window.strMyLanguage.Equals("en-US"))
                {
                    foreach (var item in thirdPartys)
                    {
                        strs.Add(item.entext);
                    }
                }
                else if (window.strMyLanguage.Equals("zh-CN"))
                {
                    foreach (var item in thirdPartys)
                    {
                        strs.Add(item.zhtext);
                    }
                }
                GeneralViewBusiness.SetStringsAndClickEventToMenuItem(window.iuc.miChildThirdParty, strs, clickEvent,false,14);
            }
        }
        public void ThirdPartysMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //不是输入模式或者没有可操作的步骤
            if (window.mode != MainWindowMode.Input || window.iuc.lbStep.SelectedIndex == -1)
                return;
            String viewString = window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].view;
            if (viewString.Equals(String.Empty))
            {
                //不需要View
                for (int k = 0; k < window.iuc.lbStep.SelectedItems.Count; k++)
                {
                    StackPanel sp = (StackPanel)window.iuc.lbStep.SelectedItems[k];
                    //没有可操作的灯光组
                    if (!window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)].Contains(window.iuc.GetStepName(sp) + "LightGroup"))
                    {
                        continue;
                    }
                    String command = String.Empty;
                    command = Environment.NewLine + "\t" + window.iuc.GetStepName(sp) + "LightGroup = Edit." + window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].name + "(" + window.iuc.GetStepName(sp) + "LightGroup);";
                    window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)] += command;
                }
                window.iuc.RefreshData();
            }
            else
            {
                ThirdPartyDialog dialog = new ThirdPartyDialog(window, viewString);
                if (window.strMyLanguage.Equals("en-US"))
                {
                    dialog.Title = window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].entext;
                }
                else if (window.strMyLanguage.Equals("zh-CN"))
                {
                    dialog.Title = window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].zhtext;
                }
                if (dialog.ShowDialog() == true)
                {
                    for (int k = 0; k < window.iuc.lbStep.SelectedItems.Count; k++)
                    {
                        StackPanel sp = (StackPanel)window.iuc.lbStep.SelectedItems[k];
                        //没有可操作的灯光组
                        if (!window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)].Contains(window.iuc.GetStepName(sp) + "LightGroup"))
                        {
                            continue;
                        }
                        String command = Environment.NewLine + "\t" + window.iuc.GetStepName(sp) + "LightGroup = Edit." + window.thirdPartys[window.iuc.miChildThirdParty.Items.IndexOf(sender)].name + "(" + window.iuc.GetStepName(sp) + "LightGroup" + dialog.result + ");";
                        window.iuc.lightScriptDictionary[window.iuc.GetStepName(sp)] += command;
                    }
                    window.iuc.RefreshData();
                }
            }
        }
       
        ///// <summary>
        ///// 添加指定文件的打开模式 移至Tool项目
        ///// </summary>
        ///// <param name="fileFormat">文件格式(".mid")</param>
        //public void AddTheOpenModeOfTheSpecifiedFile(String fileFormat)
        //{
        //    try
        //    {
        //        string strExtension = fileFormat;
        //        string strProject = "Maker";
        //        //删除
        //        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(strExtension, false);
        //        Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(strExtension).SetValue("", strProject, Microsoft.Win32.RegistryValueKind.String);
        //        using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(strProject))
        //        {
        //            //设置默认图标
        //            //Microsoft.Win32.RegistryKey iconKey = key.CreateSubKey("DefaultIcon");
        //            //iconKey.SetValue("", System.Windows.Forms.Application.StartupPath + @"\Images\midifile.ico");
        //            string strExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        //            strExePath = Path.GetDirectoryName(strExePath);
        //            strExePath += @"\Maker.exe";
        //            key.CreateSubKey(@"Shell\Open\Command").SetValue("", strExePath + " \"%1\"", Microsoft.Win32.RegistryValueKind.ExpandString);
        //        }
        //    }
        //    catch
        //    {
        //        new MessageDialog(window, "NeedAdministrator").ShowDialog();
        //    }
        //}
    }
}
