using Maker.Business;
using Maker.Business.Model.OperationModel;
using Maker.Model;
using Maker.View.Dialog;
using Maker.View.UI.Style;
using Maker.View.UI.UserControlDialog;
using Maker.View.UI.Utils;
using Operation;
using Operation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace Maker.View.Play
{
    /// <summary>
    /// PlayExportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayExportUserControl : BaseUserControl
    {
        public PlayExportUserControl(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileType = "Play";
            _fileExtension = ".playExport";
            mainView = gMain;
            //HideControl();
        }

        List<BaseOperationModel> operationModels = new List<BaseOperationModel>();
        protected override void LoadFileContent()
        {
            XDocument doc = XDocument.Load(filePath);
            XElement xnroot = doc.Element("Root");

            foreach (var xElement in xnroot.Elements())
            {
                BaseOperationModel baseOperationModel = null;
                if (xElement.Name.ToString().Equals("TutorialFile"))
                {
                    baseOperationModel = new TutorialFileExportModel();
                }
                if (xElement.Name.ToString().Equals("FirstPage"))
                {
                    baseOperationModel = new FirstPageExportModel();
                }
                if (xElement.Name.ToString().Equals("Pages"))
                {
                    baseOperationModel = new PagesExportModel();
                }
                if (xElement.Name.ToString().Equals("Model"))
                {
                    baseOperationModel = new ModelExportModel();
                }

                if (baseOperationModel != null)
                {
                    baseOperationModel.SetXElement(xElement);
                    operationModels.Add(baseOperationModel);
                }
            }

            Test();

            exportStyleUserControl = new ExportStyleUserControl(this);

            spCenter.Children.Clear();
            spCenter.Children.Add(exportStyleUserControl);

            RefreshView();
        }

        ExportStyleUserControl exportStyleUserControl;
        private void RefreshView()
        {
            exportStyleUserControl.SetData(operationModels);
        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lbMain = sender as ListBox;
            if (lbMain.SelectedIndex == -1)
                return;
            filePath = (lbMain.SelectedItem as ListBoxItem).Content.ToString();
            LoadFile(filePath);
            mw.RemoveDialog();
        }

        bool IsBuild = false;
        private static PlayExportUserControl playExportUserControl = null;

        public static PlayExportUserControl CreateInstance(NewMainWindow mw)
        {
            if (playExportUserControl == null)
            {
                playExportUserControl = new PlayExportUserControl(mw);
            }

            if (!File.Exists(mw.LastProjectPath + @"Play\" + mw.projectConfigModel.Path + ".playExport"))
            {
                mw.editUserControl.peuc.NewFileResult2(mw.projectConfigModel.Path + ".playExport");
            }
            playExportUserControl.LoadFile(mw.LastProjectPath + @"Play\" + mw.projectConfigModel.Path + ".playExport");

            return playExportUserControl;
        }

        public void Build()
        {
            GenerateLaunchpadLightProject();
        }

        bool bIsLive = true;
        string tutorialName = string.Empty;
        string firstPageName = string.Empty;
        List<string> pageNames = new List<string>();
        private void GenerateLaunchpadLightProject()
        {
            if (IsBuild)
            {
                return;
            }
            IsBuild = true;

            StaticConstant.mw.SetProgress("保存文件", 0);
            SaveFile();

            Thread thread = new Thread(new ThreadStart(() =>
            {
                Dictionary<String, String> mDictionary = new Dictionary<string, string>();
                Dictionary<String, List<Light>> mLightDictionary = new Dictionary<string, List<Light>>();
                //获取对象
                XDocument xDoc = new XDocument();
                XElement xRoot = new XElement("Root");
                xDoc.Add(xRoot);

                //Tutorial
                XElement xTutorial = new XElement("Tutorial");
                XAttribute xContent;
                if (tutorialName.Equals(String.Empty))
                {
                    xContent = new XAttribute("content", "");
                }
                else
                {
                    xContent = new XAttribute("content", Business.FileBusiness.CreateInstance().String2Base(Business.FileBusiness.CreateInstance().WriteMidiContent(AllFileToLightList(tutorialName))));
                }
                xTutorial.Add(xContent);
                xRoot.Add(xTutorial);

                //Model
                XElement xModel = new XElement("Model");
                if (bIsLive)
                {
                    xModel.Value = "0";
                }
                else
                {
                    xModel.Value = "1";
                }
                xRoot.Add(xModel);

                //Pages
                XElement xPages = new XElement("Pages");
                XAttribute xFirst = new XAttribute("first", firstPageName);
                xPages.Add(xFirst);
                xRoot.Add(xPages);

                int nowPosition = 0;
                for (int i = 0; i < pageNames.Count; i++)
                {
                    StaticConstant.mw.SetProgress("添加页面" + (i + 1), i * 1.0 / pageNames.Count);

                    XElement xPage = new XElement("Page");
                    XAttribute xPageName = new XAttribute("name", pageNames[i]);
                    xPage.Add(xPageName);

                    mw.projectUserControl.puc.ReadPageFile(mw.LastProjectPath + @"\Play\" + pageNames[i], out List<List<PageButtonModel>> pageModes);
                    for (int x = 0; x < pageModes.Count; x++)
                    {
                        if (pageModes[x].Count == 0)
                            continue;

                        XElement xButtons = new XElement("Buttons");
                        XAttribute xPosition;
                        if (bIsLive)
                        {
                            xPosition = new XAttribute("position", Business.FileBusiness.CreateInstance().midiArr[x]);
                        }
                        else
                        {
                            xPosition = new XAttribute("position", x);
                        }
                        xButtons.Add(xPosition);

                        for (int y = 0; y < pageModes[x].Count; y++)
                        {
                            XElement xButton = new XElement("Button");
                            //Down
                            XElement xDown = new XElement("Down");
                            {
                                foreach (var mItem in pageModes[x][y]._down.OperationModels)
                                {
                                    if (mItem is LightFilePlayModel)
                                    {
                                        var item = mItem as LightFilePlayModel;
                                        //TODO:
                                        //(mItem as LightFilePlayModel).Bpm = 145;
                                        if (!item.FileName.Equals(String.Empty))
                                        {
                                            if (mDictionary.ContainsKey(item.FileName))
                                            {
                                                item.FileName = mDictionary[item.FileName];
                                            }
                                            else
                                            {
                                                mDictionary.Add(item.FileName, nowPosition.ToString());
                                                List<Light> lights = AllFileToLightList(item.FileName);
                                                if (bIsLive)
                                                {
                                                    Business.FileBusiness.CreateInstance().ReplaceControl(lights, Business.FileBusiness.CreateInstance().midiArr);
                                                }
                                                mLightDictionary.Add(nowPosition.ToString(), lights);
                                                item.FileName = nowPosition.ToString();
                                                nowPosition++;
                                            }
                                        }
                                    }
                                    xDown.Add(mItem.GetXElement());
                                }
                            }
                            xButton.Add(xDown);
                            //Loop
                            XElement xLoop = new XElement("Loop");
                            {
                                foreach (var mItem in pageModes[x][y]._loop.OperationModels)
                                {
                                    if (mItem is LightFilePlayModel)
                                    {
                                        var item = mItem as LightFilePlayModel;
                                        if (!item.FileName.Equals(String.Empty))
                                        {
                                            if (mDictionary.ContainsKey(item.FileName))
                                            {
                                                item.FileName = mDictionary[item.FileName];
                                            }
                                            else
                                            {
                                                mDictionary.Add(item.FileName, nowPosition.ToString());
                                                List<Light> lights = AllFileToLightList(item.FileName);
                                                if (bIsLive)
                                                {
                                                    Business.FileBusiness.CreateInstance().ReplaceControl(lights, Business.FileBusiness.CreateInstance().midiArr);
                                                }
                                                mLightDictionary.Add(nowPosition.ToString(), lights);
                                                item.FileName = nowPosition.ToString();
                                                nowPosition++;
                                            }
                                        }
                                    }
                                    xLoop.Add(mItem.GetXElement());
                                }
                            }
                            xButton.Add(xLoop);
                            //Up
                            XElement xUp = new XElement("Up");
                            {

                                foreach (var mItem in pageModes[x][y]._up.OperationModels)
                                {
                                    if (mItem is LightFilePlayModel)
                                    {
                                        var item = mItem as LightFilePlayModel;
                                        if (!item.FileName.Equals(String.Empty))
                                        {
                                            if (mDictionary.ContainsKey(item.FileName))
                                            {
                                                item.FileName = mDictionary[item.FileName];
                                            }
                                            else
                                            {
                                                mDictionary.Add(item.FileName, nowPosition.ToString());
                                                List<Light> lights = AllFileToLightList(item.FileName);
                                                if (bIsLive)
                                                {
                                                    Business.FileBusiness.CreateInstance().ReplaceControl(lights, Business.FileBusiness.CreateInstance().midiArr);
                                                }
                                                mLightDictionary.Add(nowPosition.ToString(), lights);
                                                item.FileName = nowPosition.ToString();
                                                nowPosition++;
                                            }
                                        }
                                    }
                                    xUp.Add(mItem.GetXElement());
                                }
                            }
                            xButton.Add(xUp);

                            xButtons.Add(xButton);
                        }
                        xPage.Add(xButtons);
                    }
                    xPages.Add(xPage);
                }
                int lightCount = 0;
                //Lights
                XElement xLights = new XElement("Lights");
                xRoot.Add(xLights);
                foreach (var mItem in mLightDictionary)
                {
                    StaticConstant.mw.SetProgress("添加灯光" + (lightCount + 1), lightCount * 1.0 / mLightDictionary.Count);
                    lightCount++;

                    XElement xLight = new XElement("Light");
                    XAttribute xLightName = new XAttribute("name", mItem.Key);
                    xLight.Add(xLightName);
                    //StringBuilder builder = new StringBuilder();
                    //for (int y = 0; y < mItem.Value.Count; y++)
                    //{
                    //    Light l = mItem.Value[y];
                    //    builder.Append(l.Time + "," + l.Action + "," + l.Position + "," + l.Color + ";");
                    //}
                    //XAttribute xValue = new XAttribute("value", builder.ToString());
                    //String str = fileBusiness.String2Base(fileBusiness.WriteMidiContent(mItem.Value));
                    //Console.WriteLine(str);
                    //Console.WriteLine("-----------");

                    //String str = fileBusiness.Base2String(fileBusiness.String2Base(fileBusiness.WriteMidiContent(mItem.Value)));
                    //List<int> mList = new List<int>();
                    //for (int i = 0; i < str.Length; i++)
                    //{
                    //    mList.Add(str[i]);
                    //}
                    //LightBusiness.Print(fileBusiness.ReadMidiContent(mList));
                    XAttribute xValue = new XAttribute("value", Business.FileBusiness.CreateInstance().String2Base(Business.FileBusiness.CreateInstance().WriteMidiContentNotReplace(mItem.Value)));
                    xLight.Add(xValue);
                    xLights.Add(xLight);
                }
                DirectoryInfo d = new DirectoryInfo(mw.LastProjectPath);
                xDoc.Save(mw.LastProjectPath + @"Play\" + d.Name + ".play");

                IsBuild = false;
                StaticConstant.mw.SetProgress("", 0);
            }));
            thread.Start();
        }

        LightScriptBusiness business = new LightScriptBusiness();
        public List<Light> AllFileToLightList(String filePath)
        {
            List<Light> mLightList = new List<Light>();
            if (filePath.EndsWith(".lightScript"))
            {
                String _file = mw.LastProjectPath + @"_Cache\_" + filePath.Substring(0, filePath.Length - ".lightScript".Length) + ".mid";
                if (File.Exists(_file))
                {
                    mLightList = Business.FileBusiness.CreateInstance().ReadMidiFile(_file);
                    Business.FileBusiness.CreateInstance().ReplaceControl(mLightList, Business.FileBusiness.CreateInstance().normalArr);
                }
                else
                {
                    mLightList = ScriptFileBusiness.FileToLight(mw.LastProjectPath + @"LightScript\" + filePath);
                }
            }
            else if (filePath.EndsWith(".light"))
            {
                mLightList = Business.FileBusiness.CreateInstance().ReadLightFile(mw.LastProjectPath + @"Light\" + filePath);
            }
            else if (filePath.EndsWith(".mid"))
            {
                mLightList = Business.FileBusiness.CreateInstance().ReadMidiFile(mw.LastProjectPath + @"Light\" + filePath);
                Business.FileBusiness.CreateInstance().ReplaceControl(mLightList, Business.FileBusiness.CreateInstance().normalArr);
            }
            mLightList = Business.LightBusiness.Sort(mLightList);
            return mLightList;
        }

        public override void SaveFile()
        {
            ToSaveFile(filePath);
        }

        public void ToSaveFile(String filePath)
        {
            if (filePath.Equals(String.Empty))
                return;
            XDocument doc = new XDocument();
            XElement xnRoot = new XElement("Root");
            doc.Add(xnRoot);

            foreach (var mItem in operationModels)
            {
                xnRoot.Add(mItem.GetXElement());
            }
            doc.Save(filePath);
        }

        public void ToSaveFile(String filePath, String firstPageName, String tutorialName, List<String> pageNames, bool isLive)
        {
            operationModels.Clear();
            operationModels.Add(new FirstPageExportModel(firstPageName));
            operationModels.Add(new TutorialFileExportModel(tutorialName));
            operationModels.Add(new PagesExportModel(pageNames));
            operationModels.Add(new ModelExportModel(isLive));
            ToSaveFile(filePath);
        }

        protected override void CreateFile(String filePath)
        {
            //获取对象
            XDocument xDoc = new XDocument();
            // 添加根节点
            XElement xRoot = new XElement("Root");
            // 添加节点使用Add
            xDoc.Add(xRoot);
            // 保存该文档  
            xDoc.Save(filePath);
        }

        private void BaseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            spCenter.Width = mw.ActualWidth / 2;
        }

        private void GenerationModel(object sender, RoutedEventArgs e)
        {
            if (sender == miModel)
            {
                operationModels.Add(new ModelExportModel());
                return;
            }

            List<string> fileNames = new List<string>();
            ShowLightListDialog dialog;
            if (sender == miTutorial)
            {
                dialog = new ShowLightListDialog(mw, "", fileNames);
                fileNames.AddRange(Business.FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + @"\Light", new List<string>() { ".light", ".mid" }));
                fileNames.AddRange(Business.FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + @"\LightScript", new List<string>() { ".lightScript" }));
            }
            else
            {
                dialog = new ShowLightListDialog(mw, "", fileNames, true);
                fileNames.AddRange(Business.FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + @"\Play", new List<string>() { ".lightPage" }));
            }

            if (dialog.ShowDialog() == true)
            {
                if (sender == miTutorial)
                {
                    operationModels.Add(new TutorialFileExportModel(dialog.selectItem));
                }
                else if (sender == miFirstPage)
                {
                    operationModels.Add(new FirstPageExportModel(dialog.selectItem));
                }
                else if (sender == miPage)
                {
                    operationModels.Add(new PagesExportModel(dialog.selectItems));
                    /* for (int i = 0; i < dialog.lbMain.SelectedItems.Count; i++)
                         {
                             if (lbPages.Items.Contains(dialog.lbMain.SelectedItems[i]))
                             {
                                 continue;
                             }
                             lbPages.Items.Add(dialog.lbMain.SelectedItems[i]);
                             pageNames.Add(dialog.lbMain.SelectedItems[i].ToString());
                         }*/
                }
            }
            RefreshView();
        }

        private void menuCenter_Opened(object sender, RoutedEventArgs e)
        {
            miTutorial.IsEnabled = true;
            miFirstPage.IsEnabled = true;
            miPage.IsEnabled = true;
            miModel.IsEnabled = true;

            List<string> pages = Operation.FileBusiness.CreateInstance().GetFilesName(StaticConstant.mw.LastProjectPath + @"Play\", new List<string>() { ".lightPage" });
            if (pages.Count == 0)
            {
                miFirstPage.IsEnabled = false;
                miPage.IsEnabled = false;
            }
            foreach (BaseOperationModel baseOperationModel in operationModels)
            {
                if (baseOperationModel is TutorialFileExportModel)
                {
                    miTutorial.IsEnabled = false;
                }
                if (baseOperationModel is FirstPageExportModel)
                {
                    miFirstPage.IsEnabled = false;
                }
                if (baseOperationModel is PagesExportModel)
                {
                    miPage.IsEnabled = false;
                }
                if (baseOperationModel is ModelExportModel)
                {
                    miModel.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void Test() {
            foreach (BaseOperationModel baseOperationModel in operationModels)
            {
                if (baseOperationModel is TutorialFileExportModel)
                {
                    tutorialName = (baseOperationModel as TutorialFileExportModel).TutorialName;
                }
                if (baseOperationModel is FirstPageExportModel)
                {
                    firstPageName = (baseOperationModel as FirstPageExportModel).FirstPageName;
                }
                if (baseOperationModel is PagesExportModel)
                {
                    pageNames = (baseOperationModel as PagesExportModel).Pages;
                }
                if (baseOperationModel is ModelExportModel)
                {
                    bIsLive = (baseOperationModel as ModelExportModel).Model == 0;
                }
            }
        }

        private void LoadTemplate(object sender, RoutedEventArgs e)
        {
            mw.ShowMakerDialog(new ListDialog(mw, Business.FileBusiness.CreateInstance().GetFilesName(mw.LastProjectPath + "Play", new List<string>() { ".playExport" }), lbMain_SelectionChanged, "点击加载预置导出方案"));
        }

        /* 
       private void tbCenter_TextChanged(object sender, TextChangedEventArgs e)
       {
           if (tbCenter.SelectionStart != 0)
           {
               if (tbCenter.Text[tbCenter.SelectionStart - 1] == '<')
               {
                   ContextMenu menu = new ContextMenu();
                   if (tbCenter.Text.Substring(0, tbCenter.SelectionStart).Contains("<Pages>")){
                       {
                           MenuItem menuItem = new MenuItem();
                           menuItem.Header = ResourcesUtils.Resources2String("Page");
                           menu.Items.Add(menuItem);
                       }
                   }
                   else {
                       {
                           MenuItem menuItem = new MenuItem();
                           menuItem.Header = ResourcesUtils.Resources2String("Tutorial");
                           menuItem.Click += MenuItem_Click;
                           menu.Items.Add(menuItem);
                       }
                       {
                           MenuItem menuItem = new MenuItem();
                           menuItem.Header = ResourcesUtils.Resources2String("FirstPage");
                           menu.Items.Add(menuItem);
                       }
                       {
                           MenuItem menuItem = new MenuItem();
                           menuItem.Header = ResourcesUtils.Resources2String("Page");
                           menuItem.Click += MenuItem_Click;
                           menu.Items.Add(menuItem);
                       }
                       {
                           MenuItem menuItem = new MenuItem();
                           menuItem.Header = ResourcesUtils.Resources2String("Model");
                           menu.Items.Add(menuItem);
                       }
                   }

                   tbCenter.ContextMenu = menu;

                   menu.IsOpen = true;
               }
           }
       }

     private void MenuItem_Click(object sender, RoutedEventArgs e)
       {
           if (sender is MenuItem) {
               String strs = (sender as MenuItem).Header.ToString();
               if (strs.Equals(ResourcesUtils.Resources2String("Tutorial")))
               {
                   if (tbCenter.SelectionStart != 0)
                   {
                       string s = tbCenter.Text;
                       int idx = tbCenter.SelectionStart;
                       s = s.Insert(idx, "Tutorial></Tutorial>");

                       tbCenter.Text = s;
                       tbCenter.SelectionStart = idx + 9;
                       tbCenter.Focus();
                   }
                   else
                   {
                       tbCenter.Text = "<Tutorial></Tutorial>";
                   }
               }
               else if (strs.Equals(ResourcesUtils.Resources2String("Page")))
               {
                   if (tbCenter.SelectionStart != 0)
                   {
                       string s = tbCenter.Text;
                       int idx = tbCenter.SelectionStart;
                       s = s.Insert(idx, "Pages></Pages>");

                       tbCenter.Text = s;
                       tbCenter.SelectionStart = idx + 6;
                       tbCenter.Focus();
                   }
                   else
                   {
                       tbCenter.Text = "<Pages></Pages>";
                   }
               }

           }
       }*/
    }
}
