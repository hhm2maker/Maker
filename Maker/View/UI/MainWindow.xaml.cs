using Maker.Bridge;
using Maker.Business;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.Dialog.Online;
using Maker.View.Help;
using Maker.View.Online;
using Maker.View.Setting;
using Maker.View.Test;
using Maker.View.Tool;
using Maker.View.User.Login;
using Maker.View.Work;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using static Maker.Model.EnumCollection;

namespace Maker.View.Control
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 桥梁
        /// </summary>
        public MainWindowBridge bridge;
        public MainWindow()
        {
            //.NET平台自动执行 初始化控件等
            InitializeComponent();
            //设置工作路径
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //初始化桥梁
            bridge = new MainWindowBridge(this);
          
            //设置全屏
            bridge.SetPercentageOfScreen(1);
            //初始化主窗口
            InitMainWindow();
            //加载第三方扩展
            thirdPartys = bridge.GetThirdParty();
            bridge.InitThirdParty(thirdPartys, bridge.ThirdPartysMenuItem_Click);
            //加载历史记录
            historicals = bridge.GetHistorical();
        }

      

        /// <summary>
        /// 欢迎用户控件(起始页) 构造函数处初始化
        /// </summary>
        private WelcomeUserControl wuc;
        /// <summary>
        /// 初始化主窗口
        /// </summary>
        private void InitMainWindow()
        {
            iuc = new InputUserControl(this);
            mcuc = new MainControlUserControl(this);
            puc = new PageUserControl(this);
            //playuc = new PlayUserControl(this);
            mpjuc = new MakerpjUserControl(this);

            wuc = new WelcomeUserControl(this);
            mainDockPanel.Children.Add(wuc);
        }
        /// <summary>
        /// 第三方插件列表
        /// </summary>
        public List<ThirdPartyModel> thirdPartys = new List<ThirdPartyModel>();
        /// <summary>
        /// 历史记录列表
        /// </summary>
        public List<HistoricalModel> historicals = new List<HistoricalModel>();
        /// <summary>
        /// 模式
        /// </summary>
        public MainWindowMode mode = MainWindowMode.None;
        /// <summary>
        /// 最大化还是向下还原
        /// </summary>
        public bool minMax = false;
        /// <summary>
        /// LightScript用户控件 构造函数处初始化
        /// </summary>
        public InputUserControl iuc;
         /// <summary>
        /// Light用户控件 构造函数处初始化
        /// </summary>
        public MainControlUserControl mcuc;
        /// <summary>
        /// Page用户控件 构造函数处初始化
        /// </summary>
        public PageUserControl puc;
        /// <summary>
        /// Makerpj用户控件 构造函数处初始化
        /// </summary>
        public MakerpjUserControl mpjuc;
        /// <summary>
        /// Page用户控件 构造函数处初始化
        /// </summary>
        public PlayUserControl playuc;
        /// <summary>
        /// 灯光结果
        /// </summary>
        public List<Light> mActionBeanList = new List<Light>();
        /// <summary>
        /// 设备列表
        /// </summary>
        public Dictionary<string, PlayerUserControl> deviceDictionary = new Dictionary<string, PlayerUserControl>();
       /// <summary>
       /// 窗体加载完成事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //隐藏主窗口
            Hide();
            //打开加载窗口
            if (new LoadWindow(this).ShowDialog() == true)
            {
                Show();//显示主窗口
                if (bIsShowTip)
                {
                    new TipDialog(this).Show();
                }
                //InputPort.Hello();
            }
            else {Close();}

            //icMain.DefaultDrawingAttributes.Color = Color.FromArgb(255,255,0,0);
            icMain.DefaultDrawingAttributes.Color = colorInkCanvas;
            ImageBrush imgBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Attachments\position.png", UriKind.Absolute))
            };
            icMain.Background = imgBrush;
            icMain.ForceCursor = true;
            MatchSize();

            NewMainWindow newMainWindow = new NewMainWindow();
            newMainWindow.Show();
        }
        private void MatchSize()
        {
            icMain.Width = icMain.Height = iInkCanvasSize;
            //playuc.Width = mainDockPanel.ActualWidth;
            //playuc.Height = mainDockPanel.ActualHeight;
            //Width = mw.dInkCanvasSize + 35 + gbEditingMode.ActualWidth;
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            icMain.Strokes.Clear();
        }

        private void ChangeEditingMode(object sender, RoutedEventArgs e)
        {
            if (sender == rbInk)
            {
                icMain.EditingMode = InkCanvasEditingMode.Ink;
            }
            else if (sender == rbEraseByPoint)
            {
                icMain.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else if (sender == rbEraseByStroke)
            {
                icMain.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }
        }
      
        private void ToDisplayMouse(object sender, RoutedEventArgs e)
        {
            icMain.ForceCursor = !icMain.ForceCursor;
        }
        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog
            {
                AllowFullOpen = true,
                FullOpen = true,
                Color = System.Drawing.Color.FromArgb(colorInkCanvas.R, colorInkCanvas.G, colorInkCanvas.B)
            };
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //获取数据
                byte b = colorInkCanvas.A;
                colorInkCanvas = Color.FromArgb(b, cd.Color.R, cd.Color.G, cd.Color.B);
                //更新数据
                //颜色
                icMain.DefaultDrawingAttributes.Color = colorInkCanvas;
            }
        }
        private void ChangeOpacity(object sender, RoutedEventArgs e)
        {
            GetNumberDialog dialog = new GetNumberDialog(this, "OpacityColon", false, colorInkCanvas.A);
            if (dialog.ShowDialog() == true)
            {
                int number = dialog.OneNumber;
                if (number > 255)
                {
                    colorInkCanvas.A = Convert.ToByte(255);
                }
                else if (number < 0)
                {
                    colorInkCanvas.A = Convert.ToByte(0);
                }
                else
                {
                    colorInkCanvas.A = Convert.ToByte(number);
                }
                icMain.DefaultDrawingAttributes.Color = colorInkCanvas;
            }
        }
        private void ChangeSize(object sender, RoutedEventArgs e)
        {
            GetNumberDialog dialog = new GetNumberDialog(this, "ImageSizeColon", false, iInkCanvasSize);
            if (dialog.ShowDialog() == true)
            {
                int number = dialog.OneNumber;
                if (number <= 0)
                {
                    iInkCanvasSize = 500;
                }
                else
                {
                    iInkCanvasSize = number;
                }
                MatchSize();
            }
        }


        public void InitExternalFile()
        {
        
        }

        private void CloseWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }

        private void MinimizeWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void MaximizeWindow() {
            if (minMax)
            {
                bridge.SetPercentageOfScreen(1);// 全屏设置  
                imageMinMax.Source = new BitmapImage(new Uri("pack://application:,,,../Image/minmax.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                bridge.SetPercentageOfScreen(0.9);// 0.9屏设置  
                imageMinMax.Source = new BitmapImage(new Uri("pack://application:,,,../Image/max.png", UriKind.RelativeOrAbsolute));
            }
            minMax = !minMax;
        }
        private void MaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            MaximizeWindow();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window_Closing();
        }
        public void Window_Closing() {
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/inkcanvas.xml");
            XmlNode inkcanvasRoot = doc.DocumentElement;
            XmlNode inkcanvasColor = inkcanvasRoot.SelectSingleNode("Color");
            inkcanvasColor.InnerText = colorInkCanvas.A + "," + colorInkCanvas.R + "," + colorInkCanvas.G + "," + colorInkCanvas.B;
            XmlNode inkcanvasImageSize = inkcanvasRoot.SelectSingleNode("ImageSize");
            inkcanvasImageSize.InnerText = iInkCanvasSize.ToString();
            doc.Save("Config/inkcanvas.xml");

            if (mode == MainWindowMode.Input)
            {
                iuc.RefreshData(true);
            }
            SaveProjectConfig();
            //ClearCache();
            Environment.Exit(0);
        }

        private void SaveProjectConfig()
        {
            if (lastProjectPath.Equals(String.Empty))
                return;
            DirectoryInfo d = new DirectoryInfo(lastProjectPath);
            XDocument doc = XDocument.Load(lastProjectPath + @"\" + d.Name + ".makerpj");
            XElement project = doc.Root;
            project.Attribute("tutorial").Value = tutorial;

            XElement files = project.Element("Files");
            foreach (TreeViewItem item in tvProject.Items) {
                StackPanel sp = item.Header as StackPanel;
                TextBlock tb = sp.Children[1] as TextBlock; 
                XElement open = files.Element(tb.Text);
                if (open != null) {
                    if (item.IsExpanded)
                    {
                        open.Attribute("open").Value = "true";
                    }
                    else
                    {
                        open.Attribute("open").Value = "false";
                    }
                    //其他属性
                    if (tb.Text.Equals("Page")) {
                        open.Attribute("first").Value = firstPageName;
                    }
                }
            }
            doc.Save(lastProjectPath + @"\" + d.Name + ".makerpj");
        }

        public void CanImportToDevice() {
            btnImportPlayer.Foreground = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            miRecording.IsEnabled = true;
            miImport.IsEnabled = true;
            miExport.IsEnabled = true;
        }
        public void NotCanImportToDevice()
        {
            btnImportPlayer.Foreground = new SolidColorBrush(Color.FromArgb(255, 111, 111, 111));
            miRecording.IsEnabled = false;
            miImport.IsEnabled = false;
            miExport.IsEnabled = false;
        }
        private void CloseSomething()
        {
            //关闭某些东西
            //比如演奏界面的midi连接
            ClosePlayUserControl();
        }

        private void ClosePlayUserControl()
        {
            if(mode == MainWindowMode.Play) {
                //playuc.CloseMidiConnect();
            }
        }

        public void ToInputUserControl()
        {
            CloseSomething();
            CanImportToDevice();
            mode = MainWindowMode.Input;
            mainDockPanel.Children.Clear();
            mainDockPanel.Children.Add(iuc);
        }

        public void ToMainControlUserControl()
        {
            CloseSomething();
            CanImportToDevice();
            mode = MainWindowMode.Light;
            mainDockPanel.Children.Clear();
            mainDockPanel.Children.Add(mcuc);
        }
        public void ToPageUserControl()
        {
            CloseSomething();
            NotCanImportToDevice();
            mode = MainWindowMode.Page;
            mainDockPanel.Children.Clear();
            mainDockPanel.Children.Add(puc);
        }
        public void ToMakerpjUserControl()
        {
            CloseSomething();
            NotCanImportToDevice();
            mode = MainWindowMode.Makerpj;
            mainDockPanel.Children.Clear();
            mainDockPanel.Children.Add(mpjuc);
        }
        public void ToPlayUserControl()
        {
            DirectoryInfo d = new DirectoryInfo(lastProjectPath);
            if (!File.Exists(lastProjectPath + @"\" + d.Name + ".makerpl")) {
                return;
            }
            CloseSomething();
            NotCanImportToDevice();
            mode = MainWindowMode.Play;
            mainDockPanel.Children.Clear();
           // mainDockPanel.Children.Add(playuc);
        }
        private void ToNoneUserControl(bool isToHomePage)
        {
            CloseSomething();
            NotCanImportToDevice();
            mode = MainWindowMode.None;
            mainDockPanel.Children.Clear();
            if (isToHomePage) {
                wuc.LoadHistorical();
                mainDockPanel.Children.Add(wuc);
            }
            else {
                TextBlock tb = new TextBlock
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                tb.SetResourceReference(TextBlock.TextProperty, "NoFileWasOpened");
                tb.FontSize = 20;
                tb.FontWeight = FontWeights.Bold;
                tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6E6E6"));
                mainDockPanel.Children.Add(tb);
            }

            TreeViewItem item =  (TreeViewItem)tvProject.SelectedItem;
            if (item != null)
            { 
                item.IsSelected = false;
            }
        }

        private void GoToFile(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi;
            if (isSelectFileToFile)
            {
               psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe")
                {

                    Arguments = "/e,/select," + lightScriptFilePath
                };
            }
            else {
                psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe")
                {
                    Arguments = "/e,/select," + folderPath
                };
            }
            System.Diagnostics.Process.Start(psi);
        }

        private void ToSettingWindow(object sender, RoutedEventArgs e)
        {
            ToSettingWindow();
        }
        public void ToSettingWindow()
        {
            //new SettingWindow(this).ShowDialog();
        }
        private void SystemTool(object sender, EventArgs e)
        {
            if (sender == btnCalc)
            {
                System.Diagnostics.Process.Start("Calc");
            }
            if (sender == btnNotepad)
            {
                System.Diagnostics.Process.Start("Notepad");
            }
            if (sender == btnMspaint)
            {
                System.Diagnostics.Process.Start("Mspaint");
            }
        }
        private void CheckSaveFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = @"Save 文件|*"
            };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String result = bridge.CheckSaveFile(ofd.FileName);
                new MessageDialog(this,result,0).ShowDialog();
                //System.Windows.Forms.MessageBox.Show(result, "Save文件校验");
            }
        }

        private void OpenCalcTime(object sender, RoutedEventArgs e)
        {
            new CalcTimeWindow().Show();
        }

        private void DefaultOpenPositionTab(object sender, RoutedEventArgs e)
        {
            bridge.DefaultOpenImgFile(System.Windows.Forms.Application.StartupPath + "\\Attachments\\position.png");
        }
        private void DefaultOpenColorTab(object sender, RoutedEventArgs e)
        {
            bridge.DefaultOpenImgFile(System.Windows.Forms.Application.StartupPath + "\\Attachments\\colortab.png");
        }
   

        public void ProjectDocument_SelectionChanged_Light()
        {
            if (mode != MainWindowMode.Light)
            {
                ToMainControlUserControl();
            }
            mcuc.LoadFileData(lightScriptFilePath);
        }
        public void ProjectDocument_SelectionChanged_Page()
        {
            if (mode != MainWindowMode.Page)
            {
                ToPageUserControl();
            }
            puc.LoadFileData(lightScriptFilePath);
        }
        public void ProjectDocument_SelectionChanged_Makerpj()
        {
            if (mode != MainWindowMode.Makerpj)
            {
                ToMakerpjUserControl();
            }
        }
        public void ProjectDocument_SelectionChanged_LightScript()
        {
            if (mode != MainWindowMode.Input)
            {
                ToInputUserControl();
            }
            if (_bIsEdit)
            {
                _bIsEdit = false;
            }
            else
            {
                //清除缓存/Cache
                //ClearCache();
                //iNowPosition = -1;
            }

            //读取灯光文件
            String filePath = lightScriptFilePath;
            FileStream f2 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            int i = 0;
            StringBuilder sb2 = new StringBuilder();
            while ((i = f2.ReadByte()) != -1)
            {
                sb2.Append((char)i);
            }
            f2.Close();
            String linShi = sb2.ToString();
            //linShi = linShi.Replace("\n", "");
            //linShi = linShi.Replace("\r", "");
            if (mode == MainWindowMode.Input)
            {
                ClearInputUserControl();
                String linShi2 = linShi;
                if (linShi2.Trim().Equals(String.Empty))
                {
                    iuc.UpdateData(new List<Light>());
                    //仍需要保存到Cache
                //TODO    
                //LightScriptBusiness _scriptBusiness = new LightScriptBusiness(iuc, "", lightScriptFilePath);
                    //_scriptBusiness.SaveScriptFile("");
                }
                else
                {
                    LightScriptBusiness scriptBusiness = new LightScriptBusiness();
                    String command = scriptBusiness.LoadLightScript(lightScriptFilePath);
                    //Dictionary<String, String> dictionary = scriptBusiness.GetCatalog(iuc, command);
                    Dictionary<String, String> dictionary = null;
                    if (dictionary == null)
                    {
                        return;
                    }
                    String visibleStr = String.Empty;
                    String containStr = String.Empty;
                    String importStr = String.Empty;
                    String finalStr = String.Empty;
                    String lockedStr = String.Empty;
                    //String introduceStr = String.Empty;
                    foreach (var item in dictionary)
                    {
                        if (item.Key.Trim().Equals("NoVisible"))
                        {
                            visibleStr = item.Value;
                        }
                        else if (item.Key.Trim().Equals("Contain"))
                        {
                            containStr = item.Value;
                        }
                        else if (item.Key.Trim().Equals("Import"))
                        {
                            importStr = item.Value;
                        }
                        else if (item.Key.Trim().Equals("Introduce"))
                        {
                            iuc.introduceText = item.Value;
                        }
                        else if (item.Key.Trim().Equals("Final"))
                        {
                            finalStr = item.Value;
                        }
                        else if (item.Key.Trim().Equals("Locked"))
                        {
                            lockedStr = item.Value;
                        }
                        else
                        {
                            iuc.lightScriptDictionary.Add(item.Key, item.Value);
                            iuc.visibleDictionary.Add(item.Key, true);
                            iuc.AddStep(item.Key, "");
                        }
                    }
                    iuc.UpdateExtends();
                    iuc.UpdateIntersection();
                    iuc.UpdateComplement();
                    if (!visibleStr.Equals(String.Empty))
                    {
                        visibleStr = visibleStr.Replace(System.Environment.NewLine, "");
                        visibleStr = visibleStr.Replace("\t", "");

                        String[] visibleStrs = visibleStr.Split(';');
                        foreach (String str in visibleStrs)
                        {
                            if (str.Trim().Equals(String.Empty))
                                continue;
                            if (iuc.visibleDictionary.ContainsKey(str))
                            {
                                iuc.visibleDictionary[str] = false;
                            }
                        }
                    }
                    iuc.UpdateVisible();
                    if (!containStr.Equals(String.Empty))
                    {
                        containStr = containStr.Replace(System.Environment.NewLine, "");
                        containStr = containStr.Replace("\t", "");

                        String[] containStrs = containStr.Split(';');
                        foreach (String str in containStrs)
                        {
                            if (str.Trim().Equals(String.Empty))
                                continue;

                            String[] strContentOrTile = str.Split(':');
                            iuc.containDictionary.Add(strContentOrTile[0], new List<string>());
                            String[] strs = strContentOrTile[1].Split(',');
                            for (int x = 0; x < strs.Length; x++)
                            {
                                if (!strs[x].Equals(String.Empty))
                                    iuc.containDictionary[strContentOrTile[0]].Add(strs[x]);
                            }
                        }
                    }
                    if (!importStr.Equals(String.Empty))
                    {
                        importStr = importStr.Replace(System.Environment.NewLine, "");
                        importStr = importStr.Replace("\t", "");

                        String[] importStrs = importStr.Split(';');
                        foreach (String str in importStrs)
                        {
                            if (str.Trim().Equals(String.Empty))
                                continue;

                            if (!iuc.importList.Contains(str))
                            {
                                iuc.importList.Add(str);
                            }
                        }
                    }
                    if (!finalStr.Equals(String.Empty))
                    {
                        finalStr = finalStr.Replace(System.Environment.NewLine, "");
                        finalStr = finalStr.Replace("\t", "");

                        String[] finalStrs = finalStr.Split('.');
                        foreach (String str in finalStrs)
                        {
                            if (str.Trim().Equals(String.Empty))
                                continue;

                            String[] strs = str.Split(':');
                            iuc.finalDictionary.Add(strs[0], strs[1]);
                        }
                    }
                    if (!lockedStr.Equals(String.Empty))
                    {
                        lockedStr = lockedStr.Replace(System.Environment.NewLine, "");
                        lockedStr = lockedStr.Replace("\t", "");

                        String[] lockedStrs = lockedStr.Split('.');
                        foreach (String str in lockedStrs)
                        {
                            if (str.Trim().Equals(String.Empty))
                                continue;
                            
                            String[] strs = str.Split(':');

                            String strContent = fileBusiness.Base2String(strs[1]);
                            List<int> mContentList = new List<int>();
                            for (int x = 0; x < strContent.Length; x++)
                            {
                                mContentList.Add(strContent[x]);
                            }
                            iuc.lockedDictionary.Add(strs[0],fileBusiness.ReadMidiContent(mContentList));
                        }
                        iuc.UpdateLocked();
                    }
                    if (!iuc.RefreshData())
                    {
                        iuc.UpdateData(new List<Light>());
                    }
                }
            }
        }
        /// <summary>
        /// 清除输入控件里的数据
        /// </summary>
        public void ClearInputUserControl()
        {
            //iuc.UpdateData(new List<Light>());
            iuc.lbStep.Items.Clear();
            iuc.lightScriptDictionary.Clear();
            iuc.visibleDictionary.Clear();
            iuc.containDictionary.Clear();
            iuc.extendsDictionary.Clear();
            iuc.intersectionDictionary.Clear();
            iuc.complementDictionary.Clear();
            iuc.importList.Clear();
            iuc.finalDictionary.Clear();
            iuc.introduceText = String.Empty;
            iuc.lockedDictionary.Clear();
        }
        private void ExportFile(object sender, RoutedEventArgs e)
        {
            if (mode == MainWindowMode.Input)
            {
                if (iuc.RefreshData() == false)
                {
                    return;
                }
                mActionBeanList = iuc.mLightList;
            }
            else if (mode ==MainWindowMode.Light) {
                mcuc.RefreshData();
                mActionBeanList = mcuc.mLightList;
            }
            else {
                return;
            }
            //没有AB集合不能保存
            if (mActionBeanList.Count == 0)
            {
                new MessageDialog(this, "CanNotExportEmptyFiles").ShowDialog();
                return;
            }
            if (sender == miExportMidi)
            {
                ExportMidi(Path.GetFileNameWithoutExtension(lightScriptFilePath),false);
            }
            if (sender == miExportLight)
            {
                ExportLight(Path.GetFileNameWithoutExtension(lightScriptFilePath));
            }
            if (sender == miExportAdvanced)
            {
            //    AdvancedExportDialog dialog = new AdvancedExportDialog(this,Path.GetFileNameWithoutExtension(lightScriptFilePath));
            //    if (dialog.ShowDialog() == true)
            //    {
            //        if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 1)
            //        {
            //            mActionBeanList = LightBusiness.Split(mActionBeanList);
            //        }
            //        else if (dialog.cbDisassemblyOrSplicingColon.SelectedIndex == 2)
            //        {
            //            mActionBeanList = LightBusiness.Splice(mActionBeanList);
            //        }
            //        if (dialog.cbRemoveNotLaunchpadNumbers.IsChecked == true) {
            //            mActionBeanList = LightBusiness.RemoveNotLaunchpadNumbers(mActionBeanList);
            //        }
            //        if (dialog.cbCloseColorTo64.IsChecked == true)
            //        {
            //            mActionBeanList = LightBusiness.CloseColorTo64(mActionBeanList);
            //        }
            //        if (dialog.cbExportType.SelectedIndex == 0)
            //        {
            //            ExportMidi(dialog.tbFileName.Text,(bool)dialog.cbWriteToFile.IsChecked);
            //        }
            //        else if (dialog.cbExportType.SelectedIndex == 1)
            //        {
            //            ExportLight(dialog.tbFileName.Text);
            //        }
            //    }
           }
        }
        private void ExportMidi(String fileName,bool isWriteToFile)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件类型
            if (strMyLanguage.Equals("en-US"))
            {
                saveFileDialog.Filter = @"MIDI File|*.mid";
            }
            else if (strMyLanguage.Equals("zh-CN"))
            {
                saveFileDialog.Filter = @"MIDI 序列|*.mid";
            }
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 2;
            //默认保存名
            saveFileDialog.FileName = fileName;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bridge.ExportMidi(saveFileDialog.FileName.ToString(), fileName, mActionBeanList,isWriteToFile);
            }
        }

        private void ExportLight(String fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件类型
            if (strMyLanguage.Equals("en-US"))
            {
                saveFileDialog.Filter = @"Light File|*.light";
            }
            else if (strMyLanguage.Equals("zh-CN"))
            {
                saveFileDialog.Filter = @"Light 文件|*.light";
            }
            //设置默认文件类型显示顺序
            saveFileDialog.FilterIndex = 2;
            //默认保存名
            saveFileDialog.FileName = fileName;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            //点了保存按钮进入
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bridge.ExportLight(saveFileDialog.FileName.ToString(), mActionBeanList);
            }
        }
        private void RenameFileName(object sender, RoutedEventArgs e)
        {
            //if (lbProjectDocument.SelectedIndex == -1)
            //    return;
            //GetStringDialog dialog = new GetStringDialog(this, "FileName", "NewFileNameColon", "PleaseEnterANewFileNameThatDoesNotRepeat");
            //if (dialog.ShowDialog() == true)
            //{
            //    String oldPath = lightScriptFilePath;
            //    System.IO.File.Move(lightScriptFilePath, Path.GetDirectoryName(lightScriptFilePath) + @"\" + dialog.mString + ".lightScript");
            //    int position = lbProjectDocument.SelectedIndex;
            //    lbProjectDocument.SelectedIndex = -1;
            //    AddlbProjectDocumentItem(position, dialog.mString + ".lightScript");
            //    lbProjectDocument.Items.RemoveAt(position + 1);
            //    lbProjectDocument.SelectedIndex = position;
            //}
        }
        private void DeleteFile(String filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            File.Delete(lightScriptFilePath);
            TreeViewItem item = (TreeViewItem)tvProject.SelectedItem;
            TreeViewItem itemParent = (TreeViewItem)item.Parent;
            itemParent.Items.Remove(item);
            //ClearCache();
            //iNowPosition = -1;
            ToNoneUserControl(false);
        }
        private void OtherDrawingSoftware_Click(object sender, RoutedEventArgs e)
        {
            if (strToolOtherDrawingSoftwarePath.Equals(""))
            {
                return;
            }
            if (File.Exists(strToolOtherDrawingSoftwarePath))
            {
                Process.Start(strToolOtherDrawingSoftwarePath, "Attachments\\position.png");
            }
        }
        public bool UploadFileByHttp(string webUrl, string localFileName)
        {
            // 检查文件是否存在  
            if (!System.IO.File.Exists(localFileName))
            {
                //MessageBox.Show("{0} does not exist!", localFileName);
                return false;
            }
            try
            {
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myWebClient.UploadFile(webUrl, "POST", localFileName);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private void FanShe()
        {
            //String connStr = "server=.;uid=sa;pwd=123;database=LightDb";
            //SqlConnection conn = new SqlConnection(connStr);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = "insert into Dbo.UserInfo(UserName,UserPassword,UserBlueVip,UserContactInformation) values('hhm','pwd',1,'aaa')";
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //conn.Dispose();
            ////RefreshData();//刷新数据
        }

        private void OpenRecording(object sender, RoutedEventArgs e)
        {
            if (iuc.RefreshData() == false)
            {
                return;
            }
            mActionBeanList = iuc.mLightList;
            if (mActionBeanList.Count == 0)
            {
                return;
            }
            RecordingDialog r = new RecordingDialog(this, mActionBeanList);
            r.ShowDialog();
        }


      
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
            //RectangleGeometry gm = new RectangleGeometry(r, 5, 5);
            //((UIElement)sender).Clip = gm;
        }
        public void ShowMsg()
        {
            if (strMyLanguage.Equals("zh-CN"))
            {
                tbError.Text = "发现了一个未捕获的异常！" + Environment.NewLine + "你可以联系作者及时修复此BUG。";
            }
            else if (strMyLanguage.Equals("en-US"))
            {
                tbError.Text = "An uncaught exception was found!" + Environment.NewLine + "You can contact the author to repair this BUG in time.";
            }

            gError.Visibility = Visibility.Visible;
        }
        private void SbQueOnCompleted(object sender, EventArgs eventArgs)
        {
            gError.Visibility = Visibility.Collapsed;
        }
        private void OpenDeviceManagement(object sender, RoutedEventArgs e)
        {
            new DeviceManagementWindow(this).ShowDialog();
        }
        private void ToPlay(object sender, RoutedEventArgs e)
        {
            if (cbDevice.SelectedIndex == -1)
            {
                new DeviceManagementWindow(this).ShowDialog();
                return;
            }
            if (deviceDictionary.ContainsKey(cbDevice.SelectedItem.ToString()))
            {
                if (!iuc.RefreshData())
                    return;
                deviceDictionary[cbDevice.SelectedItem.ToString()].SetData(iuc.mLightList);
                //if (!deviceDictionary[cbDevice.SelectedItem.ToString()].IsActive)
                //{
                //    deviceDictionary[cbDevice.SelectedItem.ToString()].Show();
                //}
                //deviceDictionary[cbDevice.SelectedItem.ToString()].Topmost = true;
            }
            else
            {
                new MessageDialog(this, "DeviceBOOM").ShowDialog();
            }
        }

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="inputType">输入类型0-导入，1双击Midi列表</param>
        /// <param name="type">文件类型0 - midi，1 - Light</param>
        private void ImportFile(int inputType, int type)
        {
            if (mode != MainWindowMode.Input)
                return;
            String fileName = String.Empty;
            //文件 - 导入
            if (inputType == 0)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (type == 0)
                {
                    openFileDialog1.Filter = "Midi文件(*.mid)|*.mid|Midi文件(*.midi)|*.midi|All files(*.*)|*.*";
                }
                else
                {
                    openFileDialog1.Filter = "灯光文件(*.light)|*.light|All files(*.*)|*.*";
                }

                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }
            if (!fileName.Equals(String.Empty))
            {
                ImportOrGetDialog dialog = null;
                if (type == 0)
                {
                    dialog = new ImportOrGetDialog(this, fileName, 0);
                }
                else
                {
                    dialog = new ImportOrGetDialog(this, fileName, 1);
                }
                if (dialog.ShowDialog() == true)
                {
                    String usableStepName = dialog.UsableStepName;
                    if (dialog.rbImport.IsChecked == true)
                    {
                        iuc.lightScriptDictionary.Add(usableStepName, dialog.tbImport.Text);
                        iuc.containDictionary.Add(usableStepName, dialog.importList);
                        //如果选择导入，则或将复制文件到灯光语句同文件夹下
                        //判断同文件下是否有该文件
                        if (!File.Exists(lastProjectPath + @"\Resource\" + Path.GetFileName(fileName)))
                        {
                            //如果不存在，则复制
                            File.Copy(fileName, lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
                        }
                        else
                        {
                            //如果存在
                            //先判断是否是同路径
                            if (!Path.GetDirectoryName(fileName).Equals(lastProjectPath + @"\Resource"))
                            {
                                //不是同路径
                                //询问是否替换
                                if (System.Windows.Forms.MessageBox.Show("该文件夹下已有同名文件，是否覆盖", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //删除
                                    File.Delete(lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
                                    //复制
                                    File.Copy(fileName, lastProjectPath + @"\Resource\" + Path.GetFileName(fileName));
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    if (dialog.rbGet.IsChecked == true)
                    {
                        iuc.lightScriptDictionary.Add(usableStepName, dialog.tbGet.Text);
                        iuc.containDictionary.Add(usableStepName, dialog.getList);
                    }
                    iuc.visibleDictionary.Add(usableStepName, true);
                    iuc.AddStep(usableStepName, "");
                    iuc.lbStep.SelectedIndex = iuc.lbStep.Items.Count - 1;
                    iuc.RefreshData();
                }
            }
        }
        private void ImportFile(object sender, RoutedEventArgs e)
        {
            if (sender == miMidiFile)
            {
                ImportFile(0, 0);
            }
            else
            {
                ImportFile(0, 1);
            }
        }
       

        private void Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (bViewFileManager)
            {
                if (gMain.ActualWidth > 300)
                {
                    mainDockPanel.Width = gMain.ActualWidth - 300;
                }
                else
                {
                    mainDockPanel.Width = gMain.ActualWidth;
                }
            }
            else {
                mainDockPanel.Width = gMain.ActualWidth;
            }
            if (mode == MainWindowMode.Input)
            {
                iuc.SetSize(mainDockPanel.ActualWidth, mainDockPanel.ActualHeight);
            }
            if (mode == MainWindowMode.Play)
            {
                //playuc.Width = gMain.ActualWidth;
                //playuc.Height = gMain.ActualHeight;
            }
            wuc.Width = mainDockPanel.Width;
            rdGridInkCanvs.MaxHeight = gMain.ActualHeight - 3;
        }
        private int iTitleClickCount = 0;
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            iTitleClickCount += 1;
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 300)
            };
            timer.Tick += (s, e1) => { timer.IsEnabled = false; iTitleClickCount = 0; };
            timer.IsEnabled = true;
            if (iTitleClickCount % 2 == 0)
            {
                timer.IsEnabled = false;
                iTitleClickCount = 0;
                MaximizeWindow();
            }
        }
        private void ToOnlineWindow(object sender, RoutedEventArgs e)
        {
            OnlineWindow online = new OnlineWindow(this);
            online.Show();
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            //iuc.miLibrary.Items.Clear();
            ////加载库文件
            //DirectoryInfo folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Library");
            //foreach (FileInfo file in folder.GetFiles("*.lightScript"))
            //{
            //    System.Windows.Controls.MenuItem item = new System.Windows.Controls.MenuItem
            //    {
            //        Header = Path.GetFileNameWithoutExtension(file.FullName),
            //        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
            //        FontSize = 14
            //    };
            //    item.Click += LibraryMenuItem_Click;
            //    iuc.miLibrary.Items.Add(item);
            //}
        }


        private void ToTestWindow(object sender, RoutedEventArgs e)
        {
            if (sender == miBeautiful)
            {
                new BeautifulWindow().Show();
            }
            if (sender == mi3D)
            {
                new Test3DWindow().Show();
            }
        }
      
        private void VisitTheOfficialWebsite(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.launchpadlight.com/");
        }

        public void CreateShortCut(object sender, RoutedEventArgs e)
        {
            //bridge.CreateShortCut(System.Windows.Forms.Application.StartupPath + "\\Maker.exe", "Maker的桌面快捷方式");
            bridge.CreateShortCut(System.Windows.Forms.Application.StartupPath + "\\Maker.exe", "");
        }

        private void ViewFileManager_Checked(object sender, RoutedEventArgs e)
        {
            ChangeViewFileManager();
        }
        private void ViewFileManager_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeViewFileManager();
        }

        private void ChangeViewFileManager()
        {
            bViewFileManager = (bool)cbViewFileManager.IsChecked;
            XmlDocument doc = new XmlDocument();
            doc.Load("Config/view.xml");
            XmlNode viewRoot = doc.DocumentElement;
            XmlNode viewFileManager = viewRoot.SelectSingleNode("FileManager");
            viewFileManager.InnerText = bViewFileManager.ToString();
            if (cbViewFileManager.IsChecked == true)
            {
                cdFileManager.Width = new GridLength(300, GridUnitType.Pixel);
                mainDockPanel.Width = gMain.ActualWidth - 300;
            }
            else
            {
                cdFileManager.Width = new GridLength(0, GridUnitType.Pixel);
                mainDockPanel.Width = gMain.ActualWidth;
            }
            doc.Save("Config/view.xml");
        }
        private void ImportPictureFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                //openFileDialog1.Filter = "PNG文件(*.png)|*.png|JPG文件(*.jpg)|*.jpg|All files(*.*)|*.*";
                Filter = "图片文件(*.jpg;*.png)|*.jpg;*.png",
                RestoreDirectory = true
            };
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImportPictureDialog dialog = new ImportPictureDialog(this, openFileDialog1.FileName);
                dialog.ShowDialog();
            }
        }
      
        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ToolBar toolBar = sender as System.Windows.Controls.ToolBar;
            if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            if (toolBar.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
    
        private void BtnUserName_Click(object sender, RoutedEventArgs e)
        {
            if (mUser == null)
            {
                LoginWindow login = new LoginWindow(this);
                login.ShowDialog();
            }
            else {
                popUser.IsOpen = !popUser.IsOpen;
            }
        }
        private void LogOff(object sender, RoutedEventArgs e)
        {
            UserBusiness.Clear(this);
        }
        private void SpUpload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mUser == null)
                return;
            if (!mUser.UserOccupation.Equals("maker") || mUser.UserGrade < 1)
            {
                return;
            }
            new UploadDialog(this).ShowDialog();

        }
        private void ToolBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mUser == null)
            {
                return;
            }
            popUser.IsOpen = false;
            popUser.IsOpen = true;
        }
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mUser != null)
            {
                new UserInfoDialog(this).ShowDialog();
            }

        }
       
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (!isSelect)
                return;
        }
        private void LbProjectDocumentMenu_Closed(object sender, RoutedEventArgs e)
        {
            isSelect = false;
        }

        private void CloseMsg(object sender, MouseButtonEventArgs e)
        {
            gError.Visibility = Visibility.Hidden;
        }
       
        public bool _bIsEdit = false;
        public bool bIsEdit = false;
        
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private String strExternalFile = String.Empty;
        public void ShowFromFile(string fileName)
        {
            try
            {
                strExternalFile = fileName;
            }
            catch {
                //MessageBox.Show(exc.Message);
            }
            finally
            {
                Show();
            }
        }

        private void ChangeOpenMidiFileDefaultModify(object sender, RoutedEventArgs e)
        {
            Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory+@"Tool\Tool.exe";
            p.Start();
        }
      
        public WebBrowserWindow webBrowserWindow;
        private void ToBrowserWindow(object sender, RoutedEventArgs e)
        {
            if (webBrowserWindow == null) {
                webBrowserWindow = new WebBrowserWindow(this);
                webBrowserWindow.Show();
            }
            else
            {
                if(webBrowserWindow.Visibility == Visibility.Visible)
                {
                    webBrowserWindow.Hide(); 
                }
                else
                {
                    webBrowserWindow.Show();
                    webBrowserWindow.Activate();
                }
            }
        }
        public MediaPlayerWindow mediaPlayerWindow;
        private void ToMusicPlayerWindow(object sender, RoutedEventArgs e)
        {
            if (mediaPlayerWindow == null)
            {
                mediaPlayerWindow = new MediaPlayerWindow(this);
                mediaPlayerWindow.Show();
            }
            else
            {
                if (mediaPlayerWindow.Visibility == Visibility.Visible)
                {
                    mediaPlayerWindow.Hide();
                }
                else
                {
                    mediaPlayerWindow.Show();
                    mediaPlayerWindow.Activate();
                }
            }
        }

        private void ToHomeUserControl(object sender, RoutedEventArgs e)
        {
            ToNoneUserControl(true);
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            NewProject();
        }
        public void NewProject() {
            NewFileDialog newFile = new NewFileDialog(this);
            if (newFile.ShowDialog() == true)
            {
                String FolderName = newFile.tbLocation.Text + @"\" + newFile.tbName.Text;
                if (Directory.Exists(FolderName))
                {
                    Directory.Exists(FolderName);
                }
                Directory.CreateDirectory(FolderName);
                //搭建项目文件
                Directory.CreateDirectory(FolderName + @"\LightScript");
                Directory.CreateDirectory(FolderName + @"\Light");
                Directory.CreateDirectory(FolderName + @"\Midi");
                Directory.CreateDirectory(FolderName + @"\Resource");
                Directory.CreateDirectory(FolderName + @"\Library");

                XDocument xDoc = new XDocument();
                XElement xProject = new XElement("Project");
                XElement xFiles = new XElement("Files");
                xProject.Add(xFiles);
                xDoc.Add(xProject);
                // Light
                XElement xLight = new XElement("Light");
                xFiles.Add(xLight);
                XAttribute xOpen2 = new XAttribute("open", "false");
                xLight.Add(xOpen2);
                // LightScript
                XElement xLightScript = new XElement("LightScript");
                xFiles.Add(xLightScript);
                XAttribute xOpen3 = new XAttribute("open", "false");
                xLightScript.Add(xOpen3);
                // Midi
                XElement xMidi = new XElement("Midi");
                xFiles.Add(xMidi);
                XAttribute xOpen4 = new XAttribute("open", "false");
                xMidi.Add(xOpen4);
                // Resource
                XElement xResources = new XElement("Resource");
                xFiles.Add(xResources);
                XAttribute xOpen5 = new XAttribute("open", "false");
                xResources.Add(xOpen5);
                // Resource
                XElement xLibrary = new XElement("Library");
                xFiles.Add(xLibrary);
                XAttribute xOpen6 = new XAttribute("open", "false");
                xLibrary.Add(xOpen6);

                //选择库 则没有page相关
                if (newFile.lbMain.SelectedIndex == 0)
                {
                    //类型赋值
                    XAttribute xProjectType = new XAttribute("type", "launchpadlightproject");
                    xProject.Add(xProjectType);
                    XAttribute xProjectTutorial = new XAttribute("tutorial", "");
                    xProject.Add(xProjectTutorial);
                    //创建文件夹
                    Directory.CreateDirectory(FolderName + @"\Page");
                    // Page
                    XElement xPage = new XElement("Page");
                    xFiles.Add(xPage);
                    XAttribute xOpen = new XAttribute("open", "false");
                    XAttribute xFirst = new XAttribute("first", "");
                    xPage.Add(xOpen);
                    xPage.Add(xFirst);
                }
                else {
                    //类型赋值
                    XAttribute xProjectType = new XAttribute("type", "launchpadlightlibrary");
                    xProject.Add(xProjectType);
                }
                // 保存该文档  
                xDoc.Save(FolderName + @"\"+ newFile.tbName.Text+".makerpj");

                OpenProject(FolderName);
            }
        }
        private void OpenProject(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                //获得路径
                OpenProject(fbd.SelectedPath);
            }
        }
        public void OpenProject(String projectPath)
        {
            miPlay.IsEnabled = true;

            bool isContains = false;
            foreach (var item in historicals)
            {
                if (projectPath.Equals(item.Path)) {
                    isContains = true;
                    break;
                }
            }
            if (!isContains) {
                historicals.Add(new HistoricalModel(projectPath));
                XDocument _doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Config\historical.xml");
                XElement _root = _doc.Element("Historical");
                XElement _project = new XElement("Project");
                XAttribute xPath = new XAttribute("path", projectPath);
                _project.Add(xPath);
                _root.Add(_project);
                _doc.Save(AppDomain.CurrentDomain.BaseDirectory + @"\Config\historical.xml");
            }

            lastProjectPath = projectPath;
            ListRoots();
            ToNoneUserControl(false);

            DirectoryInfo d = new DirectoryInfo(lastProjectPath);
            XDocument doc = XDocument.Load(lastProjectPath+@"\" + d.Name +".makerpj");
            XElement project = doc.Root;
            if (project.Attribute("type").Value.Equals("launchpadlightproject")) {
                projectType = ProjectType.launchpadlightproject;
                miPlay.IsEnabled = true;
                tutorial = project.Attribute("tutorial").Value;
            }
            else if (project.Attribute("type").Value.Equals("launchpadlightlibrary"))
            {
                projectType = ProjectType.launchpadlightlibrary;
                miPlay.IsEnabled = false;
            }

            XElement files = project.Element("Files");
            XElement lightOpen = files.Element("Light");
            if (lightOpen.Attribute("open").Value.Equals("true")) {
                TreeViewItem item = (TreeViewItem)tvProject.Items[1];
                item.IsExpanded = true;
            }
            XElement lightScriptOpen = files.Element("LightScript");
            if (lightScriptOpen.Attribute("open").Value.Equals("true"))
            {
                TreeViewItem item = (TreeViewItem)tvProject.Items[2];
                item.IsExpanded = true;
            }
            XElement midiOpen = files.Element("Midi");
            if (midiOpen.Attribute("open").Value.Equals("true"))
            {
                TreeViewItem item = (TreeViewItem)tvProject.Items[3];
                item.IsExpanded = true;
            }
            XElement resourceOpen = files.Element("Resource");
            if (resourceOpen.Attribute("open").Value.Equals("true"))
            {
                TreeViewItem item = (TreeViewItem)tvProject.Items[5];
                item.IsExpanded = true;
            }
            XElement libraryOpen = files.Element("Library");
            if (libraryOpen.Attribute("open").Value.Equals("true"))
            {
                TreeViewItem item = (TreeViewItem)tvProject.Items[6];
                item.IsExpanded = true;
            }
            XElement pageOpen = files.Element("Page");
            if (pageOpen != null)
            {
                if (pageOpen.Attribute("open").Value.Equals("true"))
                {
                    TreeViewItem item = (TreeViewItem)tvProject.Items[4];
                    item.IsExpanded = true;
                }
                firstPageName = pageOpen.Attribute("first").Value;
            }
           
        }


        private void TvProject_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //只使用了Listbox单选功能
            if (!iuc.RefreshData())
                return;
            mActionBeanList = iuc.mLightList;
            //没有AB集合不能保存
            if (mActionBeanList.Count == 0)
            {
                new MessageDialog(this, "CanNotExportEmptyFiles").ShowDialog();
                return;
            }
            string[] files = new string[1];
            //files[0] = lbProjectDocument.SelectedItem.ToString();
            files[0] = AppDomain.CurrentDomain.BaseDirectory + @"\Cache\" + Path.GetFileName(lightScriptFilePath);
            files[0] = files[0].Replace('.', '_');
            files[0] += ".mid";
            bridge.ExportMidi(files[0], "", mActionBeanList, false);

            DragDrop.DoDragDrop(tvProject, new System.Windows.Forms.DataObject(System.Windows.Forms.DataFormats.FileDrop, files), System.Windows.DragDropEffects.Copy | System.Windows.DragDropEffects.Move /* | DragDropEffects.Link */);
        }
        public String firstPageName = String.Empty;
        private FileBusiness fileBusiness;

        private void ToPlayUserControl(object sender, RoutedEventArgs e)
        {
            //TreeViewItem item = null;
            //if (lightScriptFilePath.EndsWith(".light")) {
            //    item = tvProject.Items[0] as TreeViewItem;
            //    foreach (var mItem in item.Items) {
            //        TreeViewItem _mItem = mItem as TreeViewItem;
            //        _mItem.IsSelected = false;
            //    }
            //    item.IsSelected = false;
            //}
            TreeViewItem item = (TreeViewItem)tvProject.SelectedItem;
            if (item != null)
            {
                item.IsSelected = false;
            }
            if (mode != MainWindowMode.Play)
            {
                ToPlayUserControl();
                //playuc.LoadExeXml();
            }
        }


        private void GMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gInkCanvs.Width = gMain.ActualWidth - 300;
        }

        private void CbDevice_DropDownOpened(object sender, EventArgs e)
        {
            if (cbDevice.Items.Count == 0) {
                cbDevice.IsDropDownOpen = false;
            }
        }

        //public void NewFile()
        //{
        //    for (int i = 1; i < 1000; i++)
        //    {
        //        if (!ContainsStepName(i.ToString() + ".lightScript"))
        //        {
        //            AddlbProjectDocumentItem(-1, i.ToString() + ".lightScript");
        //            lbProjectDocument.SelectedIndex = lbProjectDocument.Items.Count - 1;
        //            break;
        //        }
        //    }
        //    //滚动到最后

        //    //清除输入
        //    ClearInputUserControl();
        //    iNowPosition = 0;
        //    //刷新数据
        //    iuc.RefreshData();
        //}
    }


}
