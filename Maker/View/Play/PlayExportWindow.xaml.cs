using Maker.Business;
using Maker.Model;
using Maker.View.Dialog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace Maker.View.Play
{
    /// <summary>
    /// PlayExportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayExportWindow : BaseWindow
    {
        public PlayExportWindow(NewMainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            _fileType = ".playExport";
            mainView = gMain;
            HideControl();
        }
        private String tutorialName = String.Empty;
        private String firstPageName = String.Empty;
        private List<String> pageNames = new List<string>();
        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            List<String> fileNames = new List<string>();
            FileBusiness business = new FileBusiness();
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\Light", new List<string>() { ".light" }));
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\LightScript", new List<string>() { ".lightScript" }));
            fileNames.AddRange(business.GetFilesName(mw.lastProjectPath + @"\Midi", new List<string>() { ".mid" }));
            ShowLightListDialog dialog = new ShowLightListDialog(mw, tbTutorialName.Text, fileNames);
            if (dialog.ShowDialog() == true)
            {
                if (sender == btnSelectFileTutorial)
                {
                    tbTutorialName.Text = dialog.selectItem;
                    tutorialName = tbTutorialName.Text;
                }
                else if (sender == btnSelectFileFirstPage)
                {
                    tbFirstPageName.Text = dialog.selectItem;
                    firstPageName = tbTutorialName.Text;
                }
            }
        }
        private void btnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnSelectFileTutorial)
            {
                tbTutorialName.Text = String.Empty;
                tutorialName = String.Empty;
            }
            else if (sender == btnSelectFileFirstPage)
            {
                tbFirstPageName.Text = String.Empty;
                firstPageName = String.Empty;
            }

        }
        protected override void LoadFileContent() {
            XDocument doc = XDocument.Load(filePath);
            XElement xnroot = doc.Element("Root");
            tutorialName = xnroot.Element("Tutorial").Value;
            firstPageName = xnroot.Element("FirstPageName").Value;
            tbTutorialName.Text = tutorialName;
            //XElement xnPages = xnroot.Element("Pages");
            //foreach (XElement pageElement in xnPages.Elements("Page"))
            //{
            //    Dictionary<int, List<PageButtonModel>> mDictionaryListButton = new Dictionary<int, List<PageButtonModel>>();
            //    Dictionary<int, int> mDictionaryPosition = new Dictionary<int, int>();
            //    foreach (XElement buttonsElement in pageElement.Elements("Buttons"))
            //    {
            //        List<PageButtonModel> mListButton = new List<PageButtonModel>();
            //        foreach (XElement buttonElement in buttonsElement.Elements("Button"))
            //        {
            //            PageButtonModel model = new PageButtonModel();
            //            XElement elementDown = buttonElement.Element("Down");
            //            model._down._lightName = elementDown.Attribute("lightname").Value;
            //            model._down._goto = elementDown.Attribute("goto").Value;
            //            model._down._bpm = elementDown.Attribute("bpm").Value;
            //            XElement elementLoop = buttonElement.Element("Loop");
            //            model._loop._lightName = elementLoop.Attribute("lightname").Value;
            //            model._loop._goto = elementLoop.Attribute("goto").Value;
            //            model._loop._bpm = elementLoop.Attribute("bpm").Value;
            //            XElement elementUp = buttonElement.Element("Up");
            //            model._up._lightName = elementUp.Attribute("lightname").Value;
            //            model._up._goto = elementUp.Attribute("goto").Value;
            //            model._up._bpm = elementUp.Attribute("bpm").Value;
            //            mListButton.Add(model);
            //        }
            //        mDictionaryListButton.Add(int.Parse(buttonsElement.Attribute("position").Value), mListButton);
            //        mDictionaryPosition.Add(int.Parse(buttonsElement.Attribute("position").Value), -1);
            //    }
            //    pages.Add(pageElement.Attribute("name").Value, mDictionaryListButton);
            //    positions.Add(pageElement.Attribute("name").Value, mDictionaryPosition);
            //}
        }

      
        private void GenerateExe(object sender, RoutedEventArgs e)
        {
            GenerateLaunchpadLightProject();
        }
      
        private void GenerateLaunchpadLightProject()
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
                xContent = new XAttribute("content", fileBusiness.String2Base(fileBusiness.WriteMidiContent(AllFileToLightList(tutorialName))));
            }
            xTutorial.Add(xContent);
            xRoot.Add(xTutorial);

            //Pages
            XElement xPages = new XElement("Pages");
            XAttribute xFirst = new XAttribute("first", firstPageName);
            xPages.Add(xFirst);
            xRoot.Add(xPages);

            int nowPosition = 0;
            for (int i = 0; i < pageNames.Count; i++)
            {
                XElement xPage = new XElement("Page");
                XAttribute xPageName = new XAttribute("name", pageNames[i]);
                xPage.Add(xPageName);

                mw.cuc.pmw.ReadPageFile(mw.lastProjectPath + @"\Page\" + pageNames[i], out List<List<PageButtonModel>> pageModes);
                for (int x = 0; x < pageModes.Count; x++)
                {
                    if (pageModes[x].Count == 0)
                        continue;

                    XElement xButtons = new XElement("Buttons");
                    XAttribute xPosition = new XAttribute("position", x + 28);
                    xButtons.Add(xPosition);
                    for (int y = 0; y < pageModes[x].Count; y++)
                    {
                        XElement xButton = new XElement("Button");
                        //Down
                        XElement xDown = new XElement("Down");
                        XAttribute xDownLightName = new XAttribute("lightname", "");
                        if (!pageModes[x][y]._down._lightName.Equals(String.Empty))
                        {
                            if (mDictionary.ContainsKey(pageModes[x][y]._down._lightName))
                            {
                                xDownLightName = new XAttribute("lightname", mDictionary[pageModes[x][y]._down._lightName]);
                            }
                            else
                            {
                                mDictionary.Add(pageModes[x][y]._down._lightName, nowPosition.ToString());
                                mLightDictionary.Add(nowPosition.ToString(), AllFileToLightList(pageModes[x][y]._down._lightName));
                                xDownLightName = new XAttribute("lightname", nowPosition.ToString());
                                nowPosition++;
                            }
                        }
                        XAttribute xDownGoto = new XAttribute("goto", pageModes[x][y]._down._goto);
                        XAttribute xDownBpm = new XAttribute("bpm", pageModes[x][y]._down._bpm);
                        xDown.Add(xDownLightName);
                        xDown.Add(xDownGoto);
                        xDown.Add(xDownBpm);
                        xButton.Add(xDown);
                        //Loop
                        XElement xLoop = new XElement("Loop");
                        XAttribute xLoopLightName = new XAttribute("lightname", "");
                        if (!pageModes[x][y]._loop._lightName.Equals(String.Empty))
                        {
                            if (mDictionary.ContainsKey(pageModes[x][y]._loop._lightName))
                            {
                                xLoopLightName = new XAttribute("lightname", mDictionary[pageModes[x][y]._loop._lightName]);
                            }
                            else
                            {
                                mDictionary.Add(pageModes[x][y]._loop._lightName, nowPosition.ToString());
                                mLightDictionary.Add(nowPosition.ToString(), AllFileToLightList(pageModes[x][y]._loop._lightName));
                                xLoopLightName = new XAttribute("lightname", nowPosition.ToString());
                                nowPosition++;
                            }
                        }
                        XAttribute xLoopGoto = new XAttribute("goto", pageModes[x][y]._loop._goto);
                        XAttribute xLoopBpm = new XAttribute("bpm", pageModes[x][y]._loop._bpm);
                        xLoop.Add(xLoopLightName);
                        xLoop.Add(xLoopGoto);
                        xLoop.Add(xLoopBpm);
                        xButton.Add(xLoop);
                        //Up
                        XElement xUp = new XElement("Up");
                        XAttribute xUpLightName = new XAttribute("lightname", "");
                        if (!pageModes[x][y]._up._lightName.Equals(String.Empty))
                        {
                            if (mDictionary.ContainsKey(pageModes[x][y]._up._lightName))
                            {
                                xUpLightName = new XAttribute("lightname", mDictionary[pageModes[x][y]._up._lightName]);
                            }
                            else
                            {
                                mDictionary.Add(pageModes[x][y]._up._lightName, nowPosition.ToString());
                                mLightDictionary.Add(nowPosition.ToString(), AllFileToLightList(pageModes[x][y]._up._lightName));
                                xUpLightName = new XAttribute("lightname", nowPosition.ToString());
                                nowPosition++;
                            }
                        }
                        XAttribute xUpGoto = new XAttribute("goto", pageModes[x][y]._up._goto);
                        XAttribute xUpBpm = new XAttribute("bpm", pageModes[x][y]._up._bpm);
                        xUp.Add(xUpLightName);
                        xUp.Add(xUpGoto);
                        xUp.Add(xUpBpm);
                        xButton.Add(xUp);

                        xButtons.Add(xButton);
                    }
                    xPage.Add(xButtons);
                }
                xPages.Add(xPage);
            }
            //Lights
            XElement xLights = new XElement("Lights");
            xRoot.Add(xLights);
            foreach (var mItem in mLightDictionary)
            {
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

                XAttribute xValue = new XAttribute("value", fileBusiness.String2Base(fileBusiness.WriteMidiContent(mItem.Value)));
                xLight.Add(xValue);
                xLights.Add(xLight);
            }
            DirectoryInfo d = new DirectoryInfo(mw.lastProjectPath);
            xDoc.Save(mw.lastProjectPath + @"\" + d.Name + ".makerpl");
        }
        LightScriptBusiness business = new LightScriptBusiness();
        public List<Light> AllFileToLightList(String filePath)
        {
            List<Light> mLightList = new List<Light>();
            if (filePath.EndsWith(".lightScript"))
            {
                mLightList = business.ScriptToLightGroup(business.GetCompleteLightScript(mw.lastProjectPath + @"\LightScript\" + filePath), "Main");
            }
            else if (filePath.EndsWith(".light"))
            {
                mLightList = fileBusiness.ReadLightFile(mw.lastProjectPath + @"\Light\" + filePath);
            }
            else if (filePath.EndsWith(".mid"))
            {
                mLightList = fileBusiness.ReadMidiFile(mw.lastProjectPath + @"\Midi\" + filePath);
            }
            mLightList = LightBusiness.Sort(mLightList);
            return mLightList;
        }

    }
}
