using Maker.Business;
using Maker.Model;
using Maker.View.Control;
using Maker.View.Dialog;
using Maker.ViewBusiness;
using Operation;
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
        /// 传入窗口后续的业务由桥梁来连接业务和窗口
        /// </summary>
        /// <param name="mw"></param>
        public MainWindowBridge(MainWindow mw) {
            this.window = mw;
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
                Business.FileBusiness.CreateInstance().WriteMidiFile(filePath, fileName, mActionBeanList, isWriteToFile);
            }
            else {
                Business.FileBusiness.CreateInstance().WriteMidiFile(filePath, mActionBeanList);
            }
        }
        /// <summary>
        /// 导出Light文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="mActionBeanList"></param>
        public void ExportLight(String filePath, List<Light> mActionBeanList)
        {
            Business.FileBusiness.CreateInstance().WriteLightFile(filePath, mActionBeanList);
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
            return Business.FileBusiness.CreateInstance().CheckSaveFile(saveFilePath);
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
            GeneralOtherViewBusiness.SetFullScreen(window);
        }
        /// <summary>
        /// 设置MainWindow全屏
        /// </summary>
        public void SetPercentageOfScreen(double percentage)
        {
            GeneralOtherViewBusiness.SetPercentageOfScreen(window,percentage);
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
       
       
       
    }
}
